using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Alice_Timetable.Engine;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Alice_Timetable.Models;
using Alice_Timetable.Controllers;
using alice_timetable.Engine;
using alice_timetable.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.File;
using alice_timetable;
using Hangfire;
using Hangfire.MemoryStorage;

namespace Alice_Timetable
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly("Alice_Timetable")));
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<ISchedulesRepository, SchedulesRepository>();
            services.AddControllers();
            services.Configure<IISServerOptions>(options => { options.AllowSynchronousIO = true; });
            services.AddHangfire(config =>
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseDefaultTypeSerializer()
                .UseMemoryStorage());

            services.AddHangfireServer();
            services.AddSingleton<IUpdateSchedule, UpdateSchedule>();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHangfireDashboard();
            RecurringJob.AddOrUpdate(
                                        () => serviceProvider
                                            .GetService<IUpdateSchedule>()
                                            .UpdateGroupSchedule(serviceProvider
                                            .GetService<ISchedulesRepository>()) 
                                        ,Cron.Daily()    
                                    );
        }
    }
}

using alice_timetable.Models;
using Alice_Timetable.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alice_Timetable.Engine
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            Console.WriteLine("Создан объект контекста");
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
            .Property(e => e.academicDepartment)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<Alice_Timetable.Models.Schedules.Schedule>()
            .Property(e => e.studentGroup)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<Alice_Timetable.Models.Schedules.Schedule>()
           .Property(e => e.studentGroupInformation)
           .HasConversion(
               v => string.Join(',', v),
               v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<Alice_Timetable.Models.Schedules.Schedule>()
           .Property(e => e.auditory)
           .HasConversion(
               v => string.Join(',', v),
               v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<Alice_Timetable.Models.Schedules.Schedule>()
           .Property(e => e.studentGroupModelList)
           .HasConversion(
               v => string.Join(',', v),
               v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<Alice_Timetable.Models.Schedules.Schedule>()
           .Property(e => e.gradebookLessonlist)
           .HasConversion(
               v => string.Join(',', v),
               v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<Alice_Timetable.Models.Schedules.Schedule>()
           .Property(e => e.weekNumber)
           .HasConversion(
               v => string.Join(',', v),
               v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(item => int.Parse(item)).ToArray());
        }
        public DbSet<User> Users { get; set; }
        public DbSet<SimplifiedSchedule> Schedules {get;set;}
    }
}

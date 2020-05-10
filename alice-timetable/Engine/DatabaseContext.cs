using alice_timetable.Models;
using Alice_Timetable.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries))
            .Metadata
            .SetValueComparer(new ValueComparer<string[]>(
                (c1, c2) => c1.Equals(c2), // По ссылке, но нам особо и не надо чтобы сранивалось по значениям
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToArray())
                );

            modelBuilder.Entity<Alice_Timetable.Models.Schedules.Schedule>()
            .Property(e => e.studentGroup)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries))
            .Metadata
            .SetValueComparer(new ValueComparer<string[]>(
                (c1, c2) => c1.Equals(c2), // По ссылке, но нам особо и не надо чтобы сранивалось по значениям
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToArray())
                );

            modelBuilder.Entity<Alice_Timetable.Models.Schedules.Schedule>()
           .Property(e => e.studentGroupInformation)
           .HasConversion(
               v => string.Join(',', v),
               v => v.Split(',', StringSplitOptions.RemoveEmptyEntries))
           .Metadata
           .SetValueComparer(new ValueComparer<string[]>(
                (c1, c2) => c1.Equals(c2), // По ссылке, но нам особо и не надо чтобы сранивалось по значениям
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToArray())
                );

            modelBuilder.Entity<Alice_Timetable.Models.Schedules.Schedule>()
           .Property(e => e.auditory)
           .HasConversion(
               v => string.Join(',', v),
               v => v.Split(',', StringSplitOptions.RemoveEmptyEntries))
           .Metadata
           .SetValueComparer(new ValueComparer<string[]>(
                (c1, c2) => c1.Equals(c2), // По ссылке, но нам особо и не надо чтобы сранивалось по значениям
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToArray())
                );

            modelBuilder.Entity<Alice_Timetable.Models.Schedules.Schedule>()
           .Property(e => e.studentGroupModelList)
           .HasConversion(
               v => string.Join(',', v),
               v => v.Split(',', StringSplitOptions.RemoveEmptyEntries))
           .Metadata
           .SetValueComparer(new ValueComparer<string[]>(
                (c1, c2) => c1.Equals(c2), // По ссылке, но нам особо и не надо чтобы сранивалось по значениям
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToArray())
                );

            modelBuilder.Entity<Alice_Timetable.Models.Schedules.Schedule>()
           .Property(e => e.gradebookLessonlist)
           .HasConversion(
               v => string.Join(',', v),
               v => v.Split(',', StringSplitOptions.RemoveEmptyEntries))
           .Metadata
           .SetValueComparer(new ValueComparer<string[]>(
                (c1, c2) => c1.Equals(c2), // По ссылке, но нам особо и не надо чтобы сранивалось по значениям
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToArray())
                );

            modelBuilder.Entity<Alice_Timetable.Models.Schedules.Schedule>()
           .Property(e => e.weekNumber)
           .HasConversion(
               v => string.Join(',', v),
               v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(item => int.Parse(item)).ToArray())
           .Metadata
           .SetValueComparer(new ValueComparer<int[]>(
                (c1, c2) => c1.Equals(c2), // По ссылке, но нам особо и не надо чтобы сранивалось по значениям
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToArray())
                );
        }
        public DbSet<User> Users { get; set; }
        public DbSet<BsuirScheduleResponse> Schedules {get;set;}
    }
}

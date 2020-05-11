using Alice_Timetable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alice_Timetable.Engine
{
    public class UsersRepository : IUsersRepository
    {
        private DatabaseContext context;
        public UsersRepository(DatabaseContext ctx)
        {
            Console.WriteLine("Создан User Repository");
            context = ctx;
        }
        public IQueryable<User> Users => context.Users;

        public User DeleteUser(string userID)
        {
            User dbEntry = context.Users.FirstOrDefault(u => u.ID == userID);
            if (dbEntry != null)
            {
                context.Users.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public User CreateOrUpdateUser(User user, string userID)
        {
            var dbEntry = context.Users.FirstOrDefault(u => u.ID == userID);

            if (dbEntry == null)
            {
                dbEntry = new User();
                dbEntry.ID = userID;
                context.Users.Add(dbEntry);
                user = dbEntry;

                Console.WriteLine($"Создан новый пользователь {dbEntry.ID}");
            }
            else
            {
                dbEntry.Name = dbEntry.Name != user.Name && !String.IsNullOrEmpty(user.Name) ? user.Name : dbEntry.Name;
                dbEntry.Group = dbEntry.Group != user.Group  && !String.IsNullOrEmpty(user.Group) ? user.Group : dbEntry.Group;
                dbEntry.Group = dbEntry.Group != user.Group && !String.IsNullOrEmpty(user.Group) ? user.Group : dbEntry.Group;

                user = context.Users.Update(user).Entity;
                Console.WriteLine($"Обратился пользователь {user.ID}, имя - {user.Name}, группа - {user.Group}" + 
                                    $"1 = {user.DisplayAuditory}" +
                                    $"2 = {user.DisplaySubjectType}" +
                                    $"3 = {user.DisplaySubjectTime}" +
                                    $"4 = {user.DisplayEmployeeName}");
            }

            context.SaveChanges();
            return user;
        }

        public User CreateUser(string userID)
        {
            var dbEntry = context.Users.FirstOrDefault(u => u.ID == userID);

            if (dbEntry == null)
            {
                dbEntry = new User();
                dbEntry.ID = userID;
                context.Users.Add(dbEntry);
                Console.WriteLine($"Создан новый пользователь {dbEntry.ID}");
            }
            else
            {
                Console.WriteLine($"Обратился пользователь {dbEntry.ID}, имя - {dbEntry.Name}, группа - {dbEntry.Group}" +
                                    $"1 = {dbEntry.DisplayAuditory}" +
                                    $"2 = {dbEntry.DisplaySubjectType}" +
                                    $"3 = {dbEntry.DisplaySubjectTime}" +
                                    $"4 = {dbEntry.DisplayEmployeeName}");
            }

            context.SaveChanges();
            return dbEntry;
        }

        public User UpdateUser(User user)
        {
            var dbEntry = context.Users.FirstOrDefault(u => u.ID == user.ID);
            if (dbEntry != null)
            {
                user = context.Users.Update(user).Entity;
                Console.WriteLine($"Обновлен пользователь {user.ID}, имя - {user.Name}, группа - {user.Group}" +
                                    $"1 = {user.DisplayAuditory}" +
                                    $"2 = {user.DisplaySubjectType}" +
                                    $"3 = {user.DisplaySubjectTime}" +
                                    $"4 = {user.DisplayEmployeeName}");
            }

            context.SaveChanges();
            return user;
        }
    }
}

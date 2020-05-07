﻿using Alice_Timetable.Models;
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

        public void CreateOrSaveUser(out User user, string userID)
        {
            var dbEntry = context.Users.FirstOrDefault(u => u.ID == userID);

            if (dbEntry == null)
            {
                dbEntry.ID = userID;
                context.Users.Add(dbEntry);
                user = dbEntry;

                Console.WriteLine($"Создан новый пользователь {dbEntry.ID}");
            }
            else
            {
                user = context.Users.Update(dbEntry).Entity;
                Console.WriteLine($"Обратился пользователь {dbEntry.ID}");
            }

            context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            context.Users.Update(user);
            context.SaveChanges();
        }
    }
}
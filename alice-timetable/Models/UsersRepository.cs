using Alice_Timetable.Engine;
using Alice_Timetable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alice_Timetable.Models
{
    public class UsersRepository : IUsersRepository
    {
        private DatabaseContext context;
        public UsersRepository(DatabaseContext ctx)
        {
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

        public void CreateOrSaveUser(User user, string userID)
        {
            User dbEntry = context.Users.FirstOrDefault(u => u.ID == userID);
            if (dbEntry == null)
            {
                user.ID = userID;
                context.Users.Add(user);
            }
            else
            {
                if (dbEntry != null)
                {
                    dbEntry.ID = userID;
                    // Потом сюда надо дописать WebHooks, но пока они только для чтения
                }
            }
            context.SaveChanges();
        }
    }
}

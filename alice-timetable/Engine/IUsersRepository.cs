using Alice_Timetable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alice_Timetable.Engine
{
    public interface IUsersRepository
    {
        IQueryable<User> Users { get; }
        User CreateUser(string userID);
        User DeleteUser(string userID);
        User UpdateUser(User user);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webStoreProject.Models;

namespace webStoreProject.Services
{
   public interface IUserRepository
    {
        List<User> Users();

        User FindUser(int userId);
        bool AddUser(User newUser);
        //bool RemoveUser(int userId);
        bool UpdateUser(User updatedUser);
    }
}

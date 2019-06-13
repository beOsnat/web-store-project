using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webStoreProject.Data;
using webStoreProject.Models;

namespace webStoreProject.Services
{
    public class UserRepository : IUserRepository
    {
        private myStoreDbContext _myStoreDbContext;


        public UserRepository(myStoreDbContext myStoreDbContext)
        {
            _myStoreDbContext = myStoreDbContext;
        }


        public bool AddUser(User newUser)
        {
            _myStoreDbContext.Users.Add(newUser);
            int addedRows = _myStoreDbContext.SaveChanges();
            return addedRows > 0;
        }

        public User FindUser(int userId)
        {
            var foundUser = _myStoreDbContext.Users.First((user) => user.UserId == userId);
            return foundUser;
        }


        public bool UpdateUser(User updatedUser)
        {
            var updated = _myStoreDbContext.Users.First(user => user.UserId == updatedUser.UserId);

            updated.FirstName = updatedUser.FirstName;
            updated.LastName = updatedUser.LastName;
            updated.BirthDate = updatedUser.BirthDate;
            updated.Email = updatedUser.Email;
            updated.Password = updatedUser.Password;
            updated.PasswordConfirm = updatedUser.PasswordConfirm;

            int updatedRows = _myStoreDbContext.SaveChanges();
            return updatedRows > 0;
        }

        public List<User> Users()
        {
            return _myStoreDbContext.Users.ToList();
        }



    }
}

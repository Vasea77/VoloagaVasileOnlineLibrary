using OnlineLibrary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Services
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        void CreateUser(User user);
        //List<User> GetList(int pageNumber, int pageSize, string sort, string sortDir);
        int Count();
        //List<User> GetUser(int pageNumber, int pageSize, string sort, string sortDir);
        User FindUser(int id);
        void UpdateUser(User user);
        void DeleteUser(User user);
        List<User> GetUser(int pageNumber, int pageSize, string sortOrder);
    }
}

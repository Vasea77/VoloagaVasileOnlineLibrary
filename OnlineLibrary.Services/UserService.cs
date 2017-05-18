using OnlineLibrary.Data.Infrastructure;
using OnlineLibrary.Domain.Entities;
using System.Collections.Generic;
using System.Linq;


namespace OnlineLibrary.Services
{
    public class UserService : IUserService
    {
        private IRepository<User> userRepo;
        private IUnitOfWork unitOfWork;

        public UserService(IRepository<User> userRepo, IUnitOfWork unitOfWork)
        {
            this.userRepo = userRepo;
            this.unitOfWork = unitOfWork;
        }

        public List<User> GetAllUsers()
        {
            return userRepo.GetAll().ToList();
        }

        public void CreateUser(User user)
        {
            userRepo.Add(user);
            unitOfWork.Commit();
        }

        public void UpdateUser(User user)
        {
            //TryUpdateModel(user);
            unitOfWork.Commit();
        }

        public void DeleteUser(User user)
        {
            userRepo.Delete(user);
            unitOfWork.Commit();
        }

        public List<User> GetUser(int pageNumber, int pageSize, string sortOrder)
        {
            
            var users = from u in userRepo.GetAll() select u;

            switch (sortOrder)
            {
                case "FirstName_desc":
                    users = users.OrderByDescending(u => u.FirstName);
                    break;
                case "LastName":
                    users = users.OrderBy(u => u.LastName);
                    break;
                case "LastName_desc":
                    users = users.OrderByDescending(u => u.LastName);
                    break;
                default:
                    users = users.OrderBy(u => u.FirstName);
                    break;
            }
            
            return users.Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();

            //return userRepo.GetAll().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            //return userRepo.GetAll((pageNumber - 1) * pageSize, pageSize, sort, sortDir).ToList();
        }

        public int Count()
        {
            return userRepo.Count();
        }

        public User FindUser(int id)
        {
            return userRepo.GetByID(id);
        }
    }
}

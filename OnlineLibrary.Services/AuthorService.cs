using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLibrary.Data.Infrastructure;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.Services
{
    public class AuthorService : IAuthorService
    {
        private IRepository<Author> authorRepo;
        private IUnitOfWork unitOfWork;

        public AuthorService(IRepository<Author> authorRepo, IUnitOfWork unitOfWork)
        {
            this.authorRepo = authorRepo;
            this.unitOfWork = unitOfWork;
        }

        public List<Author> GetAllAuthors()
        {
            return authorRepo.GetAll().ToList();
        }

        public void CreateAuthor(Author author)
        {
            authorRepo.Add(author);
            unitOfWork.Commit();
        }

        public List<Author> GetAuthors(int pageNumber, int pageSize, string sort, string sortDir)
        {
            return authorRepo.GetAll().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

        public int Count()
        {
            return authorRepo.Count();
        }
    }
}

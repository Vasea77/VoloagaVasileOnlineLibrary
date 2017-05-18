using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.Services
{
    public interface IAuthorService
    {
        List<Author> GetAllAuthors();
        void CreateAuthor(Author author);
        List<Author> GetAuthors(int pageNumber, int pageSize, string sort, string sortDir);
        int Count();
    }
}

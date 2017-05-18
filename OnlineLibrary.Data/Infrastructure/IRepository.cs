using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OnlineLibrary.Data.Infrastructure
{
  public interface IRepository<T> where T : class
  {
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    void Delete(Expression<Func<T, bool>> where);
    T GetByID(long ID);
    T GetByID(string ID);
    T Get(Expression<Func<T, bool>> where);
    IEnumerable<T> GetAll();
    IEnumerable<T> GetAll(int itemsCount, int pageSize, string sort, string sortDir);
    IEnumerable<T> GetMany(Expression<Func<T, bool>> where);
    IQueryable<T> Query(Expression<Func<T, bool>> where);
    //we need to add Count..
    int Count();
    int Count(Expression<Func<T, bool>> where);
  }
}



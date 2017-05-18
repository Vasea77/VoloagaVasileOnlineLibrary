using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using OnlineLibrary.Data.Entities;

namespace OnlineLibrary.Data.Infrastructure
{
	public class Repository<T> : IRepository<T> where T : class
	{

		public Guid guid = Guid.NewGuid();

		private BookStoreEntities dataContext;
		private readonly IDbSet<T> dbset;
		public Repository(IDatabaseFactory databaseFactory)
		{
			DatabaseFactory = databaseFactory;
			dbset = DataContext.Set<T>();
		}

		protected IDatabaseFactory DatabaseFactory
		{
			get;
			private set;
		}

		protected BookStoreEntities DataContext
		{
			get { return dataContext ?? (dataContext = DatabaseFactory.Get()); }
		}
		public virtual void Add(T entity)
		{
			dbset.Add(entity);
		}

		public virtual void Update(T entity)
		{
			dbset.Attach(entity);
			dataContext.Entry(entity).State = EntityState.Modified;
		}

		public virtual void Delete(T entity)
		{
			dbset.Remove(entity);
		}
		public virtual void Delete(Expression<Func<T, bool>> where)
		{
			IEnumerable<T> objects = dbset.Where<T>(where).AsEnumerable();
			foreach (T obj in objects)
				dbset.Remove(obj);
		}

		public virtual T GetByID(long ID)
		{
			return dbset.Find(ID);
		}

		public virtual T GetByID(string ID)
		{
			return dbset.Find(ID);
		}

		public virtual IEnumerable<T> GetAll()
		{
			return dbset.ToList();
		}

		public virtual IEnumerable<T> GetAll(int itemsCount, int pageSize, string sort, string sortDir)
		{
			return dbset.ToList();
		}

		public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
		{
			return dbset.Where(where).ToList();
		}

		public T Get(Expression<Func<T, bool>> where)
		{
			return dbset.Where(where).FirstOrDefault<T>();
		}

		public IQueryable<T> Query(Expression<Func<T, bool>> where)
		{
			return dbset.Where(where);
		}

		public int Count()
		{
			return dbset.Count();
		}

		public int Count(Expression<Func<T, bool>> where)
		{
			return dbset.Count(where);
		}
	}
}

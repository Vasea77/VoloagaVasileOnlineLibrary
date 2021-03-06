﻿using OnlineLibrary.Data.Entities;
using System.Data.Entity.Validation;

namespace OnlineLibrary.Data.Infrastructure
{
    using System.Diagnostics;
    using System.Text;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabaseFactory databaseFactory;
        private BookStoreEntities dataContext;

        public UnitOfWork(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        protected BookStoreEntities DataContext
        {
            get { return dataContext ?? (dataContext = databaseFactory.Get()); }
        }

        public void Commit()
        {
            try
            {
                DataContext.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();
                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }
                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                );
            }

        }
    }
}

using System;
using OnlineLibrary.Data.Entities;

namespace OnlineLibrary.Data.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
    BookStoreEntities Get();
    }
}

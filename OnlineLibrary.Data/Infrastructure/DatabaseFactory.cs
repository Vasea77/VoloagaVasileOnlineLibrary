using OnlineLibrary.Data.Entities;

namespace OnlineLibrary.Data.Infrastructure
{


  public class DatabaseFactory : Disposable, IDatabaseFactory
  {
	  private BookStoreEntities dataContext;
	  public BookStoreEntities Get()
    {
		return dataContext ?? (dataContext = new BookStoreEntities());
    }
    protected override void DisposeCore()
    {
      if (dataContext != null)
        dataContext.Dispose();
    }
  }
}

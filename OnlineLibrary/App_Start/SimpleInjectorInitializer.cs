using OnlineLibrary.Data.Infrastructure;
using OnlineLibrary.Services;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(OnlineLibrary.App_Start.SimpleInjectorInitializer), "Initialize")]
namespace OnlineLibrary.App_Start
{

    public static class SimpleInjectorInitializer
    {
        /// <summary>Initialize the container and register it as MVC5 Dependency Resolver.</summary>
        public static void Initialize()
        {
            // Did you know the container can diagnose your configuration? Go to: http://bit.ly/YE8OJj.
            var container = new Container();

            InitializeContainer(container);
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.RegisterMvcIntegratedFilterProvider();

            // System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));


        }

        private static void InitializeContainer(Container container)
        {

            //per web request lifestyle
            var webLifestyle = new WebRequestLifestyle();

            container.Register<IDatabaseFactory, DatabaseFactory>(webLifestyle);
            container.Register<IUnitOfWork, UnitOfWork>(webLifestyle);
            //start defining services
            container.Register<IUserService, UserService>(webLifestyle);
            container.Register<IAuthorService, AuthorService>(webLifestyle);

            //register generic repository
            container.Register(typeof(IRepository<>), typeof(Repository<>), webLifestyle);
        }

    }
}
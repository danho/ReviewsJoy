using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc3;
using ReviewsJoy.DAL;
using ReviewsJoy.DAL.Repository;

namespace ReviewsJoy
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<IDatabaseContext, DatabaseContext>();
            container.RegisterType<DatabaseContext, DatabaseContext>();
            //container.RegisterType<IReviewsRepository, ReviewsRepository>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType(typeof(GenericRepository<>), typeof(GenericRepository<>));
            return container;
        }
    }
}
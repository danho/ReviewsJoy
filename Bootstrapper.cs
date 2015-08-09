using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc3;
using ReviewsJoy.DAL;

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
            return container;
        }
    }
}
using BetEventScanner.DataAccess;
using BetEventScanner.DogonWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;

namespace BetEventScanner.DogonWeb.App_Start
{
    public class IocConfigurator
    {
        public static void ConfigureIoCContainer()
        {
            IUnityContainer container = new UnityContainer();
            RegisterServices(container);

            //DependencyResolver.SetResolver()
        }

        private static void RegisterServices(IUnityContainer container)
        {
            container.RegisterType<ITennisService, TennisBetService>()
                     .RegisterType<IDefaultUnitOfWork, UnitOfWork>();
        }
    }
}
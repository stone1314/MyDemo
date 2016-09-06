using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppSolution.App_Start
{
    public class AutofacConfig
    {
        public static void Bootstrapper()
        {
            ContainerBuilder builder = new ContainerBuilder();
            //注入Controller
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            //IUnitOfWork
            builder.RegisterType<>()
        }

    }
}
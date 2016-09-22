using App.Common.EFRepository;
using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerRequest();

            //注入数据库查询语句执行类
            builder.RegisterType<RepositoryBaseSql>()
                .As<IRepositoryBaseSql>()
                .InstancePerRequest();

            Assembly[] AsmSvr = new Assembly[2];
            AsmSvr[0] = Assembly.Load("App.BLL");
            AsmSvr[1] = Assembly.Load("App.IBLL");
            builder.RegisterAssemblyTypes(AsmSvr)
                .Where(t => t.Name.ToLower().EndsWith("service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

        }

    }
}
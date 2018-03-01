using Admin.Manager.App_Start;
using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Admin.Manager
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public static void CoreAutoFacInit()
        {

            var builder = new ContainerBuilder();

            // Add any Autofac modules or registrations.
            builder.RegisterModule(new AutofacModule());
            //var builder = new ContainerBuilder();
            //builder.RegisterType<ECERP_Base_UserRepository>().As<IECERP_Base_UserRepository>();

            //builder.AddSingleton<IECERP_Base_UserRepository, ECERP_Base_UserRepository>();
            //SetupResolveRules(builder);

            ////注册所有的Controllers
            //builder.RegisterControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            //注册所有的ApiControllers
            //builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());


            ////注册api容器需要使用HttpConfiguration对象
            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = resolver;

            ////注册api容器需要使用HttpConfiguration对象
            //HttpConfiguration config = GlobalConfiguration.Configuration;
            //config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            //  DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static void SetupResolveRules(ContainerBuilder builder)
        {
            //WebAPI只用引用services和repository的接口，不用引用实现的dll。
            //如需加载实现的程序集，将dll拷贝到bin目录下即可，不用引用dll
            //var iServices = Assembly.Load("WebAPI.IServices");
            //var services = Assembly.Load("WebAPI.Services");
            // var iRepository = Assembly.Load("WebAPI.IRepository");

            var servers = Assembly.Load("IChipo.YJH.Server");
            var iServers = Assembly.Load("IChipo.YJH.Interfaces");

            builder.RegisterAssemblyTypes(iServers, servers)
              .Where(t => t.Name.EndsWith("Repository"))
              .AsImplementedInterfaces();

            // var assemblys = System.Web.Compilation.BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToList();
            ////根据名称约定（服务层的接口和实现均以Services结尾），实现服务接口和服务实现的依赖
            //builder.RegisterAssemblyTypes(iServices, services)
            //  .Where(t => t.Name.EndsWith("Services"))
            //  .AsImplementedInterfaces();

            //根据名称约定（数据访问层的接口和实现均以Repository结尾），实现数据访问接口和数据访问实现的依赖
            //builder.RegisterAssemblyTypes(assemblys.ToArray())
            //  .Where(t => t.Name.EndsWith("Repository"))
            //  .AsImplementedInterfaces();
        }


        
    }

    
}

using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using WebApi.Infrastructure;
using WebApi.Repository;
using WebApi.Service;
using WebApi.API1;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Autofac;
using System.Web.Http;

namespace WebApi.API1
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            SetAutofac();
        }

        private static void SetAutofac()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(StudentRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(StudentService).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterFilterProvider();
            //builder.RegisterType<AuthenticationHelper>().As<IAuthenticationHelper>().InstancePerRequest();
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            //var config = GlobalConfiguration.Configuration;
            //config.DependencyResolver = new AutofacDependencyResolver(container);
        }
    }
}

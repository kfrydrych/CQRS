using Autofac;
using System.Reflection;
using Module = Autofac.Module;

namespace CQRS.Autofac.Modules
{
    public class MediatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(CqrsCoreAssebly)))
                .Where(x => x.Name.Contains("Bus"))
                .AsImplementedInterfaces().SingleInstance();

            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(CqrsCoreAssebly)))
                .Where(x => x.Name.Contains("Mediator"))
                .AsImplementedInterfaces().SingleInstance();
        }
    }
}

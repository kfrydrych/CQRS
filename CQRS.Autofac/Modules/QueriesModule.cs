using Autofac;
using CQRS.Query;
using System;
using Module = Autofac.Module;

namespace CQRS.Autofac.Modules
{
    public class QueriesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register<Func<Type, Type, IHandleQuery>>(c =>
            {
                var ctx = c.Resolve<IComponentContext>();

                return (t, z) =>
                {
                    var handlerType = typeof(IHandleQuery<,>).MakeGenericType(t, z);
                    return (IHandleQuery)ctx.Resolve(handlerType);
                };
            });

            builder.Register<Func<Type, Type, IHandleQueryAsync>>(c =>
            {
                var ctx = c.Resolve<IComponentContext>();

                return (t, z) =>
                {
                    var handlerType = typeof(IHandleQueryAsync<,>).MakeGenericType(t, z);
                    return (IHandleQueryAsync)ctx.Resolve(handlerType);
                };
            });
        }
    }
}

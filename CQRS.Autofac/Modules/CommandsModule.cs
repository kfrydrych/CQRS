using Autofac;
using CQRS.Commands;
using System;
using Module = Autofac.Module;

namespace CQRS.Autofac.Modules
{
    public class CommandsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register<Func<Type, IHandleCommand>>(c =>
            {
                var ctx = c.Resolve<IComponentContext>();

                return t =>
                {
                    var handlerType = typeof(IHandleCommand<>).MakeGenericType(t);
                    return (IHandleCommand)ctx.Resolve(handlerType);
                };
            });

            builder.Register<Func<Type, IHandleCommandAsync>>(c =>
            {
                var ctx = c.Resolve<IComponentContext>();

                return t =>
                {
                    var handlerType = typeof(IHandleCommandAsync<>).MakeGenericType(t);
                    return (IHandleCommandAsync)ctx.Resolve(handlerType);
                };
            });
        }
    }
}

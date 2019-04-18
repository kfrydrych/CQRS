using Autofac;
using CQRS.Validation;
using System;
using Module = Autofac.Module;

namespace CQRS.Autofac.Modules
{
    public class ValidationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register<Func<Type, IValidateCommand>>(c =>
            {
                var ctx = c.Resolve<IComponentContext>();

                return t =>
                {
                    var handlerType = typeof(IValidateCommand<>).MakeGenericType(t);
                    ctx.TryResolve(handlerType, out object instance);
                    return (IValidateCommand)instance;
                };
            });

            builder.Register<Func<Type, IValidateQuery>>(c =>
            {
                var ctx = c.Resolve<IComponentContext>();

                return t =>
                {
                    var handlerType = typeof(IValidateQuery<>).MakeGenericType(t);
                    ctx.TryResolve(handlerType, out object instance);
                    return (IValidateQuery)instance;
                };
            });
        }
    }
}

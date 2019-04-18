﻿using Autofac;
using CQRS.Events;
using System;
using System.Collections.Generic;
using Module = Autofac.Module;

namespace CQRS.Autofac.Modules
{
    public class EventsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register<Func<Type, IEnumerable<IHandleEvent>>>(c =>
            {
                var ctx = c.Resolve<IComponentContext>();
                return t =>
                {
                    var handlerType = typeof(IHandleEvent<>).MakeGenericType(t);
                    var handlersCollectionType = typeof(IEnumerable<>).MakeGenericType(handlerType);
                    return (IEnumerable<IHandleEvent>)ctx.Resolve(handlersCollectionType);
                };
            });
        }
    }
}

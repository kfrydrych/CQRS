using Autofac;
using CQRS.Autofac.Modules;
using CQRS.Commands;
using CQRS.Events;
using CQRS.Query;
using CQRS.Validation;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace CQRS.Autofac
{
    public class RegisterComponents
    {
        public static void Execute(ContainerBuilder builder, IEnumerable<string> assemblyNames)
        {
            foreach (var assemblyName in assemblyNames)
                RegisterHandlers(builder, assemblyName);

            RegisterModules(builder);

        }

        public static void Execute(ContainerBuilder builder, string assemblyName)
        {
            RegisterHandlers(builder, assemblyName);

            RegisterModules(builder);

        }

        public static void Execute(ContainerBuilder builder, Assembly assembly)
        {
            var assemblyName = assembly.GetName().Name;

            RegisterHandlers(builder, assemblyName);

            RegisterModules(builder);

        }


        private static void RegisterHandlers(ContainerBuilder builder, string assemblyName)
        {
            Assembly assembly;

            try
            {
                assembly = Assembly.Load(assemblyName);
            }
            catch (FileNotFoundException e)
            {
                throw new FileNotFoundException($"Assembly {assemblyName} could not be found", e);
            }

            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.IsAssignableTo<IHandleCommand>())
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.IsAssignableTo<IHandleCommandAsync>())
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.IsAssignableTo<IHandleEvent>())
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.IsAssignableTo<IHandleQuery>())
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.IsAssignableTo<IHandleQueryAsync>())
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.IsAssignableTo<IValidateCommand>())
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.IsAssignableTo<IValidateQuery>())
                .AsImplementedInterfaces();
        }

        private static void RegisterModules(ContainerBuilder builder)
        {
            builder.RegisterModule<CommandsModule>();
            builder.RegisterModule<EventsModule>();
            builder.RegisterModule<QueriesModule>();
            builder.RegisterModule<MediatorModule>();
            builder.RegisterModule<ValidationModule>();
        }
    }
}

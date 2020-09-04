using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Householder.Server.Commands;
using Householder.Server.Queries;
using LightInject;

namespace Householder.Server.Api
{
    public static class ContainerExtensions
    {
        public static IServiceRegistry RegisterCommandHandlers(this IServiceRegistry serviceRegistry)
        {
            var commandTypes = Assembly.GetCallingAssembly()
                .GetTypes()
                .Select(t => GetGenericInterface(t, typeof(ICommandHandler<,>)))
                .Where(m => m != null);
            RegisterHandlers(serviceRegistry, commandTypes);
            serviceRegistry.Register<ICommandProcessor>(factory => new CommandProcessor((IServiceContainer)serviceRegistry));
            return serviceRegistry;
        }

        public static IServiceRegistry RegisterQueryHandlers(this IServiceRegistry serviceRegistry)
        {
            var queryTypes = Assembly.GetCallingAssembly()
                .GetTypes()
                .Select(t => GetGenericInterface(t, typeof(IQueryHandler<,>)))
                .Where(m => m != null);
            RegisterHandlers(serviceRegistry, queryTypes);
            serviceRegistry.Register<IQueryProcessor>(factory => new QueryProcessor((IServiceContainer)serviceRegistry));
            return serviceRegistry;
        }

        private static (Type, Type)? GetGenericInterface(Type type, Type genericTypeDefinition)
        {
            var closedGenericInterface = type.GetInterfaces()
                .SingleOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == genericTypeDefinition);
            
            if (closedGenericInterface != null)
            {
                var constructor = type.GetConstructors().FirstOrDefault();
                if (constructor != null)
                {
                    var isDecorator = constructor.GetParameters().Select(p => p.ParameterType).Contains(closedGenericInterface);
                    if (!isDecorator)
                    {
                        return (closedGenericInterface, type);
                    }
                }
            }

            return null;
        }

        private static void RegisterHandlers(IServiceRegistry registry, IEnumerable<(Type serviceType, Type implementingType)?> handlers)
        {
            foreach (var handler in handlers)
            {
                if (handler.HasValue)
                {
                    registry.Register(handler.Value.serviceType, handler.Value.implementingType, handler.Value.implementingType.FullName);
                }
            }
        }
    }
}
using System;
using System.Linq;
using System.Reflection;
using Joker.DispatchProxy.BaseDispatchProxyAsync;
using Microsoft.Extensions.DependencyInjection;

namespace Joker.DispatchProxy
{
    public static class ServiceCollectionExtensions
    {
             
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <typeparam name="TInterface"></typeparam>
        /// <typeparam name="TProxy"></typeparam>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IServiceCollection DecorateWithDispatchProxy<TInterface, TProxy>(this IServiceCollection services)
            where TInterface : class
            where TProxy : DispatchProxyAsync
        {
            MethodInfo createMethod;
            try
            {
                createMethod = typeof(TProxy).GetMethods(BindingFlags.Public | BindingFlags.Static)
                    .First(info => !info.IsGenericMethod && info.ReturnType == typeof(TInterface));
            }
            catch (InvalidOperationException e)
            {
                throw new InvalidOperationException($"Looks like there is no static method in {typeof(TProxy)} " +
                                                    $"which creates instance of {typeof(TInterface)} (note that this method should not be generic)",
                    e);
            }

            var argInfos = createMethod.GetParameters();

            // Save all descriptors that needs to be decorated into a list.
            var descriptorsToDecorate = services
                .Where(s => s.ServiceType == typeof(TInterface))
                .ToList();
      
            if (descriptorsToDecorate.Count == 0)
            {
                throw new InvalidOperationException($"Attempted to Decorate services of type {typeof(TInterface)}, " +
                                                    "but no such services are present in ServiceCollection");
            }

            foreach (var descriptor in descriptorsToDecorate)
            {
                var decorated = ServiceDescriptor.Describe(
                    typeof(TInterface),
                    sp =>
                    {
                        var decoratorInstance = createMethod.Invoke(null,
                            argInfos.Select(
                                    info => info.ParameterType ==
                                            (descriptor.ServiceType ?? descriptor.ImplementationType)
                                        ? sp.CreateInstance(descriptor)
                                        : sp.GetRequiredService(info.ParameterType))
                                .ToArray());

                        return (TInterface) decoratorInstance;
                    },
                    descriptor.Lifetime);

                services.Remove(descriptor);
                services.Add(decorated);
            }

            return services;
        }
 
        private static object CreateInstance(this IServiceProvider services, ServiceDescriptor descriptor)
        {
            if (descriptor.ImplementationInstance != null)
            {
                return descriptor.ImplementationInstance;
            }

            if (descriptor.ImplementationFactory != null)
            {
                return descriptor.ImplementationFactory(services);
            }

            return ActivatorUtilities.GetServiceOrCreateInstance(services, descriptor.ImplementationType);
        }
    }
}
using Core.Application.Attributes;
using Core.Application.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application
{
    public static class ServiceRegistration
    {
        public static void ApplicationServiceInitialize(this IServiceCollection serviceDescriptors)
        {
            Assembly.GetExecutingAssembly().GetTypes()?.Where(x => x.GetCustomAttribute<DependencyRegisterAttribute>() != null).ToList()?.ForEach(classType =>
            {
                DependencyRegisterAttribute dependencyRegisterAttribute = classType.GetCustomAttribute<DependencyRegisterAttribute>();
                Type interfaceType = classType.GetInterface(dependencyRegisterAttribute.InterfaceType.Name);

                if (interfaceType == null)
                    throw new InterfaceNotFoundException(classType.Name,dependencyRegisterAttribute.InterfaceType.Name);

                if (dependencyRegisterAttribute.DependencyTypes == DependencyTypes.Scopet)
                {
                    serviceDescriptors.AddScoped(interfaceType,classType);
                }else if(dependencyRegisterAttribute.DependencyTypes == DependencyTypes.Transient)
                {
                    serviceDescriptors.AddTransient(interfaceType,classType);
                }else if (dependencyRegisterAttribute.DependencyTypes == DependencyTypes.Singleton)
                {
                    serviceDescriptors.AddSingleton(interfaceType,classType);
                }else
                {
                    serviceDescriptors.AddScoped(interfaceType,classType);
                }

            });
        }
    }
}

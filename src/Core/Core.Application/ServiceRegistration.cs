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
            //Mevcut assembly içerisine tüm type değerlerini kontrolet ve bana sadece 'DependencyRegisterAttribute' özniteliğini uygulayan sınıfları getir
            Assembly.GetExecutingAssembly().GetTypes()?.Where(x => x.GetCustomAttribute<DependencyRegisterAttribute>() != null).ToList()?.ForEach(classType =>
            {
                //öznitelik içerisindeki property değerlerine erişmek için özniteliğe eriştik
                DependencyRegisterAttribute dependencyRegisterAttribute = classType.GetCustomAttribute<DependencyRegisterAttribute>();
                //öznitelik de tanımlanan interface tipi sayesinde mevcut class içerisinde uygulanan arabirimlerden ihtiyacımız olanın tipine eriştik
                Type interfaceType = classType.GetInterface(dependencyRegisterAttribute.InterfaceType.Name);

                //eğer arabirim bulunamadıysa anlaşılabilmesi için özel bir exception tanımladık.
                if (interfaceType == null)
                    throw new InterfaceNotFoundException(classType.Name,dependencyRegisterAttribute.InterfaceType.Name);

                //ihtiyacımıza göre kayıt işlemi yaptık.
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

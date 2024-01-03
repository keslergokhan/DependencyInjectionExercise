using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Attributes
{
    /// <summary>
    /// Microsoft.Extensions.DependencyInjection yapısına bağımlılıklarımızı dinamik bir şeklide uygulayabilmemize olanak tanır,
    /// öznitelik kayıt DI içerisine kayıt edilmesini istediğimiz sınıf için uygulanır.
    /// </summary>
    public class DependencyRegisterAttribute : Attribute
    {
        /// <summary>
        /// Uygulanan arabirim tipi
        /// </summary>
        public Type InterfaceType {  get; set; }
        /// <summary>
        /// DI kayıt tipi
        /// </summary>
        public DependencyTypes DependencyTypes { get; set; }

        public DependencyRegisterAttribute(Type ınterfaceType, DependencyTypes dependencyTypes)
        {
            this.InterfaceType = ınterfaceType;
            DependencyTypes = dependencyTypes;
        }
    }

    public enum DependencyTypes
    {
        Scopet = 1,
        Singleton = 2,
        Transient = 3
    }

}

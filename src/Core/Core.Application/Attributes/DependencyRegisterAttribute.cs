using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Attributes
{
    public class DependencyRegisterAttribute : Attribute
    {
        public Type InterfaceType {  get; set; }
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

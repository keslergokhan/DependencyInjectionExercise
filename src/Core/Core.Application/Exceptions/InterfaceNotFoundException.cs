using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Exceptions
{
    public class InterfaceNotFoundException : Exception
    {
        public string Message { get; set; }
        public InterfaceNotFoundException()
        {
            this.Message = "Interface bulunamadı !";
        }
        public InterfaceNotFoundException(string className,string interfaceName)
        {
            this.Message = $"{className} içerisinde {interfaceName} bulunamadı !";
        }
    }
}

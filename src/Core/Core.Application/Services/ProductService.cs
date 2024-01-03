using Core.Application.Attributes;
using Core.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    [DependencyRegister(typeof(IProductService),DependencyTypes.Scopet)]
    public class ProductService : IProductService
    {
        public void AddProduct()
        {
            throw new NotImplementedException();
        }

        public void RemoveProduct()
        {
            throw new NotImplementedException();
        }
    }
}

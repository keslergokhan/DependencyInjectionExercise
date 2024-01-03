using Core.Application.Attributes;
using Core.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    [DependencyRegister(typeof(IProductService), DependencyTypes.Scopet)]
    public class UserService : IUserService
    {
        public void AddUser()
        {
            throw new NotImplementedException();
        }

        public void RemoveUser()
        {
            throw new NotImplementedException();
        }
    }
}

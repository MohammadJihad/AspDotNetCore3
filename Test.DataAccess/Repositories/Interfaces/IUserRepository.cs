using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test.DataAccess.Base.Interfaces;
using Test.Entities.Entities;

namespace Test.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByUserName(string userName);
    }
}

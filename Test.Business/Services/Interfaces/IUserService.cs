using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test.Business.Base.Interfaces;
using Test.Entities.Entities;

namespace Test.Business.Services.Interfaces
{
    public interface IUserService : IServiceRepository<User>
    {
        User GetByUserName(string userName);
        bool UserExists(string username);
        User Login(string username, string password);
        Task<bool> UpdateImagePath(string LoginName, string imagePath);
        void DeleteByUserName(string loginName);
    }
}

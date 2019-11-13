using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.DataAccess.Base;
using Test.DataAccess.DBContext;
using Test.DataAccess.Repositories.Interfaces;
using Test.Entities.Entities;

namespace Test.DataAccess.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly TestDbContext _context;
        public UserRepository(TestDbContext context) : base(context)
        {
            _context = context;
        }
        public User GetByUserName(string userName)
        {
            var user =  _context.Users.Include(a => a.Country).FirstOrDefault(a => a.LoginName == userName);
            return user;
        }
    }
}

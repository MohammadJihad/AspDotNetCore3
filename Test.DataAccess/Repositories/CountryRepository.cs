using System;
using System.Collections.Generic;
using System.Text;
using Test.DataAccess.Base;
using Test.DataAccess.DBContext;
using Test.DataAccess.Repositories.Interfaces;
using Test.Entities.Entities;

namespace Test.DataAccess.Repositories
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        private readonly TestDbContext _context;
        public CountryRepository(TestDbContext context) : base(context)
        {
            _context = context;
        }
    }
}

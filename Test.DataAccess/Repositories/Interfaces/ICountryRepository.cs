using System;
using System.Collections.Generic;
using System.Text;
using Test.DataAccess.Base.Interfaces;
using Test.Entities.Entities;

namespace Test.DataAccess.Repositories.Interfaces
{
    public interface ICountryRepository : IRepository<Country>
    {
    }
}

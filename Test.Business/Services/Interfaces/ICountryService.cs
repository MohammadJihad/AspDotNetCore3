using System;
using System.Collections.Generic;
using System.Text;
using Test.Business.Base.Interfaces;
using Test.Entities.Entities;

namespace Test.Business.Services.Interfaces
{
    public interface ICountryService : IServiceRepository<Country>
    {
    }
}

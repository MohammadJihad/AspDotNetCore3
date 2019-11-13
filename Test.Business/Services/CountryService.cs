using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Business.Services.Interfaces;
using Test.DataAccess.Repositories.Interfaces;
using Test.Entities.Entities;

namespace Test.Business.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public Task<Country> Create(Country entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Country country)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Country> GetAll()
        {
            return _countryRepository.GetAll();
        }

        public Task<Country> GetById(int key)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Country entity)
        {
            throw new NotImplementedException();
        }
    }
}

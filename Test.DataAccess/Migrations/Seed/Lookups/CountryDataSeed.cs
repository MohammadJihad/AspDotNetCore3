using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Test.DataAccess.DBContext;
using Test.Entities.Entities;

namespace Test.DataAccess.Migrations.Seed.Lookups
{
    public class CountryDataSeed
    {
        public static void Fill(IApplicationBuilder applicationBuilder)
            {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //I'm bombing here
                var context = serviceScope.ServiceProvider.GetService<TestDbContext>();
                if (!context.Countries.Any())
                {
                    context.Countries.Add(new Country() { Name = "Afghanistan" });
                    context.Countries.Add(new Country() { Name = "Åland" });
                    context.Countries.Add(new Country() { Name = "Albania" });
                    context.Countries.Add(new Country() { Name = "Algeria" });
                    context.Countries.Add(new Country() { Name = "American Samoa" });
                    context.Countries.Add(new Country() { Name = "Andorra" });
                    context.Countries.Add(new Country() { Name = "Anguilla" });
                    context.Countries.Add(new Country() { Name = "Antarctica" });
                    context.Countries.Add(new Country() { Name = "Antigua and Barbuda" });
                    context.Countries.Add(new Country() { Name = "Argentina" });
                    context.Countries.Add(new Country() { Name = "Armenia" });
                    context.Countries.Add(new Country() { Name = "Aruba" });
                    context.Countries.Add(new Country() { Name = "Australia" });
                    context.Countries.Add(new Country() { Name = "Azerbaijan" });
                    context.Countries.Add(new Country() { Name = "Bahamas" });
                    context.Countries.Add(new Country() { Name = "Bahrain" });
                    context.Countries.Add(new Country() { Name = "Bangladesh" });
                    context.Countries.Add(new Country() { Name = "Barbados" });
                    context.Countries.Add(new Country() { Name = "Belarus" });
                    context.Countries.Add(new Country() { Name = "Belgium" });
                    context.Countries.Add(new Country() { Name = "Belize" });
                    context.Countries.Add(new Country() { Name = "Benin" });
                    context.Countries.Add(new Country() { Name = "Bermuda" });
                    context.Countries.Add(new Country() { Name = "Bhutan" });
                    context.Countries.Add(new Country() { Name = "Bolivia" });
                    context.Countries.Add(new Country() { Name = "Bonaire" });
                    context.Countries.Add(new Country() { Name = "Bosnia and Herzegovina" });
                    context.Countries.Add(new Country() { Name = "Botswana" });
                    context.Countries.Add(new Country() { Name = "Bouvet Island" });
                    context.Countries.Add(new Country() { Name = "Brazil" });
                    context.Countries.Add(new Country() { Name = "British Indian Ocean Territory" });
                    context.Countries.Add(new Country() { Name = "British Virgin Islands" });
                    context.Countries.Add(new Country() { Name = "Brunei" });
                    context.Countries.Add(new Country() { Name = "Bulgaria" });
                    context.Countries.Add(new Country() { Name = "Burkina" });
                    context.Countries.Add(new Country() { Name = "Burundi" });
                    context.Countries.Add(new Country() { Name = "Cambodia" });
                    context.Countries.Add(new Country() { Name = "Cameroon" });
                    context.Countries.Add(new Country() { Name = "Canada" });
                    context.Countries.Add(new Country() { Name = "Cape Verde" });
                    context.Countries.Add(new Country() { Name = "Cayman Islands" });
                    context.Countries.Add(new Country() { Name = "Central African Republic" });
                    context.Countries.Add(new Country() { Name = "Chad" });
                    context.Countries.Add(new Country() { Name = "Cyprus" });
                    context.Countries.Add(new Country() { Name = "Chile" });
                    context.Countries.Add(new Country() { Name = "China" });
                    context.Countries.Add(new Country() { Name = "Christmas Island" });
                    context.Countries.Add(new Country() { Name = "Colombia" });
                    context.Countries.Add(new Country() { Name = "Comoros" });
                    context.SaveChanges();
                }
            }
        }
    }
}

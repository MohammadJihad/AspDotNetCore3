using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Test.DataAccess.DBContext;

namespace Test.DataAccess.Migrations.Seed.User
{
    public class UserDataSeed
    {
        public static void Fill(IApplicationBuilder applicationBuilder)
        {
            //using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            //{
            //    //I'm bombing here
            //    var context = serviceScope.ServiceProvider.GetService<TestDbContext>();
            //    if (!context.Users.Any())
            //    {
            //        context.SaveChanges();
            //    }
            //}
        }
    }
}

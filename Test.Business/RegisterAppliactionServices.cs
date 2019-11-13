using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;
using Test.Business.Services;
using Test.Business.Services.Interfaces;
using Test.Common.Entities;
using Test.DataAccess.Base;
using Test.DataAccess.Base.Interfaces;
using Test.DataAccess.DBContext;
using Test.DataAccess.Migrations.Seed.Lookups;
using Test.DataAccess.Migrations.Seed.User;
using Test.DataAccess.Repositories;
using Test.DataAccess.Repositories.Interfaces;

namespace Test.Business
{
    public static class RegisterAppliactionServices
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TestDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            //Repositories (DataAccess)
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<ICountryService, CountryService>();

            services.AddOptions();
            var appSettingsSection = configuration.GetSection("AppSettings");
            var appSettings = appSettingsSection.Get<AppSetting>();

            services.Configure<AppSetting>(appSettingsSection);


            //configure jwt authentication
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                        var userloginName = context.Principal.Identity.Name;
                        var user = userService.GetByUserName(userloginName);
                        if (user == null)
                        {
                            // return unauthorized if user no longer exists
                            context.Fail("Unauthorized");
                        }
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.Token)),
                    ValidateIssuer = false,
                    ValidateAudience = false,

                };
            });
        }

        public static void DataSeed(this IApplicationBuilder app)
        {
            CountryDataSeed.Fill(app);
            UserDataSeed.Fill(app);
        }
    }
}

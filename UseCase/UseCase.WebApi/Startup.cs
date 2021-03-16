using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using UseCase.Business.Interfaces;
using UseCase.Business.Services;
using UseCase.Data.Context;
using UseCase.Data.Model;
using UseCase.Data.Repositories;
using UseCase.Data.UnitOfWork;
using UseCase.Extensions;

namespace UseCase.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(options => {
                    options.JsonSerializerOptions.IgnoreNullValues = true;

                });
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IInvoiceService, InvoiceManager>();
            services.AddScoped<ISubscriptionService, SubscriptionManeger>();

            // ===== Add our DbContext ========
            services
                .AddDbContext<UseCaseContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("UseCaseDb")));
            services.AddIdentity<User, ApiRole>().AddEntityFrameworkStores<UseCaseContext>();

            services.AddIdentityCore<Customer>().AddRoles<ApiRole>().AddEntityFrameworkStores<UseCaseContext>();
            services.AddIdentityCore<Cashier>().AddRoles<ApiRole>().AddEntityFrameworkStores<UseCaseContext>();
            services.AddIdentityCore<Corporation>().AddRoles<ApiRole>().AddEntityFrameworkStores<UseCaseContext>();
            

            // ===== Password settings ========
            services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = false;

                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            });

            // ===== Add Jwt Authentication ========
            byte[] key = Encoding.ASCII.GetBytes(Configuration["Application:Secret"]);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Audience = Configuration["Application:JwtIssuer"];
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.ClaimsIssuer = Configuration["Application:JwtIssuer"];
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = true,
                    RequireExpirationTime = true,
                    ClockSkew = TimeSpan.FromMinutes(0)
                };
                x.Events = new JwtBearerEvents()
                {
                    OnTokenValidated = (context) =>
                    {
                   
                        string name = context.Principal.Identity.Name;
                        if (string.IsNullOrEmpty(name))
                        {
                            context.Fail("Unauthorized. Please re-login");
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

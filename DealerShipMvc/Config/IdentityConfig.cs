using DealerShipMvc.Areas.Identity.Data;
using DealerShipMvc.Data;
using DealerShipMvc.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DealerShipMvc.Config
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddAuthorizationConfig(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, PermissaoNecessariaHandler>();

            services.AddAuthorization(options =>
            {
                //Não está sendo utilizado
                options.AddPolicy("CanDelete", policy => policy.Requirements.Add(new PermissaoNecessaria("CanDelete")));
            });

            return services;
        }

        public static IServiceCollection AddIdentityConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<DealerShipMvcContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DealerShipMvcContext")));

            services.AddDbContext<DealerShipMvcIdentityContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DealerShipMvcIdentityContext")));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddDefaultUI()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<DealerShipMvcIdentityContext>();

            return services;
        }
    }
}

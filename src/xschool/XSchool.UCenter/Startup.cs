using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using XSchool.UCenter.Extensions;
using XSchool.UCenter.Model;

namespace XSchool.UCenter
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("ResourceAPI", "资源管理接口"),
                new ApiResource("DataAPI", "数据接口"),
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

    }

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
            var connectonString = this.Configuration.GetConnectionString("Default");
            if (string.IsNullOrWhiteSpace(connectonString))
            {
                throw new Exception("找不到ConnectionStrings:Default节点,未能正确配置数据库连接字符串");
            }

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
            .AddClientStore<ClientStore>()
            .AddInMemoryIdentityResources(Config.GetIdentityResources())
            .AddInMemoryApiResources(Config.GetApiResources())
            .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
            .AddProfileService<ProfileService>();

            services.AddIdentity<User, IdentityRole<int>>(options =>
            {
                //options.User.AllowedUserNameCharacters
                //options.Password.RequireLowercase = false;
                //options.Password.RequireUppercase = false;
                //options.Password.RequireNonAlphanumeric = false;
                //options.Password.RequireDigit = false;
            })
            .AddEntityFrameworkStores<UCenterDbContext>()
            .AddClaimsPrincipalFactory<ClaimsIdentityFactory>()
            .AddSignInManager<SignInManager<User>>()
            .AddUserManager<UserManager<User>>()
            .AddDefaultTokenProviders();
            services.AddDbContextPool<UCenterDbContext>(options => options.UseSqlServer(connectonString), poolSize: 64);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
            app.UseMvc();
        }
    }
}

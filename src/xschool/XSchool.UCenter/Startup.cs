using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using XSchool.UCenter.Extensions;
using XSchool.UCenter.Model;

namespace XSchool.UCenter
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
            var connectonString = this.Configuration.GetConnectionString("Default");
            if (string.IsNullOrWhiteSpace(connectonString))
            {
                throw new Exception("找不到ConnectionStrings:Default节点,未能正确配置数据库连接字符串");
            }

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
            //.AddClientStore<ClientStore>()
            //.AddInMemoryIdentityResources(Config.GetIdentityResources())
            //.AddInMemoryApiResources(Config.GetApiResources())
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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,UCenterDbContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            db.Database.EnsureCreated();
            app.UseMvc();
        }
    }
}

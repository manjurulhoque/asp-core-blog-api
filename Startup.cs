using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using blogapi.Contracts;
using blogapi.Mappings;
using blogapi.Models;
using blogapi.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using blogapi.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace blogapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // public void ConfigureServices(IServiceCollection services)
        // {
        //     // services.AddCors();
        //     //
        //     // services.AddControllers();
        //     //
        //     // services.AddDbContext<ApplicationDbContext>(x => x
        //     //     .UseMySql(Configuration.GetConnectionString("DefaultConnection")));
        //     //
        //     // // configure strongly typed settings objects
        //     // var appSettingsSection = Configuration.GetSection("AppSettings");
        //     // services.Configure<AppSettings>(appSettingsSection);
        //     //
        //     // // configure jwt authentication
        //     // var appSettings = appSettingsSection.Get<AppSettings>();
        //     // var key = Encoding.ASCII.GetBytes(appSettings.Secret);
        //     // services.AddAuthentication(x =>
        //     //     {
        //     //         x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //     //         x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //     //     })
        //     //     .AddJwtBearer(x =>
        //     //     {
        //     //         x.Events = new JwtBearerEvents
        //     //         {
        //     //             OnTokenValidated = context =>
        //     //             {
        //     //                 var userService = context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
        //     //                 var userId = int.Parse(context.Principal.Identity.Name);
        //     //                 var user = userService.GetById(userId);
        //     //                 if (user == null)
        //     //                 {
        //     //                     // return unauthorized if user no longer exists
        //     //                     context.Fail("Unauthorized");
        //     //                 }
        //     //
        //     //                 return Task.CompletedTask;
        //     //             }
        //     //         };
        //     //         x.RequireHttpsMetadata = false;
        //     //         x.SaveToken = true;
        //     //         x.TokenValidationParameters = new TokenValidationParameters
        //     //         {
        //     //             ValidateIssuerSigningKey = true,
        //     //             IssuerSigningKey = new SymmetricSecurityKey(key),
        //     //             ValidateIssuer = false,
        //     //             ValidateAudience = false
        //     //         };
        //     //     });
        //
        //     services.AddDbContext<ApplicationDbContext>(x => x
        //         .UseMySql(Configuration.GetConnectionString("DefaultConnection")));
        //
        //     services.AddMvc(options => options.EnableEndpointRouting = false)
        //         .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        //     services.AddCors();
        //     // configure DI for application services
        //     // services.AddScoped<IUserService, UserService>();
        //     services.AddScoped<IPostRepository, PostRepository>();
        //     services.AddScoped<IUserRepository, UserRepository>();
        //
        //     services.AddAutoMapper(typeof(Maps));
        //     services.AddAuthentication(cfg =>
        //         {
        //             cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //             cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //         })
        //         .AddJwtBearer(options =>
        //         {
        //             options.Events = new JwtBearerEvents
        //             {
        //                 OnTokenValidated = context =>
        //                 {
        //                     var userService = context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
        //                     var userId = int.Parse(context.Principal.Identity.Name);
        //                     var user = userService.GetById(userId);
        //                     if (user == null)
        //                     {
        //                         // return unauthorized if user no longer exists
        //                         context.Fail("Unauthorized");
        //                     }
        //
        //                     return Task.CompletedTask;
        //                 }
        //             };
        //             options.TokenValidationParameters = new TokenValidationParameters
        //             {
        //                 ValidateIssuerSigningKey = true,
        //                 IssuerSigningKey =
        //                     new SymmetricSecurityKey(
        //                         Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Secret").Value)),
        //                 ValidateIssuer = false,
        //                 ValidateAudience = false
        //             };
        //         });
        //     
        //     services.AddAuthorization(options => 
        //     {
        //         options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
        //             .RequireAuthenticatedUser()
        //             .Build();
        //     });
        // }
        //
        // // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        // {
        //     // if (env.IsDevelopment())
        //     //     app.UseDeveloperExceptionPage();
        //     //
        //     // app.UseCors(
        //     //     options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
        //     // );
        //     //
        //     // app.UseAuthentication();
        //     //
        //     // app.UseRouting();
        //     //
        //     // app.UseAuthorization();
        //     //
        //     // app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        //     if (env.IsDevelopment())
        //     {
        //         app.UseDeveloperExceptionPage();
        //     }
        //     else
        //     {
        //         // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        //         // app.UseHsts();
        //     }
        //
        //     // app.UseHttpsRedirection();
        //     app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        //     app.UseAuthentication();
        //     app.UseMvc();
        // }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(x => x
                .UseMySql(Configuration.GetConnectionString("DefaultConnection"))
                .ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.IncludeIgnoredWarning)));
        
            services.AddCors();
            //services.AddControllers();
        
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        
            services.AddAutoMapper(typeof(Maps));
        
            // services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            // {
            //     builder.AllowAnyOrigin()
            //         .AllowAnyMethod()
            //         .AllowAnyHeader();
            // }));
        
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
        
            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    // x.Events = new JwtBearerEvents
                    // {
                    //     OnTokenValidated = context =>
                    //     {
                    //         var userService = context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
                    //         var userId = int.Parse(context.Principal.Identity.Name);
                    //         var user = userService.GetById(userId);
                    //         if (user == null)
                    //         {
                    //             // return unauthorized if user no longer exists
                    //             context.Fail("Unauthorized");
                    //         }
                    //
                    //         return Task.CompletedTask;
                    //     }
                    // };
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key: key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        RequireExpirationTime = false,
                        ValidateLifetime = true
                        // ValidateIssuerSigningKey = true,
                        // IssuerSigningKey = new SymmetricSecurityKey(key),
                        // ValidateIssuer = false,
                        // ValidateAudience = false
                    };
                });
        
            //services.AddControllersWithViews();
            services.AddMvc(options => options.EnableEndpointRouting = false);
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseHttpsRedirection();
            }
        
            app.UseStaticFiles();
        
            // app.UseRouting();
            //
            // //app.UseCors(builder => builder.WithOrigins("http://localhost:4200"));
            // app.UseCors("MyPolicy");
            //
            // app.UseAuthorization();
            //
            // app.UseEndpoints(endpoints =>
            // {
            //     endpoints.MapControllerRoute(
            //         name: "default",
            //         pattern: "{controller=Home}/{action=Index}/{id?}");
            // });
        
            //app.UseRouting();
        
            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        
            app.UseAuthentication();
            app.UseAuthorization();
        
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        
            //app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
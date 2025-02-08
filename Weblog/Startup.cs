using CoreLayer.Services;
using CoreLayer.Services.Comments;
using CoreLayer.Services.FileManager;
using CoreLayer.Services.Posts;
using CoreLayer.Services.Users;
using DataLayer.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Weblog
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
            // Register Razor Pages and MVC with Compatibility Version
            services.AddRazorPages();
            services.AddControllersWithViews();
            // Register application services
            services.AddScoped<IUserService, UserService>();
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IFileManager, FileManager>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddScoped<IMainPageService, MainPageService>();

            // Register DbContext
            services.AddDbContext<BlogContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Default"));
            });
            services.AddAuthorization(option =>
            {
                option.AddPolicy("AdminPolicy", builder =>
                {
                    builder.RequireRole("Admin");
                });
            });

            // Configure Authentication with Cookies
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(option =>
            {
                option.LoginPath = "/Auth/Login";
                option.ExpireTimeSpan = TimeSpan.FromDays(30);
                option.SlidingExpiration = true; // Enable sliding expiration
                option.AccessDeniedPath = "/Index"; // Optional: Custom access denied page
            });
           
            // Register CORS (optional, if needed for API or cross-origin requests)
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
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
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // Apply CORS globally (optional, if needed)
            app.UseCors("AllowAll");

            app.UseRouting();

            // Authentication & Authorization Middleware
            app.UseAuthentication();
            app.UseAuthorization();

            // Endpoint Routing
            app.UseEndpoints(endpoints =>
            {
                // Default route for areas
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                // Default route for controllers
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );

                // Razor Pages routing
                endpoints.MapRazorPages();
            });
        }
    }
}

using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using FoodshareMVC.Application;
using FoodshareMVC.Application.ViewModels.Bookings;
using FoodshareMVC.Application.ViewModels.Post;
using FoodshareMVC.Application.ViewModels.Reviews;
using FoodshareMVC.Domain.Interfaces;
using FoodshareMVC.Domain.Models.HelperModels;
using FoodshareMVC.Infrastructure;
using FoodshareMVC.Infrastructure.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Extensions.Logging;
using Serilog.Formatting.Compact;
using System.Configuration;
using Microsoft.AspNetCore.Authorization;
using FoodshareMVC.Domain.Models;

namespace FoodshareMVC.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<Context>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<Context>();

            builder.Services.AddApplication();
            builder.Services.AddInfrastructure();

            builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

            builder.Services.AddControllersWithViews();
            builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
            builder.Services.AddTransient<IValidator<NewPostVm>, NewPostValidation>();
            builder.Services.AddTransient<IValidator<NewBookingVm>, NewBookingValidation>();
            builder.Services.AddTransient<IValidator<NewReviewVm>, NewReviewValidation>();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredUniqueChars = 1;

                //TODO chanage, to true and add some logic
                options.SignIn.RequireConfirmedEmail = true;
                options.User.RequireUniqueEmail = true;

            });

            builder.Services.AddAuthentication().AddGoogle(options =>
            {
                IConfigurationSection googleAuthNSection = builder.Configuration.GetSection("Authentication:Google");
                options.ClientId = googleAuthNSection["ClientId"];
                options.ClientSecret = googleAuthNSection["ClientSecret"];
            });

            var app = builder.Build();

            // Logger configuration
            var loggerFactory = app.Services.GetService<ILoggerFactory>();
            loggerFactory.AddFile(builder.Configuration["Logging:LogFilePath"].ToString());

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Post}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
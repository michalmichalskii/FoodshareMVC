using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using FoodshareMVC.Application;
using FoodshareMVC.Application.Helpers;
using FoodshareMVC.Application.ViewModels.Bookings;
using FoodshareMVC.Application.ViewModels.Post;
using FoodshareMVC.Application.ViewModels.Reviews;
using FoodshareMVC.Domain.Interfaces;
using FoodshareMVC.Infrastructure;
using FoodshareMVC.Infrastructure.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Extensions.Logging;
using Serilog.Formatting.Compact;

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
                .AddEntityFrameworkStores<Context>();

            builder.Services.AddApplication();
            builder.Services.AddInfrastructure();

            builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

            builder.Services.AddControllersWithViews();
            builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
            builder.Services.AddTransient<IValidator<NewPostVm>, NewPostValidation>();
            builder.Services.AddTransient<IValidator<NewBookingVm>, NewBookingValidation>();
            builder.Services.AddTransient<IValidator<NewReviewVm>, NewReviewValidation>();

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

            //TODO  - AFTER MAKING LOGGING SYSYEM - make login/register page as start one
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Post}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
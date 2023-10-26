using FoodshareMVC.Domain.Helpers;
using FoodshareMVC.Domain.Interfaces;
using FoodshareMVC.Infrastructure.Helpers.IPInfoManage;
using FoodshareMVC.Infrastructure.Helpers.PhotoManage;
using FoodshareMVC.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Infrastructure
{
    public static  class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<IBookingRepository, BookingRepository>();
            services.AddTransient<IReviewRepository, ReviewRepository>();
            services.AddTransient<IIPInfoManager, IPInfoManager>();
            services.AddTransient<IPhotoManager, PhotoManager>();
            return services;
        }
    }
}

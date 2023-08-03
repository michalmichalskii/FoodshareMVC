﻿using FoodshareMVC.Application.Helpers;
using FoodshareMVC.Application.Interfaces;
using FoodshareMVC.Application.ViewModels.Bookings;
using FoodshareMVC.Application.ViewModels.Home;
using FoodshareMVC.Application.ViewModels.Post;
using FoodshareMVC.Application.ViewModels.User;
using FoodshareMVC.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Globalization;
using System.Net;

namespace FoodshareMVC.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly IBookingService _bookingService;

        public PostController(IPostService postService, IBookingService bookingService)
        {
            _postService = postService;
            _bookingService = bookingService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var posts = _postService.GetAllPostsForList();
            foreach (var post in posts.Posts)
            {
                _bookingService.DeleteExpiredBookingAndMakePostActive(post.Id);
            }

            //i dont know is this proper - there is a lot of code
            var listOfPosts = new ListPostForListVm();
            var ipInfo = new IPInfo();
            try
            {
                string url = "https://ipinfo.io?token=97d6472d4b29b1";
                var info = new WebClient().DownloadString(url);
                ipInfo = JsonConvert.DeserializeObject<IPInfo>(info);
                var myRII = new RegionInfo(ipInfo.Country);
                ipInfo.Country = myRII.EnglishName;
                listOfPosts.City = ipInfo.City;
                if (ipInfo.City != null)
                {
                    listOfPosts.Posts = _postService.GetAllActivePostsByCity(listOfPosts.City);
                }
                else
                {
                    listOfPosts.Posts = null;
                }
            }
            catch (Exception ex)
            {
                listOfPosts.Posts = null;
            }

            var model = _postService.GetAllActivePostsInYourCityForList(10, 1, "", listOfPosts.City);
            
            return View(model);
        }

        //TODO - AFTER MAKING LOGGING SYSYEM - a logged user should see his posts first
        [HttpPost]
        public IActionResult Index(int pageSize, int? pageNo, string searchString, string city)
        {
            if (!pageNo.HasValue)
            {
                pageNo = 1;
            }
            if (searchString is null)
            {
                searchString = String.Empty; // to sprawia ze wyszukam wszystkie elementy w przypadku nie wpisania niczego
            }

            var model = _postService.GetAllActivePostsInYourCityForList(pageSize, pageNo.Value, searchString, city);


            return View(model);
        }


        //TODO - AFTER MAKING LOGGING SYSYEM - after user logging, booking has to change const value of new booking to specyfic user one. Or maybe make separete form of booking makeing
        //TODO - imo url needs hash
        //TODO - make a droplist filter - a possibility pickup method

        [HttpGet("Post/AddBooking/{postId}")]
        public IActionResult AddBooking(int postId, [FromQuery] string pickUpMethod, [FromQuery] string pickUpAddress)
        {
            var model = new NewBookingVm
            {
                PostId = postId,
                PickUpMethod = pickUpMethod,
                PickUpAddress = pickUpAddress
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult AddBooking(NewBookingVm model)
        {
            var id = _bookingService.AddBooking(model.PostId, model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AddPost()
        {
            return View(new NewPostVm());
        }

        [HttpPost]
        public IActionResult AddPost(NewPostVm model)
        {
            var id = _postService.AddPost(model);
            return RedirectToAction("Index");
        }

        [HttpGet("Post/EditPost/{id}")]
        public IActionResult EditPost(int id)
        {
            var post = _postService.GetPostForEdit(id);
            return View(post);
        }

        [HttpPost]
        public IActionResult EditPost(NewPostVm model)
        {
            _postService.UpdatePost(model);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            _postService.DeletePost(id);
            return RedirectToAction("Index");
        }
    }
}

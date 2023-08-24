﻿using FoodshareMVC.Application.Helpers;
using FoodshareMVC.Application.Interfaces;
using FoodshareMVC.Application.ViewModels.Bookings;
using FoodshareMVC.Application.ViewModels.Post;
using FoodshareMVC.Application.ViewModels.Post.Filters;
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
        private readonly IPhotoService _photoService;
        private readonly IIPInfoService _iPInfoService;

        public PostController(IPostService postService, IBookingService bookingService,
            IPhotoService photoService, IIPInfoService iPInfoService)
        {
            _postService = postService;
            _bookingService = bookingService;
            _photoService = photoService;
            _iPInfoService = iPInfoService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            _bookingService.DeleteExpiredBooking();
            _postService.SetPostActiveAfterBookingExpirationDateHasPassed();
            var ipInfo = _iPInfoService.SetIPInfo();
            var currentCity = ipInfo.City;
            var listOfPosts = new ListPostForListVm();
            listOfPosts.Filter.City = ipInfo.City;

            if (ipInfo.City != null)
            {
                var model = _postService.GetAllActivePostsForList(10, 1, "", currentCity, "");
                model.Filter = listOfPosts.Filter;
                return View(model);
            }
            else
            {
                return View("Index");
            }
        }

        [HttpPost]
        public IActionResult Index(int pageSize, int? pageNo, string searchCreator, string city, string pickupMethod)
        {
            if (!pageNo.HasValue)
            {
                pageNo = 1;
            }
            if (searchCreator is null)
            {
                searchCreator = String.Empty; // to sprawia ze wyszukam wszystkie elementy w przypadku nie wpisania niczego
            }
            if (pickupMethod is null)
            {
                pickupMethod = String.Empty; // to sprawia ze wyszukam wszystkie elementy w przypadku nie wpisania niczego
            }

            var model = _postService.GetAllActivePostsForList(pageSize, pageNo.Value, searchCreator, city, pickupMethod);
            model.Filter.SearchCreator = searchCreator;
            model.Filter.City = city;
            model.Filter.PickupMethod = pickupMethod;

            return View(model);
        }

        [HttpGet]
        public IActionResult RemoveFilter(int pageSize, int? pageNo, string searchCreator, string city, string pickupMethod, string filterType)
        {
            if (!pageNo.HasValue)
            {
                pageNo = 1;
            }
            if (searchCreator is null)
            {
                searchCreator = String.Empty; // to sprawia ze wyszukam wszystkie elementy w przypadku nie wpisania niczego
            }
            if (pickupMethod is null)
            {
                pickupMethod = String.Empty; // to sprawia ze wyszukam wszystkie elementy w przypadku nie wpisania niczego
            }
            var model = _postService.GetAllActivePostsForList(pageSize, pageNo.Value, searchCreator, city, pickupMethod);

            if (filterType == "city")
            {
                model.Filter.City = "";
                model.Filter.SearchCreator = searchCreator;
                model.Filter.PickupMethod = pickupMethod;
            }
            if (filterType == "pickupMethod")
            {
                model.Filter.PickupMethod = "";
                model.Filter.SearchCreator = searchCreator;
                model.Filter.City = city;
            }
            if (filterType == "searchCreator")
            {
                model.Filter.SearchCreator = "";
                model.Filter.City = city;
                model.Filter.PickupMethod = pickupMethod;
            }

            var newModel = _postService.GetAllActivePostsForList(pageSize, pageNo.Value, model.Filter.SearchCreator, model.Filter.City, model.Filter.PickupMethod);
            newModel.Filter.City = model.Filter.City;
            newModel.Filter.SearchCreator = model.Filter.SearchCreator;
            newModel.Filter.PickupMethod = model.Filter.PickupMethod;
            return View("Index", newModel);
        }

        //TODO - AFTER MAKING LOGGING SYSYEM - A booking has to get bookerId automatically.

        [HttpGet]
        public IActionResult AddBooking(int postId)
        {
            var post = _postService.GetPost(postId);

            if (post == null)
                return RedirectToAction("Index");

            var model = new NewBookingVm
            {
                PickUpAddress = post.PickUpAddress,
                PickUpMethod = post.PossibilityPickUpMethod,
                PostId = postId
            };
            return View(model);

        }

        [HttpPost]
        public IActionResult AddBooking(NewBookingVm model)
        {
            var bookingId = _bookingService.AddBooking(model.PostId, model);
            var postId = _postService.SetPostNotActive(model.PostId);
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

        [HttpGet]
        public IActionResult EditPost(int id)
        {
            var post = _postService.GetPost(id);
            return View(post);
        }

        [HttpPost]
        public IActionResult EditPost(NewPostVm model)
        {
            _postService.UpdatePost(model);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            _postService.DeletePost(id);
            return RedirectToAction("Index");
        }

    }
}

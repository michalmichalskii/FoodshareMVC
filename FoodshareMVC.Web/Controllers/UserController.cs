using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using FoodshareMVC.Application.Interfaces;
using FoodshareMVC.Application.Services;
using FoodshareMVC.Application.ViewModels.Reviews;
using FoodshareMVC.Application.ViewModels.User;
using FoodshareMVC.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace FoodshareMVC.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IReviewService _reviewService;
        private readonly IPostService _postService;
        private readonly IValidator<NewReviewVm> _validator;

        public UserController(IUserService userService, IMapper mapper, IReviewService reviewService, IValidator<NewReviewVm> validator, IPostService postService)
        {
            _userService = userService;
            _mapper = mapper;
            _reviewService = reviewService;
            _validator = validator;
            _postService = postService;
        }

        [HttpGet("User/{id}")]
        public IActionResult Index(string id)
        {
            var model = _userService.GetUserVm(id);
            model.Posts = _postService.GetAllUserPostsForList(10,1,model.FullName);
            model.MyReviews = _reviewService.GetAllReviewsForList(10, 1, model.FullName);

            var currentUserReview = model.MyReviews.Reviews.FirstOrDefault(me => me.CreatorEmail == User.Identity.Name);
            if(currentUserReview != null)
            {
                model.MyReviews.Reviews.Remove(currentUserReview);
                model.MyReviews.Reviews.Insert(0, currentUserReview);
            }  

            model.StarAverage = _reviewService.GetStarAverage(model);

            return View(model);
        }


        //TODO - AFTER MAKING LOGGING SYSYEM - if logged user  his review should appear first
        [HttpPost]
        public IActionResult AddReview(NewReviewVm newReview)
        {
            newReview.CreateDateTime = DateTime.Now;
            newReview.ReviewerId = _userService.GetUserVmByEmail(User.Identity.Name).Id;
            
            var result = _validator.Validate(newReview);

            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);

                foreach (var key in ModelState.Keys)
                {
                    var errors = ModelState[key].Errors;
                    foreach (var error in errors)
                    {
                        TempData["ErrorMessage"] = error.ErrorMessage;
                    }
                }

                return RedirectToAction($"Index", new { id = newReview.ReviewedUserId });
            }

            var res = _reviewService.AddReview(newReview);

            return RedirectToAction($"Index", new { id = newReview.ReviewedUserId });
        }

        
    }
}

using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using FoodshareMVC.Application.Interfaces;
using FoodshareMVC.Application.Services;
using FoodshareMVC.Application.ViewModels.Reviews;
using FoodshareMVC.Application.ViewModels.User;
using FoodshareMVC.Domain.Models.BaseInherited;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace FoodshareMVC.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IReviewService _reviewService;
        private readonly IPostService _postService;
        private readonly IValidator<NewReviewVm> _validator;

        public UserController(IUserService userService, IReviewService reviewService, IValidator<NewReviewVm> validator, IPostService postService)
        {
            _userService = userService;
            _reviewService = reviewService;
            _validator = validator;
            _postService = postService;
        }

        [HttpGet("User/{id}")]
        public IActionResult Index(int id)
        {
            var model = _userService.GetUserVm(id);
            model.UserPosts = _postService.GetAllUserPosts(id);
            model.Rewievs = _reviewService.GetAllReviewsOfUser(id);

            var reviews = model.Rewievs;
            int countOfReviews = reviews.Count;
            decimal sumOfStars = 0;

            foreach (var review in reviews)
            {
                sumOfStars += review.AmountOfStars;
            }
            if (countOfReviews > 0)
            {
                var starAverage = sumOfStars / countOfReviews;
                model.StarAverage = starAverage;
            }


            return View(model);
        }


        //TODO - AFTER MAKING LOGGING SYSYEM - if logged user  his review should appear first
        [HttpPost]
        public IActionResult AddReview(NewReviewVm newReview)
        {
            newReview.CreateDateTime = DateTime.Now;
            newReview.ReviewerId = _userService.GetUserByEmail(User.Identity.Name).Id;
            
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

            var res = _userService.AddReview(newReview);

            return RedirectToAction($"Index", new { id = newReview.ReviewedUserId });
        }
    }
}

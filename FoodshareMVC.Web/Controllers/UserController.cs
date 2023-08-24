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
        private readonly IValidator<NewReviewVm> _validator;

        public UserController(IUserService userService, IReviewService reviewService, IValidator<NewReviewVm> validator)
        {
            _userService = userService;
            _reviewService = reviewService;
            _validator = validator;
        }
        //TODO - AFTER MAKING LOGGING SYSYEM - An url should not have the user's Id
        [HttpGet("User/{id}")]
        public IActionResult Index(int id)
        {
            var model = _userService.GetUserWithActivePostsAndGottenReviews(id);

            var reviews = _reviewService.GetAllReviewsOfUser(id);
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


        //TODO - AFTER MAKING LOGGING SYSYEM - if logged user already wrote review he cannot write another one, also his review should appear first
        //TODO - AFTER MAKING LOGGING SYSYEM - if user id logged change (in INDEX.cshtml) const value of revieverId

        [HttpPost]
        public IActionResult AddReview(NewReviewVm newReview)
        {
            newReview.CreateDateTime = DateTime.Now;

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

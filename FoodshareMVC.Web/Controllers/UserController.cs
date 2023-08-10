using FoodshareMVC.Application.Interfaces;
using FoodshareMVC.Application.Services;
using FoodshareMVC.Application.ViewModels.Reviews;
using FoodshareMVC.Domain.Models.BaseInherited;
using Microsoft.AspNetCore.Mvc;

namespace FoodshareMVC.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IReviewService _reviewService;

        public UserController(IUserService userService, IReviewService reviewService)
        {
            _userService = userService;
            _reviewService = reviewService;
        }
        [HttpGet("User/{id}")]
        public ActionResult Index(int id)
        {
            var model = _userService.GetUserWithActivePostsAndGottenReviews(id);

            var reviews = _reviewService.GetAllReviewsOfUser(id);
            int countOfReviews = reviews.Count();
            decimal sumOfStars = 0;

            foreach (var review in reviews)
            {
                sumOfStars += review.AmountOfStars;
            }
            if(countOfReviews > 0)
            {
                var starAverage = sumOfStars / countOfReviews;
                model.StarAverage = starAverage;
            }

            return View(model);
        }

        
        //TODO - AFTER MAKING LOGGING SYSYEM - if logged user already wrote review he cannot write another one, also his review should appear first
        //TODO - AFTER MAKING LOGGING SYSYEM - if user id logged change (in INDEX.cshtml) cont value of revieverId
        //TODO - make rating system like a star rate, not like a droplist of numbers
        //TODO - show rating of user under info about him

        [HttpPost]
        public ActionResult AddReview(NewReviewVm newReview)
        {
            try
            {
                newReview.CreateDateTime = DateTime.Now;
                var res = _userService.AddReview(newReview);
                var iv = ModelState.IsValid;
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction($"Index", new { id = newReview.ReviewedUserId});
        }
    }
}

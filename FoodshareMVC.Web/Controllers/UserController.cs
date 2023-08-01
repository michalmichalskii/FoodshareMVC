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

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("User/{id}")]
        public ActionResult Index(int id)
        {
            var model = _userService.GetUserWithActivePostsAndGottenReviews(id);
            return View(model);
        }

        //TODO - if logged user already wrote review he cannot write another one, also his review should appear first
        //TODO - AFTER MAKING LOGGING SYSYEM - if user id logged change (in INDEX.cshtml) cont value of revieverId
        //TODO - make rating system like a star rate, not like a droplist of numbers
        [HttpPost]
        public ActionResult AddReview(int reviewedUserId,string reviewDescription, float amountOfStars, int reviewerId)
        {
            var model = new NewReviewVm()
            {
                ReviewedUserId = reviewedUserId,
                ReviewDescription = reviewDescription,
                AmountOfStars = amountOfStars,
                ReviewerId = reviewerId
            };
            var id = _userService.AddReview(model);
            return RedirectToAction($"Index", new { id = reviewedUserId});
        }
    }
}

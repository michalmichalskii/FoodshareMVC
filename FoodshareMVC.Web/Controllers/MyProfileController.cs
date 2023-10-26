using FoodshareMVC.Application.Interfaces;
using FoodshareMVC.Application.Services;
using FoodshareMVC.Application.ViewModels.Post;
using FoodshareMVC.Application.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Xml.Linq;

namespace FoodshareMVC.Web.Controllers
{
    [Authorize]
    public class MyProfileController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPostService _postService;
        private readonly IReviewService _reviewService;

        public MyProfileController(IUserService userService, IPostService postService, IReviewService reviewService)
        {
            _userService = userService;
            _postService = postService;
            _reviewService = reviewService;
        }

        [HttpGet("MyProfile/EmailIndex/{email}")]
        public IActionResult EmailIndex(string email)
        {
            var modelId = _userService.GetUserDetailVmByEmail(email).Id;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Index()
        {
            var detailModel = _userService.GetUserDetailVmByEmail(User.Identity.Name);
            return View(detailModel);
        }

        [HttpGet]
        public IActionResult MyPosts()
        {
            string name = _userService.GetUserVmByEmail(User.Identity.Name).FullName;
            var userPosts = _postService.GetAllUserPostsForList(10, 1, name);
            return View(userPosts);
        }

        [HttpGet]
        public IActionResult MyReviews()
        {
            var user = _userService.GetUserVmByEmail(User.Identity.Name);
            user.MyReviews = _reviewService.GetAllReviewsForList(10, 1, user.FullName);
            user.StarAverage = _reviewService.GetStarAverage(user);
            return View(user);
        }

        [HttpGet]
        public IActionResult Edit()
        {
            var model = _userService.GetUserDetailVmByEmail(User.Identity.Name);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(UserDetailVm userDetail)
        {
            _userService.UpdateUser(userDetail);
            return RedirectToAction("Index");
        }
    }
}

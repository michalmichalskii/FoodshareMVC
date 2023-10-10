using FoodshareMVC.Application.Interfaces;
using FoodshareMVC.Application.Services;
using FoodshareMVC.Application.ViewModels.Post;
using FoodshareMVC.Application.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FoodshareMVC.Web.Controllers
{
    [Authorize]
    public class MyProfileController : Controller
    {
        private readonly IUserService _userService;

        public MyProfileController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("MyProfile/{id}")]
        public IActionResult Index(int id)
        {
            var model = _userService.GetUserDetail(id);
            return View(model);
        }

        [HttpGet("MyProfile/AddProfileInfo")]
        public IActionResult AddProfileInfo()
        {
            var userEmail = User.Identity.Name;

            if (_userService.IsLoggedUserInDb(userEmail))
            {
                return RedirectToAction($"Edit", new { id = _userService.GetUserByEmail(userEmail).Id });
            }

            return View(new NewUserDetailVm());
        }

        [HttpPost]
        public IActionResult AddProfileInfo(NewUserDetailVm model)
        {
            model.Email = User.Identity.Name;
            var id = _userService.AddProfileInfo(model);
            return RedirectToAction("Index", new { id = id });
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _userService.GetUserDetail(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(UserDetailVm userDetail)
        {
            _userService.UpdateUser(userDetail);
            return RedirectToAction("Index", new { id = userDetail.Id });
        }
    }
}

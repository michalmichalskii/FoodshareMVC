using FoodshareMVC.Application.Interfaces;
using FoodshareMVC.Application.Services;
using FoodshareMVC.Application.ViewModels.Post;
using FoodshareMVC.Application.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public IActionResult AddProfileInfo()
        {
            return View(new NewUserDetailVm());
        }

        [HttpPost]
        public IActionResult AddProfileInfo(NewUserDetailVm model)
        {
            var id = _userService.AddProfileInfo(model);
            return RedirectToAction("Index");
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
            return RedirectToAction("Index", new { id = userDetail.Id});
        }
    }
}

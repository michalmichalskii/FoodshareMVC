using FoodshareMVC.Application.Interfaces;
using FoodshareMVC.Application.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace FoodshareMVC.Web.Controllers
{
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

using FoodshareMVC.Application.Interfaces;
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
        [HttpGet("Profile/{id}")]
        public IActionResult Index(int id)
        {
            var model = _userService.GetUserDetail(id);
            return View(model);
        }
    }
}

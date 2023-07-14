using FoodshareMVC.Application.Interfaces;
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
            var model = _userService.GetUser(id);
            return View(model);
        }
    }
}

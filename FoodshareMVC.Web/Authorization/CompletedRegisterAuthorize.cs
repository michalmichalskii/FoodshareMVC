using FoodshareMVC.Application.Interfaces;
using FoodshareMVC.Application.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FoodshareMVC.Web.Authorization
{
    public class CompletedRegisterAuthorize : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authorized = context.HttpContext.User.Identity.IsAuthenticated;
            if (!authorized)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            var routeData = context.RouteData;
            var username = context.HttpContext.User.Identity.Name;
            var one = 1;
            UserVm userFromUsersDb = null;
            if (routeData.Values.TryGetValue("id", out var idValue))
            {
                if (int.TryParse(idValue?.ToString(), out var id))
                {

                    var serviceProvider = context.HttpContext.RequestServices;
                    var userService = serviceProvider.GetRequiredService<IUserService>();

                    userFromUsersDb = userService.GetUserByEmail(username);
                    if (userFromUsersDb != null)
                    {
                        return;
                    }
                    
             
                }
            }

            //TOOD - to check
            context.Result = new RedirectToActionResult("Index", "MyProfile", null);
        }
    }
}

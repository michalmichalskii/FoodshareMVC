using System.Security.Claims;
using FoodshareMVC.Application.Interfaces;
using FoodshareMVC.Domain.Interfaces;
using FoodshareMVC.Domain.Models.BaseInherited;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Build.Execution;
using Microsoft.VisualBasic;

namespace FoodshareMVC.Web.Authorization
{
    //TODOD - delete AuthHandler and AuthReq if app is gonna work with prop sol
    public class PostOwnerAuthorize : AuthorizeAttribute, IAuthorizationFilter
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
            if (routeData.Values.TryGetValue("id", out var idValue))
            {
                if(int.TryParse(idValue?.ToString(),out var id))
                {
                    var username = context.HttpContext.User.Identity.Name;

                    var serviceProvider = context.HttpContext.RequestServices;
                    var postService = serviceProvider.GetRequiredService<IPostService>();
                    var userService = serviceProvider.GetRequiredService<IUserService>();

                    var postOwnerId = postService.GetPost(id).CreatorId;
                    var postOwnerEmail = userService.GetUserDetail(postOwnerId).Email;

                    if(postOwnerEmail == username)
                    {
                        return;
                    }
                }
            }
            context.Result = new ForbidResult();
        }
    }
}

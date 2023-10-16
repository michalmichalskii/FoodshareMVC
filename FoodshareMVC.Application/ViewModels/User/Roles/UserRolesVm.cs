using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Application.ViewModels.User.Roles
{
    public class UserRolesVm
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Application.ViewModels.User.Roles
{
    public class ManageUserRolesVm
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool Selected { get; set; }
    }
}

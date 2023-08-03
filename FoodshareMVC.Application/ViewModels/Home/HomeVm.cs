using FoodshareMVC.Application.ViewModels.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Application.ViewModels.Home
{
    //TODO - delete
    public class HomeVm
    {
        public IEnumerable<PostForListVm> Posts { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}

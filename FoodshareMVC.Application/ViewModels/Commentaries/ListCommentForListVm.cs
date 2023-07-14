using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Application.ViewModels.Commentaries
{
    public class ListCommentForListVm
    {
        public List<CommentForListVm> Comments { get; set; }
        public int Count { get; set; }
    }
}

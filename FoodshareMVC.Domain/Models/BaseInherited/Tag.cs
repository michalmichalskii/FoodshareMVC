using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodshareMVC.Domain.Models.Base;

namespace FoodshareMVC.Domain.Models.BaseInherited
{
    public class Tag : BaseModel
    {
        public string Name { get; set; }
        public ICollection<PostTag> PostTags { get; set; }

    }
}

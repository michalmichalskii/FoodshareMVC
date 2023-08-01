using FoodshareMVC.Domain.Models.BaseInherited;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Domain.Interfaces
{
    public interface ITagRepository
    {
        void DeleteTag(int tagId);

        int AddTag(Tag tag);

        IQueryable<Tag> GetAllTags();
    }
}

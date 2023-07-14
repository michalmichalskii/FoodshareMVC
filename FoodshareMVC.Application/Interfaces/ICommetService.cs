using FoodshareMVC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Application.Interfaces
{
    public interface ICommetService
    {
        IQueryable<Comment> GetAllCommentsInSecyficPost(Post post);
    }
}

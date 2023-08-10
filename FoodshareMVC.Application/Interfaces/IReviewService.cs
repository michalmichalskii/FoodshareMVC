using FoodshareMVC.Application.ViewModels.Reviews;
using FoodshareMVC.Domain.Models.BaseInherited;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Application.Interfaces
{
    public interface IReviewService
    {
        List<ReviewForListVm> GetAllReviewsOfUser(int userId);
    }
}

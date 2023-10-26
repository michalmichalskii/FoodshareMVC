using FoodshareMVC.Application.ViewModels.Reviews;
using FoodshareMVC.Application.ViewModels.User;
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
        List<ReviewForListVm> GetAllReviewsOfUser(string userId);
        int AddReview(NewReviewVm newReview);
        void UpdateReview(NewReviewVm model);
        ListReviewForListVm GetAllReviewsForList(int pageSize, int pageNo, string name);
        decimal GetStarAverage(UserVm model);
    }
}

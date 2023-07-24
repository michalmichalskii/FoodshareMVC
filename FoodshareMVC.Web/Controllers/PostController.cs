using FoodshareMVC.Application.Interfaces;
using FoodshareMVC.Application.ViewModels.Bookings;
using FoodshareMVC.Application.ViewModels.Post;
using FoodshareMVC.Application.ViewModels.User;
using FoodshareMVC.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FoodshareMVC.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly IBookingService _bookingService;

        public PostController(IPostService postService, IBookingService bookingService)
        {
            _postService = postService;
            _bookingService = bookingService;
        }
        public IActionResult Index()
        {
            //Utworzyc widok dla postów
            //tabla z userami
            //filtrowanie postów, np. jaki sposób przekazania
            //przekazac filtry do serwisu
            //serwis musi przygotować
            //serwis musi zwrocic dane w odpowiednim formacie

            var model = _postService.GetAllPostsForList(10, 1, "");
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(int pageSize, int? pageNo, string searchString)
        {
            if (!pageNo.HasValue)
            {
                pageNo = 1;
            }
            if (searchString is null)
            {
                searchString = String.Empty; // to sprawia ze wyszukam wszystkie elementy w przypadku nie wpisania niczego
            }
            var model = _postService.GetAllPostsForList(pageSize, pageNo.Value, searchString);
            return View(model);
        }


        //TODO - in index 
        [HttpPost]
        public IActionResult BookPost(int postId)
        {
            var model = new NewBookingVm
            {
                PickUpAddress = "Malwowa 3",
                PickUpMethod = "odbiór osobisty",
                BookerId = 1, // Stała wartość identyfikatora użytkownika (tutaj ustawiono na 1)
            };

            var id = _bookingService.AddBooking(postId, model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AddPost()
        {
            return View(new NewPostVm());
        }

        [HttpPost]
        public IActionResult AddPost(NewPostVm model)
        {
            var id = _postService.AddPost(model);
            return RedirectToAction("Index");
        }

        [HttpGet("Post/EditPost/{id}")]
        public IActionResult EditPost(int id)
        {
            var post = _postService.GetPostForEdit(id);
            return View(post);
        }

        [HttpPost]
        public IActionResult EditPost(NewPostVm model)
        {
            _postService.UpdatePost(model);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            _postService.DeletePost(id);
            return RedirectToAction("Index");
        }
    }
}

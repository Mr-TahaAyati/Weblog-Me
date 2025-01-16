using CoreLayer.Services.Posts;
using Microsoft.AspNetCore.Mvc;

namespace Weblog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : Controller
    {
      private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }
        public IActionResult Index()
        {
            var model = _postService;
            return View(model);
        }
    }
}

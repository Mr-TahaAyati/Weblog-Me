using CoreLayer.Services.Posts;
using Microsoft.AspNetCore.Mvc;

namespace Weblog.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class PostController : Controller
	{
		private readonly IPostService _PostService;

		public PostController(IPostService postService)
		{
			_PostService = postService;
		}

		public IActionResult Index()
		{
			var posts = _PostService.GetAllPosts(); // دریافت پست‌ها
			return View(posts); // ارسال به ویو
		}
		public IActionResult Add()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
        public IActionResult Add(string i)
        {
            return View();
        }
    }

}

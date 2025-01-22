using CoreLayer.DTOs.Posts;
using CoreLayer.Services.Posts;
using CoreLayer.Utilities;
using Microsoft.AspNetCore.Mvc;
using Weblog.Areas.Admin.Models.Posts;

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

        // نمایش لیست پست‌ها
        public IActionResult Index()
        {
            var posts = _postService.GetAllPosts(); // دریافت همه پست‌ها
            return View(posts); // ارسال داده‌ها به ویو Index
        }

        // صفحه ساخت پست جدید
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(CreatePostViewModel createViewModel)
        {
            if (!ModelState.IsValid)
            {
                // بازگشت به صفحه Add با داده‌های ارسال‌شده توسط کاربر
                return View(createViewModel);
            }

            var result = _postService.CreatePost(new CreatePostDto()
            {
                Description = createViewModel.Description,
                ImageFile = createViewModel.ImageFile,
                Slug = createViewModel.Slug,
                Title = createViewModel.Title,
                UserId = User.GetUserId()
            });

            if (result.Status != OperationResultStatus.Success)
            {
                // افزودن پیام خطا به ModelState
                ModelState.AddModelError("", "خطا در ثبت اطلاعات. لطفاً دوباره تلاش کنید.");
                return View(createViewModel); // بازگشت به صفحه Add
            }

            // در صورت موفقیت، هدایت به صفحه Index
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var post=_postService.GetPostById(id);
            if (post == null)
                return RedirectToAction("Index");

            var model = new EditPostViewModel()
            {
                Description = post.Description,
                Slug = post.Slug,
                Title = post.Title
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,EditPostViewModel createViewModel)
        {
            if (!ModelState.IsValid)
            {
                // بازگشت به صفحه Add با داده‌های ارسال‌شده توسط کاربر
                return View(createViewModel);
            }

            var result = _postService.EditPost(new EditPostDto()
            {
                Description = createViewModel.Description,
                ImageFile = createViewModel.ImageFile,
                Slug = createViewModel.Slug,
                Title = createViewModel.Title,
                PostId = id
            });

            if (result.Status != OperationResultStatus.Success)
            {
                // افزودن پیام خطا به ModelState
                ModelState.AddModelError("", "خطا در ثبت اطلاعات. لطفاً دوباره تلاش کنید.");
                return View(createViewModel); // بازگشت به صفحه Add
            }

            // در صورت موفقیت، هدایت به صفحه Index
            return RedirectToAction(nameof(Index));
        }
    }
}

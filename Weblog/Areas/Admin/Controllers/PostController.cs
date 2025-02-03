using CoreLayer.DTOs.Posts;
using CoreLayer.Services.Posts;
using CoreLayer.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
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

        // نمایش لیست پست‌ها با فیلتر
        public IActionResult Index(string title)
        {
            var posts = _postService.GetAllPosts();

            // فیلتر پست‌ها بر اساس عنوان
            if (!string.IsNullOrEmpty(title))
            {
                posts = posts.Where(p => p.Title.Contains(title)).ToList();
            }

            ViewBag.TitleFilter = title;  // ارسال مقدار فیلتر به ویو
            return View(posts);
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
                return View(createViewModel);
            }

            // بررسی تکراری بودن Slug
            if (_postService.IsSlugExist(createViewModel.Slug))
            {
                ModelState.AddModelError(nameof(CreatePostViewModel.Slug), "Slug وارد شده قبلاً ثبت شده است.");
                return View(createViewModel);
            }

            var result = _postService.CreatePost(new CreatePostDto()
            {
                Description = createViewModel.Description,
                ImageFile = createViewModel.ImageFile,
                Slug = createViewModel.Slug,
                Title = createViewModel.Title,
                IsSpecial = createViewModel.IsSpecial,
                UserId = User.GetUserId()
            });

            if (result.Status != OperationResultStatus.Success)
            {
                ModelState.AddModelError(nameof(CreatePostViewModel.Slug), result.Message);
                return View(createViewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        // صفحه ویرایش پست
        public IActionResult Edit(int id)
        {
            var post = _postService.GetPostById(id);
            if (post == null)
                return RedirectToAction("Index");

            var model = new EditPostViewModel()
            {
                Description = post.Description,
                Slug = post.Slug,
                Title = post.Title,
                IsSpecial = post.IsSpecial 
                
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EditPostViewModel editViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(editViewModel);
            }

            // بررسی تکراری بودن Slug
            if (_postService.IsSlugExist(editViewModel.Slug))
            {
                ModelState.AddModelError(nameof(EditPostViewModel.Slug), "Slug وارد شده قبلاً ثبت شده است.");
                return View(editViewModel);
            }

            var result = _postService.EditPost(new EditPostDto()
            {
                Description = editViewModel.Description,
                ImageFile = editViewModel.ImageFile,
                Slug = editViewModel.Slug,
                Title = editViewModel.Title,
                PostId = id,
                IsSpecial = editViewModel.IsSpecial
            });

            if (result.Status != OperationResultStatus.Success)
            {
                ModelState.AddModelError(nameof(EditPostViewModel.Slug), result.Message);
                return View(editViewModel);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

using CoreLayer.DTOs.Posts;
using CoreLayer.Services.Posts;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace Weblog.Pages
{
    public class BlogModel : PageModel
    {
        private readonly IPostService _postService;

        public BlogModel(IPostService postService)
        {
            _postService = postService;
        }

        public List<PostDto> BlogPosts { get; set; }

        public void OnGet()
        {
            BlogPosts = _postService.GetAllPosts().Where(p => !p.IsSpecial).ToList(); // ✅ حذف پست‌های ویژه
        }
    }
}

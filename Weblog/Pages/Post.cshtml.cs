using CoreLayer.DTOs.Posts;
using CoreLayer.Services.Posts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Weblog.Pages
{
    public class PostModel : PageModel
    {
        private readonly IPostService _postService;
        public PostModel(IPostService postService)
        {
			_postService = postService;
        }
        public PostDto Post { get; set; }
        public IActionResult OnGet(string slug)
        {
            Post=_postService.GetPostBySlug(slug);
            if(Post == null) 
                return NotFound();
            return Page();
        }
    }
}

using CoreLayer.DTOs.Comments;
using CoreLayer.DTOs.Posts;
using CoreLayer.Services.Comments;
using CoreLayer.Services.Posts;
using CoreLayer.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Weblog.Pages
{
    public class PostModel : PageModel
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;

        public PostModel(IPostService postService, ICommentService commentService)
        {
            _postService = postService;
            _commentService = commentService;
        }

        public PostDto Post { get; set; }

        [Required]
        [BindProperty]
        public string Text { get; set; }

        [BindProperty]
        public int PostId { get; set; }

        public List<CommentDto> Comments { get; set; }

        public IActionResult OnGet(string slug)
        {
            Post = _postService.GetPostBySlug(slug);
            if (Post == null)
                return NotFound();

            // بررسی مقدار Description و اطمینان از مقداردهی مناسب
            if (string.IsNullOrWhiteSpace(Post.Description))
            {
                Post.Description = "توضیحی برای این پست ثبت نشده است.";
            }

            Comments = _commentService.GetPostComments(Post.PostId);
            return Page();
        }

        public IActionResult OnPost(string slug)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("Post", new { slug });

            if (!ModelState.IsValid)
                return Page();

            _commentService.CreatComment(new CreateCommentDto()
            {
                PostId = PostId,
                Text = Text,
                UserId = User.GetUserId()
            });

            return RedirectToPage("Post", new { slug });
        }
    }
}

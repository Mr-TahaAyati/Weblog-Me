using CoreLayer.DTOs.Posts;
using CoreLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Weblog.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMainPageService _mainPageService;

        public PostDto SpecialPost { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IMainPageService mainPageService)
        {
            _logger = logger;
            _mainPageService = mainPageService;
        }

        public void OnGet()
        {
            var mainPageData = _mainPageService.GetData();
            SpecialPost = mainPageData.SpecialPosts.FirstOrDefault(); // نمایش اولین پست ویژه
        }
    }
}

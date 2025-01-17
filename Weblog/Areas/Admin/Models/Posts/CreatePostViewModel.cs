using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Weblog.Areas.Admin.Models.Posts
{
    public class CreatePostViewModel
    {
        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [UIHint("Ckeditor4")]
        public string Description { get; set; }
        [Display(Name ="عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }
        [Display(Name = "Slug")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Slug { get; set; }
        [Display(Name = "عکس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public IFormFile ImageFile { get; set; }
    }
}

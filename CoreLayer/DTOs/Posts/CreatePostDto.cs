using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.DTOs.Posts
{
    public class CreatePostDto
    {
        public int UserId { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public IFormFile ImageFile { get; set; }
        public bool IsSpecial { get; set; }
    }
}

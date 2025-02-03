using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.DTOs.Posts
{
    public class PostDto
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public DateTime CreationDate { get; set; }
        public string ImageName { get; set; }
        public bool IsSpecial { get; set; }
    }
}

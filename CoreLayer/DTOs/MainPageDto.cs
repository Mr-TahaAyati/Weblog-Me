using CoreLayer.DTOs.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.DTOs
{
    public class MainPageDto
    {
        public List<PostDto> LatestPosts { get; set; }
        public List<PostDto> SpecialPosts { get; set; }
    }
}

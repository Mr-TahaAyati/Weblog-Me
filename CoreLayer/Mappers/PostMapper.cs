using CoreLayer.DTOs.Posts;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Mappers
{
    public class PostMapper
    {
        public static Post MapCreateDtoToPost(CreatePostDto dto)
        {
            return new Post()
            {
                Description = dto.Description,
                Slug = dto.Slug,
                Title = dto.Title,
                UserId = dto.UserId,
                IsDelete = false
            };
        }
        public static PostDto MapToDto(Post post)
        {
            return new PostDto()
            {
                Description = post.Description,
                Slug = post.Slug,
                Title = post.Title,
                UserId = post.UserId,
                CreationDate=post.CreationDate,
                ImageName = post.ImageName,
                PostId=post.Id
            };
        }

        public static Post MapEditDtoToPost(EditPostDto editDto, Post post)
        {
            post.Description = editDto.Description;
            post.Title = editDto.Title;
            post.Slug = editDto.Slug;
            return post;
        }
    }
}

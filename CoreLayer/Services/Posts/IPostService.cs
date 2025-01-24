using CoreLayer.DTOs.Posts;
using CoreLayer.Mappers;
using CoreLayer.Services.FileManager;
using CoreLayer.Utilities;
using DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreLayer.Services.Posts
{
    public interface IPostService
    {
        OperationResult CreatePost(CreatePostDto command);
        OperationResult EditPost(EditPostDto command);
        PostDto GetPostById(int postId);
        bool IsSlugExist(string slug);
        IEnumerable<PostDto> GetAllPosts();
    }

    public class PostService : IPostService
    {
        private readonly BlogContext _context;
        private readonly IFileManager _fileManager;

        public PostService(BlogContext context, IFileManager fileManager)
        {
            _context = context;
            _fileManager = fileManager;
        }

        public IEnumerable<PostDto> GetAllPosts()
        {
            return _context.Posts.Select(post => new PostDto
            {
                PostId = post.Id,
                Title = post.Title,
                Slug = post.Slug,
                ImageName = post.ImageName
            }).ToList();
        }

        public OperationResult CreatePost(CreatePostDto command)
        {
            if (string.IsNullOrWhiteSpace(command.Title))
                return OperationResult.Error("عنوان نمی‌تواند خالی باشد.");

            if (command.ImageFile == null)
                return OperationResult.Error("تصویر باید انتخاب شود.");

            var post = PostMapper.MapCreateDtoToPost(command);

            try
            {
                post.ImageName = _fileManager.SaveFileAndReturnName(command.ImageFile, Directories.PostImage);
                _context.Posts.Add(post);
                _context.SaveChanges();
                return OperationResult.Success();
            }
            catch (Exception ex)
            {
                return OperationResult.Error($"خطا در ذخیره‌سازی: {ex.Message}");
            }
        }

        public OperationResult EditPost(EditPostDto command)
        {
            var post = _context.Posts.FirstOrDefault(c => c.Id == command.PostId);
            var oldImage = post.ImageName;
            if (post == null)
                return OperationResult.NotFound("پست مورد نظر یافت نشد.");

            try
            {
                post = PostMapper.MapEditDtoToPost(command, post);

                if (command.ImageFile != null)
                {
                    post.ImageName = _fileManager.SaveFileAndReturnName(command.ImageFile, Directories.PostImage);
                }

                _context.Posts.Update(post);
                _context.SaveChanges();
                if (command.ImageFile != null)
                    _fileManager.DeleteFile(oldImage, Directories.PostImage);

                    return OperationResult.Success();
            }
            catch (Exception ex)
            {
                return OperationResult.Error($"خطا در ویرایش پست: {ex.Message}");  
            }
        }

        public PostDto GetPostById(int postId)
        {
            var post = _context.Posts.FirstOrDefault(c => c.Id == postId);
            return post == null ? null : PostMapper.MapToDto(post);
        }

        public bool IsSlugExist(string slug)
        {
            return _context.Posts.Any(p => p.Slug == slug);
        }
    }
}

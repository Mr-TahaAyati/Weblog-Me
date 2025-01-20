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
        PostDto GetPostById(int postId); // اصلاح نام متد
        bool IsSlugExist(string slug);

        IEnumerable<PostDto> GetAllPosts(); // متد برای دریافت تمام پست‌ها
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
            return _context.Posts
                .Select(post => new PostDto
                {
                    PostId = post.PostId,
                    Title = post.Title,
                    Slug = post.Slug,
                    ImageName = post.ImageName
                }).ToList(); // به صورت یک لیست از PostDto باز می‌گرداند
        }

        public OperationResult CreatePost(CreatePostDto command)
        {
            if (command.ImageFile == null)
                return OperationResult.Error("تصویر باید انتخاب شود.");

            var post = PostMapper.MapCreateDtoToPost(command);

            try
            {
                // ذخیره فایل
                post.ImageName = _fileManager.SaveFile(command.ImageFile, Directories.PostImage);

                // افزودن به دیتابیس
                _context.Posts.Add(post);
                _context.SaveChanges();

                return OperationResult.Success();
            }
            catch (Exception ex)
            {
                // در صورت بروز خطا
                return OperationResult.Error($"خطا در ذخیره‌سازی: {ex.Message}");
            }
        }

        public OperationResult EditPost(EditPostDto command)
        {
            var post = _context.Posts.FirstOrDefault(c => c.Id == command.PostId);

            if (post == null)
                return OperationResult.NotFound("پست مورد نظر یافت نشد.");

            try
            {
                post = PostMapper.MapEditDtoToPost(command, post);

                _context.Posts.Update(post);
                _context.SaveChanges();

                return OperationResult.Success();
            }
            catch (Exception ex)
            {
                return OperationResult.Error($"خطا در ویرایش پست: {ex.Message}");
            }
        }

        public PostDto GetPostById(int postId) // اصلاح نام متد
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

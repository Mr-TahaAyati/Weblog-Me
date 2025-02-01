using CoreLayer.DTOs.Comments;
using CoreLayer.Utilities;
using DataLayer.Context;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLayer.Services.Comments
{
    public interface ICommentService
    {
        OperationResult CreatComment(CreateCommentDto command);
        List<CommentDto> GetPostComments(int postId);
    }

    public class CommentService : ICommentService
    {
        private readonly BlogContext _context;

        public CommentService(BlogContext context)
        {
            _context = context;
        }

        public OperationResult CreatComment(CreateCommentDto command)
        {
            var comment = new PostComment()
            {
                PostId = command.PostId,
                Text = command.Text,
                UserId = command.UserId,
                CreationDate = DateTime.Now // اضافه کردن تاریخ ایجاد
            };

            _context.PostComments.Add(comment); // افزودن به دیتابیس
            _context.SaveChanges(); // ذخیره تغییرات
            return OperationResult.Success(); // نتیجه موفقیت
        }

        public List<CommentDto> GetPostComments(int postId)
        {
            return _context.PostComments
                .Include(c => c.User) // شامل کاربر
                .Where(c => c.PostId == postId)
                .Select(comment => new CommentDto()
                {
                    PostId = comment.PostId,
                    Text = comment.Text,
                    UserFullName = comment.User.FullName, // استفاده از FullName
                    CommentId = comment.Id,
                    CreationDate = comment.CreationDate
                })
                .ToList();
        }
    }
}

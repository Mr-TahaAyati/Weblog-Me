using CoreLayer.DTOs.Posts;
using CoreLayer.Mappers;
using CoreLayer.Utilities;
using DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CoreLayer.Services.Posts
{
	public interface IPostService
	{
		OperationResult CreatePost(CreatePostDto command);
		OperationResult EditPost(EditPostDto command);
		PostDto GetPostBuyId(int postid);
		bool IsSlugExist(string slug);

		// افزودن متد جدید
		IEnumerable<PostDto> GetAllPosts(); // متد برای دریافت تمام پست‌ها
	}

	public class PostService : IPostService
	{
		private readonly BlogContext _context;

		public PostService(BlogContext context)
		{
			_context = context;
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
				})
				.ToList(); // به صورت یک لیست از PostDto باز می‌گرداند
		}

		public OperationResult CreatePost(CreatePostDto command)
		{
			var Post = PostMapper.MapCreateDtoToPost(command);
			_context.Posts.Add(Post);
			_context.SaveChanges();
			return OperationResult.Success();
		}

		public OperationResult EditPost(EditPostDto command)
		{
			var post = _context.Posts.FirstOrDefault(c => c.Id == command.PostId);
			if (post == null)
				return OperationResult.NotFound();

			post = PostMapper.MapEditDtoToPost(command, post);
			_context.Posts.Update(post);
			_context.SaveChanges();
			return OperationResult.Success();
		}

		public PostDto GetPostBuyId(int postId)
		{
			var post = _context.Posts.FirstOrDefault(c => c.Id == postId);
			return PostMapper.MapToDto(post);
		}

		public bool IsSlugExist(string slug)
		{
			return _context.Posts.Any(p => p.Slug == slug);
		}
	}

}

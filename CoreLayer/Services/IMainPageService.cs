using CoreLayer.DTOs;
using CoreLayer.Mappers;
using DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CoreLayer.Services
{
    public interface IMainPageService
    {
        MainPageDto GetData();
    }

    public class MainPageService : IMainPageService
    {
        private readonly BlogContext _context;

        public MainPageService(BlogContext context)
        {
            _context = context;
        }

        public MainPageDto GetData()
        {
            var specialPosts = _context.Posts
                .Where(r => r.IsSpecial)
                .OrderByDescending(d => d.PostId) // اصلاح از Id به PostId
                .Take(4)
                .Select(post => PostMapper.MapToDto(post))
                .ToList();

            var latestPosts = _context.Posts
                .OrderByDescending(d => d.PostId) // اصلاح از Id به PostId
                .Take(6)
                .Select(post => PostMapper.MapToDto(post))
                .ToList();

            return new MainPageDto
            {
                LatestPosts = latestPosts,
                SpecialPosts = specialPosts
            };
        }
    }
}

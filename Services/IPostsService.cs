﻿using blazorHramPosts.Data;
using blazorHramPosts.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blazorHramPosts.Services
{
    public interface IPostsService
    {
        List<post> posts();
        Task<List<post>> postsAsync();        
        Task<post> post(int id);
    }

    public class PostsServices : IPostsService
    {
        DbContextOptions<ApplicationDbContext> options;
        public PostsServices(DbContextOptions<ApplicationDbContext> options)
        {
            this.options = options;
        }
        public List<post> posts()
        {
            using (var context = new ApplicationDbContext(options))
            {
                //var posts = context.posts.Include(p => p.comments).Include(p => p.likes).Include(p => p.tags).ToList();
                var posts = context.posts.Where(p=>p.published==true).Include(pt=>pt.posttags).Include(p => p.comments).Include(p => p.likes).ToList();
                return posts.OrderBy(p => p.created).Reverse().ToList();
            }
        }
        public async Task<List<post>> postsAsync()
        {
            using (var context = new ApplicationDbContext(options))
            {
                //var posts = await context.posts.Include(p => p.comments).Include(p => p.likes).Include(p => p.tags).ToListAsync();
                var posts = await context.posts.Where(p => p.published == true).Include(pt => pt.posttags).Include(p => p.comments).Include(p => p.likes).ToListAsync();
                return posts.OrderBy(p=>p.created).Reverse().ToList();
            }
        }

        public async Task<post> post(int id)
        {
            using (var context = new ApplicationDbContext(options))
            {
                return await context.posts.Where(p => p.published == true).Include(pt => pt.posttags).Include(p => p.comments).Include(p => p.likes).FirstOrDefaultAsync(p=>p.ID==id);
            }
        }        
    }
}

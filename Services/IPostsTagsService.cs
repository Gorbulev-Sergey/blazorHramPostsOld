﻿using blazorHramPosts.Data;
using blazorHramPosts.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blazorHramPosts.Services
{
    interface IPostTagsService
    {
        List<posttag> posttags();
    }

    public class PostTagsService : IPostTagsService
    {
        DbContextOptions<ApplicationDbContext> options;
        public PostTagsService(DbContextOptions<ApplicationDbContext> options)
        {
            this.options = options;
        }
        public List<posttag> posttags()
        {
            using (var context = new ApplicationDbContext(options))
            {
                var pt=context.posttags.Include(p=>p.post).Where(p => p.post.published == true).ToList();
                return pt;
            }
        }
    }
}

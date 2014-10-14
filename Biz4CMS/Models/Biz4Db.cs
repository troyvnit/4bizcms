﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Biz4CMS.Models
{
    public class Biz4Db:DbContext
    {
        public Biz4Db()
        : base("DefaultConnection")
    {
    }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<QuickLink> QuickLinks { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
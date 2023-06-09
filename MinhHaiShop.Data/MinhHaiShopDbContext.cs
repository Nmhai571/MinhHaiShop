﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MinhHaiShop.Model.Models;


namespace MinhHaiShop.Data
{
    public class MinhHaiShopDbContext : IdentityDbContext<ApplicationUser>
    {
        public MinhHaiShopDbContext(DbContextOptions options) : base(options)
        {
        }

        public MinhHaiShopDbContext()
        {
            
        }

        public DbSet<Footer> Footers { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuGroup> MenuGroups { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<Slide> Slides { get; set; }
        public DbSet<SupportOnline> SupportOnlines { get; set; }
        public DbSet<SystemConfig> SystemConfigs { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<VisitorStatistic> VisitorStatistics { get; set; }
        public DbSet<Error> Errors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<OrderDetail>().HasKey(x => new { x.OrderID, x.ProductID });
            modelBuilder.Entity<PostTag>().HasKey(x => new { x.TagID, x.PostID });
            modelBuilder.Entity<ProductTag>().HasKey(x => new { x.TagID, x.ProductID });
            base.OnModelCreating(modelBuilder);
        }
    }
}

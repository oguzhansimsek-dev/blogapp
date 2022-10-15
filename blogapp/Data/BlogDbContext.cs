using System;
using Microsoft.EntityFrameworkCore;
using blogapp.Models;

namespace blogapp.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}

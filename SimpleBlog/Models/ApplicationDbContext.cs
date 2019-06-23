using System;
using SimpleBlog.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SimpleBlog.Models {
    public class ApplicationDbContext : IdentityDbContext<User> {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Post>()
                .HasOne(pt => pt.Author)
                .WithMany(px => px.Posts)
                .HasForeignKey(pt => pt.AuthorId);

            builder.Entity<Post>()
                .HasOne(pt => pt.Category)
                .WithMany(px => px.Posts)
                .HasForeignKey(pt => pt.CategoryId);

            builder.Entity<Comment>()
                .HasOne(pt => pt.Post)
                .WithMany(px => px.Comments)
                .HasForeignKey(pt => pt.PostId);

            builder.Entity<Comment>()
                .HasOne(pt => pt.User)
                .WithMany(px => px.Comments)
                .HasForeignKey(pt => pt.UserId);

            builder.Entity<Category>()
                .HasIndex(c => new {c.Id, c.Name})
                .IsUnique();
            
            var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
                v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                        property.SetValueConverter(dateTimeConverter);
                }
            }
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public new DbSet<User> Users { get; set; }
    }
}
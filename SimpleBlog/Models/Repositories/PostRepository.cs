using System.Linq;
using SimpleBlog.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace SimpleBlog.Models.Repositories
{
    public class PostRepository : IPostRepository
    {
        private ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Post> Posts => _context.Posts
            .Include("Author")
            .Include("Category")
            .Include("Comments")
            .Include("Comments.User");
        
        public Post getPost(string id)
        {
            return Posts.FirstOrDefault(p => p.Id == id);
        }

        public void addPost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public void deletePost(Post post)
        {
            if (_context.Posts.Contains(post))
            {
                _context.Posts.Remove(post);
                _context.SaveChanges();
            }
        }

        public void savePost(Post post)
        {
            if (_context.Posts.Contains(post))
            {
                _context.SaveChanges();
            }
        }
    }
}
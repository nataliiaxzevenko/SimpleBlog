using System.Linq;
using SimpleBlog.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace SimpleBlog.Models.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Comment> Comments => _context.Comments.Include("User").Include("Post");
        public Comment getComment(string id)
        {
            return Comments.FirstOrDefault(c => c.Id == id);
        }

        public void addComment(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }

        public void deleteComment(Comment comment)
        {
            if (_context.Comments.Contains(comment))
            {
                _context.Comments.Remove(comment);
                _context.SaveChanges();
            }
        }
    }
}
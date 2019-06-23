using System.Linq;
using SimpleBlog.Models.Entities;

namespace SimpleBlog.Models.Repositories
{
    public interface ICommentRepository
    {
        IQueryable<Comment> Comments { get; }
        Comment getComment(string id);
        void addComment(Comment comment);
        void deleteComment(Comment comment);
    }
}
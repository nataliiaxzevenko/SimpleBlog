using System.Collections.Generic;
using System.Linq;
using SimpleBlog.Models.Entities;

namespace SimpleBlog.Models.Repositories
{
    public interface IPostRepository
    {
        IQueryable<Post> Posts { get; }
        Post getPost(string id);
        void addPost(Post post);
        void deletePost(Post post);
        void savePost(Post post);
    }
}
using System.Collections.Generic;
using SimpleBlog.Models.Entities;

namespace SimpleBlog.Models.ViewModels
{
    public class PostListViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string PostToOpen { get; set; }
    }
}
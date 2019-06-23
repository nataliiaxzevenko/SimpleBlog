using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleBlog.Models.Entities
{
    public class Post
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public User Author { get; set; }
        public string AuthorId { get; set; }
        public string Content { get; set; }
        public Category Category { get; set; }
        public string CategoryId { get; set; }
        public DateTime PublishedDateTime { get; set; }

        public virtual List<Comment> Comments { get; set; }
    }
}
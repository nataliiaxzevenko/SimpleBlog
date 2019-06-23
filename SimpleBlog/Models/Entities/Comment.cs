using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleBlog.Models.Entities
{
    public class Comment
    {
        public string Id { get; set; }
        public Post Post { get; set; }
        public string PostId { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        [Required(ErrorMessage = "Comment field is empty")]
        [StringLength(120, ErrorMessage = "Must be minimum {0} symbol", MinimumLength = 1)]
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
    }
}
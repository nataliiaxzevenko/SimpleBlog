using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace SimpleBlog.Models.Entities
{
    public class User : IdentityUser
    {
        public virtual List<Post> Posts { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SimpleBlog.Models.Entities
{
    public class Category
    {
        public string Id { get; set; }
        public string Name { get; set; }
        
        public virtual List<Post> Posts { get; set; }
    }
}
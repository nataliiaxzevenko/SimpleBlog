using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SimpleBlog.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SimpleBlog.Models.ViewModels
{
    public class PostCreateViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Category { get; set; }
        public List<Category> Categories { get; set; }
    }
}
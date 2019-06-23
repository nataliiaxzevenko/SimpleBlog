using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using SimpleBlog.Models.Entities;
using SimpleBlog.Models.Repositories;
using SimpleBlog.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace SimpleBlog.Controllers
{
    public class PostController : Controller
    {
        private IPostRepository _postRepository;
        private ICategoryRepository _categoryRepository;
        private ICommentRepository _commentRepository;
        public UserManager<User> UserManager;
        public int PageSize = 6;

        public PostController(IPostRepository postRepository, ICategoryRepository categoryRepository, ICommentRepository commentRepository, UserManager<User> userManager)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _commentRepository = commentRepository;
            UserManager = userManager;
        }

        [Route("")]
        [Route("Post/List")]
        public ViewResult List(int postPage = 1, string postId = "", string categoryId = "")
        {
            var posts = _postRepository.Posts;
            if (!String.IsNullOrEmpty(categoryId) && _categoryRepository.getCategory(categoryId) != null)
            {
                posts = posts.Where(elem => elem.CategoryId == categoryId);
            }
            PostListViewModel list = new PostListViewModel
            {
                Posts = posts
                    .OrderByDescending(p => p.PublishedDateTime)
                    .Skip((postPage - 1) * PageSize)
                    .Take(PageSize),
                PostToOpen = postId,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = postPage,
                    ItemsPerPage = PageSize,
                    TotalItems = posts.Count()
                }
            };
            return View(list);
        }

        [Route("Post/Create")]
        public ActionResult Create()
        {
            PostCreateViewModel postCreateViewModel = new PostCreateViewModel();
            postCreateViewModel.Categories = _categoryRepository.Categories.ToList();
            return View(postCreateViewModel);
        }

        [HttpPost("Post/Create")]
        public async Task<ActionResult> Create(PostCreateViewModel model)
        {
            User user = await UserManager.FindByNameAsync(HttpContext.User.Identity.Name);
            
            if (ModelState.IsValid && user != null)
            {
                Post post = new Post
                {
                    Title = model.Title,
                    Content = model.Content,
                    Author = user,
                    PublishedDateTime = DateTime.Now,
                    Category = _categoryRepository.addCategory(new Category{Name = model.Category})
                };
                _postRepository.addPost(post);

                return RedirectToAction("List", "Post");
            }

            return View(model);
        }

        [HttpPost("Post/CreateComment")]
        [Authorize]
        public async Task<ActionResult> CreateComment(string postId, string text, int postPage, string categoryId)
        {
            User user = await UserManager.FindByNameAsync(HttpContext.User.Identity.Name);
            if (ModelState.IsValid && user != null)
            {
                Post post = _postRepository.getPost(postId);
                if (post != null)
                {
                    Comment comment = new Comment
                    {
                        Post = post,
                        Text = text,
                        User = user,
                        DateTime = DateTime.Now,
                    };
                    _commentRepository.addComment(comment);
                    return RedirectToAction("List", "Post", new {postId, postPage, categoryId}, "comment:"+comment.Id);
                }
            }

            return View("List");
        }
    }
}
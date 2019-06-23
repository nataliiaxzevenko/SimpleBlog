using System.Linq;
using SimpleBlog.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace SimpleBlog.Models.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private ApplicationDbContext _context;
        
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Category> Categories => _context.Categories.Include("Posts");
        public Category getCategory(string id)
        {
            return Categories.FirstOrDefault(c => c.Id == id);
        }

        public Category addCategory(Category category)
        {
            Category instance = _context.Categories.FirstOrDefault(c => c.Name == category.Name);
            if (instance == null)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return category;
            }

            return instance;
        }

        public void deleteCategory(Category category)
        {
            if (_context.Categories.Contains(category))
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }

        public void saveCategory(Category category)
        {
            if (_context.Categories.Contains(category))
            {
                _context.SaveChanges();
            }
        }
    }
}
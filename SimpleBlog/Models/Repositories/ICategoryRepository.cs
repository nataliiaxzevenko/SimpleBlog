using System.Linq;
using SimpleBlog.Models.Entities;

namespace SimpleBlog.Models.Repositories
{
    public interface ICategoryRepository
    {
        IQueryable<Category> Categories { get; }
        Category getCategory(string id);
        Category addCategory(Category category);
        void deleteCategory(Category category);
        void saveCategory(Category category);
    }
}
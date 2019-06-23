using System.Linq;
using SimpleBlog.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace SimpleBlog.Components
{
    public class CategoryPanelViewComponent : ViewComponent
    {
        private ICategoryRepository _categoryRepository;

        public CategoryPanelViewComponent(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IViewComponentResult Invoke()
        {
            return View(_categoryRepository.Categories.OrderBy(elem => elem.Name));
        }
    }
}
using SimpleBlog.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace SimpleBlog.Components
{
    public class LoginViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(new LoginViewModel());
        }
    }
}
using SimpleBlog.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace SimpleBlog.Components
{
    public class SignupViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(new CreateUserModel());
        }
    }
}
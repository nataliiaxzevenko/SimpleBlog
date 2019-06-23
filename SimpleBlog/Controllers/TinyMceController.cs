using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace SimpleBlog.Controllers
{
    public class TinyMceController : Controller
    {
        private readonly IHostingEnvironment hostingEnvironment;

        public TinyMceController(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TinyMceUpload(IFormFile file)
        {
            if (file != null || file.Length != 0)
            {
                FileInfo fi = new FileInfo(file.FileName);

                var newFilename = "image_" + String.Format("{0:d}",
                                      (DateTime.Now.Ticks / 10) % 100000000) + fi.Extension;
                var webPath = hostingEnvironment.WebRootPath;
                var path = Path.Combine(webPath + "/ImageFiles", newFilename);

                var pathToSave = @"/ImageFiles/" + newFilename;

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }


                return Json(new
                {
                    location = (HttpContext.Request.IsHttps
                                   ? "https://"
                                   : "http://") + HttpContext.Request.Host + pathToSave
                });
            }

            return BadRequest();
        }
    }
}
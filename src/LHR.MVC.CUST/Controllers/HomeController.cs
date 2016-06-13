using Lhr.Mvc.STH.Model;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.OptionsModel;
using Lhr.Types.System;

namespace Lhr.Mvc.Custom.Controllers
{

    public class HomeController: Controller
    {
        private IHostingEnvironment CurrentEnvironment { get; set; }
        private AppSettings _settings;
        public HomeController(IHostingEnvironment appEnvironment, IOptions<AppSettings> settings)
        {
            CurrentEnvironment = appEnvironment;
            _settings = settings.Value;
        }

        [Route("[controller]/[action]", Order = 0)]
        public IActionResult About()
        {

            ViewData["Message"] = "This is About from Custom STC controller!!!!!!!!!!!!!!</br>" + 
                                    $"Your application looking for views here: {_settings.PathToCoreViewsDirectory} and for addons here {_settings.AddonsFolderName}";

            return View();
        }
        [Route("[controller]/[action]", Order = 0)]
        public IActionResult Contact()
        {
            //HttpContext.Response.Body. Write(Encoding.UTF8.GetBytes("Hi there!"), 0, 9);
            //HttpContext.Response.ContentType = "text/html";
            ViewData["Message"] = "Lanteria contacts.";
            ViewBag.SomeData = new SomeData() { Id = 1, Name = "Jordano Bruno", Code="JB"};
            return View();
        }
    }
}

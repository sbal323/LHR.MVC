using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.FileProviders;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.OptionsModel;
using LHR.MVC.Modules.Application;
namespace LHR.MVC.Controllers
{
    public class HomeController : Controller
    {
        private IHostingEnvironment CurrentEnvironment { get; set; }
        private AppSettings _settings;
        public HomeController(IHostingEnvironment appEnvironment, IOptions<AppSettings> settings)
        {
            CurrentEnvironment = appEnvironment;
            _settings = settings.Value;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Title"] = $"Your application looking for views here: {_settings.PathToCoreViewsDirectory} and for addons here {_settings.AddonsFolderName}";

            return View();
        }

        public IActionResult Contact()
        {
            return View();

        }

        public IActionResult Error()
        {
            return View();
        }
    }
}

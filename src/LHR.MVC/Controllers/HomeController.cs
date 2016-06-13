using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.FileProviders;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.OptionsModel;
using Lhr.Types.System;
using Lhr.Bl;


namespace Lhr.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private IBlEmployee BLEmployee { get; set; }
        private AppSettings Settings { get; set; }
        public HomeController(IOptions<AppSettings> settings, IBlEmployee blEmployee)
        {
            Settings = settings.Value;
            BLEmployee = blEmployee;
        }
        public IActionResult Index()
        {
            var empl = BLEmployee.Get(0);
            return View(empl);

        }

        public IActionResult About()
        {
            ViewData["Title"] = $"Your application looking for views here: {Settings.PathToCoreViewsDirectory} and for addons here {Settings.AddonsFolderName}";

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

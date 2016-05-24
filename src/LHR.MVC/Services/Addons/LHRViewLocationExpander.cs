using Microsoft.AspNet.Mvc.Filters;
using Microsoft.AspNet.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.AspNet.Mvc.Controllers;
using LHR.MVC.Modules.Application;
using Microsoft.Extensions.OptionsModel;

namespace LHR.MVC
{
    public class LHRViewLocationExpander : IViewLocationExpander
    {
        private AppSettings settings;
        public LHRViewLocationExpander(IOptions<AppSettings> appSettings) {
            settings = appSettings.Value;
        }
        //private const string PathToCoreViewsDirectory = "";//"/approot/packages/LHR.MVC/1.0.0/root";
        public void PopulateValues(ViewLocationExpanderContext context) { }

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            //AppSettings settings = LHRSystem.GetInstance().ApplicationSettings;
            List<string> res = new List<string>();
            var actionContext = (ControllerActionDescriptor)context.ActionContext.ActionDescriptor;
            var assembly = actionContext.ControllerTypeInfo.Assembly;
            var assemblyName = assembly.GetName().Name;
            res.Add(settings.AddonsFolderName + assemblyName + "/Views/{1}/Customized/{0}.cshtml");
            foreach (var viewLocation in viewLocations)
            {
                res.Add(settings.AddonsFolderName + assemblyName + viewLocation);
            }
            res.Add(settings.PathToCoreViewsDirectory + "/Views/{1}/Customized/{0}.cshtml");
            res.Add(settings.PathToCoreViewsDirectory + "/Views/{1}/{0}.cshtml");
            res.Add(settings.PathToCoreViewsDirectory + "/Views/Shared/{0}.cshtml");
            return res;
        }
    }
}

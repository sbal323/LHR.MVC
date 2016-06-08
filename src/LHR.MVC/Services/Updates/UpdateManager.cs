using LHR.Core;
using LHR.MVC.Services.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LHR.MVC.Services.Updates
{
    public class UpdateManager
    {
        public CoreMnager Core { get; set; }
        public UpdateManager(CoreMnager coreManager)
        {
            Core = coreManager;
        }
        public void Update(UpdateVersion fromVersion , UpdateVersion toVersion , Assembly updateAssembly)
        {
            Type updateType = typeof(IUpdate);
            List<IUpdate> updates = updateAssembly.GetTypes().
                Where(t => updateType.IsAssignableFrom(t) && t.IsClass).
                Select(v => (IUpdate)Activator.CreateInstance(v)).
                Where(v => v.Version.InRange(fromVersion, toVersion)).
                OrderBy(t => t.Version.Version).ToList();
            foreach(var u in updates){
                u.ApplyChanges(this);
            }
        }
        public void Update(UpdateVersion fromVersion, UpdateVersion toVersion)
        {
            Update(fromVersion, toVersion, Assembly.GetExecutingAssembly());
        }
    }
}
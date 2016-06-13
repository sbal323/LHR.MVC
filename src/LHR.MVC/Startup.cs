using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Lhr.Mvc.Models;
using Lhr.Mvc.Services;
using Microsoft.AspNet.Mvc.Razor;
using Microsoft.AspNet.FileProviders;
using Microsoft.AspNet.Mvc.Infrastructure;
using Microsoft.Extensions.PlatformAbstractions;
using System.Reflection;
using Lhr.Mvc.Services.Di;
using Microsoft.Extensions.OptionsModel;
using System.IO;
using Lhr.Types.System;
using Lhr.Core;
using Lhr.Mvc.Services.Core;
using Lhr.Mvc.Services.Updates;

namespace Lhr.Mvc
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            CurrentEnvironment = env;
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc(
                options =>
                {
                    // Add global filters
                    options.Filters.Add(new Filters.ErrorHandlerFilter());
                });

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            // Add options
            services.AddOptions();
            services.Configure<AppSettings>(Configuration.GetSection(nameof(AppSettings)));
            var serviceAppSettings = services.BuildServiceProvider().GetService<IOptions<AppSettings>>();

            var rootFileProvider = new PhysicalFileProvider(CurrentEnvironment.WebRootPath + "\\..");

            //create the custom plugin directory provider
            services.AddSingleton<IAssemblyProvider, LhrAssemblyProvider>(provider =>
            {
                var pluginAssemblyProvider = new LhrPluginAssemblyProvider(
                    rootFileProvider,
                    PlatformServices.Default.AssemblyLoadContextAccessor,
                    PlatformServices.Default.AssemblyLoaderContainer,
                    serviceAppSettings);
                //return the composite one - this wraps the default MVC one
                return new LhrAssemblyProvider(
                    provider.GetRequiredService<ILibraryManager>(),
                    new IAssemblyProvider[] { pluginAssemblyProvider });
            });

            // Create the custom Razor View location expander
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.FileProvider = rootFileProvider;
                options.ViewLocationExpanders.Add(new LhrViewLocationExpander(serviceAppSettings));
            });
            // Init core
            CoreMnager coreManager = new CoreMnager(serviceAppSettings.Value);
            // Apply Updates
            UpdateManager upm = new UpdateManager(coreManager);
            UpdateVersion fromVersion = new UpdateVersion(coreManager.CoreGeneralSettingsManager.GetCurrentSystemVersion().Value);
            upm.Update(fromVersion, new UpdateVersion(1, 0, 0));
            // Manage DI
            DiProvider diProvider = new DiProvider(serviceAppSettings.Value, rootFileProvider, services, coreManager.CoreDIManager);
            diProvider.LoadLibraries();
            diProvider.RegisterDependencies();
        }
        private IHostingEnvironment CurrentEnvironment { get; set; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            //ConfigurationBinder.Bind(Configuration.GetSection("AppSettings"), LHRSystem.GetInstance().ApplicationSettings);
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

         
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                // For more details on creating database during deployment see http://go.microsoft.com/fwlink/?LinkID=615859
                //try
                //{
                //    using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                //        .CreateScope())
                //    {
                //        serviceScope.ServiceProvider.GetService<ApplicationDbContext>()
                //             .Database.Migrate();
                //    }
                //}
                //catch { }
            }

            app.UseIISPlatformHandler(options => options.AuthenticationDescriptions.Clear());

            app.UseStaticFiles();

            app.UseIdentity();

            // To configure external authentication please see http://go.microsoft.com/fwlink/?LinkID=532715
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}

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
using LHR.MVC.Models;
using LHR.MVC.Services;
using Microsoft.AspNet.Mvc.Razor;
using Microsoft.AspNet.FileProviders;
using Microsoft.AspNet.Mvc.Infrastructure;
using Microsoft.Extensions.PlatformAbstractions;
using System.Reflection;
using LHR.MVC.Modules.Application;
using Microsoft.Extensions.OptionsModel;
using System.IO;

namespace LHR.MVC
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

            //List<Assembly> assemblies = new List<Assembly>();
            //LHRPluginAssemblyProvider pap = new LHRPluginAssemblyProvider(new PhysicalFileProvider(CurrentEnvironment.WebRootPath + "\\.."),
            //        PlatformServices.Default.AssemblyLoadContextAccessor,
            //        PlatformServices.Default.AssemblyLoaderContainer);
            //assemblies.Add(this.GetType().GetTypeInfo().Assembly);
            //assemblies.AddRange(pap.CandidateAssemblies);
            //services.AddMvc().AddControllersAsServices(assemblies);

            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            //typeof(IServiceCollection).GetTypeInfo().DeclaredMethods.Where(x => x.Name == "AddTransient").First().Invoke(services, new object[] { contract, implementation });
            // Add options
            services.AddOptions();
            services.Configure<AppSettings>(Configuration.GetSection(nameof(AppSettings)));
            var serviceAppSettings = services.BuildServiceProvider().GetService<IOptions<AppSettings>>();

            var rootFileProvider = new PhysicalFileProvider(CurrentEnvironment.WebRootPath + "\\..");

            //create the custom plugin directory provider
            services.AddSingleton<IAssemblyProvider, LHRAssemblyProvider>(provider =>
            {
                var pluginAssemblyProvider = new LHRPluginAssemblyProvider(
                    rootFileProvider,
                    PlatformServices.Default.AssemblyLoadContextAccessor,
                    PlatformServices.Default.AssemblyLoaderContainer,
                    serviceAppSettings);
                //return the composite one - this wraps the default MVC one
                return new LHRAssemblyProvider(
                    provider.GetRequiredService<ILibraryManager>(),
                    new IAssemblyProvider[] { pluginAssemblyProvider });
            });

            // Create the custom Razor View location expander
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.FileProvider = rootFileProvider;
                options.ViewLocationExpanders.Add(new LHRViewLocationExpander(serviceAppSettings));
            });
            // Load libraries for dynamic dependencies
            List<Assembly> loadedAssemblies = new List<Assembly>();
            var libsFolder = new DirectoryInfo(rootFileProvider.Root + serviceAppSettings.Value.LibsFolderName);
            if (libsFolder.Exists)
            {
                foreach (var fileSystemInfo in libsFolder.GetFileSystemInfos("*.dll"))
                {
                    loadedAssemblies.Add(PlatformServices.Default.AssemblyLoadContextAccessor.Default.LoadFile(fileSystemInfo.FullName));
                }
            }
            // Register dynamic dependencies
            Type contract, implementation;
            contract = Assembly.Load(new AssemblyName("LHR.DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")).GetType("LHR.DAL.IDALEmployee");
            implementation = loadedAssemblies.Where(x => x.FullName == "LHR.DAL.SQL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null").First().GetType("LHR.DAL.SQL.DALEmployee");
            services.AddTransient(contract, implementation);
            contract = Assembly.Load(new AssemblyName("LHR.BL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")).GetType("LHR.BL.IBLEmployee");
            implementation = loadedAssemblies.Where(x => x.FullName == "LHR.BL.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null").First().GetType("LHR.BL.Core.BLEmployee");
            services.AddTransient(contract, implementation);
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

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Sqlink.Uni.BL;

namespace Sqlink.Uni.Web
{
     

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<RepositoryStorageOptions>(
                         Configuration.GetSection(RepositoryStorageOptions.SectionName));

            services.AddTransient(typeof(IGenericRepository<>), typeof(InMemoryRepository<>));
            services.AddTransient(typeof(IGenericRepository<>), typeof(EFRepository<>));
            services.AddTransient(typeof(IGenericRepositoryFactory<>), typeof(GenericRepositoryFactory<>));
            services.AddTransient<IUniRepository, UniRepository>();
            services.AddSingleton<InMemoryDb>();

            services.AddRazorPages();

            services.AddRazorPages().AddRazorRuntimeCompilation();

             

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}

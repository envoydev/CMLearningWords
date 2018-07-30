using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMLearningWords.AccessToData.Context;
using CMLearningWords.AccessToData.Repository.Classes;
using CMLearningWords.AccessToData.Repository.Interfaces;
using CMLearningWords.WebUI.Automapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CMLearningWords.WebUI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //Contection to database
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(ApplicationContextFactory.Path));

            //Repository Scopes
            services.AddScoped<IStageOfMethodRepository, StageOfMethodRepository>();
            services.AddScoped<ITranslationOfWordRepository, TranslationOfWordRepository>();
            services.AddScoped<IWordInEnglishRepository, WordInEnglishRepository>();

            //Automapper
            var configAutomapper = new AutoMapper.MapperConfiguration(cfg => { cfg.AddProfile(new AutomapperProfile()); });
            var mapper = configAutomapper.CreateMapper();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

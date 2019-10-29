using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MuranoTest.Data.Db;
using MuranoTest.Data.Repositories;
using MuranoTestApp.Profiles;
using MuranoTestApp.Services.Helpers;
using MuranoTestApp.Services.SearchServices;
using MuranoTestApp.Services.SearchServices.Searchers;
using MuranoTestApp.Services.SearchServices.Searchers.Bing;
using MuranoTestApp.Services.SearchServices.Searchers.Google;
using MuranoTestApp.Services.SearchServices.Searchers.Yandex;

namespace MuranoTestApp
{
    public class Startup
    {
        private IConfiguration _searchersConfiguration;
        public Startup(IWebHostEnvironment env,IConfiguration configuration)
        {
            Configuration = configuration;

            _searchersConfiguration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("SearchersConfig.json").Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddAutoMapper(typeof(SearchResultProfile));

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), builder => builder.MigrationsAssembly("MuranoTest.Data")));
            services.AddScoped<ISearchResultsRepository, SearchResultsRepository>();

            services.AddSearchService();

            services.AddScoped<ISearchersProvider, DefaultSearcherProvider>();

            services.AddYandexSearcher(conf =>
            {
                conf.Key = _searchersConfiguration.GetSection("Yandex").GetValue<string>("Key");
                conf.Username = _searchersConfiguration.GetSection("Yandex").GetValue<string>("Username");
            });

            services.AddGoogleSearcher(x =>
            {
                x.ApiKey = _searchersConfiguration.GetSection("Google").GetValue<string>("ApiKey");
                x.CX = _searchersConfiguration.GetSection("Google").GetValue<string>("CX");
            });

            services.AddBingSearcher(new BingSearcherOptions()
            {
                SubscriptionKey = _searchersConfiguration.GetSection("Bing").GetValue<string>("SubscriptionKey")
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(x =>
            {
                x.MapControllerRoute("default route", "{controller=Search}/{action=Index}/{id?}");
            });
        }
    }
}

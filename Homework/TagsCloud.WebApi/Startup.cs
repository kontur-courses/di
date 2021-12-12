using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TagsCloud.Visualization;
using TagsCloud.Visualization.ContainerVisitor;
using TagsCloud.Visualization.WordsFilter;
using TagsCloud.Visualization.WordsReaders;
using TagsCloud.WebApi.Services;

namespace TagsCloud.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider  ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            
            services.AddCors(options =>
            {
                options.AddPolicy(name: "test",
                    corsPolicyBuilder =>
                    {
                        corsPolicyBuilder
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowAnyOrigin();
                    });
            });
            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/build"; });
            
            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(type => type.ToString());
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "TagsCloud.WebApi",
                    Version = "7.0",
                });
            });
            
            var builder = RegisterDependencies();
            
            builder.Populate(services);

            ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PathFinder.Api v1"));

            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseCors("test");
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer("start");
                }
            });
        }
        
        private ContainerBuilder RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            
            var settings = new TagsCloudModuleSettings
            {
                LayouterType = typeof(CircularCloudLayouter),
                LayoutVisitor = new RandomColorDrawerVisitor()
            };

            builder.RegisterType<SimpleTextReader>().As<IWordsReadService>().AsSelf().SingleInstance();
            builder.Register(_ => new BoringWordsFilter()).As<IWordsFilter>();

            builder.RegisterModule(new TagsCloudModule(settings));

            return builder;
        }
    }
}
using AutoMapper;
using BookEntityDemo.Business;
using BookEntityDemo.Business.Services;
using BookEntityDemo.Data;
using BookEntityDemo.Data.Context;
using BookEntityDemo.Data.Mapping;
using BookEntityDemo.Repository;
using BookEntityDemo.Repository.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace BookEntityDemo
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
            services.AddControllers();
            services.AddDBContext(Configuration);
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IBooksRepository, BooksRepository>();
            services.AddScoped<IBooksService, BooksService>();
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Book Entity Demo", Version = "v1", Description = "An Asp.Net Core Web API for managing book items" });
                s.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                s.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Book Entity Demo V1");
            });

            app.ConfigureDBMigration();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

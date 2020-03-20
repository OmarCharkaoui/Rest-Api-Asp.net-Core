using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularWithAPI.Data;
using AngularWithAPI.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;


namespace AngularWithAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

     
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IPersoneRepository, MockRepositoryPersone>();
            services.AddScoped<IContactRepository,MockRepositoryContact>();
            services.AddDbContext<EfDbContext>(Options => Options.UseSqlServer(Configuration["Cnx"]));

            services.AddCors();
            services.AddMvc().AddJsonOptions(options => {

                //Return All result Not only first result Collection<Contact> 
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                //Remove Null from Collection<Contact> if Doesn't have data
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });


            services.AddSwaggerGen(c=>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "One-To-Many", Version = "v1" });
            });

        }

       
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseSwagger();
            app.UseSwaggerUI(c=>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "One To Many");
            });


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseCors(builder => builder
                 .AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                 .AllowCredentials().WithExposedHeaders("X-Pagination"));
            app.UseMvc();

            
        }
    }
}

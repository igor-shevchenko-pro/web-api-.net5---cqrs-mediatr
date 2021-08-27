using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Notes.Application;
using Notes.Application.Common.Mappings;
using Notes.Application.Interfaces;
using Notes.Application.Interfaces.Services.User;
using Notes.Persistence;
using Notes.WebAPI.Configurations.Swagger;
using Notes.WebAPI.Middlewares.Base.Exceptions;
using Notes.WebAPI.Services.User;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Notes.WebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            // AutoMapper
            services.AddAutoMapper(configuration => 
            {
                configuration.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                configuration.AddProfile(new AssemblyMappingProfile(typeof(INotesDbContext).Assembly));
            });

            // DI Core.Notes.Applicatio project
            services.AddApplication();

            // DI Infrastructure.Notes.Persistence project
            services.AddPersistence(Configuration);

            services.AddControllers();

            // CORS
            services.AddCors(options => 
            {
                options.AddPolicy("AllowAll", policy => 
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
            });

            // JWT Authentication
            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = "https://localhost:44332/";
                options.Audience = "NotesWebAPI";
                options.RequireHttpsMetadata = false;
            });

            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");

            //Swagger
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();
            //services.AddSwaggerGen(config =>
            //{
            //    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            //    config.IncludeXmlComments(xmlPath);
            //});

            services.AddApiVersioning();

            services.AddSingleton<ICurrentUserService, CurrentUserService>();
            services.AddHttpContextAccessor();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Pipeline
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                //config.RoutePrefix = string.Empty;
                //config.SwaggerEndpoint("swagger/v1/swagger.json", "Notes API");
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    config.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    config.RoutePrefix = string.Empty;
                }
            });

            app.UseCustomExceptionHandler();
            app.UseRouting();
            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseApiVersioning();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

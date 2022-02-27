using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using UserManagementAPI.Filters;
using UserManagementAPI.Infrastructure;
using UserManagementAPI.Logic.Interfaces;
using UserManagementAPI.Logic.MappingProfiles;
using UserManagementAPI.Logic.Services;
using UserManagementAPI.Logic.Validators;

namespace UserManagementAPI
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
            services.AddCors(c => c.AddDefaultPolicy(builder =>
                builder.AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins(Configuration["CORSOrigins"])));

            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SQLDatabase")));

            services.AddAutoMapper(cfg => cfg.AddProfile<UserMappingProfile>());

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAzureBlobService, AzureBlobService>();
            services.AddScoped<IImageService, ImageService>();

            services.AddControllers()
                .AddFluentValidation(fv =>
                    fv.RegisterValidatorsFromAssemblyContaining<AddUserRequestValidator>());

            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<SwaggerFileOperationFilter>();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UserManagementAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserManagementAPI v1"));
            }

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

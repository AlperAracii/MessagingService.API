using MessagingService.API.Data.Repositories;
using MessagingService.API.Filters;
using MessagingService.API.Services.Account;
using MessagingService.API.Services.Log;
using MessagingService.API.Services.Message;
using MessagingService.API.Services.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace MessagingService.API
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
            string mongoConnectionString = Configuration.GetConnectionString("MongoConnectionString");
            services.AddSingleton(s => new UserRepository(mongoConnectionString, "MesaggingServiceDB", "Users"));
            services.AddSingleton(s => new MessageRepository(mongoConnectionString, "MesaggingServiceDB", "Messages"));
            services.AddSingleton(s => new BlockRepository(mongoConnectionString, "MesaggingServiceDB", "BlockList"));
            services.AddSingleton(s => new LoggingRepository(mongoConnectionString, "LogDB", "Logs"));
            services.AddSingleton(s => new AudithLoggingRepository(mongoConnectionString, "LogDB", "AudithLogs"));



            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ILogService, LogService>();


            services.AddScoped<ActionFilter>();
            services.AddScoped<GlobalExceptionFilter>();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMvc(options => options.Filters.Add(typeof(GlobalExceptionFilter)))
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0)
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    options.JsonSerializerOptions.DictionaryKeyPolicy = null;
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                });

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ActionFilter));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("CoreSwagger", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Swagger on ASP.NET Core",
                    Version = "1.0.0",
                    Description = "Try Swagger on (ASP.NET Core 2.1)",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                    {
                        Name = "Swagger Implementation Alper Aracý",
                        Email = "alper.araci@outlook.com"
                    }
                });
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger()
            .UseSwaggerUI(c =>
            {
                //TODO: Either use the SwaggerGen generated Swagger contract (generated from C# classes)
                c.SwaggerEndpoint("/swagger/CoreSwagger/swagger.json", "Swagger Test .Net Core");

                //TODO: Or alternatively use the original Swagger contract that's included in the static files
                // c.SwaggerEndpoint("/swagger-original.json", "Swagger Petstore Original");
            });

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

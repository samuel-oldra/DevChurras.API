using DevChurras.API.Data;
using DevChurras.API.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace DevChurras.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // PARA ACESSO AO BANCO EM MEMÓRIA
            services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("dados"));

            // Injeção de Dependência
            // Tipos: Transient, Scoped, Singleton
            // Padrão Repository
            services.AddScoped<DataContext, DataContext>();
            services.AddScoped<IConvidadoRepository, ConvidadoRepository>();
            services.AddScoped<IParticipanteRepository, ParticipanteRepository>();

            services.AddControllers();

            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "DevChurras.API",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Samuel B. Oldra",
                        Email = "samuel.oldra@gmail.com",
                        Url = new Uri("https://github.com/samuel-oldra")
                    }
                });

                // Incluindo comentários XML ao Swagger
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                o.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(o =>
                {
                    o.RoutePrefix = string.Empty;
                    o.SwaggerEndpoint("/swagger/v1/swagger.json", "DevChurras.API v1");
                });
            }
        }
    }
}
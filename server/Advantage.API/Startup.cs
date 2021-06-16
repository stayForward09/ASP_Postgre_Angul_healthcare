using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Advantage.API
{
    public class Startup
    {
        private string _connectionString = null;


        //constructor
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }//end constructor



        public IConfiguration Configuration { get; }




        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(opt => {
                opt.AddPolicy("CorsPolicy",
                c => c.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            _connectionString = Configuration["secretConnectionString"];

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Advantage.API", Version = "v1" });
            });

            services.AddEntityFrameworkNpgsql().AddDbContext<ApiContext>(opt=>opt.UseNpgsql(_connectionString));

            services.AddTransient<DataSeed>();

        }//end ConfigureServices



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataSeed seed)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors("CorsPolicy");
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Advantage.API v1"));
            }

            app.UseHttpsRedirection();

            seed.SeedData(20, 1000);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // endpoints.MapControllers();
                   endpoints.MapControllerRoute(
                name: "default",
                pattern: "api/{controller}/{action}/{id?}");
            });

        }//end Configure

    }//end class

}//end namespace

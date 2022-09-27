using AutoMapper;
using GeekShopping.Product.API.Config;
using GeekShopping.Product.API.Model.Context;
using GeekShopping.Product.API.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace GeekShopping.Product.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration config) => _configuration = config;

        public void ConfigureServices(IServiceCollection services)
        {
            var connection = _configuration["MySQLConnection:MySQLConnectionString"];

            services.AddDbContext<MySQLContext>(options => options.
                UseMySql(connection, 
                        new MySqlServerVersion(
                            new Version(8,0,5))));

            IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
            services.AddSingleton(mapper);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddMvc();
            services.AddControllers();
            services.AddSwaggerGen(c => 
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GeekShopping.Product.API" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}

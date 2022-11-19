using aplikacja_webowa___praca_inzynierska.Services;
using LiteDB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace aplikacja_webowa___praca_inzynierska
{
    public class Startup
    {
        private const string DbName = "Register.db";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
      
        public IConfiguration Configuration { get; }

        //Here we add all the services to the dependency injection container 
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton<ILiteDatabase>(x => new LiteDatabase(DbName));

            services.AddSingleton<ITaxonomyProvider,TaxonomyProvider>();

            services.AddScoped<IRegisterRepository,RegisterRepository>();

            services.AddHostedService<CachePrimer>();

            services.AddHostedService<DBInitializer>();
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
            app.UseCors(cors=> cors.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

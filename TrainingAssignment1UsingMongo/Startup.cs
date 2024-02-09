using System.Threading.Tasks;
using TrainingAssignment1UsingMongo.Interface;
using TrainingAssignment1UsingMongo.Models;
using TrainingAssignment1UsingMongo.Service;

namespace TrainingAssignment1UsingMongo
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
            // Add services to the container
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.Configure<Settings>(options =>
            {
                options.connectionString = Configuration.GetSection("MongoDb:connectionString").Value;
                options.database = Configuration.GetSection("MongoDb:database").Value;
            });
            services.AddSingleton<MachineAssetRepository>();
            services.AddSingleton<TaskRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Configure middleware
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

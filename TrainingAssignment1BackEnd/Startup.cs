using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Threading.Tasks;
using TrainingAssignment1BackEnd.Interface;
using TrainingAssignment1BackEnd.Models;
using TrainingAssignment1BackEnd.Service;

namespace TrainingAssignment1BackEnd
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
            //services.AddSwaggerGen();
            services.AddSwaggerGen(option =>
            {
                var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentFilePath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
                option.IncludeXmlComments(xmlCommentFilePath);
            });

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

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using poi.Data;

namespace poi
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
            var connectionString = poi.Utility.POIConfiguration.GetConnectionString(this.Configuration);
            services.AddDbContext<POIContext>(options =>
                options.UseSqlServer(connectionString));
        }
      
    }
}

using backend.Data;
using backend.Profiles;
using backend.Service;
using Microsoft.EntityFrameworkCore;

namespace backend.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<XimDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));
            services.AddScoped<RoomRepository>();
            services.AddScoped<RoomService>();
            services.AddAutoMapper(typeof(RoomProfile).Assembly);

            return services;
        }
    }
}
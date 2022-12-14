using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repository;

namespace Persistence
{
    public static class ServiceExtensions
    {
        public static void AddPersistenceInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region DBContext
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(
                connectionString!,
                b=>b.MigrationsAssembly(typeof(ApplicationDBContext).Assembly.FullName)));
            #endregion

            #region Repositories
            services.AddTransient(typeof(IRepositoryAsync<>), typeof(ApplicationRepositoryAsync<>));
            #endregion
        }
    }
}

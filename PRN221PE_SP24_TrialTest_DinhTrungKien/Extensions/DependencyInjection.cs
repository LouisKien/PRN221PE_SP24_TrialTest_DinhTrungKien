using Microsoft.EntityFrameworkCore;
using PRN221PE_SP24_TrialTest_DinhTrungKien.Repo.Implements;
using PRN221PE_SP24_TrialTest_DinhTrungKien.Repo.Interfaces;
using PRN221PE_SP24_TrialTest_DinhTrungKien_Repo.Models;

namespace PRN221PE_SP24_TrialTest_DinhTrungKien.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<Eyeglasses2024DBContext>(options =>
                options.UseSqlServer(connectionString));
            return services;
        }
    }
}

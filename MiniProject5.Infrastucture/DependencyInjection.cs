using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniProject5.Domain.Interfaces;
using MiniProject5.Infrastucture.Data;
using MiniProject5.Infrastucture.Data.Repository;
using MiniProject5.Application.Services;
using MiniProject5.Application.Interfaces;

namespace MiniProject5.Infrastucture
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FinancialCompanyContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IWorksonRepository, WorksonRepository>();
            services.AddScoped<IDependentRepository, DependentRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();

            services.AddScoped<IEmployeeService, EmployeeService>();
            return services;
        }
    }
}

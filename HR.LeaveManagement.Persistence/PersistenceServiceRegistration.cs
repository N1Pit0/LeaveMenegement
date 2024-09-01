using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Persistence.DatabaseContext;
using HR.LeaveManagement.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HR.LeaveManagement.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddDbContext<HrDatabaseContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("HrDatabaseConnectionString"));
        });

        serviceCollection.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        serviceCollection.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
        serviceCollection.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();
        serviceCollection.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
        
        return serviceCollection;
    }
}
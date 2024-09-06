using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Logging;
using HR.LeaveManagement.Application.Models.Email;
using HR.LeaveManagement.Infrastructure.EmailService;
using HR.LeaveManagement.Infrastructure.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HR.LeaveManagement.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        serviceCollection.AddTransient<IEmailSender, EmailSender>();
        serviceCollection.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
        
        return serviceCollection;
    }
}
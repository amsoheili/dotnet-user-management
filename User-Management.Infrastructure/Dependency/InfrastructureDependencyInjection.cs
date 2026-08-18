using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {

        services.Configure<MessengerSettings>(configuration.GetSection(MessengerSettings.SectionName));

        services.AddScoped<IMessenger, BaleMessenger>();

        services.AddScoped<IUserOTPsRespository, UserOTPsRespository>();
        services.AddScoped<ISmsOutboxRepository, SmsOutboxRepository>();
        services.AddHostedService<SmsOutboxProcessor>();

        return services;
    }
}
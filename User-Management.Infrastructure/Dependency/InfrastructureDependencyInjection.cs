using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {

        services.Configure<MessengerConfiguration>(configuration.GetSection(MessengerConfiguration.SectionName));
        // add jwt configurations
        services.AddScoped<IMessenger, BaleMessenger>();
        services.AddScoped<IJwtConfiguration, JwtConfiguraion>();

        services.AddScoped<IUserOTPsRespository, UserOTPsRespository>();
        services.AddScoped<ISmsOutboxRepository, SmsOutboxRepository>();
        services.AddHostedService<SmsOutboxProcessor>();

        return services;
    }
}
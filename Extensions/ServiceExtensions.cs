using LoggerService;
using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace CompanyEmployees.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder => //разрешения на подключение к апи
                builder.AllowAnyOrigin() // любой url
                    .AllowAnyMethod() // post, get, put и все
                    .AllowAnyHeader()); // все http заголовки
        });

    public static void ConfigureIISIntegration(this IServiceCollection services) =>
        services.Configure<IISOptions>(options => {});

    public static void ConfigureLoggerService(this IServiceCollection services) =>
        services.AddScoped<ILoggerManager, LoggerManager>();

    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration
        configuration) =>
        services.AddDbContext<RepositoryContext>(opts =>
            opts.UseNpgsql(configuration.GetConnectionString("sqlConnection"), b =>
                b.MigrationsAssembly("CompanyEmployees")));

    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddScoped<IRepositoryManager, RepositoryManager>();

    public static IMvcBuilder AddCustomCSVFormatter(this IMvcBuilder builder) =>
        builder.AddMvcOptions(config => config.OutputFormatters.Add(new CsvOutputFormatter()));
}
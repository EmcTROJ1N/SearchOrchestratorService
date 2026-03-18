using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using HttpJsonOptions = Microsoft.AspNetCore.Http.Json.JsonOptions;
using MvcJsonOptions = Microsoft.AspNetCore.Mvc.JsonOptions;
using SearchOrchestratorService.Application.Indexing;
using SearchOrchestratorService.Application.Search;

namespace SearchOrchestratorService.Presentation.Infrastructure;

/// <summary>
/// Registers presentation-layer services and JSON serialization settings.
/// </summary>
public static class ServiceConfigurations
{
    /// <summary>
    /// Adds dependencies required by the presentation layer.
    /// </summary>
    /// <param name="services">Application service collection.</param>
    /// <returns>The same service collection for chaining.</returns>
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.ConfigureHttpJsonOptions(options =>
        {
            ConfigureJsonOptions(options.SerializerOptions);
        });

        services.Configure<MvcJsonOptions>(options =>
        {
            ConfigureJsonOptions(options.JsonSerializerOptions);
        });

        services.AddScoped<StartIndexingHandler>();
        services.AddScoped<ReindexSourceHandler>();
        services.AddScoped<CancelIndexingJobHandler>();
        services.AddScoped<GetIndexingJobStatusHandler>();
        services.AddScoped<ListIndexingJobsHandler>();
        services.AddScoped<SearchHandler>();

        return services;
    }

    private static void ConfigureJsonOptions(JsonSerializerOptions serializerOptions)
    {
        if (!serializerOptions.TypeInfoResolverChain.Contains(AppJsonSerializerContext.Default))
        {
            // Keep generated metadata first, but preserve reflection fallback for tools like Swagger.
            serializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
        }

        if (!serializerOptions.TypeInfoResolverChain.OfType<DefaultJsonTypeInfoResolver>().Any())
        {
            serializerOptions.TypeInfoResolverChain.Add(new DefaultJsonTypeInfoResolver());
        }
    }
}

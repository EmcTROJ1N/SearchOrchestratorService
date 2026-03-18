using System.Reflection;
using SearchOrchestratorService.Presentation.Common;

namespace SearchOrchestratorService.Presentation.Infrastructure;

/// <summary>
/// Provides conventions for discovering and mapping endpoint groups.
/// </summary>
public static class WebApplicationExtension
{
    /// <summary>
    /// Creates a route group for an endpoint module using a conventional URL prefix and Swagger tag.
    /// </summary>
    private static RouteGroupBuilder MapGroup(this WebApplication app, BaseEndpointGroup group)
    {
        var groupName = group.GroupName ?? group.GetType().Name;

        return app
            .MapGroup($"/api/{groupName}")
            .WithTags(groupName);
    }

    /// <summary>
    /// Discovers all exported <see cref="BaseEndpointGroup"/> implementations in the current assembly
    /// and maps them into the application pipeline.
    /// </summary>
    /// <param name="app">Configured web application.</param>
    /// <returns>The same application instance for chaining.</returns>
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        var endpointGroupType = typeof(BaseEndpointGroup);

        var assembly = Assembly.GetExecutingAssembly();

        var endpointGroupTypes = assembly.GetExportedTypes()
            .Where(t => t.IsSubclassOf(endpointGroupType));

        foreach (var type in endpointGroupTypes)
        {
            // Endpoint groups are instantiated conventionally to keep Program.cs free from manual mapping boilerplate.
            if (Activator.CreateInstance(type) is BaseEndpointGroup instance)
            {
                instance.Map(app.MapGroup(instance));
            }
        }

        return app;
    }
}

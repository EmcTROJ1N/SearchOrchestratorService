namespace SearchOrchestratorService.Presentation.Common;

/// <summary>
/// Base type for grouping related minimal API endpoints under a shared route prefix.
/// </summary>
public abstract class BaseEndpointGroup
{
    /// <summary>
    /// Route group name used in the URL and Swagger tag.
    /// When <see langword="null"/>, the runtime falls back to the class name.
    /// </summary>
    public virtual string? GroupName { get; } = null;

    /// <summary>
    /// Maps endpoints for the current group into the provided route builder.
    /// </summary>
    /// <param name="groupBuilder">Route builder preconfigured with the group prefix.</param>
    public abstract void Map(RouteGroupBuilder groupBuilder);
}

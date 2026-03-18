namespace SearchOrchestratorService.Application.Contracts.Search;

/// <summary>
/// Represents a single search hit returned from the external search engine.
/// </summary>
public record SearchHitResponse(
    string DocumentId,
    string SourceId,
    string FilePath,
    string Snippet,
    double Score);

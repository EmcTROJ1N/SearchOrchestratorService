namespace SearchOrchestratorService.Application.Contracts.Search;

/// <summary>
/// Represents the orchestrated search response returned to the client.
/// </summary>
public record SearchResponse(
    string Query,
    string CorrelationId,
    int Total,
    SearchHitResponse[] Hits);

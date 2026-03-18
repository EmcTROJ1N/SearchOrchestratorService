using SearchOrchestratorService.Application.Common;
using SearchOrchestratorService.Application.Contracts.Search;

namespace SearchOrchestratorService.Application.Search;

/// <summary>
/// Requests a full-text search through the external search engine.
/// </summary>
/// <param name="Text">User-entered search string.</param>
/// <param name="SourceId">Optional source filter.</param>
/// <param name="Limit">Maximum number of hits to return.</param>
public record SearchQuery(string Text, string? SourceId, int Limit = 20) : IQuery<SearchResponse>;

/// <summary>
/// Temporary handler stub for orchestrated search requests.
/// </summary>
public class SearchHandler : IQueryHandler<SearchQuery, SearchResponse>
{
    /// <summary>
    /// Returns a placeholder search result set.
    /// </summary>
    /// <param name="query">Search request parameters.</param>
    /// <returns>Stubbed search response.</returns>
    public Task<SearchResponse> HandleAsync(SearchQuery query)
    {
        var hits = new[]
        {
            new SearchHitResponse(
                "doc-001",
                query.SourceId ?? "sample-source",
                "/documents/sample.txt",
                $"Stub match for query '{query.Text}'.",
                0.98)
        };

        var response = new SearchResponse(
            query.Text,
            Guid.NewGuid().ToString("N"),
            hits.Length,
            hits);

        return Task.FromResult(response);
    }
}

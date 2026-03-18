using SearchOrchestratorService.Application.Common;
using SearchOrchestratorService.Application.Contracts.Indexing;
using SearchOrchestratorService.Domain.Indexing;

namespace SearchOrchestratorService.Application.Indexing;

/// <summary>
/// Requests a filtered list of indexing jobs known to the orchestrator.
/// </summary>
/// <param name="SourceId">Optional source filter.</param>
/// <param name="Status">Optional status filter.</param>
/// <param name="Limit">Maximum number of records to return.</param>
public record ListIndexingJobsQuery(
    string? SourceId,
    IndexingJobStatus? Status,
    int Limit = 50) : IQuery<IndexingJobSummaryResponse[]>;

/// <summary>
/// Temporary handler stub for listing indexing jobs.
/// </summary>
public class ListIndexingJobsHandler : IQueryHandler<ListIndexingJobsQuery, IndexingJobSummaryResponse[]>
{
    /// <summary>
    /// Returns a placeholder list of jobs.
    /// </summary>
    /// <param name="query">List request with optional filters.</param>
    /// <returns>Stubbed set of indexing jobs.</returns>
    public Task<IndexingJobSummaryResponse[]> HandleAsync(ListIndexingJobsQuery query)
    {
        var items = new[]
        {
            new IndexingJobSummaryResponse(
                Guid.NewGuid(),
                query.SourceId ?? "sample-source",
                query.Status ?? IndexingJobStatus.Pending,
                DateTimeOffset.UtcNow.AddMinutes(-15),
                null,
                Guid.NewGuid().ToString("N"))
        };

        return Task.FromResult(items);
    }
}

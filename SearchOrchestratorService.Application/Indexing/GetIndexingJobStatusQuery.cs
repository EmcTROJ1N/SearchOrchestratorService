using SearchOrchestratorService.Application.Common;
using SearchOrchestratorService.Application.Contracts.Indexing;
using SearchOrchestratorService.Domain.Indexing;

namespace SearchOrchestratorService.Application.Indexing;

/// <summary>
/// Requests the current status of an indexing job.
/// </summary>
/// <param name="JobId">Identifier of the job to inspect.</param>
public record GetIndexingJobStatusQuery(Guid JobId) : IQuery<IndexingJobStatusResponse>;

/// <summary>
/// Temporary handler stub for reading job status.
/// </summary>
public class GetIndexingJobStatusHandler : IQueryHandler<GetIndexingJobStatusQuery, IndexingJobStatusResponse>
{
    /// <summary>
    /// Returns a placeholder job status payload.
    /// </summary>
    /// <param name="query">Status lookup request.</param>
    /// <returns>Stubbed job status.</returns>
    public Task<IndexingJobStatusResponse> HandleAsync(GetIndexingJobStatusQuery query)
    {
        var createdAt = DateTimeOffset.UtcNow.AddMinutes(-2);
        var response = new IndexingJobStatusResponse(
            query.JobId,
            "sample-source",
            IndexingJobStatus.Running,
            Guid.NewGuid().ToString("N"),
            createdAt,
            createdAt.AddSeconds(5),
            null,
            null,
            10,
            4,
            0);

        return Task.FromResult(response);
    }
}

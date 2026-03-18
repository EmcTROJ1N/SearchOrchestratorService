using SearchOrchestratorService.Domain.Indexing;

namespace SearchOrchestratorService.Application.Contracts.Indexing;

/// <summary>
/// Describes the result of a cancellation request for an indexing job.
/// </summary>
public record CancelIndexingJobResponse(
    Guid JobId,
    IndexingJobStatus Status,
    string CorrelationId,
    string Message,
    DateTimeOffset UpdatedAtUtc);

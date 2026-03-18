using SearchOrchestratorService.Domain.Indexing;

namespace SearchOrchestratorService.Application.Contracts.Indexing;

/// <summary>
/// Represents the current state of an indexing job tracked by the orchestrator.
/// </summary>
public record IndexingJobStatusResponse(
    Guid JobId,
    string SourceId,
    IndexingJobStatus Status,
    string CorrelationId,
    DateTimeOffset CreatedAtUtc,
    DateTimeOffset? StartedAtUtc,
    DateTimeOffset? CompletedAtUtc,
    string? Error,
    int TotalItems,
    int ProcessedItems,
    int FailedItems);

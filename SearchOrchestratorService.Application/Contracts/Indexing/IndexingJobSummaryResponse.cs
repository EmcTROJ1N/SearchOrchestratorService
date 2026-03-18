using SearchOrchestratorService.Domain.Indexing;

namespace SearchOrchestratorService.Application.Contracts.Indexing;

/// <summary>
/// Represents a lightweight projection of an indexing job for listing endpoints.
/// </summary>
public record IndexingJobSummaryResponse(
    Guid JobId,
    string SourceId,
    IndexingJobStatus Status,
    DateTimeOffset CreatedAtUtc,
    DateTimeOffset? CompletedAtUtc,
    string CorrelationId);

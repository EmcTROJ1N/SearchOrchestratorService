using SearchOrchestratorService.Domain.Indexing;

namespace SearchOrchestratorService.Application.Contracts.Indexing;

/// <summary>
/// Describes a job accepted by the orchestrator for asynchronous processing.
/// </summary>
public record IndexingJobAcceptedResponse(
    Guid JobId,
    string SourceId,
    IndexingJobStatus Status,
    string CorrelationId,
    string Message,
    DateTimeOffset CreatedAtUtc,
    bool IsDuplicateRequest);

using SearchOrchestratorService.Application.Common;
using SearchOrchestratorService.Application.Contracts.Indexing;
using SearchOrchestratorService.Domain.Indexing;

namespace SearchOrchestratorService.Application.Indexing;

/// <summary>
/// Requests reindexing of a previously configured source.
/// </summary>
/// <param name="SourceId">Identifier of the source to reindex.</param>
/// <param name="Reason">Optional reason for the reindex operation.</param>
/// <param name="IdempotencyKey">Client-provided key used to deduplicate retries.</param>
public record ReindexSourceCommand(
    string SourceId,
    string? Reason,
    string? IdempotencyKey) : ICommand<IndexingJobAcceptedResponse>;

/// <summary>
/// Temporary handler stub for the reindex command.
/// </summary>
public class ReindexSourceHandler : ICommandHandler<ReindexSourceCommand, IndexingJobAcceptedResponse>
{
    /// <summary>
    /// Creates a placeholder accepted response for a reindex request.
    /// </summary>
    /// <param name="command">Reindex request parameters.</param>
    /// <returns>Stubbed accepted job contract.</returns>
    public Task<IndexingJobAcceptedResponse> HandleAsync(ReindexSourceCommand command)
    {
        var response = new IndexingJobAcceptedResponse(
            Guid.NewGuid(),
            command.SourceId,
            IndexingJobStatus.Pending,
            Guid.NewGuid().ToString("N"),
            "Reindex request accepted. Stub handler response.",
            DateTimeOffset.UtcNow,
            false);

        return Task.FromResult(response);
    }
}

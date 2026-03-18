using SearchOrchestratorService.Application.Common;
using SearchOrchestratorService.Application.Contracts.Indexing;
using SearchOrchestratorService.Domain.Indexing;

namespace SearchOrchestratorService.Application.Indexing;

/// <summary>
/// Requests the start of a new indexing process.
/// </summary>
/// <param name="SourceId">Identifier of the source that should be indexed.</param>
/// <param name="FileKeys">Logical file identifiers passed to the external search engine.</param>
/// <param name="IdempotencyKey">Client-provided key used to deduplicate retries.</param>
public record StartIndexingCommand(
    string SourceId,
    string[] FileKeys,
    string? IdempotencyKey) : ICommand<IndexingJobAcceptedResponse>;

/// <summary>
/// Temporary handler stub for the indexing start command.
/// Replace the placeholder logic with the actual orchestration flow.
/// </summary>
public class StartIndexingHandler : ICommandHandler<StartIndexingCommand, IndexingJobAcceptedResponse>
{
    /// <summary>
    /// Creates a placeholder accepted response for a new indexing request.
    /// </summary>
    /// <param name="command">Command describing the indexing request.</param>
    /// <returns>Stubbed accepted job contract.</returns>
    public Task<IndexingJobAcceptedResponse> HandleAsync(StartIndexingCommand command)
    {
        var response = new IndexingJobAcceptedResponse(
            Guid.NewGuid(),
            command.SourceId,
            IndexingJobStatus.Pending,
            Guid.NewGuid().ToString("N"),
            "Indexing request accepted. Stub handler response.",
            DateTimeOffset.UtcNow,
            false);

        return Task.FromResult(response);
    }
}

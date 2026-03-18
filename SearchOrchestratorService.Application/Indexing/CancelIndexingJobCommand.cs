using SearchOrchestratorService.Application.Common;
using SearchOrchestratorService.Application.Contracts.Indexing;
using SearchOrchestratorService.Domain.Indexing;

namespace SearchOrchestratorService.Application.Indexing;

/// <summary>
/// Requests cancellation of an active indexing job.
/// </summary>
/// <param name="JobId">Identifier of the job to cancel.</param>
/// <param name="Reason">Optional cancellation reason supplied by the client.</param>
public record CancelIndexingJobCommand(Guid JobId, string? Reason) : ICommand<CancelIndexingJobResponse>;

/// <summary>
/// Temporary handler stub for job cancellation requests.
/// </summary>
public class CancelIndexingJobHandler : ICommandHandler<CancelIndexingJobCommand, CancelIndexingJobResponse>
{
    /// <summary>
    /// Returns a placeholder cancellation response.
    /// </summary>
    /// <param name="command">Cancellation request parameters.</param>
    /// <returns>Stubbed cancellation result.</returns>
    public Task<CancelIndexingJobResponse> HandleAsync(CancelIndexingJobCommand command)
    {
        var response = new CancelIndexingJobResponse(
            command.JobId,
            IndexingJobStatus.Cancelled,
            Guid.NewGuid().ToString("N"),
            "Cancellation request accepted. Stub handler response.",
            DateTimeOffset.UtcNow);

        return Task.FromResult(response);
    }
}

namespace SearchOrchestratorService.Domain.Indexing;

/// <summary>
/// Represents the lifecycle states of an indexing orchestration job.
/// </summary>
public enum IndexingJobStatus
{
    Pending = 0,
    Running = 1,
    Succeeded = 2,
    PartiallySucceeded = 3,
    Failed = 4,
    Cancelled = 5,
    TimedOut = 6
}

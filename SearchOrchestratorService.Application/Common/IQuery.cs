namespace SearchOrchestratorService.Application.Common;

/// <summary>
/// Represents an application query that returns data without changing system state.
/// </summary>
/// <typeparam name="TResult">Response contract returned by the query handler.</typeparam>
public interface IQuery<out TResult>
{
}

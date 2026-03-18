namespace SearchOrchestratorService.Application.Common;

/// <summary>
/// Represents an application command that changes system state and returns a response contract.
/// </summary>
/// <typeparam name="TResult">Response contract returned by the command handler.</typeparam>
public interface ICommand<out TResult>
{
}

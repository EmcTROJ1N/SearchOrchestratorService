namespace SearchOrchestratorService.Application.Common;

/// <summary>
/// Defines the asynchronous handler contract for an application command that returns a response payload.
/// </summary>
/// <typeparam name="TCommand">Concrete command type handled by the implementation.</typeparam>
/// <typeparam name="TResult">Response contract returned by the handler.</typeparam>
public interface ICommandHandler<in TCommand, TResult> where TCommand : ICommand<TResult>
{
    /// <summary>
    /// Executes the command business flow and returns its result.
    /// </summary>
    /// <param name="command">Command instance with input data for execution.</param>
    /// <returns>Command result.</returns>
    Task<TResult> HandleAsync(TCommand command);
}

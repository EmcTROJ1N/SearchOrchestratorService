namespace SearchOrchestratorService.Application.Common;

/// <summary>
/// Defines the asynchronous handler contract for a command.
/// </summary>
/// <typeparam name="TCommand">Concrete command type handled by the implementation.</typeparam>
public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    /// <summary>
    /// Executes the command business flow.
    /// </summary>
    /// <param name="command">Command instance with the input data for execution.</param>
    Task HandleAsync(TCommand command);
}

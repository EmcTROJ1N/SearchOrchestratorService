namespace SearchOrchestratorService.Application.Common;

/// <summary>
/// Defines the asynchronous handler contract for an application query.
/// </summary>
/// <typeparam name="TQuery">Concrete query type handled by the implementation.</typeparam>
/// <typeparam name="TResult">Response contract returned by the handler.</typeparam>
public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult>
{
    /// <summary>
    /// Executes the query and returns the requested data.
    /// </summary>
    /// <param name="query">Query instance with filtering and projection parameters.</param>
    /// <returns>Query result.</returns>
    Task<TResult> HandleAsync(TQuery query);
}

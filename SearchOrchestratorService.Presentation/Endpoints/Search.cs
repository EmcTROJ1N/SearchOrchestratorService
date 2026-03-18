using Microsoft.AspNetCore.Http.HttpResults;
using SearchOrchestratorService.Application.Contracts.Search;
using SearchOrchestratorService.Application.Search;
using SearchOrchestratorService.Presentation.Common;

namespace SearchOrchestratorService.Presentation.Endpoints;

public class Search : BaseEndpointGroup
{
    public override void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapGet(string.Empty, SearchAsync)
            .WithName("SearchDocuments");
    }

    private static async Task<Ok<SearchResponse>> SearchAsync(
        string text,
        string? sourceId,
        int? limit,
        SearchHandler handler)
    {
        var response = await handler.HandleAsync(new SearchQuery(text, sourceId, limit ?? 20));
        return TypedResults.Ok(response);
    }
}

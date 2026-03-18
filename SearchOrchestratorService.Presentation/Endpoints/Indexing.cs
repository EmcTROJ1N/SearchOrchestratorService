using Microsoft.AspNetCore.Http.HttpResults;
using SearchOrchestratorService.Application.Contracts.Indexing;
using SearchOrchestratorService.Application.Indexing;
using SearchOrchestratorService.Domain.Indexing;
using SearchOrchestratorService.Presentation.Common;

namespace SearchOrchestratorService.Presentation.Endpoints;

public class Indexing : BaseEndpointGroup
{
    public override void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapPost(string.Empty, async Task<Accepted<IndexingJobAcceptedResponse>> (
                StartIndexingCommand request,
                StartIndexingHandler handler) =>
            {
                var response = await handler.HandleAsync(request);
                return TypedResults.Accepted($"/api/Indexing/jobs/{response.JobId}", response);
            })
            .WithName("StartIndexing");
        groupBuilder.MapPost("reindex", async Task<Accepted<IndexingJobAcceptedResponse>> (
                ReindexSourceCommand request,
                ReindexSourceHandler handler) =>
            {
                var response = await handler.HandleAsync(request);
                return TypedResults.Accepted($"/api/Indexing/jobs/{response.JobId}", response);
            })
            .WithName("ReindexSource");
        groupBuilder.MapPost("jobs/{jobId:guid}/cancel", async Task<Ok<CancelIndexingJobResponse>> (
                Guid jobId,
                CancelIndexingJobBody body,
                CancelIndexingJobHandler handler) =>
            {
                var response = await handler.HandleAsync(new CancelIndexingJobCommand(jobId, body.Reason));
                return TypedResults.Ok(response);
            })
            .WithName("CancelIndexingJob");
        groupBuilder.MapGet("jobs/{jobId:guid}", async Task<Ok<IndexingJobStatusResponse>> (
                Guid jobId,
                GetIndexingJobStatusHandler handler) =>
            {
                var response = await handler.HandleAsync(new GetIndexingJobStatusQuery(jobId));
                return TypedResults.Ok(response);
            })
            .WithName("GetIndexingJob");
        groupBuilder.MapGet("jobs", async Task<Ok<IndexingJobSummaryResponse[]>> (
                string? sourceId,
                IndexingJobStatus? status,
                int? limit,
                ListIndexingJobsHandler handler) =>
            {
                var response = await handler.HandleAsync(new ListIndexingJobsQuery(sourceId, status, limit ?? 50));
                return TypedResults.Ok(response);
            })
            .WithName("ListIndexingJobs");
    }

    public sealed record CancelIndexingJobBody(string? Reason);
}

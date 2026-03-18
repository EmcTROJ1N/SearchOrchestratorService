using System.Text.Json.Serialization;
using SearchOrchestratorService.Application.Contracts.Indexing;
using SearchOrchestratorService.Application.Contracts.Search;
using SearchOrchestratorService.Application.Indexing;
using SearchOrchestratorService.Domain.Indexing;
using SearchOrchestratorService.Presentation.Endpoints;

namespace SearchOrchestratorService.Presentation.Infrastructure;

[JsonSerializable(typeof(StartIndexingCommand))]
[JsonSerializable(typeof(ReindexSourceCommand))]
[JsonSerializable(typeof(IndexingJobAcceptedResponse))]
[JsonSerializable(typeof(CancelIndexingJobResponse))]
[JsonSerializable(typeof(IndexingJobStatusResponse))]
[JsonSerializable(typeof(IndexingJobSummaryResponse[]))]
[JsonSerializable(typeof(SearchResponse))]
[JsonSerializable(typeof(SearchHitResponse[]))]
[JsonSerializable(typeof(Indexing.CancelIndexingJobBody))]
[JsonSerializable(typeof(IndexingJobStatus))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}

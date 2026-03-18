# Search Orchestrator Service

Упрощенный сервис-оркестратор поиска на `ASP.NET Core / .NET 8`.

Проект показывает каркас сервиса, который:

- принимает запросы на индексацию и переиндексацию источников;
- хранит минимальную мета-информацию о заданиях;
- отдает статус задач;
- проксирует поисковые запросы во внешний Search Engine;
- закладывает базу под идемпотентность, обработку ошибок, корреляцию и наблюдаемость.

Фактическая реализация сейчас stub-based: внешний поисковый сервис замокан, а orchestration flow представлен контрактами, статусами и заглушками хендлеров.

## Архитектура

Репозиторий разбит на 3 слоя:

- `SearchOrchestratorService.Presentation`  
  Minimal API, HTTP endpoints, Swagger, JSON serialization, composition root.
- `SearchOrchestratorService.Application`  
  use case слой: команды, запросы, DTO-контракты, обработчики сценариев оркестрации.
- `SearchOrchestratorService.Domain`  
  доменные примитивы. Сейчас содержит enum жизненного цикла задания индексации.

### Границы ответственности

**Presentation**

- принимает HTTP-запрос;
- валидирует маршрут и bind request-модели;
- вызывает соответствующий handler application-слоя;
- возвращает API-контракт клиенту.

**Application**

- описывает бизнес-сценарии `StartIndexing`, `ReindexSource`, `CancelIndexingJob`, `GetIndexingJobStatus`, `ListIndexingJobs`, `Search`;
- должен оркестрировать взаимодействие с:
  - хранилищем метаданных задач;
  - очередью / фоновым воркером;
  - клиентом внешнего Search Engine;
- в текущем состоянии отдает stub-ответы, но структура уже готова для замены на реальные зависимости.

**Domain**

- хранит инварианты и статусы жизненного цикла задания.

## Основные сценарии

### 1. Запуск индексации

1. Клиент вызывает `POST /api/Indexing`.
2. Orchestrator принимает `SourceId`, `FileKeys`, `IdempotencyKey`.
3. Проверяет идемпотентность.
4. Создает запись о задаче со статусом `Pending`.
5. Публикует задачу в очередь / передает ее background worker’у.
6. Worker вызывает внешний Search Engine.
7. По мере результата обновляет статус в `Running`, затем в `Succeeded` / `PartiallySucceeded` / `Failed` / `TimedOut`.

Сейчас шаги 3-7 замоканы и заменены stub handler’ом.

### 2. Переиндексация источника

1. Клиент вызывает `POST /api/Indexing/reindex`.
2. Orchestrator создает отдельное задание переиндексации по `SourceId`.
3. Внешний сервис должен заново прочитать и переиндексировать источник.

### 3. Получение статуса

1. Клиент вызывает `GET /api/Indexing/jobs/{jobId}`.
2. Orchestrator возвращает известное состояние задания и счетчики прогресса.

### 4. Поиск

1. Клиент вызывает `GET /api/Search?text=...`.
2. Orchestrator валидирует запрос и пробрасывает его во внешний Search Engine.
3. Возвращает результаты поиска и `CorrelationId`.

## API контракты

### `POST /api/Indexing`

Запуск индексации набора файлов.

Пример запроса:

```json
{
  "sourceId": "source-001",
  "fileKeys": [
    "folder/a.txt",
    "folder/b.txt"
  ],
  "idempotencyKey": "req-001"
}
```

Пример ответа `202 Accepted`:

```json
{
  "jobId": "6da6d5d7-0d6a-4cde-b95d-7ce4c6e8fe03",
  "sourceId": "source-001",
  "status": "Pending",
  "correlationId": "b404629d96c74f0ab89eb9697ee3d676",
  "message": "Indexing request accepted. Stub handler response.",
  "createdAtUtc": "2026-03-18T10:00:00Z",
  "isDuplicateRequest": false
}
```

### `POST /api/Indexing/reindex`

Переиндексация источника.

Пример запроса:

```json
{
  "sourceId": "source-001",
  "reason": "manual reindex",
  "idempotencyKey": "reindex-001"
}
```

### `POST /api/Indexing/jobs/{jobId}/cancel`

Отмена задачи.

Пример запроса:

```json
{
  "reason": "requested by operator"
}
```

### `GET /api/Indexing/jobs/{jobId}`

Получение статуса задания.

Пример ответа:

```json
{
  "jobId": "6da6d5d7-0d6a-4cde-b95d-7ce4c6e8fe03",
  "sourceId": "sample-source",
  "status": "Running",
  "correlationId": "d3db28b6feae4e52a92b4f9fc70825e1",
  "createdAtUtc": "2026-03-18T09:58:00Z",
  "startedAtUtc": "2026-03-18T09:58:05Z",
  "completedAtUtc": null,
  "error": null,
  "totalItems": 10,
  "processedItems": 4,
  "failedItems": 0
}
```

### `GET /api/Indexing/jobs`

Список задач с фильтрами:

- `sourceId`
- `status`
- `limit`

Пример:

```http
GET /api/Indexing/jobs?sourceId=source-001&status=Running&limit=20
```

### `GET /api/Search`

Поиск по строке.

Query parameters:

- `text` - строка поиска;
- `sourceId` - опциональный фильтр по источнику;
- `limit` - лимит результатов.

Пример:

```http
GET /api/Search?text=contract&sourceId=source-001&limit=10
```

Пример ответа:

```json
{
  "query": "contract",
  "correlationId": "6a52a0f1f08044e9a6bd4dc0e6df6c0d",
  "total": 1,
  "hits": [
    {
      "documentId": "doc-001",
      "sourceId": "source-001",
      "filePath": "/documents/sample.txt",
      "snippet": "Stub match for query 'contract'.",
      "score": 0.98
    }
  ]
}
```

## Текущее состояние каркаса

Реализовано:

- слоистая структура проекта;
- minimal API endpoints;
- DTO-контракты для основных сценариев;
- enum статусов жизненного цикла;
- Swagger;
- stub handlers для демонстрации API.

## Как запускать

Требования:

- `.NET SDK 8`

Запуск API:

```bash
dotnet run --project SearchOrchestratorService.Presentation/SearchOrchestratorService.Presentation.csproj
```

По `launchSettings.json` локальный адрес:

```text
http://localhost:5062
```

Swagger в Development:

```text
http://localhost:5062/swagger
```

## Примеры запросов

Запуск индексации:

```bash
curl -X POST http://localhost:5062/api/Indexing \
  -H "Content-Type: application/json" \
  -d '{
    "sourceId": "source-001",
    "fileKeys": ["folder/a.txt", "folder/b.txt"],
    "idempotencyKey": "req-001"
  }'
```

Получение статуса:

```bash
curl http://localhost:5062/api/Indexing/jobs/6da6d5d7-0d6a-4cde-b95d-7ce4c6e8fe03
```

Поиск:

```bash
curl "http://localhost:5062/api/Search?text=contract&sourceId=source-001&limit=10"
```

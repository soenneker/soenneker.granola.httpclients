[![](https://img.shields.io/nuget/v/soenneker.granola.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.granola.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.granola.httpclients/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.granola.httpclients/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.granola.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.granola.httpclients/)

# Soenneker.Granola.HttpClients

A thread-safe singleton `HttpClient` for the Granola API.

## Install

```bash
dotnet add package Soenneker.Granola.HttpClients
```

## Quick start

```csharp
using Soenneker.Granola.HttpClients.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddGranolaOpenApiHttpClientAsSingleton();
```

Adds `GranolaOpenApiHttpClient` as a singleton service.

## What you get

- `IGranolaOpenApiHttpClient` — A thread-safe singleton `HttpClient` for the Granola API.
- `GranolaOpenApiHttpClientRegistrar` — Registers the OpenAPI HttpClient wrapper for dependency injection.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `IGranolaOpenApiHttpClient.Get(cancellationToken)` | Gets the shared HTTP client configured with the Granola API base address and authentication settings. | The cached HTTP client instance; repeated calls return the same client until this service is disposed. |
| `GranolaOpenApiHttpClientRegistrar.AddGranolaOpenApiHttpClientAsSingleton(services)` | Adds `GranolaOpenApiHttpClient` as a singleton service. | Returns `IServiceCollection`. |
| `GranolaOpenApiHttpClientRegistrar.AddGranolaOpenApiHttpClientAsScoped(services)` | Adds `GranolaOpenApiHttpClient` as a scoped service. | Returns `IServiceCollection`. |

## Practical notes

- Cancellation stops pending work; it does not undo work that has already completed.
- Reuse the registered client instead of constructing one per operation.
- Calls that return a cached or singleton value reuse the same instance until the owning service is disposed.
- Dispose instances you own when their scope ends so held resources can be released.

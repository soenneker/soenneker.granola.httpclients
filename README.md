[![](https://img.shields.io/nuget/v/soenneker.granola.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.granola.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.granola.httpclients/build-and-test.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.granola.httpclients/actions/workflows/build-and-test.yml)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.granola.httpclients/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.granola.httpclients/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.granola.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.granola.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.granola.httpclients/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.granola.httpclients/actions/workflows/codeql.yml)

# Soenneker.Granola.HttpClients

A lazy, cached `HttpClient` configured for Granola's public API and custom authentication schemes.

## Install

```bash
dotnet add package Soenneker.Granola.HttpClients
```

## Configuration

```json
{
  "Granola": {
    "ApiKey": "<API key>",
    "ClientBaseUrl": "https://public-api.granola.ai",
    "AuthHeaderName": "Authorization",
    "AuthHeaderValueTemplate": "Bearer {token}"
  }
}
```

Only `ApiKey` is required. The other values above are the defaults. `{token}` in the header template is replaced with the configured API key.

## Register

```csharp
using Soenneker.Granola.HttpClients.Registrars;
using Microsoft.Extensions.DependencyInjection;

services.AddGranolaOpenApiHttpClientAsSingleton();
```

Singleton is the intended registration for higher-level scoped Granola utilities: disposing a utility scope does not tear down this long-lived client.

The optional `AddGranolaOpenApiHttpClientAsScoped()` registration creates both the wrapper and its cache per scope, so disposing one scope cannot remove another scope's client.

## Direct use

```csharp
HttpClient client = await granolaHttpClient.Get(cancellationToken);
HttpResponseMessage response = await client.GetAsync("/some-endpoint", cancellationToken);
response.EnsureSuccessStatusCode();
```

For generated endpoint methods, use `Soenneker.Granola.OpenApiClientUtil` instead of sending raw HTTP requests.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `Get(cancellationToken)` | Gets or creates the configured client. | Reuses one client within the registered cache lifetime. |
| `AddGranolaOpenApiHttpClientAsSingleton()` | Registers an application-wide client cache. | Intended dependency for scoped Granola utilities. |
| `AddGranolaOpenApiHttpClientAsScoped()` | Registers an independent cache per scope. | Scope disposal affects only that scope's client. |

## Practical notes

- Cancellation can stop lazy client initialization and individual HTTP calls; it does not dispose an already cached client.
- Let the DI container dispose the registered wrapper and cache. Do not dispose the returned `HttpClient` separately.

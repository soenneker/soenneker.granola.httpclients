using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

namespace Soenneker.Granola.HttpClients.Abstract;

/// <summary>
/// A thread-safe singleton <see cref="HttpClient"/> for the Granola API.
/// </summary>
public interface IGranolaOpenApiHttpClient: IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets the configured Granola API HTTP client.
    /// </summary>
    ValueTask<HttpClient> Get(CancellationToken cancellationToken = default);
}

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
    /// Gets the shared HTTP client configured with the Granola API base address and authentication settings.
    /// </summary>
    /// <param name="cancellationToken">Stops client initialization if the shared instance has not been created yet.</param>
    /// <returns>The cached HTTP client instance; repeated calls return the same client until this service is disposed.</returns>
    ValueTask<HttpClient> Get(CancellationToken cancellationToken = default);
}

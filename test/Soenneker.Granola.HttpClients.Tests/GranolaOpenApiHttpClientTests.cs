using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Soenneker.Granola.HttpClients.Abstract;
using Soenneker.Granola.HttpClients.Registrars;
using Soenneker.Tests.HostedUnit;
using Soenneker.Utils.HttpClientCache.Abstract;

namespace Soenneker.Granola.HttpClients.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class GranolaOpenApiHttpClientTests : HostedUnitTest
{
    private readonly IGranolaOpenApiHttpClient _httpclient;

    public GranolaOpenApiHttpClientTests(Host host) : base(host)
    {
        _httpclient = Resolve<IGranolaOpenApiHttpClient>(true);
    }

    [Test]
    public void Default()
    {

    }

    [Test]
    public async Task Scoped_registration_owns_an_independent_cache()
    {
        var services = new ServiceCollection();

        services.AddGranolaOpenApiHttpClientAsScoped();

        ServiceDescriptor cache = services.Single(descriptor => descriptor.ServiceType == typeof(IHttpClientCache));
        ServiceDescriptor client = services.Single(descriptor => descriptor.ServiceType == typeof(IGranolaOpenApiHttpClient));

        await Assert.That(cache.Lifetime).IsEqualTo(ServiceLifetime.Scoped);
        await Assert.That(client.Lifetime).IsEqualTo(ServiceLifetime.Scoped);
    }
}

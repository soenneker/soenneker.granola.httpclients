using Soenneker.Granola.HttpClients.Abstract;
using Soenneker.Tests.HostedUnit;

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
}

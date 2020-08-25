namespace WebHost.Tests
{
    using System.Threading.Tasks;
    using System.Web;

    using FluentAssertions;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.Hosting;
    using Xunit;

    public class IntegrationWebHostQrCode : IAsyncLifetime
    {
        private readonly IHostBuilder _hostBuilder;
        private IHost _host;

        public IntegrationWebHostQrCode()
        {
            _hostBuilder = new HostBuilder()
                .ConfigureWebHost(webHost =>
                                  {
                                      // Add TestServer
                                      webHost.UseTestServer();
                                      webHost.UseStartup<Startup>();
                                  });
        }

        [Fact]
        public async Task QrUrlShouldReturnImageWithQrCode()
        {
            // arrange
            var client = _host.GetTestClient();

            // act
            var response = await client.GetAsync($"/qr/url/{EncodeUrl("www.google.com")}");
            var bytes = await response.Content.ReadAsByteArrayAsync();

            // assert
            response.IsSuccessStatusCode.Should().BeTrue();
            bytes.Length.Should().Be(21013);

            // should be better to verify image.
        }

        [Fact]
        public async Task TextShouldReturnImageWithQrCode()
        {
            // arrange
            var client = _host.GetTestClient();

            // act
            var response = await client.GetAsync($"/qr/text/{EncodeUrl("www.google.com")}");
            var bytes = await response.Content.ReadAsByteArrayAsync();

            // assert
            response.IsSuccessStatusCode.Should().BeTrue();
            bytes.Length.Should().Be(21013);

            // should be better to verify image.
        }

        public async Task InitializeAsync()
        {
            // Create and start up the host
            _host = await _hostBuilder.StartAsync();
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        private static string EncodeUrl(string url)
        {
            return HttpUtility.UrlEncode(url);
        }
    }
}

namespace WebHost.Tests
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web;

    using FluentAssertions;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.Hosting;
    using SixLabors.ImageSharp;
    using VerifyTests;
    using VerifyXunit;
    using Xunit;
    using Xunit.Categories;

    [UsesVerify]
    public class IntegrationWebHostQrCode : IAsyncLifetime
    {
        private static readonly VerifySettings _settings;
        private readonly IHostBuilder _hostBuilder;
        private IHost _host;

        static IntegrationWebHostQrCode()
        {
            VerifyImageSharp.Initialize();

            _settings = new VerifySettings();
            _settings.UseExtension("png");
        }

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
        [Category("ImageVerify")]
        public async Task QrUrlShouldReturnImageWithQrCode()
        {
            // arrange
            using var client = _host.GetTestClient();

            // act
            var response = await client.GetAsync($"/qr/url/{EncodeUrl("www.google.com")}");

            // assert
            response.IsSuccessStatusCode.Should().BeTrue();
            await AssertQrCodeImage(response);
        }

        [Fact]
        [Category("ImageVerify")]
        public async Task TextShouldReturnImageWithQrCode()
        {
            // arrange
            using var client = _host.GetTestClient();

            // act
            var response = await client.GetAsync($"/qr/text/{EncodeUrl("www.google.com")}");

            // assert
            response.IsSuccessStatusCode.Should().BeTrue();
            await AssertQrCodeImage(response);
        }

        [Fact]
        [Category("ImageVerify")]
        public async Task MailShouldReturnImageWithQrCode()
        {
            // arrange
            const string MESSAGE = @"Here is some feedback
Line 2
Line 3";
            using var client = _host.GetTestClient();

            // act
            var response = await client.GetAsync($"/qr/mail/{EncodeUrl("me@h0st.com")}/{EncodeUrl("Feedback on version abc")}/{EncodeUrl(MESSAGE)}");

            // assert
            response.IsSuccessStatusCode.Should().BeTrue();
            await AssertQrCodeImage(response);
        }

        [Fact]
        public async Task MailShouldReturnSameAsEmail()
        {
            // arrange
            const string MESSAGE = @"Here is some feedback
Line 2
Line 3";
            using var client = _host.GetTestClient();

            // act
            var response1 = await client.GetAsync($"/qr/mail/{EncodeUrl("me@h0st.com")}/{EncodeUrl("Feedback on version abc")}/{EncodeUrl(MESSAGE)}");
            var response2 = await client.GetAsync($"/qr/email/{EncodeUrl("me@h0st.com")}/{EncodeUrl("Feedback on version abc")}/{EncodeUrl(MESSAGE)}");

            var bytes1 = await response1.Content.ReadAsByteArrayAsync();
            var bytes2 = await response2.Content.ReadAsByteArrayAsync();

            // assert
            response1.IsSuccessStatusCode.Should().BeTrue();
            response2.IsSuccessStatusCode.Should().BeTrue();
            bytes1.Should().BeEquivalentTo(bytes2);
        }

        public async Task InitializeAsync()
        {
            // Create and start up the host
            _host = await _hostBuilder.StartAsync();
        }

        public async Task DisposeAsync()
        {
            await _host.StopAsync();
            _host.Dispose();
        }

        private static string EncodeUrl(string url)
        {
            return HttpUtility.UrlEncode(url);
        }

        private static async Task AssertQrCodeImage(HttpResponseMessage response)
        {
            var bytes = await response.Content.ReadAsByteArrayAsync();
            using var img = Image.Load(bytes);
            await Verifier.Verify(img, _settings);
        }
    }
}

using Microsoft.AspNetCore.Hosting;

namespace Tests.xUnit.tests
{
    [Collection(Constantes.Collections.PlaywrightCollection)]
    public class FetchDataTests
    {
        private readonly PlaywrightFixture _playwrightFixture;

        /// <summary>
        /// Setup test class injecting a playwrightFixture instance.
        /// </summary>
        /// <param name="playwrightFixture">The playwrightFixture instance.</param>
        public FetchDataTests(PlaywrightFixture playwrightFixture)
        {
            _playwrightFixture = playwrightFixture;
        }

        [Fact]
        public async Task FetchDataTest()
        {
            CreateWebApiHost(Constantes.URL_WEB_API);
            CreateBlazorServerHost(Constantes.URL_BLAZOR_SERVER);

            // Open a page and run test logic.
            await this._playwrightFixture.GotoPageAsync(
              Constantes.URL_BLAZOR_SERVER,
              testHandler: async (page) =>
              {
                  // Apply the test logic on the given page.

                  // Click text=Home
                  await page.Locator("text=Home").ClickAsync();
                  await page.WaitForURLAsync($"{Constantes.URL_BLAZOR_SERVER}/");
                  // Click text=Hello, world!
                  await page.Locator("text=Hello, world!").IsVisibleAsync();

                  // Click text=Counter
                  await page.Locator("text=Counter").ClickAsync();
                  await page.WaitForURLAsync($"{Constantes.URL_BLAZOR_SERVER}/counter");
                  // Click h1:has-text("Counter")
                  await page.Locator("h1:has-text(\"Counter\")").IsVisibleAsync();
                  // Click text=Click me
                  await page.Locator("text=Click me").ClickAsync();
                  // Click text=Current count: 1
                  await page.Locator("text=Current count: 1").IsVisibleAsync();
                  // Click text=Click me
                  await page.Locator("text=Click me").ClickAsync();
                  // Click text=Current count: 2
                  await page.Locator("text=Current count: 2").IsVisibleAsync();
              },
              browserType: Browser.Chromium);
        }

        private void CreateWebApiHost(string url)
        {
            using var hostFactory = new CustomWebApplicationFactory<Program>();

            hostFactory
              .WithWebHostBuilder(builder =>
              {
                  // Setup the url to use.
                  builder.UseUrls(url);
                  // Replace or add services if needed.
                  builder.ConfigureServices(services =>
                  {
                      // services.AddTransient<....>();
                  })
                  // Replace or add configuration if needed.
                  .ConfigureAppConfiguration((app, conf) =>
                  {
                      // conf.AddJsonFile("appsettings.Test.json");
                  });
              }) // Override host configuration to mock stuff if required.
              .CreateDefaultClient(); // Create the host using the CreateDefaultClient method.
        }

        private void CreateBlazorServerHost(string url)
        {
            using var hostFactory = new CustomWebApplicationFactory<Program>();

            hostFactory
              .WithWebHostBuilder(builder =>
              {
                  // Setup the url to use.
                  builder.UseUrls(url);
                  // Replace or add services if needed.
                  builder.ConfigureServices(services =>
                  {
                      // services.AddTransient<....>();
                  })
                  // Replace or add configuration if needed.
                  .ConfigureAppConfiguration((app, conf) =>
                  {
                      // conf.AddJsonFile("appsettings.Test.json");
                  });
              }) // Override host configuration to mock stuff if required.
              .CreateDefaultClient(); // Create the host using the CreateDefaultClient method.
        }
    }
}
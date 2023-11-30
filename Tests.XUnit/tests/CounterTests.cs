using Microsoft.AspNetCore.Hosting;

namespace Tests.xUnit.tests
{
    [Collection(Constantes.Collections.PlaywrightCollection)]
    public class CounterTests
    {
        private readonly PlaywrightFixture _playwrightFixture;

        /// <summary>
        /// Setup test class injecting a playwrightFixture instance.
        /// </summary>
        /// <param name="playwrightFixture">The playwrightFixture instance.</param>
        public CounterTests(PlaywrightFixture playwrightFixture)
        {
            _playwrightFixture = playwrightFixture;
        }

        [Fact]
        public async Task CounterTest()
        {
            var url = Constantes.URL_BLAZOR_SERVER;

            // Create the host factory with the App class as parameter and the url we are going to use.
            using var hostFactory = new CustomWebApplicationFactory<Program>();

            hostFactory
              // Override host configuration to mock stuff if required.
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
              })
              // Create the host using the CreateDefaultClient method.
              .CreateDefaultClient();

            // Open a page and run test logic.
            await _playwrightFixture.GotoPageAsync(
              url,
              testHandler: async (page) =>
              {
                  // Apply the test logic on the given page.

                  // Click text=Home
                  await page.Locator("text=Home").ClickAsync();
                  await page.WaitForURLAsync($"{url}/");
                  // Click text=Hello, world!
                  await page.Locator("text=Hello, world!").IsVisibleAsync();

                  // Click text=Counter
                  await page.Locator("text=Counter").ClickAsync();
                  await page.WaitForURLAsync($"{url}/counter");
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
    }
}
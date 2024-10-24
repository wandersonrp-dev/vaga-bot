using PuppeteerSharp;

namespace VagaBot.WorkerService.Extensions;
public static class PuppeteerSharpExtensions
{
    public static LaunchOptions GetLaunchOptions()
    {
        var launchOptions = new LaunchOptions()
        {
            Headless = false,
            ExecutablePath = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe",
            DefaultViewport = new()
            {
                Height = 900,
                Width = 1900
            }
        };

        return launchOptions;
    }
}

using PuppeteerSharp;
using VagaBot.WorkerService.Extensions;

namespace VagaBot.WorkerService.Services.Linkedin;
public class LinkedinServices : ILinkedinServices
{    
    private readonly Uri _baseUri;
    private readonly string _email;
    private readonly string _password;        

    public LinkedinServices(IConfiguration configuration)
    {        
        _baseUri = configuration.GetLinkedinBaseUri();
        _email = configuration.GetLinkedinEmail();
        _password = configuration.GetLinkedinPassword();        
    }

    public async Task FlowAsync()
    {
        var launchOptions = PuppeteerSharpExtensions.GetLaunchOptions();

        var loginUri = $"{_baseUri}login";

        using var browser = await Puppeteer.LaunchAsync(launchOptions);

        using var page = await browser.NewPageAsync();

        await Login(page);
        
        await Task.Delay(TimeSpan.FromSeconds(35));

        await SearchJobs(browser);              
    }

    private async Task Login(IPage page)
    {
        var loginUri = $"{_baseUri}login";       

        await page.GoToAsync(loginUri);

        var usernameSelector = "#username";

        await page.WaitForSelectorAsync(usernameSelector);

        await page.TypeAsync(usernameSelector, _email);

        var passwordSelector = "#password";

        await page.TypeAsync(passwordSelector, _password);        

        await page.ClickAsync("button[type='submit']");
    }

    private async Task SearchJobs(IBrowser browser)
    {
        var launchOptions = PuppeteerSharpExtensions.GetLaunchOptions();

        var jobsUri = $"{_baseUri}jobs";
        
        using var page = await browser.NewPageAsync();        

        await page.GoToAsync(jobsUri);        

        var inputSearchJobs = ".jobs-search-box__keyboard-text-input";

        await page.WaitForSelectorAsync(inputSearchJobs);

        // TODO: Receber a stack como parâmetro
        await page.TypeAsync(inputSearchJobs, "C# / .Net");

        await page.Keyboard.PressAsync("Enter");        

        var buttonPostDate = await page.WaitForSelectorAsync("#searchFilter_timePostedRange");

        await buttonPostDate.ClickAsync();

        await Task.Delay(TimeSpan.FromSeconds(2));

        await page.ClickAsync("#timePostedRange-r86400");

        await Task.Delay(TimeSpan.FromSeconds(2));

        await page.ClickAsync(".artdeco-button--primary");

        await Task.Delay(TimeSpan.FromSeconds(10));

        await ReadJobDescription(page);

        await Task.Delay(-1);
    }

    private async Task ReadJobDescription(IPage page)
    {
        var jobResults = await page.QuerySelectorAsync(".scaffold-layout__list-container");

        if(jobResults is null)
        {
            return;
        }

        var jobResultItems =  await jobResults.QuerySelectorAllAsync(".jobs-search-results__list-item");
        
        if(jobResultItems.Length == 0)
        {
            return;
        }

        foreach(var jobItem in jobResultItems)
        {
            await jobItem.ClickAsync();            

            await page.WaitForSelectorAsync(".jobs-search__job-details--container");            

            var jobDescriptionContainer = await page.QuerySelectorAsync(".jobs-search__job-details--container");

            var listJobDetails = await page.QuerySelectorAsync(".job-details-jobs-unified-top-card__job-insight--highlight");

            if(listJobDetails is not null)
            {
                var spanTexts = await page.EvaluateFunctionAsync<string[]>(@"(element) => {
                    const spans = element.querySelectorAll('span span');
                    return Array.from(spans).map(span => span.innerText);                    
                }", listJobDetails);                

                //if (spanText != "Remoto" || spanText != "Remote") continue;

                foreach(var spanText in spanTexts)
                {
                    Console.WriteLine($"{spanText}");                    
                }
            }            
        }
    }
}

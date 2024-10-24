using PuppeteerSharp;

namespace VagaBot.WorkerService.Services.Linkedin;
public interface ILinkedinServices
{
    Task FlowAsync();
    //Task SearchJobs(CookieParam[] cookieParams, IPage page, IBrowser browser);
    //Task SearchJobs();
}

using VagaBot.WorkerService.Services.Linkedin;

namespace VagaBot.WorkerService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;    
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;        
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {        
        using var scope = _serviceScopeFactory.CreateScope();

        var linkedinService = scope.ServiceProvider.GetRequiredService<ILinkedinServices>();

        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);                

                //var cookieFile = Path.Combine(FileExtensions.GetRootPath(), "cookies.json");                

                //if(File.Exists(cookieFile))
                //{
                //    Console.WriteLine("Cookies Existentes");
                //    var cookieParams = JsonSerializer.Deserialize<CookieParam[]>(await File.ReadAllTextAsync(cookieFile));
                //
                //    if(cookieParams is null)
                //    {                        
                //        await linkedinService.Login(page, browser);
                //    }
                //    else
                //    {                                         
                //        //await linkedinService.SearchJobs(cookieParams, page, browser);
                //    }
                //}
                //else
                //{                    
                //    await linkedinService.Login(page, browser);
                //    //await linkedinService.SearchJobs(JsonSerializer.Deserialize<CookieParam[]>(await File.ReadAllTextAsync(cookieFile))!, page, browser);
                //}                
            }

            await linkedinService.FlowAsync();

            await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
        }
    }
}

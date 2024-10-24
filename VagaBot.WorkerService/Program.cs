using VagaBot.WorkerService;
using VagaBot.WorkerService.Services.Linkedin;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddScoped<ILinkedinServices, LinkedinServices>();

var host = builder.Build();
host.Run();

namespace VagaBot.WorkerService.Extensions;
public static class FileExtensions
{
    public static string GetRootPath()
    {        
        var rootPath = Directory.GetCurrentDirectory();

        return rootPath;
    }
}

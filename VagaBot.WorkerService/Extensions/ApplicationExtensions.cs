namespace VagaBot.WorkerService.Extensions;
public static class ApplicationExtensions
{
    public static Uri GetLinkedinBaseUri(this IConfiguration configuration)
    {
        var linkedinBaseUri = configuration.GetRequiredSection("AppSettings:Linkedin:BaseUri").Value ?? 
            throw new ArgumentNullException("Provide Linkedin base uri");

        return new Uri(linkedinBaseUri);
    }

    public static string GetLinkedinEmail(this IConfiguration configuration)
    {
        var linkedinEmail = configuration.GetRequiredSection("AppSettings:Linkedin:Email").Value ??
            throw new ArgumentNullException("Provide Linkedin email");

        return linkedinEmail;
    }

    public static string GetLinkedinPassword(this IConfiguration configuration)
    {
        var linkedinPassword = configuration.GetRequiredSection("AppSettings:Linkedin:Password").Value ??
            throw new ArgumentNullException("Provide Linkedin password");

        return linkedinPassword;
    }
}

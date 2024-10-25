using Microsoft.Extensions.Logging;
using OneOf;

namespace MauiApp3;

public class MyService
{
    public OneOf<Success, Error, NotFound> GetData(int id) =>
        id switch
        {
            1 => new Success("Data found!"),
            2 => new Error("An error occurred."),
            _ => new NotFound()
        };
}

public record Success(string Message);
public record Error(string Message);
public record NotFound();

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        var service = new MyService();
        var result = service.GetData(1);

        string message = result.Match(
            success => success.Message,
            error => error.Message,
            notFound => "Data not found"
        );

        Console.WriteLine(message); // Ensure output to avoid optimization

        return builder.Build();
    }
}
using ConsoleApp.Options;

namespace ConsoleApp.Handlers;

public class ExitOptionsHandler : IOptionsHandler
{
    public bool CanParse(object options)
    {
        return options is ExitOptions;
    }

    public string WithParsed(object options)
    {
        Environment.Exit(0);
        return "Завершение выполнения программы.";
    }
}
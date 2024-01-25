using ConsoleApp.CommandLineParsers.Options;

namespace ConsoleApp.CommandLineParsers.Handlers;

public class ExitOptionsHandler: IOptionsHandler<ExitOptions>
{
    public void Map(ExitOptions options)
    {
    }

    public void Map(object options)
    {
    }

    public void Execute()
    {
        Environment.Exit(0);
    }
}
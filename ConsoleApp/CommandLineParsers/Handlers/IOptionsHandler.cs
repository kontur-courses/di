using ConsoleApp.CommandLineParsers.Options;

namespace ConsoleApp.CommandLineParsers.Handlers;

public interface IOptionsHandler<in TOptions> : IOptionsHandler where TOptions : IOptions
{
    public void Map(TOptions options);
}

public interface IOptionsHandler
{
    public void Map(object options);

    public void Execute();
}
using ConsoleApp.Options;

namespace ConsoleApp.Handlers;

public interface IOptionsHandler
{
    public bool CanParse(IOptions options);
    
    public string WithParsed(IOptions options);
}
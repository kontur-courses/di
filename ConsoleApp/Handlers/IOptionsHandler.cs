namespace ConsoleApp.Handlers;

public interface IOptionsHandler
{
    public bool CanParse(object options);
    
    public string WithParsed(object options);
}
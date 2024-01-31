using CommandLine;
using ConsoleApp.Handlers;
using ConsoleApp.Options;

namespace ConsoleApp;

public class CommandLineParser: ICommandLineParser
{
    private readonly IOptionsHandler[] handlers;
    private readonly IOptions[] options;

    public CommandLineParser(IOptionsHandler[] handlers, IOptions[] options)
    {
        this.handlers = handlers;
        this.options = options;
    }

    public void ParseFromConsole()
    {
        var types = options
            .Select(opt => opt.GetType())
            .ToArray();
        
        Console.WriteLine("Доступные команды \"--help\"");
        while (true)
        {
            var input = Console.ReadLine();
            var args = input.Split();
            Parser.Default.ParseArguments(args, types).WithParsed(Parse);
        }
    }

    private void Parse<T>(T options)
    {
        var handler = handlers.FirstOrDefault(h => h.CanParse(options));
        if (handler is null)
            throw new Exception("Обработчик параметров не найден.");
        
        var message = handler.WithParsed(options);
        Console.WriteLine(message);
    }
}
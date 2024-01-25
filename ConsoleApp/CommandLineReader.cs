using CommandLine;
using ConsoleApp.CommandLineParsers.Handlers;
using ConsoleApp.CommandLineParsers.Options;

namespace ConsoleApp;

public class CommandLineReader
{
    private readonly IOptionsHandler[] handlers;

    public CommandLineReader(IOptionsHandler[] handlers)
    {
        this.handlers = handlers;
    }

    public void Read()
    {
        while (true)
        {
            var input = Console.ReadLine();
            var args = input.Split();

            Parser.Default.ParseArguments<PreprocessTextOptions, SaveImageOptions, ExitOptions>(args)
                .WithParsed<PreprocessTextOptions>(opt =>
                {
                    var handler = handlers.FirstOrDefault(h => h is IOptionsHandler<PreprocessTextOptions>);
                    if (handler is null)
                        throw new Exception("Обработчик параметров не найден.");
                        
                    handler.Map(opt);
                    handler.Execute();
                })
                .WithParsed<SaveImageOptions>(opt =>
                {
                    var handler = handlers.FirstOrDefault(h => h is IOptionsHandler<SaveImageOptions>);
                    if (handler is null)
                        throw new Exception("Обработчик параметров не найден.");
                    
                    handler.Map(opt);
                    handler.Execute();
                })
                .WithParsed<ExitOptions>(opt =>
                {
                    var handler = handlers.FirstOrDefault(h => h is IOptionsHandler<ExitOptions>);
                    if (handler is null)
                        throw new Exception("Обработчик параметров не найден.");
                    
                    handler.Execute();
                });
        }
    }
}
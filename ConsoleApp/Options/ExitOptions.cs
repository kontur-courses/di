using CommandLine;

namespace ConsoleApp.Options;

[Verb("exit", HelpText = "Закончить выполнение программы")]
public class ExitOptions: IOptions
{
}
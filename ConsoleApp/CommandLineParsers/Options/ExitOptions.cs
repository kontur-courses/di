using CommandLine;

namespace ConsoleApp.CommandLineParsers.Options;

[Verb("exit", HelpText = "Закончить выполнение программы")]
public class ExitOptions: IOptions
{
}
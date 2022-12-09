using App.ConsoleApplication;
using FluentAssertions;
using NUnit.Framework;

namespace AppTests;

public class ConsoleAppTests
{
    private readonly ArgumentsParser parser = new();

    [TestCase("Help -h --help", TestName = "Help contains information about itself")]
    [TestCase("Image width -W --width", TestName = "Help contains information about width option")]
    [TestCase("Image height -H --height", TestName = "Help contains information about height option")]
    [TestCase("Image file to read words -f --file", TestName = "Help contains information about file option")]
    [TestCase("Font name -F --font", TestName = "Help contains information about font option")]
    public void Execute_Help_ContainsInformation(string info)
    {
        var args = new[] { "-h" };
        var sw = new StringWriter();
        Console.SetOut(sw);
        Console.SetError(sw);
        parser.ParseArgs(args);
        var result = sw.ToString();
        result.Should().Contain(info);
    }
}
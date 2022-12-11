using ConsoleApp;
using FluentAssertions;
using NUnit.Framework;

namespace ConsoleAppTests;

public class ConsoleAppTests
{
    [TestCase("--help", TestName = "Help contains information about itself")]
    [TestCase("-W, --width", TestName = "Help contains information about width option")]
    [TestCase("-H, --height", TestName = "Help contains information about height option")]
    [TestCase("-f, --file", TestName = "Help contains information about file option")]
    [TestCase("-F, --font", TestName = "Help contains information about font option")]
    [TestCase("-d, --density ", TestName = "Help contains information about density option")]
    public void Execute_Help_ContainsInformation(string info)
    {
        var args = new[] { "--help" };
        var sw = new StringWriter();
        Console.SetError(sw);
        new ArgumentsParser().ParseArgs(args);
        var result = sw.ToString();
        result.Should().Contain(info);
    }
}
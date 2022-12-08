using ConsoleApp;
using FluentAssertions;
using NUnit.Framework;

namespace ConsoleAppTests;

public class ConsoleAppTests
{
    private ArgsParser parser = new ArgsParser();

    [TestCase("Help -h --help", TestName = "Help contains information about itself")]
    [TestCase("Image width -W --width", TestName = "Help contains information about width option")]
    [TestCase("Image height -H --height", TestName = "Help contains information about height option")]
    [TestCase("Run in GUI mode -g --gui", TestName = "Help contains information about gui mode")]
    public void Help_ContainsInformation(string info)
    {
        var args = new string[]
        {
            "-h"
        };
        var sw = new StringWriter();
        Console.SetOut(sw);
        Console.SetError(sw);
        parser.ParseArgs(args);
        var result = sw.ToString();
        result.Should().Contain(info);
    }
}
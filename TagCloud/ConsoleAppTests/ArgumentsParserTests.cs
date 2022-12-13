using ConsoleApp;
using FluentAssertions;
using NUnit.Framework;

namespace ConsoleAppTests;

[TestFixture]
public class ArgumentsParserTests
{
    [TestCase("--help", TestName = "Help contains information about itself")]
    [TestCase("-W, --width", TestName = "Help contains information about width option")]
    [TestCase("-H, --height", TestName = "Help contains information about height option")]
    [TestCase("-f, --file", TestName = "Help contains information about file option")]
    [TestCase("-F, --font", TestName = "Help contains information about font option")]
    [TestCase("-d, --density", TestName = "Help contains information about density option")]
    [TestCase("--exclude", TestName = "Help contains information about exclusion option")]
    [TestCase("-o", TestName = "Help contains information about output option")]
    public void Help_ContainsInformation(string info)
    {
        var args = new[] { "--help" };
        var sw = new StringWriter();
        Console.SetError(sw);
        
        ArgumentsParser.ParseArgs(args);
        var result = sw.ToString();
        
        result.Should().Contain(info);
    }

    [TestCase("-W 720 -H wrongType -f Cloud.txt", TestName = "Wrong argument type")]
    [TestCase("-W 720 -H 310", TestName = "No required argument")]
    public void Null_OnIncorrectArguments(string argumentsLine)
    {
        var arguments = argumentsLine.Split().ToArray();
        
        ArgumentsParser.ParseArgs(arguments).Should().BeNull();
    }
}
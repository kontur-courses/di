using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;
using System.Reflection;
using TagsCloudContainer;
using TagsCloudContainer.Abstractions;

namespace ContainerTests;

public class ThrowingRunner : IRunner
{
    public const string ExceptionText = "tried to use throwing runner";
    public void Run(params string[] args)
    {
        throw new Exception(ExceptionText);
    }
}

public class ContainerTests
{

    [Test]
    public void CanChangeUsableImplementations()
    {
        var args = new[] { "--assemblies", Assembly.GetExecutingAssembly().Location, "--implement-with", $"{nameof(IRunner)} {nameof(ThrowingRunner)}" };
        var action = () => Program.Main(args);

        action.Should().Throw<Exception>().Match(x => x.Any(e => e.Message.Contains(ThrowingRunner.ExceptionText)));
    }

    [Test]
    public void MainShould_NotThrow_WhenAtLeastRequiredSettingsProvided()
    {
        var action = () => Program.Main(new[] { "--string", "тэгер тэг тэги" });

        action.Should().NotThrow();
    }

    [Test]
    public void MainShould_Throw_WhenNoRequiredSettingsProvided()
    {
        var action = () => Program.Main(Array.Empty<string>());

        action.Should().Throw<ArgumentException>();
    }
}


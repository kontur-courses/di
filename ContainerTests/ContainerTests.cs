using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;
using System.Reflection;
using TagsCloudContainer;
using TagsCloudContainer.Abstractions;
using TagsCloudContainer.Registrations;

namespace ContainerTests;

public class ThrowingRunner : IRunner
{
    public const string ExceptionText = "tried to use throwing runner";
    public void Run(params string[] args)
    {
        throw new Exception(ExceptionText);
    }
}

public interface ITotallyLegitimateService : IService
{

}

public class TotallyLegitimateService : ITotallyLegitimateService
{

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
    public void Main_ShouldNotThrow_WhenAtLeastRequiredSettingsProvided()
    {
        var action = () => Program.Main(new[] { "--string", "тэгер тэг тэги" });

        action.Should().NotThrow();
    }

    [Test]
    public void Main_ShouldThrow_WhenNoRequiredSettingsProvided()
    {
        var action = () => Program.Main(Array.Empty<string>());

        action.Should().Throw<ArgumentException>();
    }

    [Test]
    public void ServiceIndex_Should_CorrectlyIndexAvailableServicesAndImplementationsFromAssemblies()
    {
        var serviceIndex = new ServiceIndex();

        serviceIndex.AddAssemblyTypes(Assembly.GetExecutingAssembly());
        var implemetation = serviceIndex.GetImplementation(nameof(TotallyLegitimateService));
        var service = serviceIndex.GetService(nameof(ITotallyLegitimateService));

        implemetation.Implementation.Should().Be(typeof(TotallyLegitimateService));
        service.Should().Be(typeof(ITotallyLegitimateService));
        implemetation.ImplementedServices.Should().BeEquivalentTo(new[] { service });
    }
}


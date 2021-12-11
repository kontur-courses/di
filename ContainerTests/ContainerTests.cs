using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;
using System.Reflection;
using TagsCloudContainer;
using TagsCloudContainer.Abstractions;
using TagsCloudContainer.Registrations;

namespace ContainerTests;

public class ContainerTests
{

    [Test]
    public void CanChangeUsableImplementations()
    {
        var args = new[] { "--assemblies", Assembly.GetExecutingAssembly().Location, "--implement-with", $"{nameof(IRunner)} {nameof(ThrowingRunner)}" };
        var action = () => InitializationHelper.RunWithArgs(args);

        action.Should().Throw<Exception>().Match(x => x.Any(e => e.Message.Contains(ThrowingRunner.ExceptionText)));
    }

    [Test]
    public void ShouldNotThrow_WhenAtLeastRequiredSettingsProvided()
    {
        var action = () => InitializationHelper.RunWithArgs(new[] { "--string", "тэгер тэг тэги" });

        action.Should().NotThrow();
    }

    [Test]
    public void ShouldThrow_WhenNoRequiredSettingsProvided()
    {
        var action = () => InitializationHelper.RunWithArgs(Array.Empty<string>());

        action.Should().Throw<ArgumentException>();
    }

    [Test]
    public void ServiceIndex_Should_CorrectlyIndexAvailableServicesAndImplementationsFromAssemblies()
    {
        var serviceIndex = new ServiceIndex();

        serviceIndex.AddAssemblyTypes(Assembly.GetExecutingAssembly());
        var implementation = serviceIndex.GetImplementation(nameof(TotallyLegitimateService));
        var service = serviceIndex.GetService(nameof(ITotallyLegitimateService));

        implementation.Implementation.Should().Be(typeof(TotallyLegitimateService));
        service.Should().Be(typeof(ITotallyLegitimateService));
        implementation.ImplementedServices.Should().BeEquivalentTo(new[] { service });
    }

    private class ThrowingRunner : IRunner
    {
        public const string ExceptionText = "tried to use throwing runner";
        public void Run(params string[] args)
        {
            throw new Exception(ExceptionText);
        }
    }

    private interface ITotallyLegitimateService : IService
    {

    }

    private class TotallyLegitimateService : ITotallyLegitimateService
    {

    }
}


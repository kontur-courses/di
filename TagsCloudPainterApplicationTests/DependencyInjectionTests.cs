using Autofac;
using TagsCloudPainter.CloudLayouter;
using TagsCloudPainter.Drawer;
using TagsCloudPainter.FileReader;
using TagsCloudPainter.FormPointer;
using TagsCloudPainter.Parser;
using TagsCloudPainter.Settings;
using TagsCloudPainter.Tags;
using TagsCloudPainterApplication;
using TagsCloudPainterApplication.Actions;
using TagsCloudPainterApplication.Infrastructure;
using TagsCloudPainterApplication.Infrastructure.Settings;

namespace TagsCloudPainterApplicationTests;

[TestFixture]
public class DependencyInjectionTests
{
    [SetUp]
    public void Setup()
    {
        var builder = new ContainerBuilder();
        builder.RegisterModule<TagsCloudPainterLibModule>();
        builder.RegisterModule<ApplicationModule>();
        var container = builder.Build();
        scope = container.BeginLifetimeScope();
    }

    [TearDown]
    public void TearDown()
    {
        scope.Dispose();
    }

    private ILifetimeScope scope;

    private static IEnumerable<TestCaseData> InstancePerLifetimeScopeDependencesTypes => new[]
    {
        new TestCaseData(typeof(IFormPointer)),
        new TestCaseData(typeof(ICloudLayouter)),
        new TestCaseData(typeof(IUiAction)),
        new TestCaseData(typeof(MainForm))
    };

    private static IEnumerable<TestCaseData> SingleInstanceDependencesTypes => new[]
    {
        new TestCaseData(typeof(TagSettings)),
        new TestCaseData(typeof(CloudDrawer)),
        new TestCaseData(typeof(IFileReader)),
        new TestCaseData(typeof(ITextParser)),
        new TestCaseData(typeof(ITagsBuilder)),
        new TestCaseData(typeof(TextSettings)),
        new TestCaseData(typeof(CloudSettings)),
        new TestCaseData(typeof(SpiralPointerSettings)),
        new TestCaseData(typeof(Palette)),
        new TestCaseData(typeof(ImageSettings)),
        new TestCaseData(typeof(FilesSourceSettings)),
        new TestCaseData(typeof(TagsCloudSettings)),
        new TestCaseData(typeof(PictureBoxImageHolder)),
        new TestCaseData(typeof(IImageHolder))
    };

    [TestCaseSource(nameof(SingleInstanceDependencesTypes))]
    [TestCaseSource(nameof(InstancePerLifetimeScopeDependencesTypes))]
    public void Dependence_SouldResolve(Type dependenceType)
    {
        Assert.DoesNotThrow(() => scope.Resolve(dependenceType));
    }

    [TestCaseSource(nameof(SingleInstanceDependencesTypes))]
    [TestCaseSource(nameof(InstancePerLifetimeScopeDependencesTypes))]
    public void Dependence_SouldBeNotNull(Type dependenceType)
    {
        var dependence = scope.Resolve(dependenceType);
        Assert.That(dependence, Is.Not.Null);
    }

    [TestCaseSource(nameof(SingleInstanceDependencesTypes))]
    public void SingleInstanceDependence_ShouldResolveSameReferenceInDifferentScopes(Type dependenceType)
    {
        using var childScope = scope.BeginLifetimeScope();
        var dependence1 = scope.Resolve(dependenceType);
        var dependence2 = childScope.Resolve(dependenceType);

        Assert.That(dependence2, Is.SameAs(dependence1));
    }

    [TestCaseSource(nameof(InstancePerLifetimeScopeDependencesTypes))]
    public void InstancePerLifetimeScopeDependence_ShouldResolveDifferentReferencesInDifferentScopes(
        Type dependenceType)
    {
        using var childScope = scope.BeginLifetimeScope();
        var dependence1 = scope.Resolve(dependenceType);
        var dependence2 = childScope.Resolve(dependenceType);

        Assert.That(dependence2, Is.Not.EqualTo(dependence1));
    }
}
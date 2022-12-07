using System.Drawing;
using System.Drawing.Imaging;
using TagCloud.Savers;

namespace TagCloud.Tests;

[TestFixture]
public class HardDriverSaverTests
{
    [SetUp]
    public void Setup()
    {
        _saver = new HardDriveSaver();
    }

    private HardDriveSaver _saver;

    public static IEnumerable<TestCaseData> SavingArguments
    {
        get
        {
            yield return new TestCaseData(new Bitmap(1, 1), "some_file", ImageFormat.Png).SetName(
                "Save image with some_file name and png extension.");
        }
    }

    [TestCaseSource(nameof(SavingArguments))]
    public void Save_CorrectParameters_ShouldSaveToHardDriver(Bitmap bitmap, string filename, ImageFormat format)
    {
        _saver.Save(bitmap, filename, format);
        File.Exists($"{filename}.{format}").Should().BeTrue();
    }
}
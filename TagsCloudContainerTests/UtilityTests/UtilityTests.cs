using FluentAssertions;
using TagsCloudContainer.utility;

namespace TagsCloudContainerTests.UtilityTests;

[TestFixture]
public class UtilityTests
{
    [TestCase("secondsrc/deepsrc/output.txt")]
    [TestCase("thirddsrc/output.txt")]
    [TestCase("output.txt")]
    public void GetRelativeFilePath_CreatesDir_IfNot_Exist(string path)
    {
        var abs = Utility.GetAbsoluteFilePath(path);
        
        (!path.Contains('/') || Directory.Exists(abs[..abs.LastIndexOf('/')]))
            .Should().BeTrue();
    }
}
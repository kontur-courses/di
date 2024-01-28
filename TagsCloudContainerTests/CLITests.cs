using FluentAssertions;
using TagsCloudContainer.UI;

namespace TagsCloudContainerTests;

[TestFixture]
public class CLITests
{
    [Test]
    public void HelpCommand_Should_NotThrow()
    {
        var action = () => new CLI().Setup(["--help"]);
        action.Should().NotThrow();
    }

    [Test]
    public void MissRequiredCommand_Should_Throw()
    {
        var action = () => new CLI().Setup([
            """
            -i="/Users/draginsky/Rider/di/TagsCloudContainer/src/words.txt"
            -o="/Users/draginsky/Rider/di/TagsCloudContainer/out/res"
            """
        ]);
        action.Should().Throw<ArgumentNullException>();
    }
}
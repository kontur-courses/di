using FluentAssertions;
using LightInject;
using NUnit.Framework;
using System.IO;
using TagsCloudContainer;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainerTests
{
    internal class CloudImageGeneratorTests
    {
        [Test]
        public void Should_SaveToFile()
        {
            var container = ContainerProvider.GetContainer();
            var textPath = Path.Combine(Path.GetFullPath(@"..\..\..\texts"), "test.txt");
            var parsed = container.GetInstance<IParser>().Parse(textPath);
            var preprocessed = container.GetInstance<IPreprocessorsApplier>()
                .ApplyPreprocessors(parsed);
            var tags = container.GetInstance<ITagCreator>().CreateTags(preprocessed);
            var painted = container.GetInstance<ITagPainter>().Paint(tags);
            var cloudPainter = container.GetInstance<TagCloudPainter>();

            var path = cloudPainter.Paint(painted);

            File.Exists(path).Should().BeTrue();
            File.Delete(path);
        }
    }
}

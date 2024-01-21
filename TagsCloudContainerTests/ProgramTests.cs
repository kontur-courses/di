using Moq;
using NUnit.Framework;
using TagsCloudContainer;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainerTests
{
    [TestFixture]
    public class ProgramTests
    {
        private Mock<IFileReader> fileReaderMock;
        private Mock<IPreprocessor> preprocessorMock;
        private Mock<ITagCloudGenerator> tagCloudGeneratorMock;
        private Mock<IImageSettings> imageSettingsMock;

        private Program program;

        [SetUp]
        public void SetUp()
        {
            //fileReaderMock = new Mock<IFileReader>();
            //preprocessorMock = new Mock<IPreprocessor>();
            //tagCloudGeneratorMock = new Mock<ITagCloudGenerator>();
            //imageSettingsMock = new Mock<IImageSettings>();

            //program = new Program(fileReaderMock.Object, preprocessorMock.Object, tagCloudGeneratorMock.Object, imageSettingsMock.Object);
        }

        
    }
}

using TagsCloudContainer.FileReader;

namespace TagsCloudContainerTests
{
    [TestFixture]
    public class FileReaderTest
    {

        [TestCase("Text.txt")]
        [TestCase("Boring.txt")]
        public void GetReader_ShouldCorrectTxtReader(string fileName)
        {
            var reader = new FileReaderFactory().GetReader(fileName);
            reader.Should().BeOfType<TxtFileReader>();
        }
        
        [TestCase("Text.docx")]
        [TestCase("Boring.docx")]
        public void GetReader_ShouldCorrectDocxReader(string fileName)
        {
            var reader = new FileReaderFactory().GetReader(fileName);
            reader.Should().BeOfType<DocxFileReader>();
        }
        
        [TestCase("Text.mp4")]
        [TestCase("Boring.cs")]
        public void GetReader_ShouldThrowArgumentException(string fileName)
        {
            Action action = () => new FileReaderFactory().GetReader(fileName);
            action.Should().Throw<ArgumentException>();
        }
    }
}
namespace TagCloudShould.Infrastructure
{
    [TestFixture]
    public class TextRectangleShould
    {
        [TestCase("", TestName = "TextRectangle text is \"\"")]
        [TestCase(null, TestName = "TextRectangle text is null")]
        public void ThrowException_WhenNullTextRectangleFieldText(string text)
        {

            Action action = () => new TextRectangle(new Rectangle(10, 10, 10, 10), text, new Font("Times", 10));
            action.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void ThrowException_WhenNullTextRectangleFieldFont()
        {

            Action action = () => new TextRectangle(new Rectangle(10, 10, 10, 10), "text", null);
            action.Should().Throw<ArgumentNullException>();
        }
    }
}

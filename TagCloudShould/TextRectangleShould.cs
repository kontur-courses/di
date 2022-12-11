namespace TagCloudShould
{
    [TestFixture]
    public class TextRectangleShould
    {
        [Test]
        public void ThrowException_WhenNullTextRectangleFieldFont()
        {

            Action action = () => new TextRectangle(new Rectangle(10, 10, 10, 10), "text", null);
            action.Should().Throw<ArgumentNullException>();
        }
    }
}

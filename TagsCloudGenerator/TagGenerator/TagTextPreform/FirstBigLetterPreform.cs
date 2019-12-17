namespace TagsCloudGenerator
{
    public class FirstBigLetterPreform : ITagTextPreform
    {
        public string PreformToVisualize(string str)
        {
            return str.Substring(0, 1).ToUpper() + str.Substring(1);
        }
    }
}
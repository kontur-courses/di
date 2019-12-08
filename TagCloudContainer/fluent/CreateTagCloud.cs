namespace TagCloudContainer.fluent
{
    public static class CreateTagCloud
    {
        public static TagCloudConfig FromFile(string inputFile)
        {
            return new TagCloudConfig(inputFile);
        }
    }
}
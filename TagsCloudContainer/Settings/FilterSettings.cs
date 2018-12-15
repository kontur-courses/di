namespace TagsCloudContainer.Settings
{
    public class FilterSettings
    {
        public FilterSettings(string fileForBoringWords = null, int lengthForBoringWord = 4)
        {
            LengthForBoringWord = lengthForBoringWord;
            FileForBoringWords = fileForBoringWords;
        }

        public int LengthForBoringWord { get; }
        public string FileForBoringWords { get; }
        
    }
}
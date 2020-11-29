namespace TagsCloudContainer.WordsParser
{
    public class Settings: ISettings
    {
        public bool ToInitialForm { get; }

        public Settings(bool toInitialForm = false)
        {
            ToInitialForm = toInitialForm;
        }
    }
}
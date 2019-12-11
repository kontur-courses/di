namespace TagCloud
{
    public class SpeechPartListAction : IUiAction
    {
        private readonly SpeechPart[] speechParts;

        public SpeechPartListAction(SpeechPart[] speechParts)
        {
            this.speechParts = speechParts;
        }

        public MenuCategory Category => MenuCategory.Lists;
        public string Name => "Speech parts...";
        public string Description => "Speech parts choice";

        public void Perform()
        {
            CheckedListForm.For(speechParts).ShowDialog();
        }
    }
}

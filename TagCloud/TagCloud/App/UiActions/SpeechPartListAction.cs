namespace TagCloud
{
    public class SpeechPartListAction : IUiAction
    {
        private readonly SpeechPartList speechPartList;

        public SpeechPartListAction(SpeechPartList speechPartList)
        {
            this.speechPartList = speechPartList;
        }

        public MenuCategory Category => MenuCategory.Lists;
        public string Name => "Speech parts...";
        public string Description => "Speech parts choice";

        public void Perform()
        {
            CheckedListForm.For(speechPartList).ShowDialog();
        }
    }
}

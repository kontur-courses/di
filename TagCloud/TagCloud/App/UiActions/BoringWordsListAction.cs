namespace TagCloud
{
    public class BoringWordsListAction : IUiAction
    {
        private readonly BoringWord[] boringWords;

        public BoringWordsListAction(BoringWord[] boringWords)
        {
            this.boringWords = boringWords;
        }

        public MenuCategory Category => MenuCategory.Lists;
        public string Name => "Boring words...";
        public string Description => "Boring words choice";

        public void Perform()
        {
            CheckedListForm.For(boringWords).ShowDialog();
        }
    }
}

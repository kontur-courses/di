namespace TagCloud
{
    public class ParserListAction : IUiAction
    {
        private readonly IParser[] parsers;

        public ParserListAction(IParser[] parsers)
        {
            this.parsers = parsers;
        }

        public MenuCategory Category => MenuCategory.Lists;
        public string Name => "Parsers...";
        public string Description => "Parsers choice";

        public void Perform()
        {
            CheckedListForm.For(parsers).ShowDialog();
        }
    }
}

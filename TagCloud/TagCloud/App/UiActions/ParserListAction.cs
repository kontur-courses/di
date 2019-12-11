namespace TagCloud
{
    public class ParserListAction : IUiAction
    {
        private readonly ParserList parserList;

        public ParserListAction(ParserList parserList)
        {
            this.parserList = parserList;
        }

        public MenuCategory Category => MenuCategory.Lists;
        public string Name => "Parsers...";
        public string Description => "Parsers choice";

        public void Perform()
        {
            CheckedListForm.For(parserList).ShowDialog();
        }
    }
}

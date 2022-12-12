using TagCloud;

namespace GuiApp.Controls;

public sealed class ExcludedWords : TextBox
{
    private IWordsParser wordsParser;

    public List<string> Words => wordsParser.Parse(Text);
    
    public ExcludedWords(IWordsParser wordsParser)
    {
        PlaceholderText = "Write a word here to exclude it";
        Dock = DockStyle.Fill;
        this.wordsParser = wordsParser;
    }

    public event EventHandler? ExcludedWordsChanged;

    protected override void OnLostFocus(EventArgs e)
    {
        base.OnLostFocus(e);
        ExcludedWordsChanged?.Invoke(this, e);
    }
}
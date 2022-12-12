namespace GuiApp.Controls;

public sealed class ExcludedWords : TextBox
{
    public ExcludedWords()
    {
        PlaceholderText = "Write a word here to exclude it";
        Dock = DockStyle.Fill;
    }

    public event EventHandler? ExcludedWordsChanged;

    protected override void OnLostFocus(EventArgs e)
    {
        base.OnLostFocus(e);
        ExcludedWordsChanged?.Invoke(this, e);
    }
}
namespace GuiApp.Controls;

public class ExcludedWords : TextBox
{
    public event EventHandler? ExcludedWordsChanged;
    
    public ExcludedWords()
    {
        PlaceholderText = "Write a word here to exclude it";
        Dock = DockStyle.Fill;
    }

    protected override void OnLostFocus(EventArgs e)
    {
        base.OnLostFocus(e);
        ExcludedWordsChanged?.Invoke(this, e);
    }
}
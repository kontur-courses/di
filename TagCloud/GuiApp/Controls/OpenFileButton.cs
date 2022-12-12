namespace GuiApp.Controls;

public class OpenFileButton : ControlWithDescription<Button>
{
    private readonly ToolTip toolTip = new();

    private string file = string.Empty;

    public OpenFileButton() : base(new Button(), "File not opened")
    {
        Control.Text = "Open";
        Control.Click += OnClick;
    }

    public string File
    {
        get => file;
        private set
        {
            file = value;
            FileChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public event EventHandler? FileChanged;

    private void OnClick(object? sender, EventArgs eventArgs)
    {
        var fileDialog = new OpenFileDialog();

        fileDialog.Filter = "All text files|*.txt; *.doc; *.docx" + "|" +
                            "Text files|*.txt" + "|" +
                            "Word document files|*.doc; *.docx";
        fileDialog.FilterIndex = 0;
        fileDialog.RestoreDirectory = true;

        if (fileDialog.ShowDialog() != DialogResult.OK) return;
        File = fileDialog.FileName;
        Description.Text = File.Split(new[] { Path.DirectorySeparatorChar }).Last();
        toolTip.SetToolTip(Description, File);
    }
}
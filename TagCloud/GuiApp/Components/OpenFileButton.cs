namespace GuiApp.Components;

public class OpenFileButton : ControlWithDescription<Button>
{
    public event EventHandler? FileChanged;
    
    public OpenFileButton() : base(new Button(), "File not opened")
    {
        Control.Text = "Open";
        Control.Click += OnClick;
    }

    private string file = string.Empty;
    public string File 
    { 
        get => file;
        private set
        {
            file = value;
            FileChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    
    protected void OnClick(object? sender, EventArgs eventArgs)
    {
        var fileDialog = new OpenFileDialog();

        fileDialog.Filter = "All text files|*.txt; *.doc; *.docx" + "|" +
                            "Text files|*.txt" + "|" +
                            "Word document files|*.doc; *.docx";
        fileDialog.FilterIndex = 0;
        fileDialog.RestoreDirectory = true;

        if (fileDialog.ShowDialog() != DialogResult.OK) return;
        File = fileDialog.FileName;
        Description.Text = File.Split(new[]{Path.DirectorySeparatorChar}).Last();
        toolTip.SetToolTip(Description, File);
    }

    private ToolTip toolTip = new();
}
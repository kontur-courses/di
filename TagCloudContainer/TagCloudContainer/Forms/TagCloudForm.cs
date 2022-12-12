namespace TagCloudContainer;

public partial class TagCloudForm : Form
{
    private Graphics _graphics;
    private MainFormConfig _mainFormConfig; 
    
    public TagCloudForm(MainFormConfig mainFormConfig)
    {
        _mainFormConfig = mainFormConfig;
        InitializeComponent();
        SetupApplication();
    }

    private void SetupApplication()
    {
        SetupWindow();
    }

    private void SetupWindow()
    {
        Text = "Tag Cloud Container";
        TopMost = true;
        Size = _mainFormConfig.FormSize;
    }

    private void Render(object sender, PaintEventArgs e)
    {
        _graphics = e.Graphics;
        _graphics.Clear(_mainFormConfig.BackgroundColor);
    
        var pen = new Pen(_mainFormConfig.Color);

        var center = new Point(Width / 2, Height / 2);
        var standartSize = new Size(10, 10);
        var tagCloudProvider = new TagCloudProvider("words.txt", center, standartSize, _mainFormConfig);
        var words = tagCloudProvider.GetPreparedWords();

        foreach (var word in words)
        {
            _graphics.DrawString(
                word.Value, 
                new Font(_mainFormConfig.FontFamily, word.Weight * standartSize.Width), 
                pen.Brush, 
                word.Position);
        }
        
        var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        ImageCreator.Save(this, Path.Combine(projectPath, "Images", "test.png"));
    }
}
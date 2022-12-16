namespace TagCloudContainer;

public partial class TagCloudForm : Form
{
    private Graphics _graphics;
    private ITagCloudProvider _tagCloudProvider;
    
    public TagCloudForm(ITagCloudProvider tagCloudProvider)
    {
        _tagCloudProvider = tagCloudProvider;
        InitializeComponent();
        SetupWindow();
    }

    private void SetupWindow()
    {
        Text = "Tag Cloud Container";
        Size = MainFormConfig.FormSize;
    }

    public void ChangeSize(Size size)
    {
        Size = size;
    }

    private void Render(object sender, PaintEventArgs e)
    {
        _graphics = e.Graphics;
        _graphics.Clear(MainFormConfig.BackgroundColor);
    
        var pen = new Pen(MainFormConfig.Color);

        MainFormConfig.Center = new Point(Width / 2, Height / 2);
        MainFormConfig.StandartSize = new Size(10, 10);
        var words = _tagCloudProvider.GetPreparedWords();
            
        foreach (var word in words)
        {
            _graphics.DrawString(
                word.Value, 
                new Font(MainFormConfig.FontFamily, word.Weight * MainFormConfig.StandartSize.Width), 
                pen.Brush, 
                word.Position);
        }
        SaveImage(); 
    }

    private void SaveImage()
    {
        var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        ImageCreator.Save(this, Path.Combine(projectPath, "Images", "test.png"));
    }
}
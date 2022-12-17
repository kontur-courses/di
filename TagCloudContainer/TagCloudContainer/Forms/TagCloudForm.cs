namespace TagCloudContainer;

public partial class TagCloudForm : Form
{
    private Graphics _graphics;
    private readonly ITagCloudProvider _tagCloudProvider;
    private readonly IMainFormConfig _mainFormConfig;
    private readonly IImageCreator _imageCreator;
    
    public TagCloudForm(ITagCloudProvider tagCloudProvider, IMainFormConfig mainFormConfig, IImageCreator imageCreator)
    {
        _tagCloudProvider = tagCloudProvider;
        _mainFormConfig = mainFormConfig;
        _imageCreator = imageCreator;
        
        InitializeComponent();
        SetupWindow();
    }

    private void SetupWindow()
    {
        Text = "Tag Cloud Container";
        Size = _mainFormConfig.FormSize;
    }

    public void ChangeSize(Size size)
    {
        Size = size;
    }

    private void Render(object sender, PaintEventArgs e)
    {
        _graphics = e.Graphics;
        _graphics.Clear(_mainFormConfig.BackgroundColor);
    
        var pen = new Pen(_mainFormConfig.Color);

        _mainFormConfig.Center = new Point(Width / 2, Height / 2);
        _mainFormConfig.StandartSize = new Size(10, 10);
        var words = _tagCloudProvider.GetPreparedWords();
            
        foreach (var word in words)
        {
            _graphics.DrawString(
                word.Value, 
                new Font(_mainFormConfig.FontFamily, word.Weight * _mainFormConfig.StandartSize.Width), 
                pen.Brush, 
                word.Position);
        }
        SaveImage(); 
    }

    private void SaveImage()
    {
        var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        _imageCreator.Save(this, Path.Combine(projectPath, "Images", "test.png"));
    }
}
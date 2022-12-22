using TagCloudContainer.Additions.Interfaces;

namespace TagCloudContainer;

public partial class TagCloud : Form
{
    private Graphics _graphics;
    private readonly ITagCloudProvider _tagCloudProvider;
    private readonly IImageCreator _imageCreator;
    private readonly ITagCloudFormConfig _tagCloudFormConfig;
    private readonly ITagCloudContainerConfig _tagCloudContainerConfig;
    
    public TagCloud(ITagCloudProvider tagCloudProvider, 
        ITagCloudContainerConfig tagCloudContainerConfig,
        ITagCloudFormConfig tagCloudFormConfig,
        IImageCreator imageCreator)
    {
        _tagCloudProvider = tagCloudProvider;
        _imageCreator = imageCreator;
        _tagCloudContainerConfig = tagCloudContainerConfig;
        _tagCloudFormConfig = tagCloudFormConfig;
        
        InitializeComponent();
        SetupWindow();
    }

    private void SetupWindow()
    {
        Text = "Tag Cloud Container";
        Size = _tagCloudFormConfig.FormSize;
    }

    public void ChangeSize(Size size)
    {
        Size = size;
    }

    private void Render(object sender, PaintEventArgs e)
    {
        _graphics = e.Graphics;
        _graphics.Clear(_tagCloudFormConfig.BackgroundColor);
    
        var pen = new Pen(_tagCloudFormConfig.Color);

        _tagCloudContainerConfig.Center = new Point(Width / 2, Height / 2);
        _tagCloudContainerConfig.StandartSize = new Size(10, 10);
        var words = _tagCloudProvider.GetPreparedWords();
        
        foreach (var word in words)
        {
            _graphics.DrawString(
                word.Value, 
                new Font(_tagCloudFormConfig.FontFamily, word.Weight * _tagCloudContainerConfig.StandartSize.Width), 
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
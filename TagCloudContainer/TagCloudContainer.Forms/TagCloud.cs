using TagCloudContainer.Core;
using TagCloudContainer.Core.Interfaces;
using TagCloudContainer.Core.Models;

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
        Size = _tagCloudFormConfig.ImageSize;
    }

    public void ChangeSize(Size size)
    {
        Size = size;
    }

    private void Render(object sender, PaintEventArgs e)
    {
        _graphics = e.Graphics;
        _graphics.Clear(_tagCloudFormConfig.BackgroundColor);
        
        _tagCloudContainerConfig.Center = new Point(Width / 2, Height / 2);
        _tagCloudContainerConfig.StandartSize = new Size(10, 10);
        var words = _tagCloudProvider.GetPreparedWords();

        if (!words.IsSuccess)
        {
            MessageBox.Show(words.Error, "Ошибка");
            return;
        }
        
        DrawWords(e, words);            
        SaveImage();
    }

    private void DrawWords(PaintEventArgs e, Result<List<Word>> words)
    {
        var pen = new Pen(_tagCloudFormConfig.Color);
        
        foreach (var word in words.GetValueOrThrow())
        {
            var font = new Font(_tagCloudFormConfig.FontFamily,
                word.Weight * _tagCloudContainerConfig.StandartSize.Width);
            _graphics.DrawString(word.Value, font, pen.Brush, word.Position);
            
            font.Dispose();
        }
        
        pen.Dispose();
    }

    private void SaveImage()
    {
        var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        _imageCreator.Save(this, Path.Combine(projectPath, "test.png"));
    }
}
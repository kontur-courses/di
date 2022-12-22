using TagCloudContainer.Additions;
using TagCloudContainer.Additions.Interfaces;

namespace TagCloudContainer.Forms;

public partial class Settings : Form 
{
    private readonly TagCloud _tagCloud;
    private readonly ITagCloudContainerConfig _tagCloudContainerConfig;
    private readonly ITagCloudFormConfig _tagCloudFormConfig;
    private readonly ITagCloudFormResult _tagCloudFormResult;
    private readonly IWordReaderConfig _wordReaderConfig;

    public static void Main() { } // Иначе, без Main, не компилилось. Не знал как сделать

    public Settings(
        TagCloud tagCloud, 
        ITagCloudContainerConfig tagCloudContainerConfig,
        ITagCloudFormConfig tagCloudFormConfig,
        IWordReaderConfig wordReaderConfig,
        ITagCloudFormResult tagCloudFormResult)
    {
        _tagCloud = tagCloud;
        _tagCloudContainerConfig = tagCloudContainerConfig;
        _tagCloudFormConfig = tagCloudFormConfig;
        _wordReaderConfig = wordReaderConfig;
        _tagCloudFormResult = tagCloudFormResult;
        
        InitializeComponent();
    }

    private void randomStart_Click(object sender, EventArgs e)
    {
        RunTagCloudForm(true);
    }
    
    private void biggerInCenter_Click(object sender, EventArgs e)
    {
        RunTagCloudForm(false);
    }

    private void AddConfigValues(bool random)
    {
        var parsedUserSelectedSizeValue = Sizes
            .Text
            .Split("x")
            .Select(i => int.Parse(i))
            .ToArray();
        var userSelectedSize = new Size(parsedUserSelectedSizeValue[0], parsedUserSelectedSizeValue[1]);
            
        _tagCloudFormConfig.Color = Additions.Models.Colors.Get(Colors.Text);
        _tagCloudFormConfig.BackgroundColor = Additions.Models.Colors.Get(BackgroundColors.Text);
        _tagCloudFormConfig.FontFamily = Fonts.Text;
        _tagCloudFormConfig.FormSize = userSelectedSize;
        
        _wordReaderConfig.SetFilePath("words.txt");
        _wordReaderConfig.SetExcludeWordsFilePath("boring_words.txt");
        
        _tagCloudContainerConfig.NearestToTheCenterPoints = new SortedList<float, Point>();
        _tagCloudContainerConfig.PutRectangles = new List<Rectangle>();
        _tagCloudContainerConfig.Random = random;
    }

    private void RunTagCloudForm(bool random)
    {
        AddConfigValues(random);
        ValidateUserParameters();
        
        _tagCloud.ChangeSize(_tagCloudFormConfig.FormSize);
        _tagCloud.ShowDialog(this);

        if (!_tagCloudFormResult.FormResult.IsSuccess)
            MessageBox.Show("Облако тегов не влезло на изображение заданного размера", "Ошибка");
    }

    private void ValidateUserParameters()
    {
        var validator = new Validator();
        
        var tagCloudContainerConfigResult = validator.Validate(_tagCloudContainerConfig);
        var tagCloudFormConfigResult= validator.Validate(_tagCloudFormConfig);
        var wordReaderConfigResult= validator.Validate(_wordReaderConfig);
        
        if (!tagCloudContainerConfigResult.IsSuccess)
            MessageBox.Show("Invalid container options: " + tagCloudContainerConfigResult.Error);
        if (!tagCloudFormConfigResult.IsSuccess)
            MessageBox.Show("Invalid form options: " + tagCloudFormConfigResult.Error);
        if (!wordReaderConfigResult.IsSuccess)
            MessageBox.Show("Invalid words reader options: " + wordReaderConfigResult.Error);
    }
}
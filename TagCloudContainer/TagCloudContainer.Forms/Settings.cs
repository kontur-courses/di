using TagCloudContainer.Core;
using TagCloudContainer.Core.Interfaces;
using TagCloudContainer.Core.Models;
using TagCloudContainer.Forms.Interfaces;

namespace TagCloudContainer.Forms;

public partial class Settings : Form 
{
    private readonly TagCloud _tagCloud;
    private readonly ITagCloudContainerConfig _tagCloudContainerConfig;
    private readonly ITagCloudFormConfig _tagCloudFormConfig;
    private readonly IConfigValidator<ITagCloudFormConfig> _tagCloudFormConfigValidator;
    private readonly IConfigValidator<ITagCloudContainerConfig> _tagCloudContainerConfigValidator;

    public Settings(
        TagCloud tagCloud, 
        ITagCloudContainerConfig tagCloudContainerConfig,
        ITagCloudFormConfig tagCloudFormConfig,
        IConfigValidator<ITagCloudFormConfig> tagCloudFormConfigValidator,
        IConfigValidator<ITagCloudContainerConfig> tagCloudContainerConfigValidator)
    {
        _tagCloud = tagCloud ?? throw new ArgumentNullException("Tag cloud can't be null");
        _tagCloudContainerConfig = 
            tagCloudContainerConfig ?? throw new ArgumentNullException("Tag cloud config can't be null");
        _tagCloudFormConfig = 
            tagCloudFormConfig ?? throw new ArgumentNullException("Tag cloud form config can't be null");
        _tagCloudFormConfigValidator = 
            tagCloudFormConfigValidator ?? throw new ArgumentNullException("Tag cloud config validator can't be null");
        _tagCloudContainerConfigValidator = 
            tagCloudContainerConfigValidator ?? throw new ArgumentNullException("Tag cloud form config validator can't be null");

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

    private void AddUserSelectedValues(bool random)
    {
        var parsedUserSelectedSizeValue = Sizes
            .Text
            .Split("x")
            .Select(i => int.Parse(i))
            .ToArray();
        var userSelectedSize = Screens.Sizes.First(size =>
            size.Width == parsedUserSelectedSizeValue[0] && size.Height == parsedUserSelectedSizeValue[1]);

        var colorResult = GetColorsByChoosedName(Colors.Text);
        var backgroundColorResult = GetColorsByChoosedName(BackgroundColors.Text);
        
        if (!colorResult.IsSuccess || !backgroundColorResult.IsSuccess)
            return;

        _tagCloudFormConfig.Color = colorResult.GetValueOrThrow();
        _tagCloudFormConfig.BackgroundColor = backgroundColorResult.GetValueOrThrow();
        _tagCloudFormConfig.FontFamily = Fonts.Text;
        _tagCloudFormConfig.ImageSize = userSelectedSize;
        
        _tagCloudContainerConfig.SetFilePath("words.txt");
        _tagCloudContainerConfig.SetExcludeWordsFilePath("boring_words.txt");
        
        _tagCloudContainerConfig.NearestToTheCenterPoints = new SortedList<float, Point>();
        _tagCloudContainerConfig.PutRectangles = new List<Rectangle>();
        _tagCloudContainerConfig.Random = random;
    }

    private void RunTagCloudForm(bool random)
    {
        AddUserSelectedValues(random);
        ValidateUserParameters();
        
        _tagCloud.ChangeSize(_tagCloudFormConfig.ImageSize);
        _tagCloud.ShowDialog(this);
    }

    private Result<Color> GetColorsByChoosedName(string colorName)
    {
        var colorResult = Core.Models.Colors.Get(colorName);
        colorResult.OnFail(error => MessageBox.Show(error, "Ошибка"));
        
        return colorResult;
    }

    private void ValidateUserParameters()
    {
        _tagCloudContainerConfigValidator
            .Validate(_tagCloudContainerConfig)
            .OnFail(error => MessageBox.Show("Invalid container options: " + error, "Ошибка"));
        _tagCloudFormConfigValidator
            .Validate(_tagCloudFormConfig)
            .OnFail(error => MessageBox.Show("Invalid form options: " + error, "Ошибка"));
    }
}
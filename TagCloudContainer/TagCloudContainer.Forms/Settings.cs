using TagCloudContainer.Core;
using TagCloudContainer.Core.Interfaces;
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
        ValidateConstructorArguments(tagCloud, tagCloudContainerConfig, tagCloudFormConfig, tagCloudFormConfigValidator, tagCloudContainerConfigValidator);
        
        _tagCloud = tagCloud;
        _tagCloudContainerConfig = tagCloudContainerConfig;
        _tagCloudFormConfig = tagCloudFormConfig;
        _tagCloudFormConfigValidator = tagCloudFormConfigValidator;
        _tagCloudContainerConfigValidator = tagCloudContainerConfigValidator;

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
        var userSelectedSize = new Size(parsedUserSelectedSizeValue[0], parsedUserSelectedSizeValue[1]);

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
        
        if (!colorResult.IsSuccess)
            MessageBox.Show(colorResult.Error, "Ошибка");
        return colorResult;
    }

    private void ValidateUserParameters()
    {
        var tagCloudContainerConfigResult = _tagCloudContainerConfigValidator.Validate(_tagCloudContainerConfig);
        var tagCloudFormConfigResult= _tagCloudFormConfigValidator.Validate(_tagCloudFormConfig);
        
        if (!tagCloudContainerConfigResult.IsSuccess)
            MessageBox.Show("Invalid container options: " + tagCloudContainerConfigResult.Error);
        if (!tagCloudFormConfigResult.IsSuccess)
            MessageBox.Show("Invalid form options: " + tagCloudFormConfigResult.Error);
    }

    private void ValidateConstructorArguments(
        TagCloud tagCloud, 
        ITagCloudContainerConfig tagCloudContainerConfig,
        ITagCloudFormConfig tagCloudFormConfig,
        IConfigValidator<ITagCloudFormConfig> tagCloudFormConfigValidator,
        IConfigValidator<ITagCloudContainerConfig> tagCloudContainerConfigValidator)
    {
        if (tagCloud == null)
            throw new ArgumentException("Tag cloud can't be null");
        if (tagCloudContainerConfig == null)
            throw new ArgumentException("Tag cloud config can't be null");
        if (tagCloudFormConfig == null)
            throw new ArgumentException("Tag cloud form config can't be null");
        if (tagCloudContainerConfigValidator == null)
            throw new ArgumentException("Tag cloud config validator can't be null");
        if (tagCloudFormConfigValidator == null)
            throw new ArgumentException("Tag cloud form config validator can't be null");
    }
}
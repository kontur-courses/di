namespace TagCloudContainer;

public partial class Settings : Form 
{
    private readonly TagCloudForm _tagCloudForm;
    private readonly IMainFormConfig _mainFormConfig;

    public Settings(TagCloudForm tagCloudForm, IMainFormConfig mainFormConfig)
    {
        _tagCloudForm = tagCloudForm;
        _mainFormConfig = mainFormConfig;
        
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
            
        _mainFormConfig.Color = TagCloudContainer.Colors.Get(Colors.Text);
        _mainFormConfig.BackgroundColor = TagCloudContainer.Colors.Get(BackgroundColors.Text);
        _mainFormConfig.FontFamily = Fonts.Text;
        _mainFormConfig.Random = random;
        _mainFormConfig.FileName = "words.txt";
        _mainFormConfig.ExcludeWordsFileName = "boring_words.txt";
        _mainFormConfig.NearestToTheCenterPoints = new SortedList<float, Point>();
        _mainFormConfig.PutRectangles = new List<Rectangle>();
        _mainFormConfig.FormSize = userSelectedSize;
        _tagCloudForm.ChangeSize(userSelectedSize);
    }

    private void RunTagCloudForm(bool random)
    {
        AddConfigValues(random);
        _tagCloudForm.ShowDialog(this);
    }
}
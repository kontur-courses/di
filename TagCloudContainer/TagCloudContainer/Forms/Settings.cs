namespace TagCloudContainer;

public partial class Settings : Form 
{
    private readonly TagCloudForm _tagCloudForm;

    public Settings(TagCloudForm tagCloudForm)
    {
        _tagCloudForm = tagCloudForm;
        
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
        MainFormConfig.Color = TagCloudContainer.Colors.Get(Colors.Text);
        MainFormConfig.BackgroundColor = TagCloudContainer.Colors.Get(BackgroundColors.Text);
        MainFormConfig.FontFamily = Fonts.Text;
        MainFormConfig.Random = random;
        MainFormConfig.FileName = "words.txt";
        MainFormConfig.ExcludeWordsFileName = "boring_words.txt";
        MainFormConfig.NearestToTheCenterPoints = new SortedList<float, Point>();
        MainFormConfig.PutRectangles = new List<Rectangle>();

        if (!MainFormConfig.FormSize.Equals(TagCloudContainer.Sizes.Get(Sizes.Text)))
        {
            _tagCloudForm.ChangeSize(TagCloudContainer.Sizes.Get(Sizes.Text));
            MainFormConfig.FormSize = TagCloudContainer.Sizes.Get(Sizes.Text);
        }
    }

    private void RunTagCloudForm(bool random)
    {
        AddConfigValues(random);
        _tagCloudForm.ShowDialog(this);
    }
}
namespace TagCloudContainer;

public partial class Settings : Form 
{
    private MainFormConfig _mainFormConfig;
    
    public Settings()
    {
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

    private void AddConfigValues()
    {
        _mainFormConfig = new MainFormConfig()
        {
            Color = TagCloudContainer.Colors.Get(Colors.Text), 
            BackgroundColor = TagCloudContainer.Colors.Get(BackgroundColors.Text), 
            FontFamily = Fonts.Text,
            FormSize = TagCloudContainer.Sizes.Get(Sizes.Text)
        };
    }

    private void RunTagCloudForm(bool Random)
    {
        AddConfigValues();
        _mainFormConfig.Random = Random;
        TagCloudForm tagCloudForm = new TagCloudForm(_mainFormConfig);
        tagCloudForm.Show();
    }
}
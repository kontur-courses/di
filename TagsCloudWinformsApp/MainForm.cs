using Autofac;
using TagsCloudContainer;
using TagsCloudContainer.Colorers;
using TagsCloudVisualization;

namespace TagsCloudWinformsApp;

public partial class MainForm : Form
{
    private readonly string ImageFilesFilter = "png (*.png)|*.png|jpeg (*.jpeg)|*.jpeg|jpg (*.jpg)|*.jpg";
    private readonly InputFilesReader inputFilesReader = new();
    private readonly SettingsHandler settingsHandler = new();
    private readonly string TextFilesFilter = "text files|*.txt";

    public MainForm()
    {
        InitializeComponent();

        wordColoring_comboBox.SelectedIndex = 0;
        layout_comboBox.SelectedIndex = 0;
        UpdateSettingsView();
    }

    private void fontButton_Click(object sender, EventArgs e)
    {
        fontDialog.Font = settingsHandler.LocalSettings.Font;
        var dialogResult = fontDialog.ShowDialog(this);
        if (dialogResult == DialogResult.OK)
        {
            settingsHandler.LocalSettings.Font = fontDialog.Font;
            UpdateSettingsView();
        }
    }

    private void UpdateSettingsView()
    {
        fontButton.Text = settingsHandler.LocalSettings.Font.Size + Environment.NewLine +
                          settingsHandler.LocalSettings.Font.Name;
        backgroundColor_button.BackColor = settingsHandler.LocalSettings.BackgroundColor;
        fontColor_button.BackColor = settingsHandler.LocalSettings.FontColor;
        growthPercent_numeric.Value = (decimal) ((settingsHandler.LocalSettings.FrequencyRatio - 1) * 100);
        imageWidth_numeric.Value = settingsHandler.LocalSettings.ImageSize.Width;
        imageHeight_numeric.Value = settingsHandler.LocalSettings.ImageSize.Height;

        fontColor_button.Visible = wordColoring_comboBox.SelectedIndex != 1;
        fontColor_label.Visible = wordColoring_comboBox.SelectedIndex != 1;

        generate_button.Enabled = inputFilesReader.InputPath != null;
        removeFilter_button.Enabled = inputFilesReader.FilterPath != null;
        saveImage_button.Enabled = mainPictureBox.Image != null;
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
    }

    private void backgroundColor_button_Click(object sender, EventArgs e)
    {
        colorDialog.Color = settingsHandler.LocalSettings.BackgroundColor;
        var dr = colorDialog.ShowDialog(this);
        if (dr == DialogResult.OK)
        {
            settingsHandler.LocalSettings.BackgroundColor = colorDialog.Color;
            UpdateSettingsView();
        }
    }

    private void fontColor_button_Click(object sender, EventArgs e)
    {
        colorDialog.Color = settingsHandler.LocalSettings.FontColor;
        var dr = colorDialog.ShowDialog(this);
        if (dr == DialogResult.OK)
        {
            settingsHandler.LocalSettings.FontColor = colorDialog.Color;
            UpdateSettingsView();
        }
    }

    private void layout_comboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateSettingsView();
    }

    private void growthPercent_numeric_ValueChanged(object sender, EventArgs e)
    {
        settingsHandler.LocalSettings.FrequencyRatio = (float) (1 + growthPercent_numeric.Value / 100);
        UpdateSettingsView();
    }

    private void imageWidth_numeric_ValueChanged(object sender, EventArgs e)
    {
        settingsHandler.LocalSettings.ImageSize.Width = (int) imageWidth_numeric.Value;
        UpdateSettingsView();
    }

    private void imageHeight_numeric_ValueChanged(object sender, EventArgs e)
    {
        settingsHandler.LocalSettings.ImageSize.Height = (int) imageHeight_numeric.Value;
        UpdateSettingsView();
    }

    private void generate_button_Click(object sender, EventArgs e)
    {
        try
        {
            var container = BuildContainer();
            var imgDrawer = container.Resolve<IImageDrawer>();
            var bitmap = imgDrawer.DrawImage();
            mainPictureBox.Image = bitmap;
            UpdateSettingsView();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void inputFile_button_Click(object sender, EventArgs e)
    {
        var openFileDialog1 = new OpenFileDialog();
        openFileDialog1.Filter = TextFilesFilter;
        var result = openFileDialog1.ShowDialog();
        if (result == DialogResult.OK)
        {
            inputFilesReader.InputPath = openFileDialog1.FileName;
            UpdateSettingsView();
        }
    }

    private void removeFilter_button_Click(object sender, EventArgs e)
    {
        inputFilesReader.FilterPath = null;
        removeFilter_button.Enabled = false;
    }

    private void chooseFilterFile_button_Click(object sender, EventArgs e)
    {
        var openFileDialog1 = new OpenFileDialog();
        openFileDialog1.Filter = TextFilesFilter;
        var result = openFileDialog1.ShowDialog();
        if (result == DialogResult.OK)
        {
            inputFilesReader.FilterPath = openFileDialog1.FileName;
            UpdateSettingsView();
        }
    }

    private void saveImage_button_Click(object sender, EventArgs e)
    {
        var dialog = new SaveFileDialog();
        dialog.Filter = ImageFilesFilter;
        if (dialog.ShowDialog() == DialogResult.OK) mainPictureBox.Image.Save(dialog.FileName);
    }

    private void wordColoring_comboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateSettingsView();
    }

    private IContainer BuildContainer()
    {
        var builder = new ContainerBuilder();
        builder.RegisterType<DefaultImageDrawer>().As<IImageDrawer>();
        builder.RegisterType<DefaultRectanglesDistributor>().As<IRectanglesDistributor>();
        builder.RegisterType<WordsHandlerWithFilter>().As<IWordsHandler>();
        builder.RegisterInstance(settingsHandler).As<ISettingsProvider>();
        builder.RegisterInstance(inputFilesReader).As<IWordSequenceProvider>();
        builder.RegisterInstance(inputFilesReader).As<IWordFilterProvider>();

        SetColorProvider[wordColoring_comboBox.SelectedIndex](builder);
        SetLayouter[layout_comboBox.SelectedIndex](builder);
        
        return builder.Build();
    }

    private Action<ContainerBuilder>[] SetColorProvider = 
    {
        (builder) => builder.RegisterType<SimpleColorProvider>().As<IColorProvider>(),
        (builder) => builder.RegisterType<RandomColorProvider>().As<IColorProvider>(),
        (builder) => builder.RegisterType<TransparencyOverFrequencyColorProvider>().As<IColorProvider>()
    };
    
    private Action<ContainerBuilder>[] SetLayouter = 
    {
        (builder) => builder.RegisterInstance(new SpiralCloudLayouter(Point.Empty)).As<ICloudLayouter>(),
        (builder) => builder.RegisterInstance(new BlockCloudLayouter(Point.Empty)).As<ICloudLayouter>()
    };

}
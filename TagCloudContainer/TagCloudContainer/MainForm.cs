namespace TagCloudContainer;

public partial class MainForm : Form
{
    private Graphics _graphics;

    private Color _backgroundColor;
    private Color _penColor;
    
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }
    
    private MainForm()
    {
        InitializeComponent();
        SetupApplication();
    }

    private void SetupApplication()
    {
        SetupWindow();
        SetupColors();
    }

    private void SetupWindow()
    {
        Text = "Tag Cloud Container";
        TopMost = true;
        WindowState = FormWindowState.Maximized;
    }

    private void SetupColors()
    {
        _backgroundColor = Color.FromArgb(47, 47, 42);
        _penColor = Color.White;
    }

    private void Render(object sender, PaintEventArgs e)
    {
        _graphics = e.Graphics;
        _graphics.Clear(_backgroundColor);
    
        var pen = new Pen(_penColor);

        var center = new Point(Width / 2, Height / 2);
        var standartSize = new Size(10, 10);
        var tagCloudProvider = new TagCloudProvider("words.txt", center, standartSize);
        var words = tagCloudProvider.GetPreparedWords();

        foreach (var word in words)
        {
            _graphics.DrawString(
                word.Value, 
                new Font("Arial", word.Weight * standartSize.Width), 
                pen.Brush, 
                word.Position);
        }
        
        var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        ImageCreator.Save(this, Path.Combine(projectPath, "Images", "test.png"));
    }
}
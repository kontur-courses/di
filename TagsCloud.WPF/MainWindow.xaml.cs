using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using TagsCloud.CloudLayouter;
using TagsCloud.CloudLayouter.Implementation;
using TagsCloud.FileReader;
using TagsCloud.WordHandler;
using TagsCloud.WordHandler.Implementation;
using TagsCloud.WPF.PictureSaver;
using Brushes = System.Windows.Media.Brushes;
using Color = System.Windows.Media.Color;
using FontStyle = System.Windows.FontStyle;
using Point = System.Drawing.Point;

namespace TagsCloud.WPF;

public partial class MainWindow
{
    private readonly Random random = new();
    private SolidColorBrush customColor;
    private int rectanglesCount;
    private readonly DispatcherTimer timer = new();

    private readonly string[] words;
    private int wordPointer;

    private ICloudLayouter<Rectangle>? circularCloud;
    private List<UIElement> uiElements = new();

    private readonly IWordHandler[] wordHandlers;
    private readonly IPictureSaver pictureSaver;
    private readonly RecurringWordsHandler? recurringWordsHandler;

    private const int DefaultFontSize = 10;
    private FontStyle fontStyle;

    public MainWindow(IWordHandler[] wordHandlers, IFileReader reader, IPictureSaver pictureSaver, PathShell path)
    {
        InitializeComponent();
        UpdateCircularCloudFromTextBox();
        this.wordHandlers = wordHandlers;
        this.pictureSaver = pictureSaver;
        fontStyle = FontStyles.Normal;
        customColor = new SolidColorBrush(Colors.Beige);
        recurringWordsHandler = GetRecurringWordsHandler(wordHandlers);
        words = ProcessWords(reader.Read(path.path));
        rectanglesCount = words.Length;
        MyCanvas.Focus();
        timer.Interval = TimeSpan.FromSeconds(0);
        timer.Start();

        RectanglesCountTb.Text = rectanglesCount.ToString();
    }

    private static RecurringWordsHandler? GetRecurringWordsHandler(IEnumerable<IWordHandler> wordHandlers)
    {
        foreach (var handler in wordHandlers)
            if (handler is RecurringWordsHandler recurringWordsHandler)
                return recurringWordsHandler;

        return null;
    }

    private string[] ProcessWords(string[] getWordsFromTxt) => 
        wordHandlers.Aggregate(getWordsFromTxt, (current, handler) => handler.ProcessWords(current));

    private void DrawRectangle(object? sender, EventArgs e)
    {
        if (circularCloud is null || ((bool) PrintRectangles.IsChecked! && uiElements.Count >= rectanglesCount) ||
            ((bool) !PrintRectangles.IsChecked! && uiElements.Count >= words.Length)) 
            return;

        customColor = (bool) IsRandomColors.IsChecked! ? GetRandomColor() : customColor;
        Rectangle rectangleFromCloud;
        UIElement figure;
        if (PrintRectangles.IsChecked is not null && (bool) PrintRectangles.IsChecked)
        {
            rectangleFromCloud = circularCloud.PutNextRectangle(SizeCreator.GetRandomRectangleSize(25, 50));
            figure = CreateRectangle(rectangleFromCloud);
        }
        else
        {
            figure = CreateLabel(words[wordPointer++]);
            rectangleFromCloud =
                circularCloud.PutNextRectangle(SizeCreator.GetLabelSize((Label) figure));
        }

        AddFigure(figure, rectangleFromCloud);
    }

    private void AddFigure(UIElement figure, Rectangle rectangleFromCloud)
    {
        Canvas.SetLeft(figure, rectangleFromCloud.X);
        Canvas.SetTop(figure, rectangleFromCloud.Y);
        uiElements.Add(figure);

        MyCanvas.Children.Add(figure);
    }

    private Label CreateLabel(string text)
    {
        var color = new SolidColorBrush(customColor.Color);
        var wordsCount = recurringWordsHandler is not null ? recurringWordsHandler.WordCount[text] : 0;
        var mostFrequentWordCount =
            recurringWordsHandler?.GetMostFrequentPair ?? 1;
        color.Opacity = (bool) OftenWordBrighter.IsChecked! ? wordsCount / (double) mostFrequentWordCount : 1;
        return new Label
        {
            Foreground = color,
            Background = Brushes.Black,
            FontSize = DefaultFontSize + wordsCount,
            Content = text,
            FontStyle = fontStyle,
        };
    }

    private System.Windows.Shapes.Rectangle CreateRectangle(Rectangle rectangleFromCloud)
    {
        return new System.Windows.Shapes.Rectangle
        {
            Width = rectangleFromCloud.Width,
            Height = rectangleFromCloud.Height,
            Fill = customColor,
            StrokeThickness = 2,
            Stroke = Brushes.LightBlue,
        };
    }

    private SolidColorBrush GetRandomColor() => new(Color.FromRgb((byte) random.Next(1, 255),
        (byte) random.Next(1, 255),
        (byte) random.Next(1, 255)));

    private void Start(object sender, RoutedEventArgs e)
    {
        if (string.CompareOrdinal((string?) StartButton.Header, "Start") == 0)
        {
            StartButton.Header = "Stop";
            timer.Tick += DrawRectangle;
        }
        else
        {
            StartButton.Header = "Start";
            timer.Tick -= DrawRectangle;
        }
    }

    private void Clear(object sender, RoutedEventArgs e)
    {
        UpdateCircularCloudFromTextBox();
        uiElements = new List<UIElement>();
        MyCanvas.Children.Clear();
        wordPointer = 0;

        if (ColorPicker?.SelectedColor is not null)
            customColor = new SolidColorBrush(ColorPicker.SelectedColor.Value);
    }

    private void UpdateCircularCloudFromTextBox()
    {
        var isNumber = int.TryParse(TbSteps.Text, out var steps);
        if (!isNumber)
            steps = 1;

        circularCloud =
            new CircularCloudLayouter(new Point((int) (MyWindow.Width / 2),
                (int) (MyWindow.Height / 2)), steps);
    }

    private void UpdateInterval(object sender, TextChangedEventArgs e)
    {
        var isNumber = double.TryParse(TbSpeed.Text, out var speed);
        if (!isNumber)
            speed = 0.1;

        timer.Interval = TimeSpan.FromSeconds(speed);
        uiElements = new List<UIElement>();
        MyCanvas.Children.Clear();
    }

    private void StepSliderChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (StepSlider is not null && TbSteps is not null)
            TbSteps.Text = StepSlider.Value.ToString(CultureInfo.InvariantCulture).Split('.')[0];
    }

    private void SavePicture(object sender, RoutedEventArgs e) => pictureSaver.SavePicture(sender, e, this, MyCanvas);

    private void UpdateRectanglesCount(object sender, TextChangedEventArgs e)
    {
        var tryParse = int.TryParse(RectanglesCountTb.Text, out var count);
        if (!tryParse) 
            return;
            
        rectanglesCount = (bool) PrintRectangles.IsChecked! ? count : words.Length;
        RectanglesCountTb.Text = (bool) PrintRectangles.IsChecked! ? count.ToString() : words.Length.ToString();
    }

    private void ChangeFontStyle(object sender, RoutedEventArgs e)
    {
        var menuItem = sender as MenuItem;
        var header = menuItem!.Name;

        fontStyle = header switch
        {
            "Italic" => FontStyles.Italic,
            "Oblique" => FontStyles.Oblique,
            _ => FontStyles.Normal
        };
            
        Clear(sender, e);
    }
}
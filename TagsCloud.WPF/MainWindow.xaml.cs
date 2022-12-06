using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using TagsCloud.CloudLayouter;
using TagsCloud.CloudLayouter.Implementation;
using Brush = System.Windows.Media.Brush;
using Brushes = System.Windows.Media.Brushes;
using Color = System.Windows.Media.Color;
using FontFamily = System.Windows.Media.FontFamily;
using Point = System.Drawing.Point;

namespace TagsCloud.WPF
{
    public partial class MainWindow : Window
    {
        private readonly Random random = new();
        private Brush customColor = Brushes.Beige;
        private int rectanglesCount = DefaultRectanglesCount;
        private readonly DispatcherTimer timer = new();
        
        private const double DefaultDpi = 96.0;
        private const int DefaultRectanglesCount = 100;

        private readonly string[] words;
        private const string PathToWords = "../../../Words.txt";

        private ICloudLayouter<Rectangle>? circularCloud;
        private List<UIElement> uiElements = new();
        
        public MainWindow()
        {
            InitializeComponent();
            UpdateCircularCloudFromTextBox();
            words = GetWordsFromTxt(PathToWords);
            MyCanvas.Focus();
            timer.Interval = TimeSpan.FromSeconds(0);
            timer.Start();

            RectanglesCountTb.Text = rectanglesCount.ToString();
        }

        private static string[] GetWordsFromTxt(string path) => File.ReadAllLines(path);

        private void DrawRectangle(object? sender, EventArgs e)
        {
            if (circularCloud is null || uiElements.Count >= rectanglesCount)
                return;

            customColor = IsRandomColors.IsChecked is not null && (bool) IsRandomColors.IsChecked
                ? GetRandomColor()
                : customColor;
            var currentWord = words[random.Next(words.Length)];

            Rectangle rectangleFromCloud;
            UIElement figure;
            if (PrintRectangles.IsChecked is not null && (bool) PrintRectangles.IsChecked)
            {
                rectangleFromCloud = circularCloud.PutNextRectangle(SizeCreator.GetRandomRectangleSize(25, 50));
                figure = CreateRectangle(rectangleFromCloud);
            }
            else
            {
                figure = CreateLabel(currentWord);
                rectangleFromCloud =
                    circularCloud.PutNextRectangle(SizeCreator.GetLabelSize((Label) figure));
            }
            
            Canvas.SetLeft(figure, rectangleFromCloud.X);
            Canvas.SetTop(figure, rectangleFromCloud.Y);
            uiElements.Add(figure);
            
            MyCanvas.Children.Add(figure);
        }

        private Label CreateLabel(string text)
        {
            return new Label
            {
                Foreground = customColor,
                Background = Brushes.Black,
                FontSize = random.Next(10, 15),
                Content = text,
                FontFamily = new FontFamily("Verdana")
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

            if (ColorPicker?.SelectedColor is not null)
                customColor = new SolidColorBrush((Color) ColorPicker.SelectedColor);
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

        private void SavePicture(object sender, RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.SaveFileDialog
            {
                FileName = "Image", 
                DefaultExt = ".png",
                Filter = "PNG File (.png)|*.png"
            };

            var result = dlg.ShowDialog();

            if (result != true) 
                return;
            var filename = dlg.FileName;
            SaveCanvasToFile(this, MyCanvas, DefaultDpi, filename);
        }
        
        private void ColorPickChanged(object sender, RoutedPropertyChangedEventArgs<Color?> routedPropertyChangedEventArgs)
        {
            if (ColorPicker.SelectedColor is not null)
                customColor = new SolidColorBrush((Color) ColorPicker.SelectedColor);
        }

        private static void SaveCanvasToFile(FrameworkElement window, UIElement canvas, double dpi, string filename)
        {
            var size = new System.Windows.Size(window.Width, window.Height);
            canvas.Measure(size);
 
            var rtb = new RenderTargetBitmap(
                (int)window.Width,
                (int)window.Height,
                dpi,
                dpi,
                PixelFormats.Pbgra32
            );
            rtb.Render(canvas);
 
            SaveRtbAsPngbmp(rtb, filename);
        }
 
        private static void SaveRtbAsPngbmp(BitmapSource bmp, string filename)
        {
            var enc = new PngBitmapEncoder();
            enc.Frames.Add(BitmapFrame.Create(bmp));

            using var stm = File.Create(filename);
            enc.Save(stm);
        }

        private void UpdateRectanglesCount(object sender, TextChangedEventArgs e)
        {
            var tryParse = int.TryParse(RectanglesCountTb.Text, out var count);
            if (tryParse)
                rectanglesCount = count;
        }
    }
}
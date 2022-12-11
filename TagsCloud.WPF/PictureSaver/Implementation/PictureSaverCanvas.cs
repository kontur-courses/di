using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TagsCloud.WPF.PictureSaver.Implementation;

public class PictureSaverCanvas : IPictureSaver
{
    private const double DefaultDpi = 96d;
    private readonly UIElement myCanvas;
    private readonly FrameworkElement window;

    public PictureSaverCanvas(FrameworkElement window, UIElement myCanvas)
    {
        this.myCanvas = myCanvas;
        this.window = window;
    }

    public void SavePicture(object sender, RoutedEventArgs e)
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
        SaveCanvasToFile(window, myCanvas, DefaultDpi, filename);
    }

    private static void SaveCanvasToFile(FrameworkElement window, UIElement canvas, double dpi, string filename)
    {
        var size = new Size(window.Width, window.Height);
        canvas.Measure(size);

        var rtb = new RenderTargetBitmap(
            (int) window.Width,
            (int) window.Height,
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
}
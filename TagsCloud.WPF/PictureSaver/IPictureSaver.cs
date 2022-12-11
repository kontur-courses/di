using System.Windows;

namespace TagsCloud.WPF.PictureSaver;

public interface IPictureSaver
{
    public void SavePicture(object sender, RoutedEventArgs e, FrameworkElement window, UIElement canvas);
}
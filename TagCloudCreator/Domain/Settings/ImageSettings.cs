using System.ComponentModel;
using System.Drawing;

namespace TagCloudCreator.Domain.Settings;

public class ImageSettings
{
    private Size _size;

    [DisplayName("Width")]
    [Description("Tag cloud image width")]
    public int Width
    {
        get => _size.Width;
        set
        {
            if (value <= 0)
                throw new ArgumentException($"{nameof(Width)} should be positive!");
            _size.Width = value;
            OnChange?.Invoke(_size);
        }
    }

    [DisplayName("Height")]
    [Description("Tag cloud image height")]
    public int Height
    {
        get => _size.Height;
        set
        {
            if (value <= 0)
                throw new ArgumentException($"{nameof(Height)} should be positive!");
            _size.Height = value;
            OnChange?.Invoke(_size);
        }
    }

    public event Action<Size>? OnChange;
}
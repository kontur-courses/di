namespace TagCloudApp.Domain;

public class ImageSettings
{
    private int _width = 800;
    private int _height = 600;

    public int Width
    {
        get => _width;
        set
        {
            if (value <= 0)
                throw new ArgumentException($"{nameof(Width)} should be positive!");
            _width = value;
            OnChange?.Invoke(new Size(Width, Height));
        }
    }

    public int Height
    {
        get => _height;
        set
        {
            if (value <= 0)
                throw new ArgumentException($"{nameof(Height)} should be positive!");
            _height = value;
            OnChange?.Invoke(new Size(Width, Height));
        }
    }

    public event Action<Size>? OnChange;
}
namespace TagCloud.Domain.Settings;

public class LayoutSettings
{
    private Size dimensions = new Size(800, 800);

    public Size Dimensions
    {
        get => dimensions;
        set
        {
            if (value.Width < 100 || value.Height < 100)
                throw new ArgumentException("Размеры должны быть не менне 100x100");
            
            dimensions = value;
        }
    }
    
    public bool BigToCenter { get; set; }
}
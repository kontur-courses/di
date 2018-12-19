using System;
using System.Drawing;
using System.Threading;


namespace TagsCloudContainer
{
    public class DrawSettings<T>
    {
        private Size _imageSize;
        private string _fontName;
        private Func<IItemToDraw<T>, Color> _itemPainter;
        private string _filePath;
        private ImageFileFormat _imageFileFormat;

        public DrawSettings()
        {
            InitDefaultSettings();
        }

        public DrawSettings(string filePath)
        {
            InitDefaultSettings();
            _filePath = filePath;
        }

        private void InitDefaultSettings()
        {
            _imageSize = new Size(1000, 500);
            _fontName = "Times new Roman";
            _itemPainter = i => TakeRandomColor();
            _filePath = "result";
            _imageFileFormat = ImageFileFormat.Png;
        }

        public string GetFullFileName()
        {
            return _filePath + "." + _imageFileFormat.ToString().ToLower();
        }

        public void SetImageSize(Size imageSize)
        {
            if (imageSize.Width <= 0 || imageSize.Height <= 0)
                throw new ArgumentException("both image size parameters should be positive");

            _imageSize = imageSize;
        }

        public void SetFontName(string fontName)
        {
            _fontName = fontName;
        }

        public void SetItemPainter(Func<IItemToDraw<T>, Color> itemPainter)
        {
            _itemPainter = itemPainter;
        }

        public void SetFilePath(string filePath)
        {
            _filePath = filePath;
        }

        public void SetImageFileFormat(ImageFileFormat imageFileFormat)
        {
            _imageFileFormat = imageFileFormat;
        }

        public Size GetImageSize()
        {
            return _imageSize;
        }

        public string GetFontName()
        {
            return _fontName;
        }

        public SolidBrush GetBrush(IItemToDraw<T> item)
        {
            return new SolidBrush(_itemPainter(item));
        }

        public string GetFilePath()
        {
            return _filePath;
        }

        public ImageFileFormat GetImageFileFormat()
        {
            return _imageFileFormat;
        }

        private static Color TakeRandomColor()
        {
            var rnd = new Random();
            Thread.Sleep(20);
            var color = Color.FromArgb(rnd.Next());

            switch (rnd.Next() % 4)
            {
                case 0:
                    color = Color.Green;
                    break;
                case 1:
                    color = Color.Red;
                    break;
                case 2:
                    color = Color.Gold;
                    break;
                case 3:
                    color = Color.Aqua;
                    break;
                default:
                    color = Color.BlueViolet;
                    break;
            }

            return color;
        }
    }
}

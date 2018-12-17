using System;
using System.Drawing;


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
            _imageSize = new Size(1000, 1000);
            _fontName = "Times new Roman";
            _itemPainter = i => Color.Gold;
            _filePath = "result";
            _imageFileFormat = ImageFileFormat.Png;
        }

        public string GetFileFullName()
        {
            return _filePath + "." + _imageFileFormat.ToString().ToLower();
        }

        public void SetImageSize(Size imageSize)
        {
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
    }
}

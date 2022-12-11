using System;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TagCloudGui.Infrastructure;
using TagCloudGui.Infrastructure.Common;

namespace TagCloudGui.Actions
{
    public class ImageSaveAction : IUiAction
    {
        private readonly IImageHolder imageHolder;

        public ImageSaveAction(IImageHolder imageHolder)
        {
            this.imageHolder = imageHolder;
        }
        public MenuCategory Category => MenuCategory.File;
        public string Name => "Сохранить изображение";
        public string Description => "Сохранить изображение в заданный файл";

        public void Perform()
        {
            using FileDialog fd = new SaveFileDialog();
            fd.Filter = string.Join('|', ImageCodecInfo.GetImageEncoders().Select(codec => 
                $"{GetFileNameFrom(codec)} file ({codec.FilenameExtension})|{codec.FilenameExtension}"));
            if (fd.ShowDialog() == DialogResult.OK)
            {
                imageHolder.SaveImage(fd.FileName, GetImageFormatBy(Path.GetExtension(fd.FileName)));
            }
        }

        private ImageFormat GetImageFormatBy(string extenstion)
        {
            return extenstion.ToLower() switch
            {
                ".bmp" => ImageFormat.Bmp,
                ".jpg" => ImageFormat.Jpeg,
                ".tif" => ImageFormat.Tiff,
                ".gif" => ImageFormat.Gif,
                ".png" => ImageFormat.Png,
                _ => throw new FormatException("Не удалось определить формат изображения")
            };
        }

        private string GetFileNameFrom(ImageCodecInfo imageCodecInfo) =>
            imageCodecInfo.CodecName.AsSpan(9).ToString().Split()[0];
    }
}

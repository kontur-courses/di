using System.Drawing;

namespace TagsCloudContainer.FileSavers
{
    /// <summary>
    ///  Нужен чтобы можно было реализовать сохранение разных форматов
    /// </summary>
    public interface IFileSaver
    {
        public string Path { get; }
        public Bitmap Canvas { get; set; }
        public void SaveCanvas();
    }
}
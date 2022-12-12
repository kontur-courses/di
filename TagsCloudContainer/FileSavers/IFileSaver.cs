using System.Drawing;

namespace TagsCloudContainer.FileSavers
{
    /// <summary>
    ///  Нужен чтобы можно было реализовать сохранение разных форматов
    /// </summary>
    public interface IFileSaver
    {
        public void SaveCanvas(string pathToSave, Bitmap canvas);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Services
{
    public interface ITagCloudService
    {
        public void SetSettings<TSettings>(TSettings property);
        public string SetFilePath(string filePath);
        public void DrawImage(string sourceFilePath, string customBoringFilePath, int imgWidth, int imgHeight);
        public void SaveImage(string filePath);
    }
}

using System.Drawing;

namespace TagCloud
{
    public class ClientConfig
    {
        public ClientConfig()
        {
            ToCreateNewImage = false;
            ToExit = false;
            ImageToSave = null;
        }

        public bool ToExit { get; set; }
        public bool ToCreateNewImage { get; set; }
        public Bitmap ImageToSave { get; set; }
    }
}
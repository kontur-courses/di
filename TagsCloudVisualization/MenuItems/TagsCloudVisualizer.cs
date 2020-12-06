using System;
using System.Drawing;
using System.Windows.Forms;
using TagsCloudLayouters.Contracts;
using TagsCloudVisualization.App;
using TagsCloudVisualization.Contracts;

namespace TagsCloudVisualization.MenuItems
{
    public class TagsCloudVisualizer : IMenuItem
    {
        public string MenuAffiliation => "Draw";
        public string ItemName => "Cloud from file...";
        private ITagsCloudCreator CloudCreator { get; }
        private IWordsReader Reader { get; }
        private TagsCloudPictureHolder PictureHolder { get; }

        public TagsCloudVisualizer(
            ITagsCloudCreator cloudCreator,
            ITagsCloudLayouter layouter, 
            IWordsReader reader, 
            TagsCloudPictureHolder pictureHolder)
        {
            layouter.Recreate();
            CloudCreator = cloudCreator;
            Reader = reader;
            PictureHolder = pictureHolder;
        }
        
        public DialogResult Execute() => CreateImage();

        private DialogResult CreateImage()
        {
            var fileName = GetUserFile();
            if (string.IsNullOrEmpty(fileName))
                return DialogResult.Abort;

            var words = Reader.GetAllData(fileName);
            var tags = CloudCreator.CreateTagsCloud(words);
            var graphics = Graphics.FromImage(PictureHolder.Image);
            
            graphics.FillRectangle(Brushes.White, new Rectangle(0, 0, PictureHolder.Image.Width, PictureHolder.Image.Height));

            foreach (var tag in tags)
            {
                graphics.DrawString(tag.Word, tag.Font, new SolidBrush(tag.Color), tag.Frame);
                PictureHolder.UpdateUi();
            }

            return DialogResult.OK;
        }

        private static string GetUserFile()
        {
            var fileInput = new OpenFileDialog
            {
                Title = "Source for tags cloud",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                Filter = "Text files(*.txt)|*.txt"
            };
            fileInput.ShowDialog();
            return fileInput.FileName;
        }
    }
}
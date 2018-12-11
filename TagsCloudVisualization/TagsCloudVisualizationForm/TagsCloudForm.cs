using System;
using System.Windows.Forms;
using TagsCloudVisualization;
using Autofac;

namespace TagsCloudVisualizationForm
{
    public partial class TagsCloudForm : Form
    {
        public TagsCloudForm()
        {
            InitializeComponent();
        }

        private void GenerateBtn_Click(object sender, EventArgs e)
        {
            var options = new Options
            {
                Color = colorTextBox.Text,
                PointGenerator = formPointGeneratorTextBox.Text,
                FilePath = inputFilePathTextBox.Text,
                FontName = "arial",
                ImageSize = "800x800",
                OutFormat = "Jpeg"
            };
            var container = TagsCloudVisualizationRoot.GetCompositionRoot();
            container.Resolve<TagsCloudApp>().Run(options, container);
            MessageBox.Show("Картинка сохранена", "Шапка", MessageBoxButtons.OK);
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

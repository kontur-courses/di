using System.Windows.Forms;

namespace TagsCloudVisualization
{
    public class FormApplicationRunner : IApplicationRunner
    {
        private MainForm form;

        public FormApplicationRunner(MainForm form)
        {
            this.form = form;
        }

        public void Run(string[] args)
        {
            Application.Run(form);
        }
    }
}

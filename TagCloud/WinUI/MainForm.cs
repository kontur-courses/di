using System.Drawing;
using System.Windows.Forms;
using TagsCloudVisualisation.Output;

namespace WinUI
{
    public partial class MainForm : Form
    {
        public MainForm(IConfigEntry<string>[] requesters)
        {
            InitializeComponent();
            for (var i = 0; i < requesters.Length; i++)
            {
                var textBox = new TextBox {Text = "input" + i}; //TODO description
                panel1.Controls.Add(textBox);
                textBox.Location += new SizeF(0, textBox.Height * i * 1.5f).ToSize();
            }
        }
    }
}
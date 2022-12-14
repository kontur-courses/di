using System;
using System.Linq;
using System.Windows.Forms;
using TagsCloudContainer;

namespace TagsCloudGUI
{
    public partial class ExludedWordsForm : Form
    {
        private IWordFilter wordsFilter;
        public ExludedWordsForm(IWordFilter wordsFilter)
        {
            this.wordsFilter = wordsFilter;
            InitializeComponent();
            listBox1.Items.Clear();
            listBox1.Items.AddRange(wordsFilter.WordsToFilter.ToArray());
        }

        private void but_add_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(textBox1.Text);
            wordsFilter.WordsToFilter.Add(textBox1.Text);
            listBox1.Items.Clear();
            listBox1.Items.AddRange(wordsFilter.WordsToFilter.ToArray());
        }

        private void but_del_Click(object sender, EventArgs e)
        {
            var item = (string)listBox1.SelectedItem;
            wordsFilter.WordsToFilter.Remove(item);
            listBox1.Items.Clear();
            listBox1.Items.AddRange(wordsFilter.WordsToFilter.ToArray());
        }
    }
}
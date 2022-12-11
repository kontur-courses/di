using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TagsCloudContainer
{
    public partial class ExludedWordsForm : Form
    {
        private WordsFilter wordsFilter;
        public ExludedWordsForm(WordsFilter wordsFilter)
        {
            this.wordsFilter = wordsFilter;
            InitializeComponent();
            listBox1.Items.Clear();
            listBox1.Items.AddRange(wordsFilter.GetExcludedWords().ToArray());
        }

        private void but_add_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(textBox1.Text);
            wordsFilter.AddExludedWord(textBox1.Text);
            listBox1.Items.Clear();
            listBox1.Items.AddRange(wordsFilter.GetExcludedWords().ToArray());
        }

        private void but_del_Click(object sender, EventArgs e)
        {
            var item = (string)listBox1.SelectedItem;
            wordsFilter.DeleteExludedWord(item);
            listBox1.Items.Clear();
            listBox1.Items.AddRange(wordsFilter.GetExcludedWords().ToArray());
        }
    }
}
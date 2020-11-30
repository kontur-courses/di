using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TagsCloud.App;

namespace TagsCloud.Infrastructure
{
    public partial class SetExcludingWordsForm : Form
    {
        private readonly HashSet<string> excludedWords;
        private readonly TextBox textBox;
        private readonly IWordsConverter converter;

        public SetExcludingWordsForm(HashSet<string> excludedWords, IWordsConverter converter)
        {
            var okButton = new Button
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Dock = DockStyle.Bottom
            };
            textBox = new TextBox
            {
                Multiline = true,
                Dock = DockStyle.Fill,
                ScrollBars = ScrollBars.Vertical,
                Text = string.Join(Environment.NewLine, excludedWords) + Environment.NewLine
            };
            this.excludedWords = excludedWords;
            okButton.Click += OnOkButtonClick;
            Controls.Add(okButton);
            Controls.Add(textBox);
        }

        private void OnOkButtonClick(object sender, EventArgs e)
        {
            foreach (var word in textBox.Text.Split('\n'))
                excludedWords.Add(converter.ConvertWord(word));
        }
    }
}

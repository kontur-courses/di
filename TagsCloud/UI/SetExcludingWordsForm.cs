using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TagsCloud.App;

namespace TagsCloud.UI
{
    public partial class SetExcludingWordsForm : Form
    {
        private readonly HashSet<string> excludedWords;
        private readonly TextBox textBox;
        private readonly IWordNormalizer converter;

        public SetExcludingWordsForm(HashSet<string> excludedWords, IWordNormalizer converter)
        {
            var okButton = new Button
            {
                Text = @"OK",
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
            this.converter = converter;
            Controls.Add(okButton);
            Controls.Add(textBox);
        }

        private void OnOkButtonClick(object sender, EventArgs e)
        {
            foreach (var word in textBox.Text.Split('\n'))
                excludedWords.Add(converter.NormalizeWord(word));
        }
    }
}

using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace TagCloud.Interfaces.GUI.Forms
{
    public class ApplicationSettingsForm : Form
    {
        private ApplicationSettings settings;
        private WordSelectorForm wordSelectorForm;

        public ApplicationSettingsForm(ApplicationSettings settings, WordSelectorForm wordSelectorForm)
        {
            this.settings = settings;
            this.wordSelectorForm = wordSelectorForm;
            InitializeForm();
        }

        private void InitializeForm()
        {
            Controls.Add(new Button
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Dock = DockStyle.Bottom,
            });

            var labelAnalyzer = new Label { Location = new Point(15, 15), Text = "Анализатор текста" };
            var wordSeletorBtn = new Button
            {
                Text = "Настройки выборки слов",
                Dock = DockStyle.Bottom
            };
            wordSeletorBtn.Click += (a,b) => wordSelectorForm.ShowDialog();

            var textAnalyzers = new ComboBox
            {
                Location = labelAnalyzer.Location + new Size(labelAnalyzer.Size.Width, 0)
            };

            var analyzerNames = settings.TextAnalyzers
                .Select(w => w.GetType().GetCustomAttribute<NameAttribute>()?.Name ?? "UnknownName")
                .ToArray();

            textAnalyzers.Items.AddRange(analyzerNames);

            Controls.Add(labelAnalyzer);
            Controls.Add(textAnalyzers);
            Controls.Add(wordSeletorBtn);

            textAnalyzers.SelectedIndexChanged += OnUpdateTextAnalyzer;
        }

        private void OnUpdateTextAnalyzer(object sender, EventArgs e)
        {
            settings.CurrentTextAnalyzer = settings.TextAnalyzers[((ComboBox) sender).SelectedIndex];
        }
    }
}

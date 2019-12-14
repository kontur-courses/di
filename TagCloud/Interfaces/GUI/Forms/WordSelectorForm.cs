using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagCloud.WordsPreprocessing;
using TagCloud.WordsPreprocessing.WordsSelector;

namespace TagCloud.Interfaces.GUI.Forms
{
    public class WordSelectorForm : Form
    {
        private ListBox ignoredWordsBox;
        private WordSelectorSettings wordSelectorSettings;
        private TextBox textBox;

        public WordSelectorForm(WordSelectorSettings wordSelectorSettings)
        {
            this.wordSelectorSettings = wordSelectorSettings;
            InitializeForm();
        }

        private void InitializeForm()
        {
            AutoSize = true;
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true
            };
            Controls.Add(layout);

            // Список игнорируемых Частей речи
            var speechPartsLabel = new Label
            {
                Text = "Игнорируемые части речи",
                Dock = DockStyle.Fill,
                Font = new Font(FontFamily.GenericSansSerif, 14)
            };
            var checkedListBox = new CheckedListBox
            {
                Dock = DockStyle.Fill,
            };
            checkedListBox.Items.AddRange(Enum.GetValues(typeof(SpeechPart)).Cast<object>().ToArray());
            checkedListBox.ItemCheck += OnItemCheckSpeechParts;

            // Установка минимального размера слова участвующего в выборке
            var minLength = new Label
            {
                Text = "Минимальный размер слова",
                Dock = DockStyle.Fill
            };
            var lengthUpDown = new NumericUpDown
            {
                Minimum = 0,
                Maximum = 30,
                Value = 3,
                Dock = DockStyle.Right
            };
            lengthUpDown.ValueChanged += OnLengthChanged;

            //Список с игнорируемыми словами
            var ignoredWordsLabel = new Label
            {
                Text = "Игнорируемые слова"
            };
            ignoredWordsBox = new ListBox();
            ignoredWordsBox.Items.AddRange(wordSelectorSettings.IgnoredWords.ToArray());
            
            //Кнопка убрать выделенное слово
            var deleteWordBtn = new Button
            {
                Text = "Убрать выделенный элемент",
                Dock = DockStyle.Fill
            };
            deleteWordBtn.Click += OnClickDeleteBtn;

            // Поле для ввода слова на добавление
            textBox = new TextBox();
            var addBtn = new Button
            {
                Text = "+",
                Dock = DockStyle.Right
            };
            addBtn.Click += OnAddBtnClick;


            layout.ColumnCount = 2;

            layout.Controls.Add(speechPartsLabel, 1, 0);
            layout.SetColumnSpan(speechPartsLabel, 2);
            layout.Controls.Add(checkedListBox, 1, 1);
            layout.SetColumnSpan(checkedListBox, 2);

            layout.Controls.Add(minLength, 1, 2);
            layout.Controls.Add(lengthUpDown, 2, 2);

            layout.Controls.Add(ignoredWordsLabel, 1,3);
            layout.SetColumnSpan(ignoredWordsLabel, 2);
            layout.Controls.Add(ignoredWordsBox, 1, 4);
            layout.SetColumnSpan(ignoredWordsBox, 2);
            layout.Controls.Add(deleteWordBtn, 1, 5);
            layout.SetColumnSpan(deleteWordBtn, 2);

            layout.Controls.Add(textBox, 1, 6);
            layout.Controls.Add(addBtn, 2, 6);
        }

        private void OnItemCheckSpeechParts(object sender, EventArgs e)
        {
            var item = (SpeechPart)((CheckedListBox) sender).SelectedItem;
            if (((ItemCheckEventArgs) e).NewValue == CheckState.Checked)
                wordSelectorSettings.AddIgnoredSpeechPart(item);
            else
                wordSelectorSettings.RemoveIgnoredSpeechPart(item);
        }

        private void OnLengthChanged(object sender, EventArgs e)
        {
            wordSelectorSettings.IgnoreWordsWithLengthLessThan = (int) ((NumericUpDown) sender).Value;
        }

        private void OnClickDeleteBtn(object sender, EventArgs e)
        {
            wordSelectorSettings.RemoveIgnoredWord((string) ignoredWordsBox.SelectedItem);
            ignoredWordsBox.Items.Remove(ignoredWordsBox.SelectedItem);
        }

        private void OnAddBtnClick(object sender, EventArgs e)
        {
            wordSelectorSettings.AddIgnoredWord(textBox.Text);
            ignoredWordsBox.Items.Add(textBox.Text);
        }
    }
}

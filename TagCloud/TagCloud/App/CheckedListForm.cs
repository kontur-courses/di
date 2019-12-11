using System;
using System.Windows.Forms;

namespace TagCloud
{
    public static class CheckedListForm
    {
        public static CheckedListForm<T> For<T>(T[] items) where T : ICheckable
        {
            return new CheckedListForm<T>(items);
        }
    }

    public class CheckedListForm<T> : Form
    {
        private CheckedListBox checkedListBox;
        private readonly T[] items;

        public CheckedListForm(T[] items)
        {
            this.items = items;
            var okButton = new Button
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Dock = DockStyle.Bottom,
            };
            okButton.Click += new EventHandler(OkButton_Click);
            Controls.Add(okButton);

            checkedListBox = new CheckedListBox
            {
                Dock = DockStyle.Fill,
                CheckOnClick = true,
            };

            foreach (var item in items)
                checkedListBox.Items.Add((item as ICheckable).Name);

            for (int i = 0; i < checkedListBox.Items.Count; i++)
            {
                var item = checkedListBox.Items[i];
                checkedListBox.SetItemChecked(i, (items[i] as ICheckable).IsChecked);
            }
            Controls.Add(checkedListBox);
            AcceptButton = okButton;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox.Items.Count; i++)
            {
                var item = checkedListBox.Items[i];
                (items[i] as ICheckable).IsChecked = checkedListBox.GetItemChecked(i);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Text = "CheckedList";
        }
    }
}

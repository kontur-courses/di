using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TagCloud
{
    public static class CheckedListForm
    {
        public static CheckedListForm<T> For<T>(IItemList<T> list)
        {
            return new CheckedListForm<T>(list);
        }
    }

    public class CheckedListForm<T> : Form
    {
        private CheckedListBox checkedListBox;
        private IItemList<T> list;

        public CheckedListForm(IItemList<T> items)
        {
            list = items;
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

            foreach (var item in items.AllItems)
                checkedListBox.Items.Add(item);

            for (int i = 0; i < checkedListBox.Items.Count; i++)
            {
                var item = checkedListBox.Items[i];
                if (items.SelectedItems.Contains((T)item))
                    checkedListBox.SetItemChecked(i, true);
            }
            Controls.Add(checkedListBox);
            AcceptButton = okButton;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            var selectedItems = new HashSet<T>();
            foreach (var item in checkedListBox.CheckedItems)
                selectedItems.Add((T)item);
            list.SelectedItems = selectedItems;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Text = "CheckedList";
        }
    }
}

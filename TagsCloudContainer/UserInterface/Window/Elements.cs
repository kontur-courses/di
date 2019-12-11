using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MetroFramework.Controls;

namespace TagsCloudContainer.UserInterface.Window
{
    public class Elements
    {
        public static MetroTextBox GetLabel(string text) => new MetroTextBox
        {
            Text = text,
            Enabled = false
        };

        public static ComboBox TypeBox(IEnumerable<string> items, string selectedItem, string name = "")
        {
            var box = new MetroComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Name = name
            };
            box.Items.AddRange(items.OfType<object>().ToArray());
            box.SelectedIndex = box.Items.IndexOf(selectedItem);
            return box;
        }
    }
}
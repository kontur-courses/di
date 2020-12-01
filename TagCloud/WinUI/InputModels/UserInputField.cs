using System.Windows.Forms;

namespace WinUI.InputModels
{
    public class UserInputField
    {
        private TextBox? textBox;

        public string Description { get; set; }

        public string Value
        {
            get => textBox?.Text ?? string.Empty;
            set
            {
                if (textBox != null)
                    textBox.Text = value;
            }
        }

        public void LinkTo(TextBox newTextBox) => textBox = newTextBox;
    }
}
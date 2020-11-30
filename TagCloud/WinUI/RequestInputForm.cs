using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinUI
{
    public sealed class RequestInputForm : Form
    {
        private readonly TextBox textBox;
        public string Value => textBox.Text;

        public RequestInputForm(string description)
        {
            var lines = SplitTextBy("Enter " + description, 30).ToArray();

            Text = lines[0];
            MaximizeBox = false;
            MinimizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;

            var label = new Label
            {
                Text = string.Join(Environment.NewLine, lines),
                Location = new Point(10, 10),
                AutoSize = true
            };
            Controls.Add(label);

            textBox = new TextBox
            {
                Text = string.Empty,
                Location = new Point(10, label.Height + 20),
                Size = new Size(label.Width, 20)
            };

            textBox.KeyDown += (sender, args) =>
            {
                if ((args.KeyCode & Keys.Enter) == Keys.Enter || (args.KeyCode & Keys.Escape) == Keys.Escape)
                    Close();
            };

            var screenRect = RectangleToScreen(ClientRectangle);
            var titleHeight = screenRect.Top - Top;
            Size = new Size(label.Size.Width + 40, textBox.Location.Y + textBox.Size.Height + 20 + titleHeight);
            Controls.Add(textBox);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (e.KeyChar == '\n')
                Close();
            base.OnKeyPress(e);
        }

        public static string RequestInput(string description)
        {
            var form = new RequestInputForm(description);
            form.ShowDialog();
            return form.Value;
        }

        private static IEnumerable<string> SplitTextBy(string source, int symbolsCount)
        {
            var current = new StringBuilder();
            for (var i = 0; i < source.Length; i++)
            {
                if (current.Length >= symbolsCount)
                {
                    yield return current.ToString();
                    current.Clear();
                }

                current.Append(source[i]);
            }

            if (current.Length != 0)
                yield return current.ToString();
        }
    }
}
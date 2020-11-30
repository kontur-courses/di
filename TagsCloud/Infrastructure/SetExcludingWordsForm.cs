using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TagsCloud.Infrastructure
{
    public partial class SetExcludingWordsForm : Form
    {
        public SetExcludingWordsForm()
        {
            var okButton = new Button
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Dock = DockStyle.Bottom
            };
            Controls.Add(okButton);
            Controls.Add(new TextBox
            {
                Multiline = true,
                Dock = DockStyle.Fill,
                ScrollBars = ScrollBars.Vertical
            });
        }
    }
}

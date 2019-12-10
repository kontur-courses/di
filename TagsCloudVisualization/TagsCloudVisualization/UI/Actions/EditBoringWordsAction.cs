using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Windows.Forms;

namespace TagsCloudVisualization.Actions
{
    public class EditBoringWordsAction : IUiAction
    {
        public string Name { get; }

        public EditBoringWordsAction()
        {
            Name = "Edit boring words";
        }

        public void Perform(object sender, EventArgs e)
        {
            var collection = new List<string>()
            {
                "Dog",
                "Ape",
                "Cat"
            };
            var editor = new UITypeEditor();
            var form = new Form();
            form.Controls.Add(editor);
        }
    }
}
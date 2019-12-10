using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Drawing.Design;

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
            //editor.

        }
    }
}
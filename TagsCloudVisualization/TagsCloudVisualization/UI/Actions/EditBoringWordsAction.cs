using System;
using TagsCloudVisualization.Services;

namespace TagsCloudVisualization.UI.Actions
{
    public class EditBoringWordsAction : IUiAction
    {
        private readonly IBoringWordsProvider boringWordsProvider;

        public string Name { get; }

        public EditBoringWordsAction(IBoringWordsProvider boringWordsProvider)
        {
            this.boringWordsProvider = boringWordsProvider;
            Name = "Edit boring words";
        }

        public void Perform(object sender, EventArgs e)
        {
            var editor = new BoringWordsEditor(boringWordsProvider);
            editor.ShowDialog();
        }
    }
}
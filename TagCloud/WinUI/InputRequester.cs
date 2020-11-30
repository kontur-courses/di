using System;
using System.Windows.Forms;
using TagsCloudVisualisation.Output;

namespace WinUI
{
    public class InputRequester<T> : IConfigEntry<T>
    {
        public T GetValue(string description)
        {
            throw new NotImplementedException(); //TODO implement
        }
    }
}
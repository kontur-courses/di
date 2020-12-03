using System;

namespace WinUI.InputModels
{
    public class UserInputBoolean
    {
        public UserInputBoolean(string description, bool value)
        {
            Description = description;
            Value = value;
        }

        public string Description { get; }
        public bool Value { get; private set; }

        public void SetValue(bool newValue)
        {
            if (Value == newValue)
                return;
            Value = newValue;
            ValueChanged?.Invoke(Value);
        }

        public event Action<bool>? ValueChanged;
    }
}
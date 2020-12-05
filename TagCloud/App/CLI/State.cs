using System;

namespace TagCloud.App.CLI
{
    public class State
    {
        public readonly string Name;
        public State(string name, bool isFinal = false) : this(isFinal)
        {
            this.Name = name;
        }
        
        public State(bool isFinal)
        {
            IsFinal = isFinal;
        }
        
        public delegate void ConsoleInputEventHandler(object sender, ConsoleInputEventArgs args);
        public delegate void StateShowEventHandler(State sender, EventArgs args);

        public event ConsoleInputEventHandler Act;
        public event StateShowEventHandler Show;
        public bool IsFinal { get; }

        public void OnAct(ConsoleInputEventArgs e)
        {
            Act?.Invoke(this, e);
        }

        public virtual void OnShow()
        {
            Show?.Invoke(this, EventArgs.Empty);
        }
    }
}
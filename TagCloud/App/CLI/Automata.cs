using System.Collections.Generic;
using System.Linq;

namespace TagCloud.App.CLI
{
    public class Automata
    {
        private readonly Dictionary<State, IEnumerable<Transition>> transitionsMap;
        private State currentState;

        public Automata()
        {
            transitionsMap = new Dictionary<State, IEnumerable<Transition>>();
        }

        public Automata(State initialState, IEnumerable<Transition> transitions) : this()
        {
            currentState = initialState;
            foreach (var transition in transitions)
                AddTransition(transition);
        }

        public void Add(Transition transition)
        {
            AddTransition(transition);
        }

        private void AddTransition(Transition transition)
        {
            if (!transitionsMap.TryGetValue(transition.Origin, out var allTransitions))
                allTransitions = new List<Transition>();
            transitionsMap[transition.Origin] = allTransitions.Append(transition);
        }

        public void Move(string input)
        {
            var isTransfer = TryTransfer(input, out var newState);
            currentState.OnAct(new ConsoleInputEventArgs(input, isTransfer));
            if (isTransfer)
                currentState = newState;
        }

        private bool TryTransfer(string input, out State newState)
        {
            newState = null;
            if (!transitionsMap.TryGetValue(currentState, out var transitions))
                return false;
            var possibleTransition = transitions.FirstOrDefault(transition => transition.DoesTransfer(input));
            if (possibleTransition == null)
                return false;
            newState = possibleTransition.Destination;
            return true;
        }

        public bool Show()
        {
            currentState.OnShow();
            return !currentState.IsFinal;
        }

        public void Init(State mainState)
        {
            currentState = mainState;
        }
    }
}
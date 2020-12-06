using System.Text.RegularExpressions;

namespace TagCloud.App.CLI
{
    public class Transition
    {
        public State Destination;
        public State Origin;
        public Regex Regex;

        public Transition(State origin, string pattern, State destination)
        {
            Origin = origin;
            Destination = destination;
            Regex = new Regex(pattern, RegexOptions.Compiled);
        }

        public bool DoesTransfer(string input)
        {
            var match = Regex.Match(input);
            return match.Success;
        }
    }
}
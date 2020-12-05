using System;
using System.Collections.Generic;
using TagCloud.Infrastructure.Graphics;
using TagCloud.Infrastructure.Settings;
using TagCloud.Infrastructure.Settings.UISettingsManagers;
using TagCloud.Infrastructure.Text;

namespace TagCloud.App.CLI
{
    public class TagCloudLayouterCli : IApp
    {
        private readonly IPainter<string> painter;
        private readonly IEnumerable<ISettingsManager> settingsManagers;
        private readonly IReader<string> reader;
        private readonly Func<Settings> settingsFactory;
        private readonly WordAnalyzer<string> wordAnalyzer;

        public TagCloudLayouterCli(
            IReader<string> reader,
            WordAnalyzer<string> wordAnalyzer,
            Func<Settings> settingsFactory, 
            IPainter<string> painter,
            IEnumerable<ISettingsManager> settingsManagers)
        {
            this.reader = reader;
            this.wordAnalyzer = wordAnalyzer;
            this.settingsFactory = settingsFactory;
            this.painter = painter;
            this.settingsManagers = settingsManagers;
        }

        public void Run()
        {
            var automata = new Automata();
            
            var mainState = new State("Main");
            mainState.Show += DisplayState;
            mainState.Act += OnMainInput;
            var helpState = new State("Help");
            helpState.Show += DisplayState;
            helpState.Show += OnHelp;
            var exitState = new State(true);
            exitState.Show += OnExit;
            var aboutState = new State("About");
            aboutState.Show += OnAbout;
            

            automata.Init(mainState);
            automata.Add(new Transition(mainState, "help", helpState));
            automata.Add(new Transition(helpState, "quit", mainState));
            automata.Add(new Transition(mainState, "exit", exitState));
            automata.Add(new Transition(helpState, "about", aboutState));
            automata.Add(new Transition(aboutState, ".*", helpState));
            
            settingsFactory().Import(Program.GetDefaultSettings());
            Console.WriteLine("Default settings imported");
            // var tokens = reader.ReadTokens();
            // var analyzedTokens = wordAnalyzer.Analyze(tokens);
            // Console.WriteLine("Tokens analyzed");
            // using var image = painter.GetImage(analyzedTokens);
            // Console.WriteLine("Layout ready");
            // var imagePath = settingsFactory().ImagePath;
            // Console.WriteLine($"Saving into {Path.GetFullPath(imagePath)}");
            // image.Save(imagePath);
            // Console.WriteLine("Image saved");
            
            while (automata.Show())
            {
                Console.Write("> ");
                var inp = Console.ReadLine();
                Console.Clear();
                automata.Move(inp);
            }
        }

        private void OnAbout(State sender, EventArgs args)
        {
            Console.WriteLine("I\nAM\nRUS\nLAND");
        }

        private void OnHelp(object sender, EventArgs args)
        {
            Console.WriteLine("Available commands");
            Console.WriteLine("\thelp");
            Console.WriteLine("\texit");
            Console.WriteLine("\tquit");
            Console.WriteLine("\tabout");
            Console.WriteLine("\tsettings");
        }

        private void OnExit(State state, EventArgs eventArgs)
        {
            Console.WriteLine("Bye bye");
        }

        private void DisplayState(State sender, EventArgs args)
        {
            Console.WriteLine($"$ {sender.Name}:");
        }

        private void OnMainInput(object sender, ConsoleInputEventArgs args)
        {
            Console.WriteLine($"Type help for help");
        }
    }
}
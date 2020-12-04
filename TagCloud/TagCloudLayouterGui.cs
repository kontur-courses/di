using Gtk;
using System;

namespace TagCloud
{
    class TagCloudLayouterGui : IApp
    {
        static void conwrite(object obj, EventArgs e)
        {
            Console.WriteLine("Button clicked!");
        }

        static void delete_window(object obj, DeleteEventArgs e)
        {
            Application.Quit();
        }

        public void Run()
        {
            Application.Init();
            Window window = new Window("Test Application");
            Button button = new Button("Write to console");
            window.DeleteEvent += delete_window;
            button.Clicked += new EventHandler(conwrite);
            window.Add(button);
            window.ShowAll();
            Application.Run();
        }
    }
}
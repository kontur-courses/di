using System;
using System.Collections.Generic;
using System.IO;
using Gdk;
using Gtk;
using TagCloud.Infrastructure.Graphics;
using TagCloud.Infrastructure.Settings.UISettingsManagers;
using TagCloud.Infrastructure.Text;
using Settings = TagCloud.Infrastructure.Settings.Settings;
using Window = Gtk.Window;

namespace TagCloud.App.GUI
{
    internal class TagCloudLayouterGui : IApp
    {
        private readonly IPainter<string> painter;


        private readonly IReader<string> reader;
        private readonly Func<Settings> settingsFactory;
        private readonly WordAnalyzer<string> wordAnalyzer;
        private static StatusIcon icon;
        private IEnumerable<ISettingsManager> settingsManagers;

        public TagCloudLayouterGui(IReader<string> reader, WordAnalyzer<string> wordAnalyzer,
            Func<Settings> settingsFactory, IPainter<string> painter, IEnumerable<ISettingsManager> settingsManagers)
        {
            this.reader = reader;
            this.wordAnalyzer = wordAnalyzer;
            this.settingsFactory = settingsFactory;
            this.painter = painter;
            this.settingsManagers = settingsManagers;


            Application.Init();
            
            var window = new Window("Tag Cloud Layouter");
            window.DeleteEvent += Close;

            icon = new StatusIcon(new Gdk.Pixbuf("drawing.bmp"));
            icon.Visible = true;
            icon.PopupMenu += OnPopup;
            icon.Activate += delegate {
                window.Visible = !window.Visible;
            };
            
            window.Resize(200,200);
            window.SetDefaultSize(250, 100);
            window.SetPosition(WindowPosition.Center);
                
            var runButton = new Button("Generate");
            runButton.Clicked += OnGenerateButtonClicked;
            
            var settingsButton = new Button("Settings");
            settingsButton.Clicked += OnSettingsButtonClicked;

            var box = new HBox();
            window.Add(box);
            var arrow = new Arrow(ArrowType.Right, ShadowType.Out);
            box.PackStart(settingsButton, false, true, 10);
            box.PackStart(arrow, false, true, 10);
            box.PackStart(runButton, false, true, 10);
            // box.PackStart(new Image("drawing.bmp"), false, true, 10);
            
            window.BorderWidth = 30;
            window.Resizable = true;
            window.SetDefaultSize(100, 100);

            window.ShowAll();
        }

        private void OnSettingsButtonClicked(object sender, EventArgs args)
        {
            var wind = new Window("Settings");
            wind.BorderWidth = 30;
            wind.SetPosition(WindowPosition.Mouse);
            wind.Resizable = true;
            var box = new VBox();
            foreach (var manager in settingsManagers)
            {
                box.Add(GetWidget(manager));
                box.PackStart(new VSeparator(), false, false, 10);
            }

            
            var okBox = new HBox();
            okBox.PackStart(new Arrow(ArrowType.Right, ShadowType.Out), true, true, 10);
            var okButton = new Button("ok");
            okBox.PackStart(okButton, false, false, 0);
            box.PackStart(okBox, true, true, 0);

            wind.Add(box);
            wind.ShowAll();
        }

        private Widget GetWidget(ISettingsManager manager)
        {
            const int padding = 10; 
            var settings = new HBox();
            settings.PackStart(new Label(manager.Help), false, false, padding);
            settings.PackStart(new Label(manager.Title), false, false, padding);
            settings.PackStart(new VSeparator(), false, false, padding);
            var table = new TextTagTable();
            var buffer = new TextBuffer(table);
            var textBox = new TextView(buffer);
            settings.PackStart(textBox, true, true, padding);
            textBox.Shown += (o, args) =>
            {
                buffer.Text = manager.Get();
            };
            textBox.FocusOutEvent += (sender, args) =>
            {
                // todo show some info about wrong input
                var isSuccess = manager.TrySet(buffer.Text);
                buffer.Text = manager.Get();
            };
            return settings;
        }

        private void OnGenerateButtonClicked(object sender, EventArgs args)
        {
            var dialog = new AboutDialog();
            dialog.SetPosition(WindowPosition.Center);
            var img = new Pixbuf("drawing.bmp");
            dialog.Resizable = true;
            dialog.SetDefaultSize(500, 500);
            dialog.Show();
            var allocation = dialog.Allocation;
            var desired_width = allocation.Width;
            dialog.Logo = img.ScaleSimple(desired_width, desired_width, InterpType.Bilinear);
        }

        static void OnPopup (object sender, EventArgs args)
        {
            Menu pMenu = new Menu();
            MenuItem menuQuit = new MenuItem ("Выход");
            pMenu.Add(menuQuit);
            menuQuit.Activated += delegate { Application.Quit(); };
            pMenu.ShowAll();
            pMenu.Popup();
        }

        public void Run()
        {
            settingsFactory().Import(Program.GetDefaultSettings());
            Console.WriteLine("Default settings imported");
            
            Application.Run();
        }

        private void Click(object obj, EventArgs e)
        {
            var tokens = reader.ReadTokens();
            var analyzedTokens = wordAnalyzer.Analyze(tokens);
            Console.WriteLine("Tokens analyzed");
            using var image = painter.GetImage(analyzedTokens);
            Console.WriteLine("Layout ready");
            var imagePath = settingsFactory().ImagePath;
            Console.WriteLine($"Saving into {Path.GetFullPath(imagePath)}");
            image.Save(imagePath);
            Console.WriteLine("Image saved");
        }

        private void Close(object obj, DeleteEventArgs e)
        {
            Console.WriteLine("Closed!");
            Application.Quit();
        }
    }
}
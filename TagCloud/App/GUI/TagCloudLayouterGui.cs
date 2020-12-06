using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Gdk;
using Gtk;
using TagCloud.Infrastructure.Graphics;
using TagCloud.Infrastructure.Settings.UISettingsManagers;
using TagCloud.Infrastructure.Text;
using Color = System.Drawing.Color;
using Image = System.Drawing.Image;
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

            window.Resize(200,200);
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

            window.BorderWidth = 30;
            window.Resizable = true;
            window.SetDefaultSize(100, 100);

            window.ShowAll();
        }

        private void OnSettingsButtonClicked(object sender, EventArgs args)
        {
            var window = new Window("Settings");
            window.BorderWidth = 30;
            window.SetPosition(WindowPosition.Mouse);
            window.Resizable = true;
            var box = new VBox();
            foreach (var manager in settingsManagers)
            {
                box.Add(GetWidget(manager));
                box.PackStart(new VSeparator(), false, false, 10);
            }

            
            var okBox = new HBox();
            okBox.PackStart(new Arrow(ArrowType.Right, ShadowType.Out), true, true, 10);
            var okButton = new Button("ok");
            okButton.ButtonPressEvent += (o, eventArgs) =>
            {
                window.Close();
            };
            okBox.PackStart(okButton, false, false, 0);
            box.PackStart(okBox, true, true, 0);

            window.Add(box);
            window.ShowAll();
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

        private static Stream ToStream(Image image, ImageFormat format) {
            var stream = new System.IO.MemoryStream();
            image.Save(stream, format);
            stream.Position = 0;
            return stream;
        }
        
        private void OnGenerateButtonClicked(object sender, EventArgs args)
        {
            var window = new Window("Settings");
            window.BorderWidth = 30;
            window.SetPosition(WindowPosition.Mouse);
            window.Resizable = true;
            var box = new VBox();


            var image = GenerateImage();
            var stream = ToStream(image, ImageFormat.Bmp);
            var buf = new Pixbuf(stream);
            
            buf = buf.ScaleSimple(500, 500, InterpType.Bilinear);
            var img = new Gtk.Image(buf);
            box.PackStart(img, false, false, 0);
            
            var okBox = new HBox();
            okBox.PackStart(new Arrow(ArrowType.None, ShadowType.None), true, true, 10);
            var closeButton = new Button("discard");
            closeButton.Pressed += (o, eventArgs) =>
            {
                image.Dispose();
                window.Close();
            };
            var saveButton = new Button("save");
            saveButton.Pressed += (o, eventArgs) =>
            {
                var imagePath = settingsFactory().ImagePath;
                Console.WriteLine($"Saving into {Path.GetFullPath(imagePath)}");
                image.Save(imagePath);
                image.Dispose();
            };
            okBox.PackStart(closeButton, false, false, 0);
            okBox.PackStart(saveButton, false, false, 0);
            box.PackStart(okBox, true, true, 0);

            window.Add(box);
            window.ShowAll();
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

        private Image GenerateImage()
        {
            var tokens = reader.ReadTokens();
            var analyzedTokens = wordAnalyzer.Analyze(tokens);
            return painter.GetImage(analyzedTokens);
        }

        private void Close(object obj, DeleteEventArgs e)
        {
            Console.WriteLine("Closed!");
            Application.Quit();
        }
    }
}
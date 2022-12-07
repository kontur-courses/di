using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TagsCloud.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private App()
        {
            InitializeComponent();
        }
 
        [STAThread]
        static void Main()
        {
            var app = new App();
            var window = new MainWindow();
            app.Run(window);
        } 
    }
}
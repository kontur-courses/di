using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using TagsCloud;
using TagsCloud.Decorators;
using TagsCloud.FileParsers;
using TagsCloud.ImageSavers;
using TagsCloud.Layouters;
using TagsCloud.WordsFiltering;

namespace TagsCloud_wpf
{
    public partial class MainWindow : Window
    {
        private string inputFileName = string.Empty;
        private readonly IFileParser[] parsers;
        private readonly IFilter[] filters;
        private readonly ITagsCloudLayouter[] layouters;
        private ITagsCloudLayouter selectedLayouter;
        private readonly ITagsCloudDecorator[] decorators;
        private ITagsCloudDecorator selectedDecorator;
        private readonly IImageSaver[] imageSavers;
        private TagsCloudGenerator tagsCloud;

        private readonly Regex digitsRegex = new Regex("[^0-9]+");

        public MainWindow(IFileParser[] parsers, 
                          IFilter[] filters, 
                          ITagsCloudLayouter[] layouters, 
                          ITagsCloudDecorator[] decorators, 
                          IImageSaver[] imageSavers)
        {
            InitializeComponent();

            this.parsers = parsers;
            this.filters = filters;
            this.layouters = layouters;
            this.decorators = decorators;
            this.imageSavers = imageSavers;

            PrepareFiltersMenu();
            PrepareLayoutersMenu();
            PrepareDecoratorsMenu();
            UpdateImageControl();
        }

        private void MenuItemOpenFile_Click(object sender, RoutedEventArgs e)
        {
            var filenameFilters = string.Join(";", parsers.Select(parser =>
                string.Join(";", parser.FileExtensions.Select(ext => $"*{ext}"))));
            filenameFilters = $"Text files|{filenameFilters}";

            var dlg = new OpenFileDialog
            {
                Filter = filenameFilters
            };
            if (dlg.ShowDialog(this) != true) { return; }

            inputFileName = dlg.FileName;
        }

        private void PrepareFiltersMenu()
        {
            var filtersStackPanel = new StackPanel();
            foreach (var filter in filters.OrderBy(f => f.GetType().Name))
            {
                var groupBox = new GroupBox { Header = filter.GetType().Name };
                filtersStackPanel.Children.Add(groupBox);
                var propsStackPanel = new StackPanel();
                groupBox.Content = propsStackPanel;
                FillPropertiesPanel(filter, propsStackPanel);
            }

            FiltersSettingsGroupBox.Content = filtersStackPanel;
        }

        private void PrepareLayoutersMenu()
        {
            ComboBoxLayouters.ItemsSource = layouters.OrderBy(l => l.GetType().Name).Select(layouter =>
                new KeyValuePair<ITagsCloudLayouter, string>(layouter, layouter.GetType().Name));
            ComboBoxLayouters.DisplayMemberPath = "Value";
            ComboBoxLayouters.SelectionChanged += (sender, e) =>
            {
                if (sender is ComboBox comboBox && comboBox.SelectedValue is KeyValuePair<ITagsCloudLayouter, string> kv)
                    selectedLayouter = kv.Key;
                if (selectedLayouter == null) return;
                FillPropertiesPanel(selectedLayouter, LayouterSettingsPanel);
            };
            ComboBoxLayouters.SelectedIndex = 0;
        }

        private void PrepareDecoratorsMenu()
        {
            ComboBoxDecorators.ItemsSource = decorators.OrderBy(d => d.GetType().Name).Select(decorator =>
                new KeyValuePair<ITagsCloudDecorator, string>(decorator, decorator.GetType().Name));
            ComboBoxDecorators.DisplayMemberPath = "Value";
            ComboBoxDecorators.SelectionChanged += (sender, e) =>
            {
                if (sender is ComboBox comboBox && comboBox.SelectedValue is KeyValuePair<ITagsCloudDecorator, string> kv)
                    selectedDecorator = kv.Key;
                if (selectedDecorator == null) return;
                FillPropertiesPanel(selectedDecorator, DecoratorSettingsPanel);
            };
            ComboBoxDecorators.SelectedIndex = 0;
        }

        private void FillPropertiesPanel(object obj, StackPanel settingsPanel)
        {
            settingsPanel.Children.Clear();
            settingsPanel.DataContext = obj;
            foreach (var prop in obj.GetType().GetProperties().Where(p => p.CanWrite))
                settingsPanel.Children.Add(GetCorrespondingControl(obj, prop));
        }

        public FrameworkElement GetCorrespondingControl(object obj, PropertyInfo propertyInfo)
        {
            var label = new Label { Content = propertyInfo.Name };
            var control = GetControl(propertyInfo);
            control.DataContext = obj;
            var stack = new StackPanel { Orientation = Orientation.Horizontal };
            stack.Children.Add(label);
            stack.Children.Add(control);
            stack.Margin = new Thickness(0, 5, 0, 0);
            return stack;
        }

        private FrameworkElement GetControl(PropertyInfo propertyInfo)
        {
            if (propertyInfo.PropertyType == typeof(bool))
            {
                var chkBox = new CheckBox
                {
                    Height = double.NaN,
                    VerticalAlignment = VerticalAlignment.Center
                };
                chkBox.SetBinding(CheckBox.IsCheckedProperty, propertyInfo.Name);
                return chkBox;
            }

            if (propertyInfo.PropertyType == typeof(int))
            {
                var textBox = new TextBox();
                textBox.SetBinding(TextBox.TextProperty, propertyInfo.Name);
                textBox.PreviewTextInput += (sender, e) => e.Handled = digitsRegex.IsMatch(e.Text);
                textBox.MinWidth = 50;
                textBox.VerticalAlignment = VerticalAlignment.Center;
                return textBox;
            }

            if (propertyInfo.PropertyType == typeof(Color))
            {
                var comboBox = new ComboBox();
                var knownColorsNames = Enum.GetNames(typeof(KnownColor));
                comboBox.ItemsSource = knownColorsNames;
                comboBox.VerticalContentAlignment = VerticalAlignment.Center;
                var binding = new Binding(propertyInfo.Name)
                {
                    Converter = new ColorToNameConverter()
                };
                comboBox.SetBinding(ComboBox.SelectedValueProperty, binding);
                return comboBox;
            }

            if (propertyInfo.PropertyType == typeof(Font))
            {
                var comboBox = new ComboBox
                {
                    ItemsSource = System.Drawing.FontFamily.Families.Select(f => f.Name),
                    VerticalContentAlignment = VerticalAlignment.Center
                };
                var binding = new Binding(propertyInfo.Name)
                {
                    Converter = new FontToNameConverter()
                };
                comboBox.SetBinding(ComboBox.SelectedValueProperty, binding);
                return comboBox;
            }

            var label = new Label { Content = "Value" };
            return label;
        }

        private void MenuItemGenerate_Click(object sender, RoutedEventArgs e)
        {
            tagsCloud = new TagsCloudGenerator(parsers, filters, selectedLayouter, selectedDecorator, imageSavers);
            tagsCloud.GenerateCloud(inputFileName);
            UpdateImageControl();
        }

        private void UpdateImageControl()
        {
            if (tagsCloud == null || tagsCloud.TagCloudImage == null) return;

            var bitmapImage = new BitmapImage();
            using (var ms = new MemoryStream())
            {
                tagsCloud.TagCloudImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                ms.Position = 0;

                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = ms;
                bitmapImage.EndInit();
            }
            TagsCloudImage.Source = bitmapImage;
        }

        private void MenuItemSaveToFile_Click(object sender, RoutedEventArgs e)
        {
            var filenameFilters = string.Join(";", imageSavers.Select(saver =>
                string.Join(";", saver.FileExtensions.Select(ext => $"*{ext}"))));
            filenameFilters = $"Image files|{filenameFilters}";

            var dlg = new SaveFileDialog
            {
                Filter = filenameFilters
            };
            if (dlg.ShowDialog(this) != true) { return; }

            tagsCloud.SaveTo(dlg.FileName);
        }
    }
}

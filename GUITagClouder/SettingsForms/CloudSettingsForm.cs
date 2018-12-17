using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Extensions;
using TagCloud;

namespace GUITagClouder
{
    public class CloudSettingsForm : Form
    {
        public CloudSettingsForm(CloudSettings settings)
        {
            Controls.Add(new Button
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Dock = DockStyle.Bottom
            });

            var layout = new TableLayoutPanel();

            var layouterBox = CreateTypeComboBox<ICloudLayouter>();
            layouterBox.Location = new Point(40,40);
            Controls.Add(layouterBox);
            layouterBox.SelectedValueChanged += (s, a) => settings.TLayouter = layouterBox.SelectedText;
            
            var counterBox = CreateTypeComboBox<IWordsCounter>();
            counterBox.Location = new Point(40,80); //TODO use layouters 
            Controls.Add(counterBox);
            counterBox.SelectedValueChanged += (s, a) => settings.TCounter = counterBox.SelectedText;
            
            var weightBox = CreateTypeComboBox<IWeightScaler>();
            weightBox.Location = new Point(40,120);
            Controls.Add(weightBox);
            weightBox.SelectedValueChanged += (s, a) => settings.TScaler = weightBox.SelectedText;
        }

        private ComboBox CreateTypeComboBox<T>()
        {
            var box = new ComboBox();
            FindInterfaceImplementations<T>()
                .Select(t=>t.Name)
                .Foreach(n=>box.Items.Add(n));
            return box;
        }

        private IEnumerable<Type> FindInterfaceImplementations<TInterface>() =>
            AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes())
                .Where(t => typeof(TInterface).IsAssignableFrom(t))
                .Where(t => !t.IsInterface);

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Text = "Настройки";
        }
    }
}
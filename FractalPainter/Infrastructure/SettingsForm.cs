using System;
using System.Windows.Forms;

namespace FractalPainting.Infrastructure
{
	public static class SettingsForm
	{
		public static SettingsForm<TSettings> For<TSettings>(TSettings settings)
		{
			return new SettingsForm<TSettings>(settings);
		}
	}

	public class SettingsForm<TSettings> : Form
	{
		public SettingsForm(TSettings settings)
		{
			Controls.Add(new Button
			{
				Text = "OK",
				DialogResult = DialogResult.OK,
				Dock = DockStyle.Bottom
			});
			Controls.Add(new PropertyGrid
			{
				SelectedObject = settings,
				Dock = DockStyle.Fill
			});
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			Text = "Настройки";
		}
	}
}
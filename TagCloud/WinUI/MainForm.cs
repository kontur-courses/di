using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using TagsCloudVisualisation.Extensions;
using WinUI.Helpers;
using WinUI.InputModels;
using WinUI.Utils;

namespace WinUI
{
    public partial class MainForm : Form
    {
        private FormWindowState previousState;
        private Image? currentResultImage;

        public MainForm()
        {
            InitializeComponent();
            previousState = WindowState;
        }

        public event Action? ExecuteButtonClicked;

        public void SetImage(Image newImage)
        {
            currentResultImage = newImage;
            UpdatePreviewImage();
        }

        public UiLockingOperation StartLockingOperation()
        {
            var ctSource = new CancellationTokenSource();

            ExecuteButton.Enabled = false;
            StopButton.Enabled = true;
            StopButton.Click += StopButtonClickHandler;
            StopButton.Focus();
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.MarqueeAnimationSpeed = 20;

            return new UiLockingOperation(ctSource.Token, progressBar: progressBar,
                disposer: new ActionDisposable(() =>
                {
                    ExecuteButton.Enabled = true;
                    StopButton.Enabled = false;
                    StopButton.Click -= StopButtonClickHandler;
                    ExecuteButton.Focus();
                    progressBar.Style = ProgressBarStyle.Blocks;
                }));

            void StopButtonClickHandler(object _, EventArgs __) => ctSource.Cancel();
        }

        public void AddUserInput<T>(UserInputSelector<T> inputModel)
        {
            CreateUserInputContainer(inputModel.Description, () =>
            {
                var combobox = new ComboBox();

                combobox.Items.AddRange(inputModel.Available.Select(x => x.Name).Cast<object>().ToArray());
                combobox.SelectedItem = inputModel.Selected.Name;

                combobox.SelectionChangeCommitted +=
                    (_, __) => inputModel.SetSelected(combobox.SelectedItem.ToString()!);

                return combobox;
            });
        }

        public void AddUserInput(UserInputField fieldInput)
        {
            CreateUserInputContainer(fieldInput.Description, () =>
            {
                var textBox = new TextBox();
                fieldInput.LinkTo(textBox);
                return textBox;
            });
        }

        private void ExecuteButton_Click(object sender, EventArgs args)
        {
            ExecuteButtonClicked?.Invoke();
        }

        private void CreateUserInputContainer(string showingText, Func<Control> innerCreator)
        {
            var offset = new Size(10, 5);
            var startY = rightPanel.Controls.Cast<Control>().Select(x => x.Bottom).MaxOrDefault();

            var panel = new Panel
            {
                Location = new Point(0, startY + offset.Height),
                Width = rightPanel.Width - offset.Width * 2
            };
            rightPanel.Controls.Add(panel);

            var itemWidth = (panel.Size - offset * 2).Width;
            var label = new Label
            {
                Text = showingText,
                Location = new Point(offset),
                Width = itemWidth
            };
            panel.Controls.Add(label);

            var inner = innerCreator.Invoke();
            inner.Location = new Point(label.Location.X, label.Bottom + offset.Height / 3);
            inner.Width = itemWidth;
            panel.Controls.Add(inner);

            panel.Height = inner.Bottom;
        }

        private void UpdatePreviewImage()
        {
            if (currentResultImage == null)
                return;
            pictureBox.Image = currentResultImage.PlaceAtCenter(pictureBox.Size).FillBackground(Color.Black);
        }

        protected override void OnResizeEnd(EventArgs e)
        {
            base.OnResizeEnd(e);
            UpdatePreviewImage();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (WindowState != previousState)
            {
                previousState = WindowState;
                if (WindowState != FormWindowState.Minimized)
                    UpdatePreviewImage();
            }
        }
    }
}
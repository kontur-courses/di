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
    public interface IGui
    {
        void SetImage(Image? newImage);
        UiLockingOperation StartLockingOperation();
        void AddUserInput<T>(UserInputSingleChoice<T> inputModel);
        void AddUserInput<T>(UserInputMultipleChoice<T> inputModel);
        void AddUserInput(UserInputField fieldInput);
        void AddUserInput(UserInputBoolean booleanInput);
        void SetImageBackground(Color color);

        event Action? ExecutionRequested;
    }

    public partial class MainForm : Form, IGui
    {
        private FormWindowState previousState;
        private Image? currentResultImage;

        public MainForm()
        {
            InitializeComponent();
            previousState = WindowState;
        }

        public event Action? ExecutionRequested;

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

        public void AddUserInput<T>(UserInputSingleChoice<T> inputModel)
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

        public void AddUserInput<T>(UserInputMultipleChoice<T> inputModel)
        {
            CreateUserInputContainer(inputModel.Description, () =>
            {
                var treeView = new TreeView {CheckBoxes = true, AutoSize = true, Scrollable = true};

                treeView.Nodes.AddRange(
                    inputModel.Available
                        .Select(x => new TreeNode(x.Name) {Checked = inputModel.IsSelected(x.Name)})
                        .ToArray()
                );

                treeView.AfterCheck += (_, args) => inputModel.SetSelection(args.Node.Text, args.Node.Checked);
                return treeView;
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

        public void AddUserInput(UserInputBoolean booleanInput)
        {
            CreateUserInputContainer(null, () =>
            {
                var checkBox = new CheckBox {Text = booleanInput.Description};
                checkBox.CheckedChanged += (_, __) => booleanInput.SetValue(checkBox.Checked);
                return checkBox;
            });
        }

        private void ExecuteButton_Click(object sender, EventArgs args)
        {
            ExecutionRequested?.Invoke();
        }

        private void CreateUserInputContainer(string? showingText, Func<Control> innerCreator)
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
            var nextY = offset.Height;
            if (showingText != null)
            {
                var label = new Label
                {
                    Text = showingText,
                    Location = new Point(offset.Width, nextY),
                    Width = itemWidth
                };
                panel.Controls.Add(label);
                nextY = label.Bottom + offset.Height / 3;
            } 

            var inner = innerCreator.Invoke();
            inner.Location = new Point(offset.Width, nextY);
            inner.Width = itemWidth;
            panel.Controls.Add(inner);

            panel.Height = inner.Bottom;
        }

        private void UpdatePreviewImage()
        {
            if (currentResultImage == null)
                return;
            pictureBox.Image = currentResultImage.PlaceAtCenter(pictureBox.Size);
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

        public void SetImageBackground(Color color)
        {
            pictureBox.BackColor = color;
        }
    }
}
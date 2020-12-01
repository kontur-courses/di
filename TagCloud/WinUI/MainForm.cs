using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace WinUI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public event Action ExecuteButtonClicked;

        public void SetImage(Image newImage) => pictureBox.Image = newImage;

        public UiLockingOperation StartLockingOperation()
        {
            var ctSource = new CancellationTokenSource();

            ExecuteButton.Enabled = false;
            StopButton.Enabled = true;
            StopButton.Click += StopButtonClickHandler;
            StopButton.Focus();

            return new UiLockingOperation(ctSource.Token, progressBar: progressBar,
                disposer: new ActionDisposable(() =>
                {
                    ExecuteButton.Enabled = true;
                    StopButton.Enabled = false;
                    StopButton.Click -= StopButtonClickHandler;
                    ExecuteButton.Focus();
                }));

            void StopButtonClickHandler(object _, EventArgs __) => ctSource.Cancel();
        }

        public void AddControlToPanel(Panel panel)
        {
            this.rightPanel.Controls.Add(panel);
        }

        public Size PictureBoxSize => pictureBox.Size;

        private void ExecuteButton_Click(object sender, EventArgs args)
        {
            ExecuteButtonClicked?.Invoke();
        }
    }

    public class ProgressBarContext : IDisposable
    {
        private readonly int previousMax;
        private readonly int previousMin;
        private readonly int previousValue;
        private readonly ProgressBar progressBar;

        public int Value
        {
            get => progressBar.Value;
            set => progressBar.Value = value;
        }

        public ProgressBarContext(ProgressBar progressBar, int minValue, int maxValue)
        {
            previousMin = progressBar.Minimum;
            previousMax = progressBar.Maximum;
            previousValue = progressBar.Value;

            progressBar.Minimum = minValue;
            progressBar.Maximum = maxValue;
            progressBar.Value = minValue;

            this.progressBar = progressBar;
        }

        public void Increment()
        {
            progressBar.Increment(1);
        }

        public void Dispose()
        {
            progressBar.Minimum = previousMin;
            progressBar.Maximum = previousMax;
            progressBar.Value = previousValue;
        }
    }

    public class UiLockingOperation : IDisposable
    {
        private readonly ActionDisposable disposer;
        private readonly ProgressBar progressBar;

        public UiLockingOperation(CancellationToken cancellationToken, ActionDisposable disposer,
            ProgressBar progressBar)
        {
            CancellationToken = cancellationToken;
            this.disposer = disposer;
            this.progressBar = progressBar;
        }

        public CancellationToken CancellationToken { get; }

        public ProgressBarContext GetProgressBarContext(int minValue, int maxValue)
        {
            return new ProgressBarContext(progressBar, minValue, maxValue);
        }

        public void Dispose()
        {
            disposer.Dispose();
        }
    }
}
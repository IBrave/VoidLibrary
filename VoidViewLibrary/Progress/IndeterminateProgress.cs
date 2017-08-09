using System;
using System.Drawing;
using System.Windows.Forms;
using VoidViewLibrary.View.Helper;

namespace VoidViewLibrary.Progress
{
    public partial class IndeterminateProgress : Form
    {
        private Timer _watch_async_result_timer;
        private DateTime _start_time;

        private EventHandler<EventArgs> _method;
        private int _max_wait_time;
        private string _wait_msg;
        private bool _cancel_enable;

        private const int _no_wait_time = Int32.MaxValue;

        private IAsyncResult iAsyncResult;

        private Control _form_control;

        private DrawRotateCircleHelper _draw_rotate_circle_helper;

        public IndeterminateProgress(string waitMessage, bool cancelEnable, int maxWaitTime, EventHandler<EventArgs> method)
        {
            InitializeComponent();
            init(waitMessage, cancelEnable, maxWaitTime, method);
        }

        public IndeterminateProgress(string waitMessage, bool cancelEnable, EventHandler<EventArgs> method)
        {
            InitializeComponent();
            init(waitMessage, cancelEnable, _no_wait_time, method);
        }

        public IndeterminateProgress(string waitMessage, EventHandler<EventArgs> method)
        {
            InitializeComponent();
            init(waitMessage, false, _no_wait_time, method);
        }

        private void init(string waitMessage, bool cancelEnable, int maxWaitTime, EventHandler<EventArgs> method)
        {
            this.components = new System.ComponentModel.Container();
            _draw_rotate_circle_helper = new DrawRotateCircleHelper(_picture_box);

            _watch_async_result_timer = new Timer(this.components);
            _watch_async_result_timer.Interval = 300;
            _watch_async_result_timer.Tick += new EventHandler(this.Watch_Async_Result_Timer_Tick);

            int maxBorder = Math.Min(_picture_box.Width, _picture_box.Height);

            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ShowInTaskbar = false;

            this._wait_msg = waitMessage;
            this._cancel_enable = cancelEnable;
            this._max_wait_time = maxWaitTime;
            this._method = method;

            this.label_loading_prompt_msg.Text = waitMessage;

            StartPosition = FormStartPosition.CenterScreen;

            _rp_progress_dialog.Location = new Point(0, 0);
            _rp_progress_dialog.Size = Size;
            _form_control = _rp_progress_dialog;
            BackColor = new EdgeShadowHelper(_form_control).Add_Paint().GetBackColor();
        }

        private void initEvents()
        {
        }

        private void Progress_Shown(object sender, EventArgs e)
        {
            if (_method != null)
            {
                iAsyncResult = _method.BeginInvoke(null, null, null, null);
            }

            Start();
        }

        private void Progress_Closing(object sender, FormClosingEventArgs e)
        {
            Stop();
        }

        private Image ScaleImage(Image img, int maxBorder)
        {
            int destWidth = maxBorder;

            Image newImg = new Bitmap(destWidth, destWidth);
            Graphics g = Graphics.FromImage(newImg);
            g.DrawImage(img, 0, 0, destWidth, destWidth);
            return newImg;
        }

        private void Start()
        {
            _watch_async_result_timer.Start();
            _draw_rotate_circle_helper.Start();
            _start_time = DateTime.Now;
        }

        private void Stop()
        {
            _watch_async_result_timer.Stop();
            _draw_rotate_circle_helper.Stop();
            if (_method != null)
            {
                _method.EndInvoke(iAsyncResult);
            }
        }

        private void Watch_Async_Result_Timer_Tick(object sender, EventArgs e)
        {
            if (sender is Timer)
            {
                int eclapsedMillisecond = DateTime.Now.Millisecond - _start_time.Millisecond;
            }

            if (iAsyncResult != null && iAsyncResult.IsCompleted)
            {
                Close();
            }
        }
    }
}

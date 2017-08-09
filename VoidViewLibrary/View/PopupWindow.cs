using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Drawing.Drawing2D;

namespace VoidViewLibrary.View
{
    public partial class PopupWindow : UserControl
    {
        public delegate void PopupWindowResultListener(DialogResult dialogResult);
        public PopupWindowResultListener popupWindowResultListener;

        private Control _parent_control;

        private GraphicsPath _no_shadow_graphics_path;
        private Pen _shadow_pen;

        public PopupWindow()
        {
            InitializeComponent();

            this.Paint += new PaintEventHandler(this.PopupWindow_Paint);
            new MoveControlAtParentControlHelper(this);
        }

        // 先添加控件后调整控件
        public void ShowAtLocation(Control parentControl, string msg, MessageBoxButtons messageBoxButtons = MessageBoxButtons.OKCancel, int type = 0)
        {
            _parent_control = parentControl;

            parentControl.Controls.Add(this);

            int popupWindowWidth = (int) (parentControl.Width * 0.8F);
            int popupWindowHeight = (int) (popupWindowWidth * 0.618F);
            Size = new Size(popupWindowWidth, popupWindowHeight);

            int innerMaxWidth = (int) (popupWindowWidth * 0.8F);
            int left = (popupWindowWidth - innerMaxWidth) / 2;
            int top = left;
            int bottom = top;

            LabelControl labelControl = new LabelControl();
            labelControl.Name = "msgLabelControl";
            labelControl.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            labelControl.AutoSizeMode = LabelAutoSizeMode.None;
            labelControl.Text = msg;
            Controls.Add(labelControl);
            Graphics graphics = labelControl.CreateGraphics();
            SizeF sizeF = graphics.MeasureString(labelControl.Text, labelControl.Font);
            int row = (int)(sizeF.Width / innerMaxWidth + ((sizeF.Width % innerMaxWidth) != 0 ? 1 : 0));
            int msg_text_height = (int)(row * sizeF.Height);
            labelControl.Size = new System.Drawing.Size(innerMaxWidth, msg_text_height);
            labelControl.Location = new System.Drawing.Point((popupWindowWidth - innerMaxWidth) / 2, top);
            labelControl.BackColor = Color.FromArgb(255, 225, 225, 225);
            if (row == 1)
            {
                labelControl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            }
            labelControl.Location = new System.Drawing.Point((popupWindowWidth - innerMaxWidth) / 2, (popupWindowHeight - (bottom + 20) - msg_text_height) / 2);

            new MoveControlAtParentControlHelper(labelControl, true);

            // 当未执行.Controls.Add(this)这个动作时Size.Width==popupWindowWidth，但执行后就变了
            //parentControl.Controls.Add(this);
            // popupWindowWidth:175 popupWindowHeight:108
            // Size.Width:204 Size.Height:126

            int x = (parentControl.Width - Size.Width) / 2;
            int y = (parentControl.Height - Size.Height) / 2;

            Location = new Point(x, y);

            BackColor = Color.Transparent;//Color.FromArgb(0, 225, 225, 225);//Color.FromArgb(0, Color.Red);
            ResumeLayout(false);
            SuspendLayout();

            int btn_width = 75;
            int div_width = 20;
            int btn_num = messageBoxButtons == MessageBoxButtons.OK ? 1 : 2;
            int btn_div_width_sum = btn_width * btn_num + div_width * (btn_num - 1);
            string[] btn_names = new string[] { "确定", "取消"};
            DialogResult[] dialog_results = new DialogResult[] {
                DialogResult.OK, DialogResult.Cancel
            };

            for (int i = 0; i < btn_num; ++i)
            {
                SimpleButton simple_btn = new DevExpress.XtraEditors.SimpleButton();
                simple_btn.Size = new System.Drawing.Size(btn_width, 23);
                simple_btn.Location = new System.Drawing.Point((Size.Width - btn_div_width_sum) / 2 + btn_width * i + div_width * i, Size.Height - simple_btn.Size.Height - bottom);
                simple_btn.Name = "btn_confirm";
                simple_btn.TabIndex = btn_num - i;
                simple_btn.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
                simple_btn.Text = btn_names[i];
                simple_btn.DialogResult = dialog_results[i];

                Controls.Add(simple_btn);

                simple_btn.Click += new EventHandler(Btn_Click);
            }
        }

        private void Btn_Click(object obj, EventArgs e)
        {
            SimpleButton btn = obj as SimpleButton;
            Console.WriteLine(btn.DialogResult);
            _parent_control.Controls.Remove(this);
            if (popupWindowResultListener != null)
            {
                popupWindowResultListener(btn.DialogResult);
            }
        }

        //Edge Shadow Effect
        private void PopupWindow_Paint(object sender, PaintEventArgs e)
        {
            /*
            Bitmap bm = new Bitmap(Size.Width, Size.Height);
            GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddRectangle(new Rectangle(0, 0, Size.Width, Size.Height));
            // path.AddArc(0, 0, Size.Width, Size.Height, 0, 360);
            Graphics g = Graphics.FromImage(bm);
            Matrix mx = new Matrix(1.0F, 0, 0, 1.0F, -1.0F/ 5, -1.0F / 5);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Transform = mx;
            Pen p = new Pen(Color.Yellow, 3);
            g.DrawPath(p, path);
            g.FillPath(Brushes.Yellow, path);
            g.Dispose();
            e.Graphics.Transform = new Matrix(1, 0, 0, 1, 50, 50);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImage(bm, ClientRectangle, 0, 0, bm.Width, bm.Height, GraphicsUnit.Pixel);
            //e.Graphics.FillPath(Brushes.Black, path);
            path.Dispose();
             * */
            /*
            int borderSize = Width / 2;
            GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(new Rectangle(Left - borderSize / 2, Top - borderSize * 2, Width + borderSize, Height + borderSize * 4));
            borderSize = borderSize / 2;
            Bitmap bm = new Bitmap(Size.Width - 40, Size.Height - 40);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clip = new Region(path);

            Rectangle rect = new Rectangle(Left - borderSize*4, Top - borderSize, Width + borderSize * 8, Height + borderSize * 2);
            LinearGradientBrush linGrBrush = new LinearGradientBrush(rect, Color.White, Color.Gray, -90);
            g.FillEllipse(linGrBrush, rect);
            g.DrawEllipse(new Pen(Color.Gray, 4), rect);
             * */
            /*
            Rectangle rect = new Rectangle(0, 0, Size.Width, 5);
            LinearGradientBrush linGrBrush = new LinearGradientBrush(rect, Color.LightGray, Color.Gray, 90);
            Pen pen = new Pen(linGrBrush);
            e.Graphics.DrawLine(pen, 0, 10, 200, 10);
            // e.Graphics.FillEllipse(linGrBrush, 0, 30, 200, 100);
            e.Graphics.FillRectangle(linGrBrush, 0, 0, Size.Width, 5);
            linGrBrush = new LinearGradientBrush(rect, Color.LightGray, Color.Gray, -90);
            e.Graphics.FillRectangle(linGrBrush, 0, Size.Height - 5, Size.Width, 5);

            rect = new Rectangle(0, 0, 5, Size.Height);
            linGrBrush = new LinearGradientBrush(new Point(0, 0), new Point(5, Size.Height), Color.LightGray, Color.Gray);
            e.Graphics.FillRectangle(linGrBrush, Size.Width - 5, 0, 5, Size.Height);
            //Bitmap
            */
            /*
            Control panel = (Control)sender;
            Color[] shadow = new Color[3];
            shadow[0] = Color.FromArgb(181, 181, 181);
            shadow[1] = Color.FromArgb(195, 195, 195);
            shadow[2] = Color.FromArgb(211, 211, 211);
            Pen pen = new Pen(shadow[0]);
            using (pen)
            {
                foreach (Control p in panel.Controls.OfType<Control>())
                {
                    Point pt = p.Location;
                    pt.Y += p.Height;
                    for (var sp = 0; sp < 3; sp++)
                    {
                        pen.Color = shadow[sp];
                        e.Graphics.DrawLine(pen, pt.X, pt.Y, pt.X + p.Width - 1, pt.Y);
                        pt.Y++;
                    }
                }
            }*/
            if (_no_shadow_graphics_path == null)
            {
                _no_shadow_graphics_path = new GraphicsPath();
                Rectangle rect = DisplayRectangle;
                rect.Inflate(-5, -5);
                _no_shadow_graphics_path.AddRectangle(rect);
            }
            int _Glow = 15, _Feather = 50;

            if (_shadow_pen == null)
            {
                _shadow_pen = new Pen(Color.Gray);
            }

            for (int i = 0; i < _Glow; i += 2)
            {
                int glow_alpha = (int)(_Feather - (_Feather / _Glow) * i);
                _shadow_pen.Color = Color.FromArgb(glow_alpha, Color.Gray);
                _shadow_pen.Width = i;
                _shadow_pen.LineJoin = LineJoin.Round;
                e.Graphics.DrawPath(_shadow_pen, _no_shadow_graphics_path);
            }

            // Color.FromArgb(255, 235, 236, 239);
            System.Drawing.SolidBrush solid_brush = new System.Drawing.SolidBrush(Color.FromArgb(255, 225, 225, 225));
            System.Drawing.Graphics popup_window_graphics = this.CreateGraphics();
            popup_window_graphics.FillRectangle(solid_brush, new Rectangle(5, 5, Size.Width - 10, Size.Height - 10));
        }
    }

    public class MoveControlAtParentControlHelper
    {

        private Point _popup_window_mouse_down = new Point();
        private bool _transmit_to_parent;

        /// <summary>
        /// I know this post is quite old, but it seems to me that the simplest method would be for the form to implement IMessageFilter. In the constructor (or in OnHandleCreated) you call
        /// Application.AddMessageFilter(this);
        /// and then you can catch the messages of all windows in your implementation of IMessageFilter.PreFilterMessage.
        /// You'd likely need to use P/Invoke for the WIN32 IsChild method
        /// [DllImport("user32.dll")]
        /// public static extern bool IsChild(IntPtr hWndParent, IntPtr hWnd);
        /// along with the form's Handle property to ensure that you're handling the right messages.
        /// </summary>
        /// <param name="movable_control"></param>
        /// <param name="transmit_to_parent"></param>

        public MoveControlAtParentControlHelper(Control movable_control, bool transmit_to_parent = false)
        {
            movable_control.MouseMove += new MouseEventHandler(PopupWindow_MouseMove);
            movable_control.MouseDown += new MouseEventHandler(PopupWindow_MouseDown);
            _transmit_to_parent = transmit_to_parent;
        }

        private void PopupWindow_MouseMove(object sender, MouseEventArgs e)
        {
            Control control = (Control)sender;
            if (_transmit_to_parent)
            {
                control = control.Parent;
            }
            Control parent_control = control.Parent;

            if (parent_control == null || _popup_window_mouse_down == null)
            {
                return;
            }
            if (e.Button == MouseButtons.Left)
            {
                int distance_x = e.X - _popup_window_mouse_down.X;
                int distance_y = e.Y - _popup_window_mouse_down.Y;
                int new_left = control.Left + distance_x;
                int new_top = control.Top + distance_y;
                control.Left = Math.Min(parent_control.Width - control.Width, Math.Max(0, new_left));
                control.Top = Math.Min(parent_control.Height - control.Height, Math.Max(0, new_top));
            }
        }

        private void PopupWindow_MouseDown(object sender, MouseEventArgs e)
        {
            _popup_window_mouse_down.X = e.X;
            _popup_window_mouse_down.Y = e.Y;
        }

    }
}

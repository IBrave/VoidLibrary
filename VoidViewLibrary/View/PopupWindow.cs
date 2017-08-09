using DevExpress.XtraEditors;
using System;
using System.Drawing;
using System.Windows.Forms;
using VoidViewLibrary.View.Helper;

namespace VoidViewLibrary.View
{
    public partial class PopupWindow : UserControl
    {
        public delegate void PopupWindowResultListener(DialogResult dialogResult);
        public PopupWindowResultListener popupWindowResultListener;

        private Control _parent_control;

        public PopupWindow()
        {
            InitializeComponent();

            new EdgeShadowHelper(this).Add_Paint();
            new MoveControlAtParentControlHelper(this);
        }

        // 先添加控件后调整控件
        public void ShowAtLocation(Control parentControl, string msg, MessageBoxButtons messageBoxButtons = MessageBoxButtons.OKCancel, int type = 0)
        {
            _parent_control = parentControl;
            parentControl.Controls.Add(this);

            /*
            PictureBox p = new PictureBox();
            parentControl.Controls.Add(p);
            p.BringToFront();
            p.BackColor = Color.Transparent;
            p.Image = Image.FromFile(@"C:\Users\LiuYi\Desktop\AnyCodeAnyWhere.png");
            p.Size = parentControl.Size;

            PanelControl panel_control = new PanelControl();
            panel_control.Parent = p;
            //parentControl.Controls.Add(panel_control);
            panel_control.BringToFront();
            panel_control.Size = parentControl.Size;
            panel_control.Controls.Add(this);
            panel_control.BackColor = Color.Transparent;
             * */

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
    }
}

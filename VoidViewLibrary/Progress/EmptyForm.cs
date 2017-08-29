//                                                                        101010101010101010101010101010101                                                    
//                                                                    10101010101010101010101010101010101010101010                                             
//                                                       101      10101010101010101010101010101010101010101010101010                                           
//                                               1       1010101010101010101010101010101010101010101010101010101010101                                         
//                                               1     10101010101010101010101010101010101010101010101010101010101010 1                                        
//                                             1     1010101010101010101010101010101010101010101010101010101010101010 1010                                     
//                                            1    1010101010101010101010101010101010101010101010101010101010101010101010101                                   
//                                          10    1010101010101010101010101010101010101010101010101010101010101010101010101010                                 
//                                         10    101010101010101010101010101010101010101010101010101010101010101010101010101010                                
//                                         10    101010101010101010101010101010101010101010101010101010101010101010101010101010                                
//                                        10     101010101010101010101010101010101010101010101010101010101010101010101010101010                                
//                                       101 010101010101010101010101010101010101010        10101010101010101010101010101010101                                
//                                       10  101010101010101010101010101010101010             10101010101010101010101010101010                                 
//                                       10  101010101010101010101010101010101                 1010101010101010101010101010101                                 
//                                      101  01010101010101010101010101010                               101010101010101010101                                 
//                                      101  0101010101010101010101010                                     1010101010101010101010                              
//                                      10101010101010101010101010                                            1010101010101010101                              
//                                    101010 101010101010101010                                                101010101010101010                              
//                                   10101010101010101010101                                                       10101010101010                              
//                                   101010101010101010                                                               101010101010                             
//                                  10101010101010                                                                      101010101                              
//                                  1010101010101                                                                        101010101                             
//                                 1010101010101        1010101010                                                       1010101010                            
//                                 101010101010 1    101010101010101010                                                 10101010101                            
//                                 101010101010                   10101010                                             101010101010                            
//                                 10101010101                        10101010                                        1010101010101                            
//                                101010101010                           10101010                                     1010101010101                            
//                                101010101010             10 10101010       10101                                    1010101010101                            
//                                 1010101010             1010  1010101010                        101010101010        101010101010                             
//                                 101010101                 10101010101010                   10101010101010101010    10101010101                              
//                                 101010101                    10101   10101                           1            101010101010                              
//                                1010101010                           10101                                         101010101010                              
//                               10101010101                                                   1010101010101         10101010101                               
//                              101010101010                                                  10  1010101 0101      101010101010                               
//                              1010101010101                                                 1  0101010  10101     1010101010101                              
//                             10101010101010                                                         10101         1010101010101                              
//                            101010101010101                                                                      1010101010101                               
//                           1010101010101010                                                                      1010101010101                               
//                          101010101010101010                                                                    101010101010                                 
//                         1010101010101010101                                                                    10101010101                                  
//                        101010101010101010101                                                                  101010101010                                  
//                       1010101010101010101010                          1                                      1010101010101                                  
//                      101010101010101010101010                                                              10101010101010                                   
//                     1010101010101010101010101                                   10                        1010101010 1010                                   
//                    101010101010101010101010101                                                           101010101                                          
//                    1010101010101010101010101010                                                        1010101010                                           
//                   101010101010101010101010101010             10                                       1010101010                                            
//                  10101010101010101010101010101010              1010                                 1010101010                                              
//                 10101010101010101010101010101010101              1010101  0101     1010           101010101                                                 
//                1010101010101010101010101010101010101               1010101010101010             10101010                                                    
//               101010101010101010101010101010101010 10                 101010101              10101010   10101010101010                                      
//               10101010101010101010101010101010      10                                     101  01                    10101                                 
//             101010101010101010101010101010101        101                                  10                               1010                             
//            101010101010101010101010101010              101                            1010                                    1010                          
//           101010101010101010101010101                     101                       101                                         1010                        
//          101010101010101010101010                          1010                  1010                                              10                       
//         1010101010101010101010                                10101       10101010                                                  10                      
//        10101010101010101                                           1010101010                                                        10                     
//        1010101010101                                                                                                                  10                    
//       1010101010                                                                                                                       10                   
//      10101010101                                                                                                                        10                  
//     10101010                                                                                                                             10                 
//     1010101                                                                                                                              10                 
//    10101                                                                                                                                  1                 
//   10                                                                                                                                      10                
//  10                                                                                                                                       10                
// 10                                                                                                                                         1                
//1   
using DevExpress.XtraEditors;
using System;
using System.Drawing;
using System.Windows.Forms;
using VoidViewLibrary.View.Helper;

namespace SocketHelperDemo.View
{
#if DEV_EXPRESS_ON
    public partial class EmptyForm : DevExpress.XtraEditors.XtraForm
#else
    public partial class EmptyForm : Form
#endif
    {
        public delegate void DialogResultListener(DialogResult dialogResult);
        public DialogResultListener _dialog_result_listener;

        private EmptyForm()
        {
            InitializeComponent();

            Color back_color = new EdgeShadowHelper(this).Add_Paint().GetBackColor();

            FormBorderStyle = FormBorderStyle.None;
        }

        public static EmptyForm CreateEmptyForm(Form parent, string msg, MessageBoxButtons messageBoxButtons = MessageBoxButtons.OKCancel)
        {
            EmptyForm empty_form = new EmptyForm();
            empty_form.ShowAtLocation(msg, messageBoxButtons);
            empty_form.StartPosition = FormStartPosition.CenterScreen;
            empty_form.FormBorderStyle = FormBorderStyle.None;
            if (parent != null)
            {
                empty_form.Owner = parent;
            }

            return empty_form;
        }

        public void ShowAtLocation(string msg, MessageBoxButtons messageBoxButtons = MessageBoxButtons.OKCancel)
        {
            int Width = 300;
            int Height = 180;

            int popupWindowWidth = (int)(Width * 0.8F);
            int popupWindowHeight = (int)(popupWindowWidth * 0.618F);
            Size = new Size(popupWindowWidth, popupWindowHeight);

            int innerMaxWidth = (int)(popupWindowWidth * 0.8F);
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
#if DEV_EXPRESS_ON
#else
            labelControl.BackColor = Color.FromArgb(255, 225, 225, 225);
#endif
            if (row == 1)
            {
                labelControl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            }
            labelControl.Location = new System.Drawing.Point((popupWindowWidth - innerMaxWidth) / 2, (popupWindowHeight - (bottom + 20) - msg_text_height) / 2);

            int x = (Width - Size.Width) / 2;
            int y = (Height - Size.Height) / 2;

            Location = new Point(x, y);

            ResumeLayout(false);
            SuspendLayout();

            int btn_width = 75;
            int div_width = 20;
            int btn_num = messageBoxButtons == MessageBoxButtons.OK ? 1 : 2;
            int btn_div_width_sum = btn_width * btn_num + div_width * (btn_num - 1);
            string[] btn_names = new string[] { "Ok", "Cancel" };
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

            labelControl.LookAndFeel.SkinName = LookAndFeel.SkinName;
            labelControl.LookAndFeel.UseDefaultLookAndFeel = LookAndFeel.UseDefaultLookAndFeel;
        }

        protected override void OnResize(EventArgs e)
        {
            SetNewSize();
            base.OnResize(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            SetNewSize();
            base.OnSizeChanged(e);
        }

        private void SetNewSize()
        {
            // int size = Math.Max(Width, Height);
            // Size = new Size(size, size);
        }

        private void Btn_Click(object obj, EventArgs e)
        {
            SimpleButton btn = obj as SimpleButton;
            Console.WriteLine(btn.DialogResult);
            if (_dialog_result_listener != null)
            {
                _dialog_result_listener(btn.DialogResult);
            }

            Close();
        }
    }
}

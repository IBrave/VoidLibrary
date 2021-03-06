﻿//                                                                        101010101010101010101010101010101                                                    
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
using System;
using System.Drawing;
using System.Windows.Forms;
using VoidViewLibrary.View.Helper;

namespace SocketHelperDemo.View
{
#if DEV_EXPRESS_ON
    public partial class LoadingProgress : DevExpress.XtraEditors.XtraForm
#else
    public partial class LoadingProgress : Form
#endif
    {
        public delegate void CheckValue();
        public delegate void ButtonEvent(DialogResult dialogResult);

        private float _circleSize = 0.8f;

        private Timer _check_method_timer;
        private CheckValue _check_method;
        private bool _ignore_inner_check_method_timeout_invok_close_event;

        public ButtonEvent _timeout_button_event;

        private DrawRotateCircleHelper _draw_rotate_circle_helper;

        public LoadingProgress()
        {
            InitializeComponent();

            _draw_rotate_circle_helper = new DrawRotateCircleHelper(_picture_box);

            Closed += new EventHandler(Closed_Click);
            this._btn_yes.DialogResult = DialogResult.Yes;
            this._btn_no.DialogResult = DialogResult.No;

            Color back_color = new EdgeShadowHelper(this).Add_Paint().GetBackColor();

            FormBorderStyle = FormBorderStyle.None;
            BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;

            _picture_box.BackColor = back_color;
            _label_hint_msg.BackColor = back_color;

#if DEV_EXPRESS_ON
            BackColor = Color.Transparent;
#else
            BackColor = back_color;
#endif

            _draw_rotate_circle_helper.Start();
        }

        public static LoadingProgress ShowProgress(Form parent)
        {
            LoadingProgress loading = new LoadingProgress();
            loading.StartPosition = FormStartPosition.CenterScreen;
            loading.FormBorderStyle = FormBorderStyle.FixedDialog;
            if (parent != null)
            {
                loading.Owner = parent;
            }
            return loading;
        }

        public Color CircleColor
        {
            get { return _draw_rotate_circle_helper.CircleColor; }
            set
            {
                _draw_rotate_circle_helper.CircleColor = value;
                Invalidate();
            }
        }

        public float CircleSize
        {
            get { return _circleSize; }
            set
            {
                if (value <= 0.0F)
                    _circleSize = 0.05F;
                else
                    _circleSize = value > 4.0F ? 4.0F : value;
                Invalidate();
            }
        }

        public CheckValue CheckMethod
        {
            get { return _check_method; }
            set
            {
                _check_method = value;
                if (_check_method_timer == null)
                {
                    _check_method_timer = new Timer();
                    _check_method_timer.Tick += new EventHandler(Timer_CheckValue);
                    _check_method_timer.Interval = 1000;
                    _check_method_timer.Start();
                }
            }
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

        private void Timer_CheckValue(object sender, EventArgs e)
        {
            if (_check_method != null)
            {
                _check_method.Invoke();
            }
        }

        

        public void Notify_Finished()
        {
            if (_check_method_timer != null)
            {
                _check_method_timer.Stop();
            }
            _draw_rotate_circle_helper.Stop();
            this._btn_yes.PerformClick();
            if (_timeout_button_event != null) _timeout_button_event(DialogResult.Yes);
            _ignore_inner_check_method_timeout_invok_close_event = true;
            Close();
        }

        public void Notify_Failed()
        {
            if (_check_method_timer != null)
            {
                _check_method_timer.Stop();
            }
            _draw_rotate_circle_helper.Stop();
            this._btn_no.PerformClick();
            if (_timeout_button_event != null) _timeout_button_event(DialogResult.No);
            _ignore_inner_check_method_timeout_invok_close_event = true;
            Close();
        }

        private void Closed_Click(object sender, EventArgs e)
        {
            if (_check_method_timer != null)
            {
                _check_method_timer.Stop();
            }
            _draw_rotate_circle_helper.Stop();
            if (_ignore_inner_check_method_timeout_invok_close_event)
            {
            }
            else
            {
                if (_timeout_button_event != null) _timeout_button_event(DialogResult.Cancel);
            }
        }

        private void Yes_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void No_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

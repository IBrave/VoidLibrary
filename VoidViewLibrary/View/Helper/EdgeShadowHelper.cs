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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace VoidViewLibrary.View.Helper
{
    public class EdgeShadowHelper
    {
        private GraphicsPath _no_shadow_graphics_path;
        private Pen _shadow_pen;

        private Color _back_color;
        private System.Drawing.SolidBrush _solid_brush;

        private Control _add_edge_control;

        public EdgeShadowHelper(Control add_edge_control)
        {
            _add_edge_control = add_edge_control;

#if DEV_EXPRESS_ON
            _back_color = add_edge_control.BackColor;
#else
            _back_color = Color.FromArgb(255, 225, 225, 225);
#endif
            _solid_brush = new System.Drawing.SolidBrush(_back_color);
        }

        public EdgeShadowHelper Add_Paint()
        {
            _add_edge_control.Paint += new PaintEventHandler((object obj, PaintEventArgs e) =>
            {
                Draw_Effect_Shadow(e);
            });

            return this;
        }

        public Color GetBackColor()
        {
            return _back_color;
        }

        //Edge Shadow Effect
        private void Draw_Effect_Shadow(PaintEventArgs e)
        {
            int edge_shadow_width = 5;
            if (_no_shadow_graphics_path == null)
            {
                _no_shadow_graphics_path = new GraphicsPath();
                Rectangle rect = _add_edge_control.DisplayRectangle;
                rect.Inflate(-edge_shadow_width, -edge_shadow_width);
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

            System.Drawing.Graphics popup_window_graphics = _add_edge_control.CreateGraphics();
            popup_window_graphics.FillRectangle(_solid_brush, new Rectangle(edge_shadow_width, edge_shadow_width, _add_edge_control.Size.Width - edge_shadow_width * 2, _add_edge_control.Size.Height - edge_shadow_width * 2));
        }
    }
}

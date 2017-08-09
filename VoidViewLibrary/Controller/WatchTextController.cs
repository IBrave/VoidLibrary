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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoidViewLibrary.Controller
{
    public class WatchTextController
    {
        public delegate void WatchTextChangedListener(int text_changed_num);

        private List<TextEdit> watch_text_edit_list = new List<TextEdit>();
        private List<WrapWatchControlObj> wrap_watch_text_edit_list = new List<WrapWatchControlObj>();

        private List<TextEdit> cache_watch_text_edit_changed_list = new List<TextEdit>();

        private EventHandler watch_text_change_state_handler;

        private WatchTextChangedListener notify_text_changed;

        public WatchTextChangedListener SetWatchTextChangedListener
        {
            set { notify_text_changed = value; }
        }


        public WatchTextController()
        {
            watch_text_change_state_handler = new EventHandler(WatchTextChangeState);
        }

        public void WatchTextEdit(TextEdit text_edit)
        {
            WrapWatchControlObj watchControlObj = new WrapWatchControlObj(text_edit);
            watchControlObj.saved_text_edit_value = text_edit.Text;

            watch_text_edit_list.Add(text_edit);
            wrap_watch_text_edit_list.Add(watchControlObj);

            text_edit.TextChanged += watch_text_change_state_handler;
        }

        private void WatchTextChangeState(object obj, EventArgs eventArgs)
        {
            TextEdit text_edit = (TextEdit)obj;
            UpdateTextState(text_edit, null, false);
        }

        public void UpdateTextState(TextEdit text_edit, string saved_edit_text_value, bool is_update_edit_text_value = true)
        {
            int index = watch_text_edit_list.IndexOf(text_edit);
            if (text_edit == null || index == -1)
            {
                return;
            }

            WrapWatchControlObj watchControlObj = wrap_watch_text_edit_list[index];
            if (is_update_edit_text_value)
            {
                watchControlObj.saved_text_edit_value = saved_edit_text_value;
                watchControlObj.text_edit.Text = saved_edit_text_value;
            }

            watchControlObj.is_text_edit_value_changed = !watchControlObj.text_edit.Text.Equals(watchControlObj.saved_text_edit_value);
            if (watchControlObj.is_text_edit_value_changed)
            {
                if (!cache_watch_text_edit_changed_list.Contains(text_edit))
                {
                    cache_watch_text_edit_changed_list.Add(text_edit);
                }
                text_edit.ForeColor = System.Drawing.Color.Chocolate;
            }
            else
            {
                cache_watch_text_edit_changed_list.Remove(text_edit);
                text_edit.ForeColor = System.Drawing.Color.Black;
            }

            if (notify_text_changed != null) {
                notify_text_changed.Invoke(cache_watch_text_edit_changed_list.Count);
            }
        }

        private class WrapWatchControlObj
        {
            public TextEdit text_edit;
            public string saved_text_edit_value;
            public bool is_text_edit_value_changed;

            public WrapWatchControlObj(TextEdit text_edit)
            {
                this.text_edit = text_edit;
            }
        }
    }
}

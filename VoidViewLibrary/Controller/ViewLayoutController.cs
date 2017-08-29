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
//1                                                                                                                                           10               

using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using VoidViewLibrary.Entity;

namespace VoidViewLibrary.Controller
{
    public class ViewLayoutController
    {
        private PanelControl panelControl;
        private List<ViewLayoutObj> viewLayoutObjList;
        private WatchTextController watchTextController;

        private int unit_max_width = 0;

        public ViewLayoutController(WatchTextController watch_text_controller)
        {
            watchTextController = watch_text_controller;
        }

        public PanelControl CreateLayout(string fileNameNoExtension)
        {
            panelControl = new PanelControl();
            List<ViewLayoutObj> viewLayoutObjList = ViewLayoutParser.ParseFile(fileNameNoExtension + ".xml");
            // panelControl.Controls.Add(this.label_frequency_warning_info);
            int x = 10, y = 10, max_width = 0;
            for (int i = 0; i < viewLayoutObjList.Count; ++i)
            {
                viewLayoutObjList[i].total_width = x * 2;
                y += CreateItemControl(panelControl, x, y, viewLayoutObjList[i], i, i == 0 ? viewLayoutObjList : null);
                y += 2;
                max_width = Math.Max(viewLayoutObjList[i].total_width, max_width);
            }
            y -= 2;
            panelControl.Location = new System.Drawing.Point(0, 0);
            panelControl.LookAndFeel.UseDefaultLookAndFeel = false;
            panelControl.Name = "panelControl";
            panelControl.Size = new System.Drawing.Size(max_width, y + 10);
            panelControl.TabIndex = 0;
            panelControl.SuspendLayout();

            this.viewLayoutObjList = viewLayoutObjList;

            return panelControl;
        }

        public List<ViewLayoutObj> ViewLayoutObjList
        {
            get { return viewLayoutObjList; }
        }

        public LabelControl UpdatePanelControlLabel(Control.ControlCollection parentControlCollection, PanelControl panelControl, string panelName, int location = -1)
        {
            LabelControl labelControl = new LabelControl();
            Graphics graphics = labelControl.CreateGraphics();
            SizeF sizeF = graphics.MeasureString(panelName, labelControl.Font);
            int width = (int)sizeF.Width;
            int height = (int)sizeF.Height;

            if (-1 == location)
            {
                labelControl.Location = new System.Drawing.Point(panelControl.Location.X + 10, panelControl.Location.Y - height / 2);
            }
            else
            {
                labelControl.Location = new System.Drawing.Point(panelControl.Location.X + (panelControl.Width - width) / 2, panelControl.Location.Y - height / 2);
            }
            labelControl.Size = new System.Drawing.Size(width, height);
            labelControl.Name = "labelControl";
            labelControl.Text = panelName;
            //labelControl.BackColor = Color.Transparent;
            labelControl.Appearance.BackColor = System.Drawing.Color.Transparent;
            // labelControl.Appearance.Options.UseBackColor = true;

            parentControlCollection.Add(labelControl);
            labelControl.BringToFront();

            return labelControl;
        }

        private int CreateItemControl(PanelControl panelControl, int x, int y, ViewLayoutObj viewLayoutObj, int index, List<ViewLayoutObj> view_obj_list = null)
        {
            int LabelHeight = 14, LabelWidth = viewLayoutObj.label_width == 0 ? 140 : viewLayoutObj.label_width;
            int EditHeight = 20, EditWidth = viewLayoutObj.edit_width == 0 ? 84 : viewLayoutObj.edit_width;
            int distanceY = (EditHeight - LabelHeight) / 2;

            LabelControl labelControl = new LabelControl();
            TextEdit textEdit = new DevExpress.XtraEditors.TextEdit();

            viewLayoutObj.label_name = labelControl;
            viewLayoutObj.edit_value = textEdit;

            panelControl.Controls.Add(labelControl);
            panelControl.Controls.Add(textEdit);

            int labelY = y + distanceY;

            labelControl.Location = new System.Drawing.Point(x, labelY);
            labelControl.Name = "nameLabelControl";
            labelControl.AutoSizeMode = LabelAutoSizeMode.None;
            labelControl.Size = new System.Drawing.Size(LabelWidth, LabelHeight);
            labelControl.TabIndex = index + 1;
            labelControl.Text = viewLayoutObj.label_text;

            x += labelControl.Size.Width;

            textEdit.Location = new System.Drawing.Point(x, y);
            textEdit.Name = "textEdit";
            //textEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            //textEdit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            textEdit.Size = new System.Drawing.Size(EditWidth, EditHeight);
            textEdit.TabIndex = index + 1;
            textEdit.ReadOnly = viewLayoutObj.read_only;
            // textEdit.Enabled = !viewLayoutObj.read_only;
            SetViewState(textEdit, viewLayoutObj);
            textEdit.Text = viewLayoutObj.default_value;
            if (!textEdit.ReadOnly)
            {
                if (watchTextController != null)
                {
                    watchTextController.WatchTextEdit(textEdit);
                    watchTextController.UpdateTextState(textEdit, textEdit.Text);
                }
            }
            else
            {
                //textEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            }

            viewLayoutObj.total_width += labelControl.Size.Width + textEdit.Size.Width;

            if (view_obj_list != null)
            {
                unit_max_width = view_obj_list[0].unit_width;
                if (unit_max_width == 0)
                {
                    LabelControl unitLabelControl = new LabelControl();

                    for (int i = view_obj_list.Count - 1; i >= 0; --i)
                    {
                        if (view_obj_list[i].unit != null && view_obj_list[i].unit.Length != 0)
                        {
                            Graphics graphics = unitLabelControl.CreateGraphics();
                            SizeF sizeF = graphics.MeasureString(view_obj_list[i].unit, unitLabelControl.Font);
                            unit_max_width = (int)Math.Max(sizeF.Width + 2, unit_max_width);
                        }
                    }
                }
            }

            if (viewLayoutObj.unit != null && viewLayoutObj.unit.Length != 0) // 吴宇森
            {
                x += textEdit.Size.Width;
                LabelControl unitLabelControl = new LabelControl();
                panelControl.Controls.Add(unitLabelControl);

                Graphics graphics = unitLabelControl.CreateGraphics();
                SizeF sizeF = graphics.MeasureString(viewLayoutObj.unit, unitLabelControl.Font);
                int width = unit_max_width;//(int) sizeF.Width + 2;
                int height = (int) sizeF.Height + 2;


                unitLabelControl.Location = new System.Drawing.Point(x, labelY);
                unitLabelControl.Name = "unitLabelControl";
                unitLabelControl.AutoSizeMode = LabelAutoSizeMode.None;
                unitLabelControl.Size = new System.Drawing.Size(width, LabelHeight);
                unitLabelControl.TabIndex = index + 1;
                unitLabelControl.Text = viewLayoutObj.unit;
                unitLabelControl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                unitLabelControl.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

                viewLayoutObj.total_width += width;

                viewLayoutObj.label_unit = unitLabelControl;
            }
            else
            {
                viewLayoutObj.total_width += unit_max_width;
            }


            return textEdit.Size.Height;
        }

        private void SetViewState(TextEdit textEdit, ViewLayoutObj viewLayoutObj)
        {
            if (viewLayoutObj.input_type == null)
            {
                return;
            }

            switch(viewLayoutObj.input_type)
            {
                case "numeric":
                    textEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                    textEdit.Properties.Mask.EditMask = viewLayoutObj.edit_mask;
                    textEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
                    break;
                case "date_time":
                    if (viewLayoutObj.edit_mask == "00时00分")
                    {
                        textEdit.Properties.Mask.EditMask = "(0?\\d|1\\d|2[0-3])\\时[0-5]\\d分";
                        textEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                        textEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
                    }
                    else if (viewLayoutObj.edit_mask == "00:00:00")
                    {
                        textEdit.Properties.Mask.EditMask = "T";
                        textEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
                        textEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
                    }
                    
                    break;
                case "RegEx":
                    textEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                    textEdit.Properties.Mask.EditMask = viewLayoutObj.edit_mask;
                    textEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
                    break;
            }
        }
    }

    public class ViewLayoutParser
    {
        public static List<ViewLayoutObj> ParseFile(string fileName)
        {
            string szConfigFile = Path.Combine(Directory.GetCurrentDirectory(), "layout", fileName);
            if (!File.Exists(szConfigFile))
            {
                return new List<ViewLayoutObj>();
            }

            XmlDocument clsXmlDoc = GetXmlDocument(szConfigFile);
            if (clsXmlDoc == null)
            {
                return new List<ViewLayoutObj>();
            }

            XmlNodeList xmlNodeList = clsXmlDoc.SelectNodes("layout");
            int count = xmlNodeList.Count;
            XmlNode xmlNode = xmlNodeList.Item(0);
            XmlNodeList childXmlNodeList = xmlNode.SelectNodes("layout");
            List<ViewLayoutObj> viewLayoutObjList = new List<ViewLayoutObj>();

            for (int i = 0; i < childXmlNodeList.Count; ++i)
            {
                ViewLayoutObj viewLayoutObj = new ViewLayoutObj();
                XmlNode childXmlNode = childXmlNodeList.Item(i);
                for (int aI = 0; aI < childXmlNode.Attributes.Count; ++aI)
                {
                    string name = childXmlNode.Attributes[aI].Name;
                    string value = childXmlNode.Attributes[aI].Value;
                    // 可用反射
                    switch (name)
                    {
                        case "class_name":
                            viewLayoutObj.class_name = value;
                            break;
                        case "label_text":
                            viewLayoutObj.label_text = value;
                            break;
                        case "default_value":
                            viewLayoutObj.default_value = value;
                            break;
                        case "input_type":
                            viewLayoutObj.input_type = value;
                            break;
                        case "edit_mask":
                            viewLayoutObj.edit_mask = value;
                            break;
                        case "unit":
                            viewLayoutObj.unit = value;
                            break;
                        case "label_width":
                            int.TryParse(value, out viewLayoutObj.label_width);
                            break;
                        case "edit_width":
                            int.TryParse(value, out viewLayoutObj.edit_width);
                            break;
                        case "unit_width":
                            int.TryParse(value, out viewLayoutObj.unit_width);
                            break;
                        case "read_only":
                            bool.TryParse(value, out viewLayoutObj.read_only);
                            break;
                        case "width":
                            int.TryParse(value, out viewLayoutObj.width);
                            break;
                        case "height":
                            int.TryParse(value, out viewLayoutObj.height);
                            break;
                    }
                }
                viewLayoutObjList.Add(viewLayoutObj);
            }

            return viewLayoutObjList;
        }

        private static XmlDocument GetXmlDocument(string szXmlFile)
        {
            if (IsEmptyString(szXmlFile))
                return null;
            if (!File.Exists(szXmlFile))
                return null;
            XmlDocument clsXmlDoc = new XmlDocument();
            try
            {
                clsXmlDoc.Load(szXmlFile);
            }
            catch
            {
                return null;
            }
            return clsXmlDoc;
        }

        private static bool IsEmptyString(string szXmlFile)
        {
            return szXmlFile == null || szXmlFile.Length == 0;
        }
    }
}

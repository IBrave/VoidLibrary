using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoidViewLibrary.Entity
{
    public class ViewLayoutObj
    {
        // >> Init State >>
        public string class_name;
        public string label_text;
        public string default_value;
        public string input_type;
        public string edit_mask;
        public string unit;
        public bool read_only;

        public int width;
        public int height;

        public int total_width;
        // << Init State <<

        public LabelControl label_name;
        public TextEdit edit_value;
        public LabelControl label_unit;

        public string saved_edit_value;
        public bool is_edit_value_changed;

        public int LabelUnitWidth
        {
            set
            {
                if (label_unit != null)
                {
                    label_unit.Size = new System.Drawing.Size(value, label_unit.Size.Height);
                }
            }
            get
            {
                if (label_unit != null)
                {
                    return label_unit.Size.Width;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}

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
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraGrid.Menu;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoidViewLibrary.Helper
{
    public class GridViewParams
    {
        public GridViewParams()
        {

        }

        public GridViewParams(Point point, Size size, string title, List<DataColumn> data_columns, List<bool> edits_states, bool add_new_rows = false)
        {
            _point = point;
            _size = size;
            _title = title;
            _data_columns = data_columns;
            _data_columns_allow_edit_state = edits_states;
            _allow_add_new_rows = add_new_rows;
        }

        public Point _point;
        public Size _size;
        public string _title;
        public List<DataColumn> _data_columns;
        public List<bool> _data_columns_allow_edit_state = new List<bool>();
        public bool _allow_add_new_rows;
    }

    public class GridViewHelper
    {
        private DevExpress.XtraGrid.GridControl _grid_control;
        public DevExpress.XtraGrid.Views.Grid.GridView _grid_view;
        public DataTable _data_table;

        public GridControl CreateUnbGridView(GridViewParams grid_view_params)
        {
            _grid_control = new DevExpress.XtraGrid.GridControl();
            _grid_view = new DevExpress.XtraGrid.Views.Grid.GridView();

            _grid_view.Name = "grid_view";

            _grid_view.OptionsView.ShowGroupPanel = false;

            _grid_control.MainView = _grid_view;
            _grid_control.Size = grid_view_params._size;
            _grid_control.Location = grid_view_params._point;

            _grid_view.GridControl = _grid_control;

            InitGridView(_grid_view);

            _grid_view.OptionsBehavior.Editable = true;
            _grid_view.OptionsSelection.MultiSelect = true;
            _grid_view.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
            _grid_view.OptionsSelection.InvertSelection = true; // 一行还是单个Cell
            _grid_view.OptionsSelection.EnableAppearanceFocusedCell = true;
            _grid_view.OptionsView.NewItemRowPosition = grid_view_params._allow_add_new_rows ? NewItemRowPosition.Bottom : NewItemRowPosition.None;
            _grid_view.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

            return _grid_control;
        }

        public GridControl Create(GridViewParams grid_view_params)
        {
            _grid_control = new DevExpress.XtraGrid.GridControl();
            _grid_view = CreateGridView();

            _grid_view.GroupPanelText = grid_view_params._title;

            _grid_control.Location = grid_view_params._point;
            _grid_control.MainView = _grid_view;
            _grid_control.Name = "grid_control";
            _grid_control.Size = grid_view_params._size;
            _grid_control.TabIndex = 0;
            _grid_control.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            _grid_view});

            DataTable data_table = new DataTable();
            data_table.TableName = "table_name";
            data_table.Columns.AddRange(grid_view_params._data_columns.ToArray());

            BindData(data_table, _grid_view);

            InitGridView(_grid_view);
            _grid_view.OptionsBehavior.Editable = true;
            //设置列表是否多选、多选的模式（行或单元格）、选择的行或单元格背景是否倒置
            _grid_view.OptionsSelection.MultiSelect = true;
            _grid_view.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
            _grid_view.OptionsSelection.InvertSelection = true; // 一行还是单个Cell
            _grid_view.OptionsSelection.EnableAppearanceFocusedCell = true;
            _grid_view.OptionsView.NewItemRowPosition = grid_view_params._allow_add_new_rows ? NewItemRowPosition.Bottom : NewItemRowPosition.None;
            // _grid_view.Columns[0].AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            _grid_view.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

            new GridViewFunction().AddPasteColumn(_grid_control, _data_table);

            for (int i = grid_view_params._data_columns_allow_edit_state.Count - 1; i >= 0; --i)
            {
                _grid_view.Columns[i].OptionsColumn.AllowEdit = grid_view_params._data_columns_allow_edit_state[i];
            }

            return _grid_control;
        }

        public void UpdateColumnAllowEditState(int index, bool allow_edit)
        {
            _grid_view.Columns[index].OptionsColumn.AllowEdit = allow_edit;
        }

        public void UseEmbeddedNavigator(bool use)
        {
            _grid_control.UseEmbeddedNavigator = use;
        }

        public GridControl Create()
        {
            _grid_control = new DevExpress.XtraGrid.GridControl();
            _grid_view = CreateGridView();
            GridView grid_view_2 = CreateGridView();

            _grid_control.Location = new System.Drawing.Point(407, 41);
            _grid_control.MainView = _grid_view;
            _grid_control.Name = "grid_control";
            _grid_control.Size = new System.Drawing.Size(455, 200);
            _grid_control.TabIndex = 0;
            _grid_control.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            _grid_view});

            BindData(CreateDataTable(), _grid_view);
            // BindData(CreateDataTable(), grid_view_2);

            InitGridView(_grid_view);

            _grid_view.OptionsBehavior.Editable = true;
            //设置列表是否多选、多选的模式（行或单元格）、选择的行或单元格背景是否倒置
            _grid_view.OptionsSelection.MultiSelect = true;
            _grid_view.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
            _grid_view.OptionsSelection.InvertSelection = false;
            _grid_view.OptionsSelection.EnableAppearanceFocusedCell = true;
            _grid_view.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;

            _grid_view.GroupPanelText = "T";

            // _data_table.Columns[0].ReadOnly = true;
            Console.WriteLine(_grid_view.Columns.Count);

            new GridViewFunction().AddPasteColumn(_grid_control, _data_table);

            _grid_view.Columns[0].OptionsColumn.AllowEdit = true;
            _grid_view.Columns[1].OptionsColumn.AllowEdit = true;
            
            return _grid_control;
        }

        private GridView CreateGridView() 
        {
            GridView grid_view = new DevExpress.XtraGrid.Views.Grid.GridView();
            grid_view.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { new GridColumn()});
            grid_view.GridControl = _grid_control;
            grid_view.Name = "grid_view";
            return grid_view;
        }

        public void BindData(DataTable data_talbe, GridView grid_view)
        {
            _data_table = data_talbe;
            _grid_control.DataSource = _data_table;
            // grid_view.PopulateColumns(); 不能立即创建Columns
            grid_view.PopulateColumns(_data_table);
        }

        private DataTable CreateDataTable()
        {
            DataTable data_table = new DataTable();
            data_table.TableName = "table_name";
            data_table.Columns.Add(new DataColumn("1", typeof(string)));
            data_table.Columns.Add(new DataColumn("2", typeof(string)));
            DataColumn check_box_column = new DataColumn("3", typeof(bool));
            data_table.Columns.Add(check_box_column);

            data_table.Rows.Add(new object[] { 1, 2, true});
            data_table.Rows.Add(new object[] { 11, 12, true });
            data_table.Rows.Add(new object[] { 21, 22, false });
            data_table.Rows.Add(new object[] { 1, 2, true });
            data_table.Rows.Add(new object[] { 11, 12, true });
            data_table.Rows.Add(new object[] { 21, 22, true });

            return data_table;
        }

        public void AddRowsValueForTable(int row_index, object[] objs, int head_column_index = 0)
        {
            DataTable data_table = _data_table;
            int column_count = data_table.Columns.Count;
            int row_count;
            while (row_index >= (row_count = data_table.Rows.Count))
            {
                data_table.Rows.Add(data_table.NewRow());
            }

            int insert_value_num = Math.Min(column_count, objs.Length + head_column_index);
            for (int i = head_column_index; i < insert_value_num; ++i)
            {
                data_table.Rows[row_index][i] = objs[i - head_column_index];
            }
        }

        /// <summary>
        /// https://zhidao.baidu.com/question/1174521530470594219.html
        /// </summary>
        /// <param name="grid_view"></param>
        private void InitGridView(GridView grid_view)
        {
            /*
            grid_view.BackColorFixed = Color.FromArgb(90, 158, 214);
            grid_view.BackColorFixedSel = Color.FromArgb(110, 180, 230);
            grid_view.BackColorBkg = Color.FromArgb(90, 158, 214);
            grid_view.BackColor1 = Color.FromArgb(231, 235, 247);
            grid_view.BackColor2 = Color.FromArgb(239, 243, 255);
            grid_view.CellBorderColorFixed = Color.Black;
            grid_view.GridColor = Color.FromArgb(148, 190, 231);
             * **/

            //grid_view.Appearance.Row.BackColor = Color.FromArgb(231, 235, 247);
            //grid_view.Appearance.Row.BackColor2 = Color.FromArgb(239, 243, 255);
            grid_view.Appearance.Row.Options.UseBackColor = true;

            grid_view.Appearance.Row.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            grid_view.Appearance.Row.Options.UseForeColor = true;
            grid_view.Appearance.Row.Options.UseTextOptions = true;
            grid_view.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            grid_view.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

            Color selected_color = Color.FromArgb(120, SystemColors.Highlight.R, SystemColors.Highlight.G, SystemColors.Highlight.B);
            grid_view.Appearance.SelectedRow.BackColor = selected_color;
            grid_view.Appearance.SelectedRow.BackColor2 = selected_color;
            grid_view.Appearance.SelectedRow.Options.UseBackColor = false;

            grid_view.Appearance.OddRow.BackColor = Color.FromArgb(231, 235, 247);
            grid_view.Appearance.OddRow.BackColor2 = Color.FromArgb(231, 235, 247);
            grid_view.Appearance.OddRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            grid_view.Appearance.OddRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            grid_view.Appearance.OddRow.Options.UseBackColor = true;
            grid_view.Appearance.OddRow.Options.UseTextOptions = true;

            grid_view.Appearance.EvenRow.BackColor = Color.FromArgb(239, 243, 255);
            grid_view.Appearance.EvenRow.BackColor2 = Color.FromArgb(239, 243, 255);
            grid_view.Appearance.EvenRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            grid_view.Appearance.EvenRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            grid_view.Appearance.EvenRow.Options.UseBackColor = true;
            grid_view.Appearance.EvenRow.Options.UseTextOptions = false;

            grid_view.OptionsView.EnableAppearanceEvenRow = true;
            grid_view.OptionsView.EnableAppearanceOddRow = true;

            Color focused_color = Color.FromArgb(255, selected_color.R, selected_color.G, selected_color.B);
            grid_view.Appearance.FocusedRow.BackColor = focused_color;
            grid_view.Appearance.FocusedRow.BackColor2 = focused_color;
            grid_view.Appearance.FocusedRow.BorderColor = System.Drawing.Color.White;
            grid_view.Appearance.FocusedRow.Options.UseBackColor = false;
            grid_view.Appearance.FocusedRow.Options.UseBorderColor = true;

            grid_view.Appearance.GroupRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            grid_view.Appearance.GroupRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            grid_view.Appearance.GroupRow.Options.UseTextOptions = true;

            grid_view.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.White;
            grid_view.Appearance.HideSelectionRow.BackColor2 = System.Drawing.Color.White;
            grid_view.Appearance.HideSelectionRow.Options.UseBackColor = true;

            grid_view.Appearance.GroupPanel.Options.UseTextOptions = true;
            grid_view.Appearance.GroupPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            grid_view.Appearance.GroupPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

            // 标题列
            grid_view.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            grid_view.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            grid_view.Appearance.HeaderPanel.Options.UseTextOptions = true;

            grid_view.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;

            grid_view.OptionsBehavior.Editable = false;
            grid_view.OptionsBehavior.ReadOnly = false;
            grid_view.OptionsFind.AllowFindPanel = false;
            grid_view.OptionsSelection.UseIndicatorForSelection = false;
            // grid_view.OptionsView.ShowColumnHeaders = false;
            grid_view.OptionsMenu.EnableColumnMenu = false;
            grid_view.OptionsView.ShowIndicator = false;
            grid_view.OptionsCustomization.AllowColumnMoving = false;
            grid_view.OptionsCustomization.AllowSort = false;
            grid_view.OptionsCustomization.AllowFilter = false;
            // gridView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never;
            // 不显示右键菜单
            /*
            grid_view.PopupMenuShowing += new PopupMenuShowingEventHandler((object sender, PopupMenuShowingEventArgs e) =>
            {
                if (e.MenuType == GridMenuType.Group || e.MenuType == GridMenuType.Column)
                {
                    // GridViewColumnMenu
                    // GridViewGroupPanelMenu
                    GridViewMenu gridViewMenu = e.Menu as GridViewMenu;
                    if (gridViewMenu == null)
                    {
                        return;
                    }

                    foreach (DXMenuItem menuItem in gridViewMenu.Items)
                    {
                        if (menuItem.Caption.Equals(GridLocalizer.Active.GetLocalizedString(GridStringId.MenuColumnGroup))
                            || menuItem.Caption.Equals(GridLocalizer.Active.GetLocalizedString(GridStringId.MenuColumnGroupBox)))
                        {
                        }
                        menuItem.Visible = false;
                    }
                }
            });
             * **/
        }

        public static void InitBaseForGridView(GridView grid_view)
        {
            grid_view.Appearance.Row.Options.UseBackColor = true;

            grid_view.Appearance.Row.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            grid_view.Appearance.Row.Options.UseForeColor = true;
            grid_view.Appearance.Row.Options.UseTextOptions = true;
            grid_view.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            grid_view.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

            Color selected_color = Color.FromArgb(120, SystemColors.Highlight.R, SystemColors.Highlight.G, SystemColors.Highlight.B);
            grid_view.Appearance.SelectedRow.BackColor = selected_color;
            grid_view.Appearance.SelectedRow.BackColor2 = selected_color;
            grid_view.Appearance.SelectedRow.Options.UseBackColor = false;

            grid_view.Appearance.OddRow.BackColor = Color.FromArgb(231, 235, 247);
            grid_view.Appearance.OddRow.BackColor2 = Color.FromArgb(231, 235, 247);
            grid_view.Appearance.OddRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            grid_view.Appearance.OddRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            grid_view.Appearance.OddRow.Options.UseBackColor = true;
            grid_view.Appearance.OddRow.Options.UseTextOptions = true;

            grid_view.Appearance.EvenRow.BackColor = Color.FromArgb(239, 243, 255);
            grid_view.Appearance.EvenRow.BackColor2 = Color.FromArgb(239, 243, 255);
            grid_view.Appearance.EvenRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            grid_view.Appearance.EvenRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            grid_view.Appearance.EvenRow.Options.UseBackColor = true;
            grid_view.Appearance.EvenRow.Options.UseTextOptions = false;

            grid_view.OptionsView.EnableAppearanceEvenRow = true;
            grid_view.OptionsView.EnableAppearanceOddRow = true;

            Color focused_color = Color.FromArgb(255, selected_color.R, selected_color.G, selected_color.B);
            grid_view.Appearance.FocusedRow.BackColor = focused_color;
            grid_view.Appearance.FocusedRow.BackColor2 = focused_color;
            grid_view.Appearance.FocusedRow.BorderColor = System.Drawing.Color.White;
            grid_view.Appearance.FocusedRow.Options.UseBackColor = false;
            grid_view.Appearance.FocusedRow.Options.UseBorderColor = true;

            grid_view.Appearance.GroupRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            grid_view.Appearance.GroupRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            grid_view.Appearance.GroupRow.Options.UseTextOptions = true;

            grid_view.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.White;
            grid_view.Appearance.HideSelectionRow.BackColor2 = System.Drawing.Color.White;
            grid_view.Appearance.HideSelectionRow.Options.UseBackColor = true;

            grid_view.Appearance.GroupPanel.Options.UseTextOptions = true;
            grid_view.Appearance.GroupPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            grid_view.Appearance.GroupPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

            // 标题列
            grid_view.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            grid_view.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            grid_view.Appearance.HeaderPanel.Options.UseTextOptions = true;

            grid_view.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;

            grid_view.OptionsBehavior.Editable = false;
            grid_view.OptionsBehavior.ReadOnly = false;
            grid_view.OptionsFind.AllowFindPanel = false;
            grid_view.OptionsSelection.UseIndicatorForSelection = false;
            // grid_view.OptionsView.ShowColumnHeaders = false;
            grid_view.OptionsMenu.EnableColumnMenu = false;
            grid_view.OptionsView.ShowIndicator = false;
            grid_view.OptionsCustomization.AllowColumnMoving = false;
            grid_view.OptionsCustomization.AllowSort = false;
            grid_view.OptionsCustomization.AllowFilter = false;
        }

        public class GridViewFunction
        {
            private GridView _grid_view;
            private DataTable _data_table;

            private KeyEventHandler _key_down_event_handler;

            public GridViewFunction()
            {
                _key_down_event_handler = new KeyEventHandler(KeyDownListener);
            }

            public void AddPasteColumn(GridControl grid_control, DataTable data_table)
            {
                _grid_view = grid_control.ViewCollection[0] as GridView;
                grid_control.KeyDown += _key_down_event_handler;
                _data_table = data_table;
            }

            public void RemovePasteColumn(GridControl grid_control)
            {
                grid_control.KeyDown -= _key_down_event_handler;
            }

            private void KeyDownListener(object sender, KeyEventArgs e)
            {
                if (_grid_view.OptionsBehavior.ReadOnly)
                {
                    return;
                }

                if ((e.Control == true) && e.KeyCode == Keys.C)
                {
                    //_cells = CopyDatas((sender as GridControl).FocusedView as GridView);
                    //_isControlV = false;
                }
                else if ((e.Control == true) && e.KeyCode == Keys.V)
                
                {
                    IDataObject data_object = Clipboard.GetDataObject();
                    e.Handled = PasteClipboard((sender as GridControl).FocusedView as GridView, _data_table);
                    e.SuppressKeyPress = e.Handled;
                }
                else if ((e.Control == true) && e.KeyCode == Keys.Z)
                {
                }
            }

            private bool PasteClipboard(GridView data_grid_view, DataTable data_table)
            {
                bool paste_result = false;
                if (data_grid_view == null)
                {
                    return paste_result;
                }

                int[] selected_rows_handles = data_grid_view.GetSelectedRows();
                if (selected_rows_handles == null || selected_rows_handles.Length == 0)
                {
                    return paste_result;
                }
                int selected_row_handle_head = selected_rows_handles[0];
                GridColumn[] grid_columns = null;
                for (int i = 0; i < selected_rows_handles.Length; ++i)
                {
                     grid_columns = data_grid_view.GetSelectedCells(selected_row_handle_head);
                     if (grid_columns.Length > 0)
                     {
                         break;
                     }
                }
                GridColumn focused_column = data_grid_view.FocusedColumn;

                int visible_row_index = data_grid_view.GetVisibleIndex(selected_row_handle_head);
                int visible_column_index = focused_column.AbsoluteIndex;// grid_columns[0].VisibleIndex;

                int visible_row_num = data_table.Rows.Count;
                int visible_column_num = data_table.Columns.Count;

                DataObject o = (DataObject)Clipboard.GetDataObject();
                if (o.GetDataPresent(DataFormats.Text))
                {
                    Console.WriteLine(o.GetData(DataFormats.Text).ToString());
                    string[] pastedRows = Regex.Split(o.GetData(DataFormats.Text).ToString().TrimEnd("\r\n".ToCharArray()), "\r\n");

                    int r_index = visible_row_index;
                    foreach (string pastedRow in pastedRows)
                    {
                        if (r_index >= visible_row_num)
                        {
                            data_table.Rows.Add(new object[visible_column_num]);
                        }

                        string[] pasted_row_column_values = pastedRow.Split(new char[] { '\t' });

                        for (int c_index = 0; c_index < pasted_row_column_values.Length; ++c_index)
                        {
                            if (c_index + visible_column_index >= visible_column_num)
                            {
                                break;
                            }
                            try
                            {
                                data_table.Rows[r_index][c_index + visible_column_index] = pasted_row_column_values[c_index];
                            } catch (Exception ex){
                                Console.WriteLine(ex.ToString());
                            }
                        }

                        r_index++;
                    }
                }

                paste_result = true;
                return paste_result;
            }
        }
    }
}

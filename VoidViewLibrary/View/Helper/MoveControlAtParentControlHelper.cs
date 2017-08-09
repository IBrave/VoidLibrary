using System;
using System.Drawing;
using System.Windows.Forms;

namespace VoidViewLibrary.View.Helper
{
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

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace VoidViewLibrary.View.Helper
{
    public class DrawRotateCircleHelper
    {
        private Color _circle_color = Color.FromArgb(255, 1, 207, 19);
        private Image _org_rotate_image;
        private float _rotate_angle;

        private PictureBox _picture_box;
        private System.Windows.Forms.Timer _rotation_bitmap_timer;

        public DrawRotateCircleHelper(PictureBox picture_box)
        {
            _picture_box = picture_box;
            _rotation_bitmap_timer = new System.Windows.Forms.Timer();
            _rotation_bitmap_timer.Tick += new System.EventHandler(this.Rotation_Bitmap_Timer_Tick);
            _rotation_bitmap_timer.Interval = 40;
        }

        public Color CircleColor
        {
            get { return _circle_color; }
            set
            {
                _circle_color = value;
            }
        }

        private void Rotation_Bitmap_Timer_Tick(object sender, EventArgs e)
        {
            MakeRotateBitmap();
            _picture_box.Image = RotateImageByAngle(_org_rotate_image, _rotate_angle);
            _rotate_angle += 360 / 60;
        }

        public void Start()
        {
            _rotation_bitmap_timer.Start();
        }

        public void Stop()
        {
            _rotation_bitmap_timer.Stop();
        }

        private void MakeRotateBitmap()
        {
            int Width = _picture_box.Width;
            int Height = _picture_box.Height;

            if (_org_rotate_image == null)
            {
                // Bitmap map = new Bitmap(DrawRotateBitmap(), new Size(Width, Height)); // 嵌套后就居中了
                _org_rotate_image = (Image)DrawRotateBitmap();
                _picture_box.Image = _org_rotate_image;
            }
            _picture_box.Size = _picture_box.Image.Size;

        }

        private Bitmap DrawRotateBitmap()
        {
            const int point_num = 12;
            Bitmap bitmap = new Bitmap(_picture_box.Width, _picture_box.Height);
            Graphics graphics = Graphics.FromImage(bitmap);

            int Width = bitmap.Width;
            int Height = bitmap.Height;
            float every_circle_rotate_angle = 360.0F / point_num;

            graphics.TranslateTransform(bitmap.Width / 2.0F, bitmap.Height / 2.0F);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            graphics.RotateTransform(-90);

            int max_circle_diameter = (int)(Width * 0.5F * 0.26);
            for (int i = 0; i < point_num; i++)
            {
                int alpha = 255 - (int)(200 * (float)i / point_num);
                Color draw_color = Color.FromArgb(alpha, _circle_color);
                using (SolidBrush brush = new SolidBrush(draw_color))
                {
                    float cur_circle_diameter = max_circle_diameter * (1 - (1.0F) / (point_num + 0.8F * point_num) * i);
                    float x = Width * 0.5F - cur_circle_diameter - 5;
                    graphics.FillEllipse(brush, x, -cur_circle_diameter / 2, cur_circle_diameter, cur_circle_diameter);
                }
                graphics.RotateTransform(every_circle_rotate_angle);
                // graphics.DrawLine(new Pen(new SolidBrush(Color.Blue)), new Point(0, 0), new Point(0, Width));
            }
            return bitmap;
        }

        private Bitmap RotateImageByAngle(Image oldBitmap, float angle)
        {
            var newBitmap = new Bitmap(oldBitmap.Width, oldBitmap.Height);
            newBitmap.SetResolution(oldBitmap.HorizontalResolution, oldBitmap.VerticalResolution);
            var graphics = Graphics.FromImage(newBitmap);
            graphics.TranslateTransform((float)oldBitmap.Width / 2, (float)oldBitmap.Height / 2);
            graphics.RotateTransform(angle);
            graphics.TranslateTransform(-(float)oldBitmap.Width / 2, -(float)oldBitmap.Height / 2);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.DrawImage(oldBitmap, new Point(0, 0));
            return newBitmap;
        }
    }
}

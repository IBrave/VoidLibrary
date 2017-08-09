namespace SocketHelperDemo.View
{
    partial class LoadingProgress
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoadingProgress));
            this._picture_box = new System.Windows.Forms.PictureBox();
            this._btn_yes = new System.Windows.Forms.Button();
            this._btn_no = new System.Windows.Forms.Button();
            this._label_hint_msg = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._picture_box)).BeginInit();
            this.SuspendLayout();
            // 
            // _picture_box
            // 
            this._picture_box.Location = new System.Drawing.Point(20, 20);
            this._picture_box.Name = "_picture_box";
            this._picture_box.Size = new System.Drawing.Size(60, 60);
            this._picture_box.TabIndex = 0;
            this._picture_box.TabStop = false;
            // 
            // _btn_yes
            // 
            this._btn_yes.Location = new System.Drawing.Point(107, 77);
            this._btn_yes.Name = "_btn_yes";
            this._btn_yes.Size = new System.Drawing.Size(75, 23);
            this._btn_yes.TabIndex = 1;
            this._btn_yes.Text = "Yes";
            this._btn_yes.UseVisualStyleBackColor = true;
            this._btn_yes.Visible = false;
            this._btn_yes.Click += new System.EventHandler(this.Yes_Click);
            // 
            // _btn_no
            // 
            this._btn_no.Location = new System.Drawing.Point(194, 77);
            this._btn_no.Name = "_btn_no";
            this._btn_no.Size = new System.Drawing.Size(75, 23);
            this._btn_no.TabIndex = 1;
            this._btn_no.Text = "No";
            this._btn_no.UseVisualStyleBackColor = true;
            this._btn_no.Visible = false;
            this._btn_no.Click += new System.EventHandler(this.No_Click);
            // 
            // _label_hint_msg
            // 
            this._label_hint_msg.AutoSize = true;
            this._label_hint_msg.Location = new System.Drawing.Point(106, 44);
            this._label_hint_msg.Name = "_label_hint_msg";
            this._label_hint_msg.Size = new System.Drawing.Size(161, 12);
            this._label_hint_msg.TabIndex = 2;
            this._label_hint_msg.Text = "The airbag is inflating...";
            // 
            // LoadingProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 103);
            this.Controls.Add(this._label_hint_msg);
            this.Controls.Add(this._btn_no);
            this.Controls.Add(this._btn_yes);
            this.Controls.Add(this._picture_box);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoadingProgress";
            this.Text = "Prompt";
            ((System.ComponentModel.ISupportInitialize)(this._picture_box)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox _picture_box;
        private System.Windows.Forms.Button _btn_yes;
        private System.Windows.Forms.Button _btn_no;
        public System.Windows.Forms.Label _label_hint_msg;
    }
}
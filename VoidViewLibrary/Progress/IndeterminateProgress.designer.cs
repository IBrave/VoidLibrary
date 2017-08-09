using System.Drawing;
using System.Windows.Forms;
namespace VoidViewLibrary.Progress
{
    partial class IndeterminateProgress
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
            this._rp_progress_dialog = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this._picture_box = new System.Windows.Forms.PictureBox();
            this.label_loading_prompt_msg = new System.Windows.Forms.Label();
            this._rp_progress_dialog.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._picture_box)).BeginInit();
            this.SuspendLayout();
            // 
            // rp_progress_dialog
            // 
            this._rp_progress_dialog.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._rp_progress_dialog.Controls.Add(this.panel1);
            this._rp_progress_dialog.Controls.Add(this.label_loading_prompt_msg);
            this._rp_progress_dialog.Location = new System.Drawing.Point(-2, 0);
            this._rp_progress_dialog.Margin = new System.Windows.Forms.Padding(0);
            this._rp_progress_dialog.Name = "rp_progress_dialog";
            this._rp_progress_dialog.Size = new System.Drawing.Size(220, 112);
            this._rp_progress_dialog.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this._picture_box);
            this.panel1.Location = new System.Drawing.Point(23, 22);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(65, 66);
            this.panel1.TabIndex = 4;
            // 
            // _picture_box
            // 
            this._picture_box.BackColor = System.Drawing.Color.Transparent;
            this._picture_box.Location = new System.Drawing.Point(3, 3);
            this._picture_box.MaximumSize = new System.Drawing.Size(60, 60);
            this._picture_box.MinimumSize = new System.Drawing.Size(60, 60);
            this._picture_box.Name = "_picture_box";
            this._picture_box.Size = new System.Drawing.Size(60, 60);
            this._picture_box.TabIndex = 0;
            this._picture_box.TabStop = false;
            // 
            // label_loading_prompt_msg
            // 
            this.label_loading_prompt_msg.AutoSize = true;
            this.label_loading_prompt_msg.BackColor = System.Drawing.Color.Transparent;
            this.label_loading_prompt_msg.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_loading_prompt_msg.Location = new System.Drawing.Point(105, 48);
            this.label_loading_prompt_msg.Name = "label_loading_prompt_msg";
            this.label_loading_prompt_msg.Size = new System.Drawing.Size(112, 16);
            this.label_loading_prompt_msg.TabIndex = 3;
            this.label_loading_prompt_msg.Text = "请耐心等待...";
            // 
            // IndeterminateProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(217, 110);
            this.ControlBox = false;
            this.Controls.Add(this._rp_progress_dialog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "IndeterminateProgress";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "IndeterminateProgress";
            this.TransparencyKey = System.Drawing.Color.Transparent;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Progress_Closing);
            this.Shown += new System.EventHandler(this.Progress_Shown);
            this._rp_progress_dialog.ResumeLayout(false);
            this._rp_progress_dialog.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._picture_box)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel _rp_progress_dialog;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox _picture_box;
        private System.Windows.Forms.Label label_loading_prompt_msg;
    }
}
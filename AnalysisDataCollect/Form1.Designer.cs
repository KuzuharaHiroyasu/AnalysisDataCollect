﻿namespace AnalysisDataCollect
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.photorefStartButton = new System.Windows.Forms.Button();
            this.path_textbox = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.acceStartButton = new System.Windows.Forms.Button();
            this.apneaStartButton = new System.Windows.Forms.Button();
            this.Counter_label = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.stopButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // photorefStartButton
            // 
            this.photorefStartButton.Location = new System.Drawing.Point(45, 86);
            this.photorefStartButton.Name = "photorefStartButton";
            this.photorefStartButton.Size = new System.Drawing.Size(132, 50);
            this.photorefStartButton.TabIndex = 0;
            this.photorefStartButton.Text = "photorefデータまとめ開始";
            this.photorefStartButton.UseVisualStyleBackColor = true;
            this.photorefStartButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.photorefStartButton_MouseClickAsync);
            // 
            // path_textbox
            // 
            this.path_textbox.AllowDrop = true;
            this.path_textbox.Location = new System.Drawing.Point(43, 51);
            this.path_textbox.Name = "path_textbox";
            this.path_textbox.Size = new System.Drawing.Size(527, 19);
            this.path_textbox.TabIndex = 1;
            this.path_textbox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.path_textbox_MouseClick);
            this.path_textbox.DragDrop += new System.Windows.Forms.DragEventHandler(this.path_textbox_DragDrop);
            this.path_textbox.DragEnter += new System.Windows.Forms.DragEventHandler(this.path_textbox_DragEnter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(325, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "ax\\photoref、ax\\apnea、ax\\acce配下の取得データフォルダを指定";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(408, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "取得データ内で1テキスト1データとなっている演算結果を１つのテキストファイルにまとめる";
            // 
            // acceStartButton
            // 
            this.acceStartButton.Location = new System.Drawing.Point(438, 86);
            this.acceStartButton.Name = "acceStartButton";
            this.acceStartButton.Size = new System.Drawing.Size(132, 50);
            this.acceStartButton.TabIndex = 4;
            this.acceStartButton.Text = "acceデータまとめ開始";
            this.acceStartButton.UseVisualStyleBackColor = true;
            this.acceStartButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.acceStartButton_MouseClickAsync);
            // 
            // apneaStartButton
            // 
            this.apneaStartButton.Location = new System.Drawing.Point(241, 86);
            this.apneaStartButton.Name = "apneaStartButton";
            this.apneaStartButton.Size = new System.Drawing.Size(132, 50);
            this.apneaStartButton.TabIndex = 5;
            this.apneaStartButton.Text = "apneaデータまとめ開始";
            this.apneaStartButton.UseVisualStyleBackColor = true;
            this.apneaStartButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.apneaStartButton_MouseClickAsync);
            // 
            // Counter_label
            // 
            this.Counter_label.AutoSize = true;
            this.Counter_label.Location = new System.Drawing.Point(285, 144);
            this.Counter_label.Name = "Counter_label";
            this.Counter_label.Size = new System.Drawing.Size(15, 12);
            this.Counter_label.TabIndex = 6;
            this.Counter_label.Text = " /";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(45, 159);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(525, 23);
            this.progressBar.TabIndex = 7;
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(474, 13);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(96, 32);
            this.stopButton.TabIndex = 8;
            this.stopButton.Text = "停止";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 194);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.Counter_label);
            this.Controls.Add(this.apneaStartButton);
            this.Controls.Add(this.acceStartButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.path_textbox);
            this.Controls.Add(this.photorefStartButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "データまとめツール";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button photorefStartButton;
        private System.Windows.Forms.TextBox path_textbox;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button acceStartButton;
        private System.Windows.Forms.Button apneaStartButton;
        private System.Windows.Forms.Label Counter_label;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button stopButton;
    }
}


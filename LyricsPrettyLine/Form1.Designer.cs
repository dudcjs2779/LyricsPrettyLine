namespace LyricsPrettyLine
{
    partial class LyricsPrettyLine
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.LoadBtn = new System.Windows.Forms.Button();
            this.LyricsListView1 = new System.Windows.Forms.ListView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.OperateBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.DeleteBtn = new System.Windows.Forms.Button();
            this.ResetBtn = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // LoadBtn
            // 
            this.LoadBtn.Location = new System.Drawing.Point(12, 15);
            this.LoadBtn.Name = "LoadBtn";
            this.LoadBtn.Size = new System.Drawing.Size(130, 70);
            this.LoadBtn.TabIndex = 0;
            this.LoadBtn.Text = "불러오기";
            this.LoadBtn.UseVisualStyleBackColor = true;
            this.LoadBtn.Click += new System.EventHandler(this.LoadBtn_Click);
            // 
            // LyricsListView1
            // 
            this.LyricsListView1.HideSelection = false;
            this.LyricsListView1.Location = new System.Drawing.Point(159, 15);
            this.LyricsListView1.Name = "LyricsListView1";
            this.LyricsListView1.Size = new System.Drawing.Size(417, 360);
            this.LyricsListView1.TabIndex = 2;
            this.LyricsListView1.UseCompatibleStateImageBehavior = false;
            this.LyricsListView1.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.LyricsListView1_ItemSelectionChanged);
            this.LyricsListView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.LyricsListView1_DragDrop);
            this.LyricsListView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.LyricsListView1_DragEnter);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // OperateBtn
            // 
            this.OperateBtn.Location = new System.Drawing.Point(12, 153);
            this.OperateBtn.Name = "OperateBtn";
            this.OperateBtn.Size = new System.Drawing.Size(130, 70);
            this.OperateBtn.TabIndex = 3;
            this.OperateBtn.Text = "변환";
            this.OperateBtn.UseVisualStyleBackColor = true;
            this.OperateBtn.Click += new System.EventHandler(this.OperateBtn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(34, 91);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DeleteBtn
            // 
            this.DeleteBtn.Location = new System.Drawing.Point(12, 229);
            this.DeleteBtn.Name = "DeleteBtn";
            this.DeleteBtn.Size = new System.Drawing.Size(130, 70);
            this.DeleteBtn.TabIndex = 5;
            this.DeleteBtn.Text = "삭제";
            this.DeleteBtn.UseVisualStyleBackColor = true;
            this.DeleteBtn.Click += new System.EventHandler(this.DeleteBtn_Click);
            // 
            // ResetBtn
            // 
            this.ResetBtn.Location = new System.Drawing.Point(12, 305);
            this.ResetBtn.Name = "ResetBtn";
            this.ResetBtn.Size = new System.Drawing.Size(130, 70);
            this.ResetBtn.TabIndex = 6;
            this.ResetBtn.Text = "초기화";
            this.ResetBtn.UseVisualStyleBackColor = true;
            this.ResetBtn.Click += new System.EventHandler(this.ResetBtn_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(34, 122);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 25);
            this.textBox1.TabIndex = 7;
            this.textBox1.Visible = false;
            // 
            // LyricsPrettyLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 398);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.ResetBtn);
            this.Controls.Add(this.DeleteBtn);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.OperateBtn);
            this.Controls.Add(this.LyricsListView1);
            this.Controls.Add(this.LoadBtn);
            this.Name = "LyricsPrettyLine";
            this.Text = "LyricsPrettyLine";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LoadBtn;
        private System.Windows.Forms.ListView LyricsListView1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button OperateBtn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button DeleteBtn;
        private System.Windows.Forms.Button ResetBtn;
        private System.Windows.Forms.TextBox textBox1;
    }
}


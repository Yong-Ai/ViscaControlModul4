namespace ViscaControlModule
{
    partial class Form1
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
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnZoomPlus = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.comboBaudrate = new System.Windows.Forms.ComboBox();
            this.comboProtocol = new System.Windows.Forms.ComboBox();
            this.comboPort = new System.Windows.Forms.ComboBox();
            this.btnEnvironSave = new System.Windows.Forms.Button();
            this.btnSpeedUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnZoomMinus = new System.Windows.Forms.Button();
            this.btnSpeedDown = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnPreset1 = new System.Windows.Forms.Button();
            this.btnPreset2 = new System.Windows.Forms.Button();
            this.btnPreset3 = new System.Windows.Forms.Button();
            this.btnPreset4 = new System.Windows.Forms.Button();
            this.btnPreset5 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnExit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnZoomPlus
            // 
            this.btnZoomPlus.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnZoomPlus.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnZoomPlus.Location = new System.Drawing.Point(291, 505);
            this.btnZoomPlus.Name = "btnZoomPlus";
            this.btnZoomPlus.Size = new System.Drawing.Size(75, 42);
            this.btnZoomPlus.TabIndex = 0;
            this.btnZoomPlus.Text = "ZOOM+";
            this.btnZoomPlus.UseVisualStyleBackColor = false;
            // 
            // btnLeft
            // 
            this.btnLeft.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnLeft.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnLeft.Location = new System.Drawing.Point(291, 560);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(75, 37);
            this.btnLeft.TabIndex = 1;
            this.btnLeft.Text = "◀";
            this.btnLeft.UseVisualStyleBackColor = false;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // comboBaudrate
            // 
            this.comboBaudrate.FormattingEnabled = true;
            this.comboBaudrate.Items.AddRange(new object[] {
            "2400",
            "4800",
            "9600",
            "19200",
            "38400"});
            this.comboBaudrate.Location = new System.Drawing.Point(154, 505);
            this.comboBaudrate.Name = "comboBaudrate";
            this.comboBaudrate.Size = new System.Drawing.Size(121, 20);
            this.comboBaudrate.TabIndex = 2;
            this.comboBaudrate.Text = "비트/초(B)";
            this.comboBaudrate.SelectionChangeCommitted += new System.EventHandler(this.comboBaudrate_SelectionChangeCommitted);
            // 
            // comboProtocol
            // 
            this.comboProtocol.FormattingEnabled = true;
            this.comboProtocol.Items.AddRange(new object[] {
            "VISCAToTeacher",
            "VISCAToStudent"});
            this.comboProtocol.Location = new System.Drawing.Point(154, 541);
            this.comboProtocol.Name = "comboProtocol";
            this.comboProtocol.Size = new System.Drawing.Size(121, 20);
            this.comboProtocol.TabIndex = 3;
            this.comboProtocol.Text = "프로토콜(P)";
            this.comboProtocol.SelectionChangeCommitted += new System.EventHandler(this.comboProtocol_SelectionChangeCommitted);
            // 
            // comboPort
            // 
            this.comboPort.FormattingEnabled = true;
            this.comboPort.Location = new System.Drawing.Point(154, 579);
            this.comboPort.Name = "comboPort";
            this.comboPort.Size = new System.Drawing.Size(121, 20);
            this.comboPort.TabIndex = 4;
            this.comboPort.Text = "Port1";
            this.comboPort.SelectionChangeCommitted += new System.EventHandler(this.comboPort_SelectionChangeCommitted);
            // 
            // btnEnvironSave
            // 
            this.btnEnvironSave.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnEnvironSave.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnEnvironSave.Location = new System.Drawing.Point(154, 617);
            this.btnEnvironSave.Name = "btnEnvironSave";
            this.btnEnvironSave.Size = new System.Drawing.Size(121, 35);
            this.btnEnvironSave.TabIndex = 5;
            this.btnEnvironSave.Text = "환경저장";
            this.btnEnvironSave.UseVisualStyleBackColor = false;
            this.btnEnvironSave.Click += new System.EventHandler(this.btnEnvironSave_Click);
            // 
            // btnSpeedUp
            // 
            this.btnSpeedUp.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnSpeedUp.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSpeedUp.Location = new System.Drawing.Point(291, 617);
            this.btnSpeedUp.Name = "btnSpeedUp";
            this.btnSpeedUp.Size = new System.Drawing.Size(75, 35);
            this.btnSpeedUp.TabIndex = 6;
            this.btnSpeedUp.Text = "속도+";
            this.btnSpeedUp.UseVisualStyleBackColor = false;
            this.btnSpeedUp.Click += new System.EventHandler(this.btnSpeedUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnDown.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDown.Location = new System.Drawing.Point(389, 617);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(75, 35);
            this.btnDown.TabIndex = 7;
            this.btnDown.Text = "▼";
            this.btnDown.UseVisualStyleBackColor = false;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnUp.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnUp.Location = new System.Drawing.Point(389, 505);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(75, 42);
            this.btnUp.TabIndex = 8;
            this.btnUp.Text = "▲";
            this.btnUp.UseVisualStyleBackColor = false;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnZoomMinus
            // 
            this.btnZoomMinus.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnZoomMinus.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnZoomMinus.Location = new System.Drawing.Point(484, 508);
            this.btnZoomMinus.Name = "btnZoomMinus";
            this.btnZoomMinus.Size = new System.Drawing.Size(75, 39);
            this.btnZoomMinus.TabIndex = 9;
            this.btnZoomMinus.Text = "ZOOM-";
            this.btnZoomMinus.UseVisualStyleBackColor = false;
            // 
            // btnSpeedDown
            // 
            this.btnSpeedDown.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnSpeedDown.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSpeedDown.Location = new System.Drawing.Point(484, 617);
            this.btnSpeedDown.Name = "btnSpeedDown";
            this.btnSpeedDown.Size = new System.Drawing.Size(75, 35);
            this.btnSpeedDown.TabIndex = 10;
            this.btnSpeedDown.Text = "속도-";
            this.btnSpeedDown.UseVisualStyleBackColor = false;
            this.btnSpeedDown.Click += new System.EventHandler(this.btnSpeedDown_Click);
            // 
            // btnRight
            // 
            this.btnRight.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnRight.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRight.Location = new System.Drawing.Point(484, 560);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(75, 37);
            this.btnRight.TabIndex = 11;
            this.btnRight.Text = "▶";
            this.btnRight.UseVisualStyleBackColor = false;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // btnPreset1
            // 
            this.btnPreset1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnPreset1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnPreset1.Location = new System.Drawing.Point(565, 508);
            this.btnPreset1.Name = "btnPreset1";
            this.btnPreset1.Size = new System.Drawing.Size(75, 23);
            this.btnPreset1.TabIndex = 12;
            this.btnPreset1.Text = "Preset1";
            this.btnPreset1.UseVisualStyleBackColor = false;
            this.btnPreset1.Click += new System.EventHandler(this.btnPreset1_Click);
            // 
            // btnPreset2
            // 
            this.btnPreset2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnPreset2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnPreset2.Location = new System.Drawing.Point(565, 537);
            this.btnPreset2.Name = "btnPreset2";
            this.btnPreset2.Size = new System.Drawing.Size(75, 23);
            this.btnPreset2.TabIndex = 13;
            this.btnPreset2.Text = "Preset2";
            this.btnPreset2.UseVisualStyleBackColor = false;
            // 
            // btnPreset3
            // 
            this.btnPreset3.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnPreset3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnPreset3.Location = new System.Drawing.Point(565, 567);
            this.btnPreset3.Name = "btnPreset3";
            this.btnPreset3.Size = new System.Drawing.Size(75, 23);
            this.btnPreset3.TabIndex = 14;
            this.btnPreset3.Text = "Preset3";
            this.btnPreset3.UseVisualStyleBackColor = false;
            this.btnPreset3.Click += new System.EventHandler(this.btnPreset3_Click);
            // 
            // btnPreset4
            // 
            this.btnPreset4.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnPreset4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnPreset4.Location = new System.Drawing.Point(565, 596);
            this.btnPreset4.Name = "btnPreset4";
            this.btnPreset4.Size = new System.Drawing.Size(75, 23);
            this.btnPreset4.TabIndex = 15;
            this.btnPreset4.Text = "Preset4";
            this.btnPreset4.UseVisualStyleBackColor = false;
            // 
            // btnPreset5
            // 
            this.btnPreset5.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnPreset5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnPreset5.Location = new System.Drawing.Point(565, 629);
            this.btnPreset5.Name = "btnPreset5";
            this.btnPreset5.Size = new System.Drawing.Size(75, 23);
            this.btnPreset5.TabIndex = 16;
            this.btnPreset5.Text = "Preset5";
            this.btnPreset5.UseVisualStyleBackColor = false;
            this.btnPreset5.Click += new System.EventHandler(this.btnPreset5_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label2.Location = new System.Drawing.Point(397, 567);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 24);
            this.label2.TabIndex = 17;
            this.label2.Text = "CAMERA\r\n  컨트롤";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pictureBox1.Location = new System.Drawing.Point(6, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(640, 480);
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnExit.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnExit.Location = new System.Drawing.Point(28, 629);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 19;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(661, 680);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnPreset5);
            this.Controls.Add(this.btnPreset4);
            this.Controls.Add(this.btnPreset3);
            this.Controls.Add(this.btnPreset2);
            this.Controls.Add(this.btnPreset1);
            this.Controls.Add(this.btnRight);
            this.Controls.Add(this.btnSpeedDown);
            this.Controls.Add(this.btnZoomMinus);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnSpeedUp);
            this.Controls.Add(this.btnEnvironSave);
            this.Controls.Add(this.comboPort);
            this.Controls.Add(this.comboProtocol);
            this.Controls.Add(this.comboBaudrate);
            this.Controls.Add(this.btnLeft);
            this.Controls.Add(this.btnZoomPlus);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "ViscaControl";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnZoomPlus;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.ComboBox comboBaudrate;
        private System.Windows.Forms.ComboBox comboProtocol;
        private System.Windows.Forms.ComboBox comboPort;
        private System.Windows.Forms.Button btnEnvironSave;
        private System.Windows.Forms.Button btnSpeedUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnZoomMinus;
        private System.Windows.Forms.Button btnSpeedDown;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnPreset1;
        private System.Windows.Forms.Button btnPreset2;
        private System.Windows.Forms.Button btnPreset3;
        private System.Windows.Forms.Button btnPreset4;
        private System.Windows.Forms.Button btnPreset5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnExit;
    }
}


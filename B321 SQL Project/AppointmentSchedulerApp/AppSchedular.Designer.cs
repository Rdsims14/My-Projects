namespace AppointmentSchedulerApp
{
    partial class frmAppSched
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAppSched));
            calAppCalendar = new MonthCalendar();
            cboAdvisor = new ComboBox();
            cboApptType = new ComboBox();
            cboDuration = new ComboBox();
            cboModality = new ComboBox();
            btnConfirm = new Button();
            btnUpdateView = new Button();
            cboStartTime = new ComboBox();
            btnCancel = new Button();
            lblModality = new Label();
            lblStartTime = new Label();
            lblApptDuration = new Label();
            lblApptType = new Label();
            lblAdvisor = new Label();
            SuspendLayout();
            // 
            // calAppCalendar
            // 
            calAppCalendar.Location = new Point(360, 32);
            calAppCalendar.MaxSelectionCount = 1;
            calAppCalendar.Name = "calAppCalendar";
            calAppCalendar.TabIndex = 0;
            // 
            // cboAdvisor
            // 
            cboAdvisor.FormattingEnabled = true;
            cboAdvisor.Location = new Point(154, 36);
            cboAdvisor.Name = "cboAdvisor";
            cboAdvisor.Size = new Size(151, 28);
            cboAdvisor.TabIndex = 1;
            cboAdvisor.Text = "Select an Advisor";
            cboAdvisor.SelectedIndexChanged += cboAdvisor_SelectedIndexChanged;
            // 
            // cboApptType
            // 
            cboApptType.FormattingEnabled = true;
            cboApptType.Location = new Point(154, 89);
            cboApptType.Name = "cboApptType";
            cboApptType.Size = new Size(151, 28);
            cboApptType.TabIndex = 2;
            cboApptType.Text = "Select the Type";
            // 
            // cboDuration
            // 
            cboDuration.FormattingEnabled = true;
            cboDuration.Location = new Point(154, 196);
            cboDuration.Name = "cboDuration";
            cboDuration.Size = new Size(151, 28);
            cboDuration.TabIndex = 3;
            cboDuration.Text = "Duration";
            // 
            // cboModality
            // 
            cboModality.FormattingEnabled = true;
            cboModality.Location = new Point(154, 246);
            cboModality.Name = "cboModality";
            cboModality.Size = new Size(151, 28);
            cboModality.TabIndex = 4;
            cboModality.Text = "Select a Modality";
            // 
            // btnConfirm
            // 
            btnConfirm.Font = new Font("Segoe UI", 12F);
            btnConfirm.Location = new Point(360, 301);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(115, 41);
            btnConfirm.TabIndex = 7;
            btnConfirm.Text = "Confirm";
            btnConfirm.UseVisualStyleBackColor = true;
            btnConfirm.Click += btnConfirm_Click;
            // 
            // btnUpdateView
            // 
            btnUpdateView.Font = new Font("Segoe UI", 12F);
            btnUpdateView.Location = new Point(154, 301);
            btnUpdateView.Margin = new Padding(3, 4, 3, 4);
            btnUpdateView.Name = "btnUpdateView";
            btnUpdateView.Size = new Size(151, 41);
            btnUpdateView.TabIndex = 9;
            btnUpdateView.Text = "Update Cache";
            btnUpdateView.UseVisualStyleBackColor = true;
            btnUpdateView.Click += btnUpdateView_Click;
            // 
            // cboStartTime
            // 
            cboStartTime.FormattingEnabled = true;
            cboStartTime.Location = new Point(154, 143);
            cboStartTime.Name = "cboStartTime";
            cboStartTime.Size = new Size(151, 28);
            cboStartTime.TabIndex = 14;
            cboStartTime.Text = "Select a Start Time";
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("Segoe UI", 12F);
            btnCancel.Location = new Point(507, 301);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(115, 41);
            btnCancel.TabIndex = 15;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // lblModality
            // 
            lblModality.AutoSize = true;
            lblModality.Font = new Font("Segoe UI", 12F);
            lblModality.Location = new Point(44, 243);
            lblModality.Name = "lblModality";
            lblModality.Size = new Size(95, 28);
            lblModality.TabIndex = 20;
            lblModality.Text = "Modality:";
            // 
            // lblStartTime
            // 
            lblStartTime.AutoSize = true;
            lblStartTime.Font = new Font("Segoe UI", 12F);
            lblStartTime.Location = new Point(81, 140);
            lblStartTime.Name = "lblStartTime";
            lblStartTime.Size = new Size(58, 28);
            lblStartTime.TabIndex = 19;
            lblStartTime.Text = "Time:";
            // 
            // lblApptDuration
            // 
            lblApptDuration.AutoSize = true;
            lblApptDuration.Font = new Font("Segoe UI", 12F);
            lblApptDuration.Location = new Point(46, 193);
            lblApptDuration.Name = "lblApptDuration";
            lblApptDuration.Size = new Size(93, 28);
            lblApptDuration.TabIndex = 18;
            lblApptDuration.Text = "Duration:";
            // 
            // lblApptType
            // 
            lblApptType.AutoSize = true;
            lblApptType.Font = new Font("Segoe UI", 12F);
            lblApptType.Location = new Point(33, 85);
            lblApptType.Name = "lblApptType";
            lblApptType.Size = new Size(106, 28);
            lblApptType.TabIndex = 17;
            lblApptType.Text = "Appt Type:";
            // 
            // lblAdvisor
            // 
            lblAdvisor.AutoSize = true;
            lblAdvisor.Font = new Font("Segoe UI", 12F);
            lblAdvisor.Location = new Point(56, 32);
            lblAdvisor.Name = "lblAdvisor";
            lblAdvisor.Size = new Size(83, 28);
            lblAdvisor.TabIndex = 16;
            lblAdvisor.Text = "Advisor:";
            // 
            // frmAppSched
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(680, 378);
            Controls.Add(lblModality);
            Controls.Add(lblStartTime);
            Controls.Add(lblApptDuration);
            Controls.Add(lblApptType);
            Controls.Add(lblAdvisor);
            Controls.Add(btnCancel);
            Controls.Add(cboStartTime);
            Controls.Add(btnUpdateView);
            Controls.Add(btnConfirm);
            Controls.Add(cboModality);
            Controls.Add(cboDuration);
            Controls.Add(cboApptType);
            Controls.Add(cboAdvisor);
            Controls.Add(calAppCalendar);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "frmAppSched";
            Text = "Appointment Schedular";
            Load += frmAppSched_Load_1;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MonthCalendar calAppCalendar;
        private ComboBox cboAdvisor;
        private ComboBox cboApptType;
        private ComboBox cboDuration;
        private ComboBox cboModality;
        private Button btnConfirm;
        private Button btnUpdateView;
        private ComboBox cboStartTime;
        private Button btnCancel;
        private Label lblModality;
        private Label lblStartTime;
        private Label lblApptDuration;
        private Label lblApptType;
        private Label lblAdvisor;
    }
}

namespace AppointmentSchedulerApp
{
    partial class frmReschduleForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReschduleForm));
            calAppCalendar = new MonthCalendar();
            btnConfirm = new Button();
            btnCancel = new Button();
            lblAdvisor = new Label();
            lblApptType = new Label();
            lblApptDuration = new Label();
            lblStartTime = new Label();
            lblModality = new Label();
            lblApptDate = new Label();
            txtAdvisorName = new TextBox();
            cboStartTime = new ComboBox();
            txtApptDate = new TextBox();
            txtDuration = new TextBox();
            txtModality = new TextBox();
            txtApptType = new TextBox();
            SuspendLayout();
            // 
            // calAppCalendar
            // 
            calAppCalendar.Location = new Point(300, 48);
            calAppCalendar.Name = "calAppCalendar";
            calAppCalendar.TabIndex = 0;
            calAppCalendar.DateSelected += calAppCalendar_DateSelected;
            // 
            // btnConfirm
            // 
            btnConfirm.Font = new Font("Segoe UI", 12F);
            btnConfirm.Location = new Point(300, 303);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(110, 36);
            btnConfirm.TabIndex = 1;
            btnConfirm.Text = "Confirm";
            btnConfirm.UseVisualStyleBackColor = true;
            btnConfirm.Click += btnConfirm_Click;
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("Segoe UI", 12F);
            btnCancel.Location = new Point(452, 303);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(110, 36);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // lblAdvisor
            // 
            lblAdvisor.AutoSize = true;
            lblAdvisor.Font = new Font("Segoe UI", 12F);
            lblAdvisor.Location = new Point(52, 47);
            lblAdvisor.Name = "lblAdvisor";
            lblAdvisor.Size = new Size(83, 28);
            lblAdvisor.TabIndex = 3;
            lblAdvisor.Text = "Advisor:";
            // 
            // lblApptType
            // 
            lblApptType.AutoSize = true;
            lblApptType.Font = new Font("Segoe UI", 12F);
            lblApptType.Location = new Point(29, 306);
            lblApptType.Name = "lblApptType";
            lblApptType.Size = new Size(106, 28);
            lblApptType.TabIndex = 4;
            lblApptType.Text = "Appt Type:";
            // 
            // lblApptDuration
            // 
            lblApptDuration.AutoSize = true;
            lblApptDuration.Font = new Font("Segoe UI", 12F);
            lblApptDuration.Location = new Point(42, 208);
            lblApptDuration.Name = "lblApptDuration";
            lblApptDuration.Size = new Size(93, 28);
            lblApptDuration.TabIndex = 5;
            lblApptDuration.Text = "Duration:";
            // 
            // lblStartTime
            // 
            lblStartTime.AutoSize = true;
            lblStartTime.Font = new Font("Segoe UI", 12F);
            lblStartTime.Location = new Point(77, 155);
            lblStartTime.Name = "lblStartTime";
            lblStartTime.Size = new Size(58, 28);
            lblStartTime.TabIndex = 6;
            lblStartTime.Text = "Time:";
            // 
            // lblModality
            // 
            lblModality.AutoSize = true;
            lblModality.Font = new Font("Segoe UI", 12F);
            lblModality.Location = new Point(40, 258);
            lblModality.Name = "lblModality";
            lblModality.Size = new Size(95, 28);
            lblModality.TabIndex = 7;
            lblModality.Text = "Modality:";
            // 
            // lblApptDate
            // 
            lblApptDate.AutoSize = true;
            lblApptDate.Font = new Font("Segoe UI", 12F);
            lblApptDate.Location = new Point(78, 99);
            lblApptDate.Name = "lblApptDate";
            lblApptDate.Size = new Size(57, 28);
            lblApptDate.TabIndex = 8;
            lblApptDate.Text = "Date:";
            // 
            // txtAdvisorName
            // 
            txtAdvisorName.Location = new Point(152, 48);
            txtAdvisorName.Name = "txtAdvisorName";
            txtAdvisorName.ReadOnly = true;
            txtAdvisorName.Size = new Size(125, 27);
            txtAdvisorName.TabIndex = 9;
            txtAdvisorName.Text = "AdvisorName";
            // 
            // cboStartTime
            // 
            cboStartTime.FormattingEnabled = true;
            cboStartTime.Location = new Point(152, 155);
            cboStartTime.Name = "cboStartTime";
            cboStartTime.Size = new Size(125, 28);
            cboStartTime.TabIndex = 12;
            // 
            // txtApptDate
            // 
            txtApptDate.Location = new Point(152, 100);
            txtApptDate.Name = "txtApptDate";
            txtApptDate.Size = new Size(125, 27);
            txtApptDate.TabIndex = 14;
            // 
            // txtDuration
            // 
            txtDuration.Location = new Point(152, 209);
            txtDuration.Name = "txtDuration";
            txtDuration.ReadOnly = true;
            txtDuration.Size = new Size(125, 27);
            txtDuration.TabIndex = 15;
            // 
            // txtModality
            // 
            txtModality.Location = new Point(152, 259);
            txtModality.Name = "txtModality";
            txtModality.ReadOnly = true;
            txtModality.Size = new Size(125, 27);
            txtModality.TabIndex = 16;
            // 
            // txtApptType
            // 
            txtApptType.Location = new Point(152, 307);
            txtApptType.Name = "txtApptType";
            txtApptType.ReadOnly = true;
            txtApptType.Size = new Size(125, 27);
            txtApptType.TabIndex = 17;
            // 
            // frmReschduleForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(606, 377);
            Controls.Add(txtApptType);
            Controls.Add(txtModality);
            Controls.Add(txtDuration);
            Controls.Add(txtApptDate);
            Controls.Add(cboStartTime);
            Controls.Add(txtAdvisorName);
            Controls.Add(lblApptDate);
            Controls.Add(lblModality);
            Controls.Add(lblStartTime);
            Controls.Add(lblApptDuration);
            Controls.Add(lblApptType);
            Controls.Add(lblAdvisor);
            Controls.Add(btnCancel);
            Controls.Add(btnConfirm);
            Controls.Add(calAppCalendar);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmReschduleForm";
            Text = "Reschedule Appointment";
            Load += frmReschduleForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MonthCalendar calAppCalendar;
        private Button btnConfirm;
        private Button btnCancel;
        private Label lblAdvisor;
        private Label lblApptType;
        private Label lblApptDuration;
        private Label lblStartTime;
        private Label lblModality;
        private Label lblApptDate;
        private TextBox txtAdvisorName;
        private ComboBox cboStartTime;
        private TextBox txtApptDate;
        private TextBox txtDuration;
        private TextBox txtModality;
        private TextBox txtApptType;
    }
}
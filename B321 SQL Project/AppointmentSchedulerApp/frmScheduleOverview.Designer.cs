namespace AppointmentSchedulerApp
{
    partial class frmScheduleOverview
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
            ColumnHeader ApptDate;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmScheduleOverview));
            lvwScheduledAppts = new ListView();
            AdvisorName = new ColumnHeader();
            ApptTime = new ColumnHeader();
            ApptDuration = new ColumnHeader();
            ApptAddress = new ColumnHeader();
            btnUpdateView = new Button();
            btnCancelAppt = new Button();
            btnReschedule = new Button();
            btnNewAppt = new Button();
            lblGreeting = new Label();
            ApptDate = new ColumnHeader();
            SuspendLayout();
            // 
            // ApptDate
            // 
            ApptDate.Text = "Appt Date";
            // 
            // lvwScheduledAppts
            // 
            lvwScheduledAppts.Columns.AddRange(new ColumnHeader[] { AdvisorName, ApptDate, ApptTime, ApptDuration, ApptAddress });
            lvwScheduledAppts.FullRowSelect = true;
            lvwScheduledAppts.GridLines = true;
            lvwScheduledAppts.Location = new Point(33, 138);
            lvwScheduledAppts.Name = "lvwScheduledAppts";
            lvwScheduledAppts.Size = new Size(463, 266);
            lvwScheduledAppts.TabIndex = 0;
            lvwScheduledAppts.UseCompatibleStateImageBehavior = false;
            lvwScheduledAppts.View = View.Details;
            // 
            // AdvisorName
            // 
            AdvisorName.Text = "Advisor Name";
            // 
            // ApptTime
            // 
            ApptTime.Text = "Time";
            // 
            // ApptDuration
            // 
            ApptDuration.Text = "Duration";
            // 
            // ApptAddress
            // 
            ApptAddress.Text = "Address";
            // 
            // btnUpdateView
            // 
            btnUpdateView.Font = new Font("Segoe UI", 12F);
            btnUpdateView.Location = new Point(527, 138);
            btnUpdateView.Name = "btnUpdateView";
            btnUpdateView.Size = new Size(209, 44);
            btnUpdateView.TabIndex = 1;
            btnUpdateView.Text = "Update View";
            btnUpdateView.UseVisualStyleBackColor = true;
            btnUpdateView.Click += btnUpdateView_Click;
            // 
            // btnCancelAppt
            // 
            btnCancelAppt.Font = new Font("Segoe UI", 12F);
            btnCancelAppt.Location = new Point(527, 360);
            btnCancelAppt.Name = "btnCancelAppt";
            btnCancelAppt.Size = new Size(209, 44);
            btnCancelAppt.TabIndex = 2;
            btnCancelAppt.Text = "Cancel Appointment";
            btnCancelAppt.UseVisualStyleBackColor = true;
            btnCancelAppt.Click += btnCancelAppt_Click;
            // 
            // btnReschedule
            // 
            btnReschedule.Font = new Font("Segoe UI", 12F);
            btnReschedule.Location = new Point(527, 286);
            btnReschedule.Name = "btnReschedule";
            btnReschedule.Size = new Size(209, 44);
            btnReschedule.TabIndex = 3;
            btnReschedule.Text = "Reschedule Appt";
            btnReschedule.UseVisualStyleBackColor = true;
            btnReschedule.Click += btnReschedule_Click;
            // 
            // btnNewAppt
            // 
            btnNewAppt.Font = new Font("Segoe UI", 12F);
            btnNewAppt.Location = new Point(527, 212);
            btnNewAppt.Name = "btnNewAppt";
            btnNewAppt.Size = new Size(209, 44);
            btnNewAppt.TabIndex = 4;
            btnNewAppt.Text = "New Appointment";
            btnNewAppt.UseVisualStyleBackColor = true;
            btnNewAppt.Click += btnNewAppt_Click;
            // 
            // lblGreeting
            // 
            lblGreeting.AutoSize = true;
            lblGreeting.Font = new Font("Segoe UI", 32F);
            lblGreeting.Location = new Point(37, 31);
            lblGreeting.Name = "lblGreeting";
            lblGreeting.Size = new Size(409, 72);
            lblGreeting.TabIndex = 5;
            lblGreeting.Text = "Hi, Current User";
            // 
            // frmScheduleOverview
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(761, 450);
            Controls.Add(lblGreeting);
            Controls.Add(btnNewAppt);
            Controls.Add(btnReschedule);
            Controls.Add(btnCancelAppt);
            Controls.Add(btnUpdateView);
            Controls.Add(lvwScheduledAppts);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "frmScheduleOverview";
            Text = "Schedule Overview";
            Load += frmScheduleOverview_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView lvwScheduledAppts;
        private Button btnUpdateView;
        private Button btnCancelAppt;
        private Button btnReschedule;
        private Button btnNewAppt;
        private Label lblGreeting;
        private ColumnHeader AdvisorName;
        private ColumnHeader ApptDate;
        private ColumnHeader ApptTime;
        private ColumnHeader ApptDuration;
        private ColumnHeader ApptAddress;
    }
}
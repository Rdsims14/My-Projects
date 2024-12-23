using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppointmentSchedulerApp
{
    public partial class frmReschduleForm : Form
    {
        public frmReschduleForm()
        {
            InitializeComponent();
        }

        private void frmReschduleForm_Load(object sender, EventArgs e)
        {
            clsScheduledAppointments appt = clsSessionInfo.selectedAppt;
            txtAdvisorName.Text = appt.AdvisorFullName;
            txtApptDate.Text = appt.ApptDate.ToString();
            txtDuration.Text = appt.ApptDuration.ToString();

            DatasourceTimes();
        }

        private void DatasourceTimes()
        {
            TimeOnly startTime = new TimeOnly(8, 0);

            for (int i = 0; i < 8; i++)
            {
                TimeOnly currTime = startTime.AddHours(i);
                cboStartTime.Items.Add(currTime);
            }
            //for (DateTime i = 08:00:00;)

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (DateOnly.FromDateTime(calAppCalendar.SelectionStart) >= DateOnly.FromDateTime(DateTime.Now))
            {


                string connectionString = clsDBUtil.getConnectionString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand RescheduleAppt = new SqlCommand("RescheduleAppointment", conn);
                    RescheduleAppt.CommandType = CommandType.StoredProcedure;
                    RescheduleAppt.Parameters.AddWithValue("@AppointmentID", clsSessionInfo.selectedAppt.ApptID);
                    RescheduleAppt.Parameters.AddWithValue("@NewDate", DateOnly.FromDateTime(calAppCalendar.SelectionStart));
                    RescheduleAppt.Parameters.AddWithValue("@NewStartTime", (TimeOnly)cboStartTime.SelectedItem);

                    try
                    {
                        conn.Open();
                        RescheduleAppt.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally { conn.Close(); this.Close(); }

                }
            }
            else
            {
                MessageBox.Show("Cannot schedule an appointment before the current date!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void calAppCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            txtApptDate.Text = DateOnly.FromDateTime(calAppCalendar.SelectionStart).ToString();
        }

        private void UpdateCalendar()
        {
            
        }


    }
}
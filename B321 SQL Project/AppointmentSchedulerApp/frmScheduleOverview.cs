using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppointmentSchedulerApp
{
    public partial class frmScheduleOverview : Form
    {
        private Dictionary<int, clsScheduledAppointments> dctSchedAppts = new Dictionary<int, clsScheduledAppointments>();

        public frmScheduleOverview()
        {
            InitializeComponent();
        }

        private void btnUpdateView_Click(object sender, EventArgs e)
        {
            UpdateUserAppts();
        }

        private void btnCancelAppt_Click(object sender, EventArgs e)
        {
            if (lvwScheduledAppts.FocusedItem != null)
            {
                DialogResult cancel = 
                MessageBox.Show("Are you sure you want to cancel this appointment?",
                "Cancel Appointment",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

                if (cancel == DialogResult.Yes)
                {
                    ListViewItem apptToCancel = lvwScheduledAppts.FocusedItem;

                    clsScheduledAppointments apptTag = (clsScheduledAppointments)apptToCancel.Tag;

                    string connectionString = clsDBUtil.getConnectionString();
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        SqlCommand CancelAppt = new SqlCommand("CancelAppointment", conn);
                        CancelAppt.CommandType = CommandType.StoredProcedure;
                        CancelAppt.Parameters.AddWithValue("AppointmentID", apptTag.ApptID);

                        try
                        {
                            conn.Open();
                            CancelAppt.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        finally { conn.Close(); }
                    }
                    MessageBox.Show("Your appointment has been canceled", "Cancellation Confirmation", MessageBoxButtons.OK);
                }

                UpdateUserAppts();
            }
            else
            {
                MessageBox.Show("Please select an appointment.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnReschedule_Click(object sender, EventArgs e)
        {
            if (lvwScheduledAppts.FocusedItem != null)
            {
                this.Tag = lvwScheduledAppts.FocusedItem;
                clsSessionInfo.selectedAppt = (clsScheduledAppointments)lvwScheduledAppts.FocusedItem.Tag;
                Form Reschedule = new frmReschduleForm();
                Reschedule.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select an appointment.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnNewAppt_Click(object sender, EventArgs e)
        {
            Form NewAppt = new frmAppSched();
            NewAppt.ShowDialog();
        }

        private void UpdateUserAppts()
        {
            string connectionString = clsDBUtil.getConnectionString();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand GetAppts = new SqlCommand("GetUserAppointments", conn);
                GetAppts.CommandType = CommandType.StoredProcedure;
                GetAppts.Parameters.AddWithValue("@UserID", clsSessionInfo.UserID);

                try
                {
                    conn.Open();
                    using (SqlDataReader reader = GetAppts.ExecuteReader())
                    {
                        dctSchedAppts.Clear();

                        while (reader.Read())
                        {
                            clsScheduledAppointments currAppt = new clsScheduledAppointments();
                            currAppt.ApptID = (int)reader["AppointmentID"];
                            currAppt.AdviseeID = (int)reader["AdviseeID"];
                            currAppt.AdvisorID = (int)reader["AdvisorID"];
                            currAppt.AdvisorFullName = (string)reader["AdvisorFullName"];
                            currAppt.AdvisorEmail = (string)reader["AdvisorEmail"];
                            currAppt.ApptDate = DateOnly.FromDateTime((DateTime)reader["AppointmentDate"]);
                            currAppt.ApptStartTime = (TimeSpan)reader["AppointmentStartTime"];
                            currAppt.ApptDuration = (int)reader["AppointmentDuration"];

                            dctSchedAppts.Add(currAppt.ApptID, currAppt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally { conn.Close(); }
            }

            UpdateApptsView();
        }

        private void UpdateApptsView()
        {
            lvwScheduledAppts.Items.Clear();
            lvwScheduledAppts.View = View.Details;

            clsScheduledAppointments appt = new clsScheduledAppointments();
            foreach (KeyValuePair<int, clsScheduledAppointments> kvp in dctSchedAppts)
            {
                appt = kvp.Value;
                ListViewItem item = new ListViewItem(appt.AdvisorFullName);
                item.Tag = appt;
                item.SubItems.Add(appt.ApptDate.ToString());
                item.SubItems.Add(appt.ApptStartTime.ToString());
                item.SubItems.Add(appt.ApptDuration.ToString());
                item.SubItems.Add(appt.BuildingAddress1);
                lvwScheduledAppts.Items.Add(item);
            }
        }

        private void frmScheduleOverview_Load(object sender, EventArgs e)
        {
            Form login = new frmLogin();
            login.ShowDialog();

            GreetUser();
            UpdateUserAppts();
            
        }

        private void GreetUser()
        {
            lblGreeting.Text = "Hi, " + clsSessionInfo.UserFName;
        }
    }
}

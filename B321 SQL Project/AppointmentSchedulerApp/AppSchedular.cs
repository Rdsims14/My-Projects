using Microsoft.Data.SqlClient;
using System.Data;

namespace AppointmentSchedulerApp
{
    public partial class frmAppSched : Form
    {
        private Dictionary<int, clsAdvisor> dctAdvisors = new Dictionary<int, clsAdvisor>();
        private Dictionary<int, clsAppointmentType> dctAppointmentTypes = new Dictionary<int, clsAppointmentType>();
        private Dictionary<int, clsModality> dctModalities = new Dictionary<int, clsModality>();
        private Dictionary<int, clsLocation> dctLocations = new Dictionary<int, clsLocation>();
        private Dictionary<int, clsScheduledAppointments> dctAppts = new Dictionary<int, clsScheduledAppointments>();

        private List<int> lstDurations = new List<int>([15, 30, 45, 60]);
        private Dictionary<int, string> dctDurations = new Dictionary<int, string>();

        private clsNewAppointment newAppointment = new clsNewAppointment();
        private string apptString = "";

        private bool dataSrubbed = false;

        public frmAppSched()
        {
            InitializeComponent();
        }

        private void frmAppSched_Load_1(object sender, EventArgs e)
        {
            UpdateCache();
            UpdateCheckBoxes();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            StoreAppointmentInfo();
            //PreviewAppt();
            if (dataSrubbed)
            {
                CreateNewAppt();
            }
            else
            {
                MessageBox.Show("Please make sure to select all fields.", "Warning");
            }
        }

        private void btnUpdateView_Click(object sender, EventArgs e)
        {
            UpdateCache();
            //UpdateLiveView();
            UpdateCheckBoxes();
        }

        private void cboAdvisor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void UpdateCache()
        {
            string connectionString = clsDBUtil.getConnectionString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    
                    connection.Open();

                    SqlCommand GetAdvisors = new SqlCommand("GetAdvisors", connection);
                    GetAdvisors.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = GetAdvisors.ExecuteReader())
                    {
                        dctAdvisors.Clear();

                        while (reader.Read())
                        {
                            clsAdvisor advisor = new clsAdvisor();
                            advisor.AdvisorID = (int)reader["AdvisorID"];
                            advisor.AdvisorUsername = (string)reader["AdvisorUsername"];
                            advisor.AdvisorFName = (string)reader["AdvisorFName"];
                            advisor.AdvisorLName = (string)reader["AdvisorLName"];
                            advisor.AdvisorPhone = (string)reader["AdvisorPhone"];
                            advisor.AdvisorEmail = (string)reader["AdvisorEmail"];
                            advisor.DefaultLocation = (int)reader["DefaultLocation"];
                            dctAdvisors.Add(advisor.AdvisorID, advisor);
                        }
                    }// end GetAdvisors

                    SqlCommand GetModalities = new SqlCommand("GetModalities", connection);
                    GetModalities.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = GetModalities.ExecuteReader())
                    {
                        dctModalities.Clear();

                        while (reader.Read())
                        {
                            clsModality modality = new clsModality();
                            modality.ModalityID = (int)reader["ModalityID"];
                            modality.MeetingType = (string)reader["MeetingType"];
                            modality.MeetingTypeDesc = (string)reader["MeetingTypeDesc"];
                            dctModalities.Add(modality.ModalityID, modality);
                        }
                    }// end GetModality


                    SqlCommand GetApptTypes = new SqlCommand("GetApptTypes", connection);
                    GetApptTypes.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = GetApptTypes.ExecuteReader())
                    {
                        dctAppointmentTypes.Clear();

                        while (reader.Read())
                        {
                            clsAppointmentType apptType = new clsAppointmentType();
                            apptType.AppointmentTypeID = (int)reader["AppointmentTypeID"];
                            apptType.TypeName = (string)reader["TypeName"];
                            apptType.TypeDescription = (string)reader["TypeDescription"];
                            dctAppointmentTypes.Add(apptType.AppointmentTypeID, apptType);
                        }
                    }// end GetApptType

                    SqlCommand GetLocations = new SqlCommand("GetLocations", connection);
                    GetLocations.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = GetLocations.ExecuteReader())
                    {
                        dctLocations.Clear();

                        while (reader.Read())
                        {
                            clsLocation location = new clsLocation();
                            location.LocationID = (int)reader["LocationID"];
                            location.BuildingTitle = (string)reader["BuildingTitle"];
                            location.BuildingAddress1 = (string)reader["BuildingAddress1"];

                            // Address2 Contains null values. Currently no way to test nor convert these values!!!
                            //location.BuildingAddress2 = (string)reader["BuildingAddress2"]; 

                            location.BuildingCity = (string)reader["BuildingCity"];
                            location.BuildingState = (string)reader["BuildingState"];
                            location.LocationDescription = (string)reader["LocationDescription"];
                            dctLocations.Add(location.LocationID, location);
                        }
                    }// end GetLocations
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                { connection.Close(); }// end try catch
            }// end using
        }

        /*
        private void UpdateLiveView()
        {
            lvwTestView.Items.Clear();

            clsAdvisor currentAdvisor = new clsAdvisor();
            foreach (KeyValuePair<int, clsAdvisor> kvp in dctAdvisors)
            {
                currentAdvisor = kvp.Value;
                ListViewItem item = new ListViewItem(currentAdvisor.AdvisorFullName());
                item.Tag = currentAdvisor;
                lvwTestView.Items.Add(item);
            }
        }
        */

        private void UpdateCheckBoxes()
        {
            DatasourceAdvisor();
            DatasourceModalities();
            DatasourceAppointmentType();
            cboDuration.DataSource = lstDurations;

            DatasourceTimes();
        }

        #region DataSourceCreation
        private void DatasourceAdvisor()
        {


            //cboAdvisor.Items.Clear();

            List<KeyValuePair<int, string>> dsAdvisor = new List<KeyValuePair<int, string>>();

            //foreach (int i in dctAdvisors.Keys)
            //{
            //    cboAdvisor.Items.Add(dctAdvisors[i]);
            //    cboAdvisor.DisplayMember = dctAdvisors[i].AdvisorFullName();
            //}

            foreach (KeyValuePair<int, clsAdvisor> kvp in dctAdvisors)
            {
                dsAdvisor.Add(new KeyValuePair<int, string>(kvp.Key, kvp.Value.AdvisorFullName()));
            }

            cboAdvisor.DataSource = new BindingSource(dsAdvisor, null);
            cboAdvisor.DisplayMember = "Value";
            cboAdvisor.ValueMember = "Key";
            cboAdvisor.SelectedIndex = -1;
            cboAdvisor.Text = "Select an Advisor";
        }

        private void DatasourceModalities()
        {
            //cboModality.Items.Clear();
            //foreach (int i in dctModalities.Keys)
            //{
            //    cboModality.Items.Add(dctModalities[i].MeetingType);
            //}

            List<KeyValuePair<int, string>> dsModalities = new List<KeyValuePair<int, string>>();

            foreach (KeyValuePair<int, clsModality> kvp in dctModalities)
            {
                dsModalities.Add(new KeyValuePair<int, string>(kvp.Key, kvp.Value.MeetingType));
            }

            cboModality.DataSource = new BindingSource(dsModalities, null);
            cboModality.DisplayMember = "Value";
            cboModality.ValueMember = "Key";
            cboModality.SelectedIndex = -1;
            cboModality.Text = "Select a Modality";
        }

        private void DatasourceAppointmentType()
        {
            /*
            cboApptType.Items.Clear();
            foreach (int i in dctAppointmentTypes.Keys)
            {
                cboApptType.Items.Add(dctAppointmentTypes[i].TypeName);
            }
            */
            List<KeyValuePair<int, string>> dsApptType = new List<KeyValuePair<int, string>>();

            foreach (KeyValuePair<int, clsAppointmentType> kvp in dctAppointmentTypes)
            {
                dsApptType.Add(new KeyValuePair<int, string>(kvp.Key, kvp.Value.TypeName));
            }

            cboApptType.DataSource = new BindingSource(dsApptType, null);
            cboApptType.DisplayMember = "Value";
            cboApptType.ValueMember = "Key";
            cboApptType.SelectedIndex = -1;
            cboApptType.Text = "Select an Appt Type";
        }

        private List<string> DatasourceLocation()
        {
            List<string> dataSource = new List<string>();
            dataSource.Add("Select an App Type");

            foreach (KeyValuePair<int, clsLocation> kvp in dctLocations)
            {
                clsLocation currentLocations = kvp.Value;
                //dataSource.Add(currentLocations.LocationName);
            }
            return dataSource;
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

        #endregion
        private void StoreAppointmentInfo()
        {

            // Get Advisor
            if (cboAdvisor.SelectedItem != null)
            {
                KeyValuePair<int, string> adv = (KeyValuePair<int, string>)cboAdvisor.SelectedItem;
                int selectedAdvisor = adv.Key;
                newAppointment.AdvisorID = selectedAdvisor;
            }
            else
            {
                return;
            }
            // Get Location
            if (newAppointment.AdvisorID != 0)
            {
                int locationID = dctAdvisors[newAppointment.AdvisorID].DefaultLocation;
                newAppointment.LocationID = locationID;
            }
            else
            {
                return;
            }
            // Get AppType
            if (cboApptType.SelectedItem != null)
            {
                KeyValuePair<int, string> apt = (KeyValuePair<int, string>)cboApptType.SelectedItem;
                int selectedAppType = apt.Key;
                newAppointment.ApptTypeID = selectedAppType;
            }
            else
            {
                return;
            }
            // Get Modality
            if (cboModality.SelectedItem != null)
            {
                KeyValuePair<int, string> mod = (KeyValuePair<int, string>)cboModality.SelectedItem;
                int selectedModality = mod.Key;
                newAppointment.ModalityID = selectedModality;
            }
            else
            {
                return;
            }
            // Get Advisee
            //Random rnd = new();
            //newAppointment.AdviseeID = rnd.Next(1, 25);
            newAppointment.AdviseeID = clsSessionInfo.UserID;

            // Get Date
            DateOnly selectedDate = DateOnly.FromDateTime(calAppCalendar.SelectionStart);
            newAppointment.ApptDate = selectedDate;

            // Get Selected Duration
            if (cboDuration.SelectedItem != null)
            {
                int selectedDuration = (int)cboDuration.SelectedItem;
                newAppointment.ApptDuration = selectedDuration;
            }
            else
            {
                return;
            }
            // Get Time
            if (cboStartTime.SelectedItem != null)
            {
                TimeOnly startTime = (TimeOnly)cboStartTime.SelectedItem;
                newAppointment.ApptStartTime = startTime;
            }
            else
            {
                return;
            }
            // Get App Desc
            string apptDesc = "";
            newAppointment.ApptDesc = apptDesc;

            dataSrubbed = true;
        }

        /*
        private void PreviewAppt()
        {
            lvwTestView.Items.Clear();

            lvwTestView.Items.Add(new ListViewItem(newAppointment.LocationID.ToString()));
            lvwTestView.Items.Add(new ListViewItem(newAppointment.ModalityID.ToString()));
            lvwTestView.Items.Add(new ListViewItem(newAppointment.AdvisorID.ToString()));
            lvwTestView.Items.Add(new ListViewItem(newAppointment.AdviseeID.ToString()));
            lvwTestView.Items.Add(new ListViewItem(newAppointment.ApptTypeID.ToString()));
            lvwTestView.Items.Add(new ListViewItem(newAppointment.ApptDate.ToString()));
            lvwTestView.Items.Add(new ListViewItem(newAppointment.ApptStartTime.ToString()));
            lvwTestView.Items.Add(new ListViewItem(newAppointment.ApptDuration.ToString()));
            lvwTestView.Items.Add(new ListViewItem(newAppointment.ApptDesc.ToString()));
        }
        */
        private void CreateNewAppt()
{
    if (DateOnly.FromDateTime(calAppCalendar.SelectionStart) >= DateOnly.FromDateTime(DateTime.Now))
    {
        string connectionString = clsDBUtil.getConnectionString();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand CheckForOverlappingAppointments = new SqlCommand("CheckForOverlappingAppointments", connection);
            CheckForOverlappingAppointments.CommandType = CommandType.StoredProcedure;
            CheckForOverlappingAppointments.Parameters.AddWithValue("@LocationID", newAppointment.LocationID);
            CheckForOverlappingAppointments.Parameters.AddWithValue("@ModalityID", newAppointment.ModalityID);
            CheckForOverlappingAppointments.Parameters.AddWithValue("@AdvisorID", newAppointment.AdvisorID);
            CheckForOverlappingAppointments.Parameters.AddWithValue("@AdviseeID", newAppointment.AdviseeID);
            CheckForOverlappingAppointments.Parameters.AddWithValue("@AppointmentTypeID", newAppointment.ApptTypeID);
            CheckForOverlappingAppointments.Parameters.AddWithValue("@AppointmentDate", newAppointment.ApptDate);
            CheckForOverlappingAppointments.Parameters.AddWithValue("@AppointmentStartTime", newAppointment.ApptStartTime);
            CheckForOverlappingAppointments.Parameters.AddWithValue("@AppointmentDuration", newAppointment.ApptDuration);

            try
            {
                connection.Open();
                CheckForOverlappingAppointments.ExecuteNonQuery();

                SqlCommand CreateAppointment = new SqlCommand("CreateScheduledAppointment", connection);
                CreateAppointment.CommandType = CommandType.StoredProcedure;
                CreateAppointment.Parameters.AddWithValue("@LocationID", newAppointment.LocationID);
                CreateAppointment.Parameters.AddWithValue("@ModalityID", newAppointment.ModalityID);
                CreateAppointment.Parameters.AddWithValue("@AdvisorID", newAppointment.AdvisorID);
                CreateAppointment.Parameters.AddWithValue("@AdviseeID", newAppointment.AdviseeID);
                CreateAppointment.Parameters.AddWithValue("@AppointmentTypeID", newAppointment.ApptTypeID);
                CreateAppointment.Parameters.AddWithValue("@AppointmentDate", newAppointment.ApptDate);
                CreateAppointment.Parameters.AddWithValue("@AppointmentStartTime", newAppointment.ApptStartTime);
                CreateAppointment.Parameters.AddWithValue("@AppointmentDuration", newAppointment.ApptDuration);
                CreateAppointment.Parameters.AddWithValue("@AppointmentDescription", newAppointment.ApptDesc);

                int count = CreateAppointment.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { connection.Close(); }
        }
    }
    else
    {
        MessageBox.Show("Cannot schedule an appointment before the current date!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }
}
    }
}

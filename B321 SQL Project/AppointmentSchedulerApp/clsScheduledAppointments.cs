using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentSchedulerApp
{
    internal class clsScheduledAppointments
    {
        private int mApptID;
        private int mAdvisorID;
        private string mAdvisorFullName = "";
        private string mAdvisorEmail = "";
        private DateOnly mApptDate;
        private TimeSpan mApptStartTime;
        private string mApptType = "";
        private string mBuildingAddress1 = "";
        private int mAdviseeID;
        private int mApptDuration;
        private string mApptModality = "";


        public int ApptID {  get { return mApptID; } set { mApptID = value; } }
        public int AdvisorID { get { return mAdvisorID; } set { mAdvisorID = value; } }
        public string AdvisorFullName { get { return mAdvisorFullName; }  set { mAdvisorFullName = value; } }
        public string AdvisorEmail {  get { return mAdvisorEmail; } set { mAdvisorEmail = value; } }
        public DateOnly ApptDate { get { return mApptDate; } set { mApptDate = value; } }
        public TimeSpan ApptStartTime { get { return mApptStartTime; } set { mApptStartTime = value; } }
        public int ApptDuration { get { return mApptDuration; } set { mApptDuration = value; } }
        public string BuildingAddress1 { get { return mBuildingAddress1; } set { mBuildingAddress1 = value; } }
        public int AdviseeID { get { return mAdviseeID; } set { mAdviseeID = value; } }
    }
}

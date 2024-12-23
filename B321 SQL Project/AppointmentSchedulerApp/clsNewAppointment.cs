using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentSchedulerApp
{
    internal class clsNewAppointment
    {
        private int mLocationID;
        private int mModalityID;
        private int mAdvisorID;
        private int mAdviseeID;
        private int mApptTypeID;
        private DateOnly mApptDate;
        private TimeOnly mApptStartTime;
        private int mApptDuration;
        private string mApptDesc = "";

        public int LocationID { get { return mLocationID; } set { mLocationID = value; } }
        public int ModalityID { get {return mModalityID; } set {mModalityID = value; } }
        public int AdvisorID { get {return mAdvisorID;} set {mAdvisorID = value;} }
        public int AdviseeID { get {return mAdviseeID;} set { mAdviseeID = value;} }
        public int ApptTypeID { get {return mApptTypeID;} set {mApptTypeID = value;} }
        public DateOnly ApptDate { get {return mApptDate;} set {mApptDate = value;} }
        public TimeOnly ApptStartTime { get { return mApptStartTime; } set { mApptStartTime = value; } }
        public int ApptDuration { get { return mApptDuration; } set { mApptDuration = value; } }
        public string ApptDesc { get { return mApptDesc; } set { mApptDesc = value; } }

    }
}

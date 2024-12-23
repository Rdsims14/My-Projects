using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentSchedulerApp
{
    internal class clsAdvisee
    {
        // Advisee properties
        private int mAdviseeID;
        private string mAdviseeUsername = "";
        private string mAdviseeFName = "";
        private string mAdviseeLName = "";
        private string mAdviseePhone = "";
        private string mAdviseeEmail = "";
        
        // Getters and Setters

        public int AdviseeID { get { return mAdviseeID; } set { mAdviseeID = value; } }
        public string AdviseeUsername { get { return mAdviseeUsername; } set { mAdviseeUsername = value; } }
        public string AdviseeFName { get { return mAdviseeFName; } set { mAdviseeFName = value; } }
        public string AdviseeLName { get { return mAdviseeLName; } set { mAdviseeLName = value; } }
        public string AdviseePhone { get { return mAdviseePhone; } set { mAdviseePhone = value; } }
        public string AdviseeEmail { get { return mAdviseeEmail; } set { mAdviseeEmail = value; } }

    }
}

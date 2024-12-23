using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentSchedulerApp
{
    internal class clsAppointmentType
    {
        // App properties
        private int mAppointmentTypeID;
        private string mTypeName = "";
        private string mTypeDescription = "";

        // App Getters
        public int AppointmentTypeID { get { return mAppointmentTypeID; } set { mAppointmentTypeID = value; } }
        public string TypeName { get { return mTypeName; } set { mTypeName = value; } }
        public string TypeDescription { get { return mTypeDescription; } set { mTypeDescription = value; } }
    }
}

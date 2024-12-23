using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentSchedulerApp
{
    internal class clsModality
    {
        // Modality properties
        private int mModalityID;
        private string mMeetingType = "";
        private string mMeetingTypeDesc = "";

        // Modality Getters
        public int ModalityID { get { return mModalityID; } set { mModalityID = value; } }
        public string MeetingType { get { return mMeetingType; } set { mMeetingType = value; } }
        public string MeetingTypeDesc { get { return mMeetingTypeDesc; } set { mMeetingTypeDesc = value; } }

    }
}

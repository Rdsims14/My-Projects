using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentSchedulerApp
{
    internal static class clsSessionInfo
    {
        private static string mUserFName = "Guest";
        private static string mUserLName = "";
        private static int mUserID = 8;

        private static string guestUser = "B321_scpoole";
        private static string guestPass = "Password1!";

        public static clsScheduledAppointments selectedAppt = new clsScheduledAppointments();

        public static int UserID { get { return mUserID; } set { mUserID = value; } }

        public static string UserFName { get { return mUserFName; } set { mUserFName = value; } }
        public static string UserLName { get { return mUserLName; } set { mUserLName = value; } }

        public static string GetUser()
        {
            return guestUser;
        }
        public static string GetPass()
        {
            return guestPass;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentSchedulerApp
{
    internal class clsAdvisor
    {
        // Class Columns
        private int mAdvisorID;
        private string mAdvisorUsername = "";
        private string mAdvisorPassword = "";
        private string mAdvisorFName = "";
        private string mAdvisorLName = "";
        private string mAdvisorPhone = "";
        private string mAdvisorEmail = "";
        private int mDefaultLocation;


        // Class Getters and Setters
        public int AdvisorID { get => mAdvisorID; set => mAdvisorID = value; }
        public string AdvisorUsername { get => mAdvisorUsername; set => mAdvisorUsername = value; }
        public string AdvisorPassword { get => mAdvisorPassword; set => mAdvisorPassword = value; }
        public string AdvisorFName { get => mAdvisorFName; set => mAdvisorFName = value; }
        public string AdvisorLName { get => mAdvisorLName; set => mAdvisorLName = value; }
        public string AdvisorPhone { get => mAdvisorPhone; set => mAdvisorPhone = value; }
        public string AdvisorEmail { get => mAdvisorEmail; set => mAdvisorEmail = value; }
        public int DefaultLocation { get => mDefaultLocation; set => mDefaultLocation = value; }

        public string AdvisorFullName()
        {
            string name = AdvisorFName + ' ' + AdvisorLName;
            return name;
        }

        public clsAdvisor()
        {
        }

    }
}

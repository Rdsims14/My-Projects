using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentSchedulerApp
{
    internal class clsLocation
    {
        private int mLocationID;
        private string mBuildingTitle = "";
        private string mBuildingAddress1 = "";
        private string mBuildingAddress2 = "";
        private string mBuildingCity = "";
        private string mBuildingState = "";
        private string mLocationDescription = "";


        public int LocationID { get { return mLocationID; } set { mLocationID = value; } }
        public string BuildingTitle { get { return mBuildingTitle; } set {  mBuildingTitle = value; } }
        public string BuildingAddress1 { get { return mBuildingAddress1; } set { mBuildingAddress1 = value; } }
        public string BuildingAddress2 { get { return mBuildingAddress2; } set { mBuildingAddress2 = value; } }
        public string BuildingCity { get { return mBuildingCity; } set { mBuildingCity = value; } }
        public string BuildingState { get { return mBuildingState; } set {mBuildingState = value; } }
        public string LocationDescription { get { return mLocationDescription; } set { mLocationDescription = value; } }

    }
}

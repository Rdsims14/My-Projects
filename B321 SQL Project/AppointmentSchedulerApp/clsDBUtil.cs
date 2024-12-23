using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentSchedulerApp
{
    internal class clsDBUtil
    {
        public static string getConnectionString()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "uscb-poole.database.windows.net";
            builder.InitialCatalog = "B321-F24";
            builder.UserID = clsSessionInfo.GetUser();
            builder.Password = clsSessionInfo.GetPass();
            builder.Authentication = SqlAuthenticationMethod.SqlPassword;
            builder.Encrypt = false;

            return builder.ConnectionString.ToString();
        }

    }
}

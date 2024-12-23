using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppointmentSchedulerApp
{
    public partial class frmLogin : Form
    {


        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            string connectionString = clsDBUtil.getConnectionString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("LoginAdvisee", connection);
                command.CommandType = CommandType.StoredProcedure;
                // Get users info
                command.Parameters.AddWithValue("@Username", txtUsername.Text);
                command.Parameters.AddWithValue("@Password", txtPassword.Text);

                try
                {

                    lblNameOfApp.Text = "Connecting";
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            lblNameOfApp.Text = "Reading";
                            while (reader.Read())
                            {
                                clsSessionInfo.UserID = (int)reader["AdviseeID"];
                                clsSessionInfo.UserFName = (string)reader["AdviseeFName"];
                                clsSessionInfo.UserLName = (string)reader["AdviseeLName"];
                            }
                            
                        }
                        else
                        {
                            lblNameOfApp.Text = "Incorrect Login";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    //MessageBox.Show(connectionString);
                }
                finally { connection.Close(); this.Close(); }
            }
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

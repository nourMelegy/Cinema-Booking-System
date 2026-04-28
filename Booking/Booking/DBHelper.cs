using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace BookingFlow
{
    public static class DBHelper
    {        
        private static string connString = @"Server=.;Database=FinalISCinemaProject;Integrated Security=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connString); 
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronwoodLibrary
{
    internal static class Data
    {
        // Decalre an internal connection object
        static internal System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection();

        static Data()
        {
            // set the connection object to the database
            con.ConnectionString = global::IronwoodLibrary.Properties.Settings.Default.devon_IronwoodConnectionString;
        }
    }
}

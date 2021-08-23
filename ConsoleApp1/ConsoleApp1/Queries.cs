using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace ConsoleApp1.NewFolder
{
    class Queries
    {

        static void Main()
        {
            SqlConnection condb = new SqlConnection("Data Source = NOOB; Initial Catalog = DemoProj; Integrated Security = true");
            Console.WriteLine("Establish Connection ");


            SqlCommand insertcmd = new SqlCommand("insert into simp values(2, 'manish')");
            insertcmd.Connection = condb;

            insertcmd.ExecuteNonQuery(); // for executing a non-query type commands 
            Console.WriteLine("Record Inserted ");

        }
    }
}

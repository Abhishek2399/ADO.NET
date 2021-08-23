using System;
using System.Data.SqlClient;



namespace ado1
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection condb = new SqlConnection("server=AEPL7-PC;database=CUSTOMERDB;trusted_connection=True");
            condb.Open();



            SqlCommand cmd = new SqlCommand("select * from emps", condb);
            //cmd.CommandText = "select * from emps";
            //cmd.Connection = condb;



            SqlDataReader red = cmd.ExecuteReader();



            while (red.Read() == true)
            {
                Console.WriteLine("employee code : " + red.GetValue(0));
                Console.WriteLine("employee Name : " + red.GetValue(1));
                Console.WriteLine("------------------------");
            }




            condb.Close();





        }
    }
}
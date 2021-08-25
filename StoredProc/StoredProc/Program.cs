using System;
using System.Data.SqlClient;
using System.Data;

namespace StoredProc
{
    class Program
    {
        private static SqlConnection con = new SqlConnection();
        private static SqlCommand cmd = new SqlCommand();
        private static SqlDataAdapter sda = new SqlDataAdapter(); // interface between the database and dataset, fills the dataset and updates the database
        private static DataSet ds = new DataSet();// a dataset object to fill the data from the database
     
        public static void DisplayAll()
        {
            try
            {
                con.Open();
                //cmd.CommandText = "select * from emps";// this can be done in stored procedure
                // 1. create a procedure in the studio 
                // 2. just use the name of the procedure instead of Query 
                cmd.Connection = con;
                cmd.CommandText = "showEmp"; // calling the procedure 
                cmd.CommandType = CommandType.StoredProcedure;

                sda.SelectCommand = cmd;
                sda.Fill(ds, "emp"); // fill the dataset with data

                Console.WriteLine("------------------------------------------");
                foreach(DataRow row in ds.Tables["emp"].Rows)
                {
                    Console.Write($"Emp ID :{row[0]} | ");
                    Console.Write($"Emp Name :{row[1]} | ");
                    Console.Write($"Dept ID :{row[2]} | ");
                    Console.Write($"Salary :{row[3]} \n");
                    Console.WriteLine("------------------------------------------");
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
        
        
        static void Main(string[] args)
        {
            con.ConnectionString = "Server = NOOB; Database = DemoProj; Trusted_connection =  true";
            DisplayAll();
        }
    }
}

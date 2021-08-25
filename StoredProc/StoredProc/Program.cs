using System;
using System.Data.SqlClient;
using System.Data;

namespace StoredProc
{
    class Program
    {
        private static SqlConnection spCon = new SqlConnection(); // stored Procedure connection 
        private static SqlCommand spCmd = new SqlCommand(); // StoredProc Command 
        
        private static SqlDataAdapter spSda = new SqlDataAdapter(); // interface between the database and dataset, fills the dataset and updates the database
        // StoredProc Adapter 
        private static DataSet ds = new DataSet();// a dataset object to fill the data from the database
     
        public static void DisplayAllEmp() // to Display all employee details 
        {
            try
            {
                //cmd.CommandText = "select * from emps";// this can be done in stored procedure
                // 1. create a procedure in the studio 
                // 2. just use the name of the procedure instead of Query 
                spCmd.Connection = spCon;
                spCmd.CommandText = "showEmp"; // calling the procedure 
                spCmd.CommandType = CommandType.StoredProcedure;

                spSda.SelectCommand = spCmd;
                spSda.Fill(ds, "emp"); // fill the dataset with data

                Console.WriteLine("------------------------------------------");
                foreach(DataRow row in ds.Tables["emp"].Rows) // display all the records 
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
        }
        
        public static void DisplayEmpById() // displaying employee by id 
        {
            try
            {
                spCmd.CommandText = "";
                int idToSearch = 1;

                // create the command we want to execute 
                spCmd.CommandText = "disp_by_id @id";
                spCmd.CommandType = CommandType.StoredProcedure;
                spCmd.Parameters.AddWithValue("@id", idToSearch);

                // bind the adapter with the created command 
                spSda.SelectCommand = spCmd; // select the command we want to execute 
                spSda.Fill(ds, "emp"); // fill the data set with queried data 
                Console.WriteLine("------------------------------------------");
                if (ds.Tables["emp"].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables["emp"].Rows) // display all the records 
                    {
                        Console.Write($"Emp ID :{row[0]} | ");
                        Console.Write($"Emp Name :{row[1]} | ");
                        Console.Write($"Dept ID :{row[2]} | ");
                        Console.Write($"Salary :{row[3]} \n");
                        Console.WriteLine("------------------------------------------");
                    }
                }
                else
                {
                    Console.WriteLine("Record not Found");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }



        
        static void Main(string[] args)
        {
            try
            {
                spCon.ConnectionString = "Server = NOOB; Database = DemoProj; Trusted_connection =  true";
                DisplayAllEmp();
                DisplayEmpById();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                spCon.Close();
            }
        }
    }
}

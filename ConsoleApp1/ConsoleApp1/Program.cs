using System;
using System.Data.SqlClient; // we had to get this from the manage nugget pkgs 
// works as a provider for the sql server
// sqlclient -> SQL server



namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            // As our provider is SQL Server we use SqlConnection class
            // -------------------<Initiating Connection>----------------------
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "server=NOOB;database=DemoProj;trusted_connection=true"; // SQL server information we want to connect 
            // 'server=' name of the server
            // 'database=' name of the database we want to use
            // 'trusted_connection =  true' for windows authentication 
            con.Open(); // Initiating Database Connection
            // 'con' now represents the data base 
            Console.WriteLine("Connection Successful"); // indicator of connection established 
            // ---------------------------------------------------------

            // --------------<Creating an editor to write and execute commands >---------------
            SqlCommand cmd = new SqlCommand();// like the editor to write our commands in the SQL-editor 
            cmd.Connection = con; // linking the database with the cmd 
            cmd.CommandText = "select * from emps";// writing the query 

            SqlDataReader sdr = cmd.ExecuteReader();// executes and reads data and stores in sql data reader
            // Reader reads one data at a time {read-only, forward-only}
            // only read data can't modify it 
            sdr.Read(); // reads the very frist single record 

            Console.WriteLine($"Emp id : {sdr.GetInt32(0)}");// getting the first Column - 0
            Console.WriteLine($"Emp Name : {sdr.GetString(1)}");// getting the 2nd Column - 1
            Console.WriteLine($"Dept id : {sdr.GetInt32(2)}");// getting the 3rd Column - 2
            Console.WriteLine($"Salary : {sdr.GetInt32(3)}");// getting the 4th Column - 3
            
            Console.WriteLine("===============================");

            sdr.Read(); // 2nd Record

            Console.WriteLine($"Emp id : {sdr.GetInt32(0)}");// getting the first Column - 0
            Console.WriteLine($"Emp Name : {sdr.GetString(1)}");// getting the 2nd Column - 1
            Console.WriteLine($"Dept id : {sdr.GetInt32(2)}");// getting the 3rd Column - 2
            Console.WriteLine($"Salary : {sdr.GetInt32(3)}");// getting the 4th Column - 3

            Console.WriteLine("\nQuery Executed\n");


        }
    }
}

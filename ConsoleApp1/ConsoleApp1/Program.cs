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
            Console.WriteLine("Hello World!");
            // As our provider is SQL Server we use SqlConnection class
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "server=NOOB;database=DemoProj;trusted_connection=true"; // SQL server information we want to connect 
            // 'server=' name of the server
            // 'database=' name of the database we want to use
            // 'trusted_connection =  true' for windows authentication 

        }
    }
}

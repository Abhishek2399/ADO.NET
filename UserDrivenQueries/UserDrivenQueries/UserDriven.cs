using System;
using System.Data.SqlClient;


namespace UserDrivenQueries
{

    class UserDriven
    {
        static SqlConnection con = new SqlConnection(); // connection object for establishing connection 

        static SqlCommand cmd = new SqlCommand(); // creating a command object to write and execute commands 
     
        static SqlDataReader sdr; // reader to read one record at a time

        public static void Login() // checking if the data is present 
        {
            // var to hold is user present or not             
            try
            {
                if (sdr != null) // check is reader opened earlier 
                    sdr.Close(); // CLose any pre-existing reader
                Console.WriteLine("Enter the User : ");
                string uName = Console.ReadLine();

                Console.WriteLine("Enter the Password : ");
                string uPass = Console.ReadLine();

                //------------- <Sql Command initiation> ------------- 
                cmd.Connection = con; // connection between the command obj and the db conn
                // not a good way to pass parameters, this my lead to sql injection 
                cmd.CommandText = $"Select * from Users where uid = '{uName}' and pwd = '{uPass}'"; // Checking for availability of single user
                sdr = cmd.ExecuteReader(); // Executing the reading command as our command will send us block of data
                bool isPresent = sdr.HasRows;
                if (isPresent)
                {
                    ShowUsers();
                }
                else
                {
                    Console.WriteLine("User not valid");
                    Console.Clear(); // clearing the console 
                    Login();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public static void ShowUsers()
        {
            try
            {
                if (sdr != null) // check is reader is opened before 
                    sdr.Close(); // CLose any pre-existing reader
                //------------- <Sql Command initiation> ------------- 
                cmd.Connection = con; // connection between the command obj and the db conn
                cmd.CommandText = "Select * from Users"; // Query we want to execute 
                sdr = cmd.ExecuteReader(); // Executing the reading command as our command will send us block of data
                Console.WriteLine("-----------------------------------");
                while (sdr.Read()) // .Read() will return true if data is present in the table 
                {
                    Console.WriteLine($"User : {sdr.GetValue(0)}   Pass : {sdr.GetValue(1)}");
                    Console.WriteLine("-----------------------------------");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                sdr.Close();
            }

        }


        static void Main(string[] args)
        {
            try
            {
                //------------- <SqlConnection Establishment> ------------- 
                con.ConnectionString = "Data Source = NOOB; Initial Catalog = ADODay2; Integrated Security = true";
                // "Data Source" : Server where our db is present 
                // "Initial Catalog" : Name of the Db
                // "Integrated Security" : Windows Authentication / manual user name / password 

                // alternate connection string 
                con.ConnectionString = "Server = NOOB; Database = ADODay2; Trusted_connection = true";

                con.Open();
                Console.WriteLine("Connection Successful");
                //---------------------<>----------------------------------

                Login();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close(); // always close the connection 
            }
        }
    }
}

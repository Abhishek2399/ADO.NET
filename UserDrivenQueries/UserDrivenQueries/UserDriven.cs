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
                // direct way of injecting data, not secured
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


        public static void AddUser()
        {
            try
            {
                string uName, uPass;
                Console.WriteLine("Enter the User : ");
                uName = Console.ReadLine();

                Console.WriteLine("Enter the Password : ");
                uPass = Console.ReadLine();

                cmd.Connection = con;
                cmd.CommandText = "Insert into Users values(@p1, @p2)"; // better and safe way of injecting data 
                // inserting record 
                cmd.Parameters.AddWithValue("@p1", uName);
                cmd.Parameters.AddWithValue("@p2", uPass);

                int isExecuted = cmd.ExecuteNonQuery(); // 1 -> successful execution, 0 -> UnSucessful Execution 
                if (isExecuted > 0)
                {
                    Console.WriteLine("User Added Successfully");
                    return;
                }
                Console.WriteLine("User Not Added");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static void DelUser()
        {
            try
            {
                string uName, uPass;
                Console.WriteLine("Enter the User : ");
                uName = Console.ReadLine();

                cmd.Connection = con;
                cmd.CommandText = "Delete from Users uid = @p1"; // better and safe way of injecting data 
                // inserting record 
                cmd.Parameters.AddWithValue("@p1", uName);

                int isExecuted = cmd.ExecuteNonQuery(); // 1 -> successful execution, 0 -> UnSucessful Execution 
                if (isExecuted > 0)
                {
                    Console.WriteLine("User Deleted Successfully");
                    return;
                }
                Console.WriteLine("User Not Found");
            }
            catch (Exception ex)
            {
                throw ex;
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

                //Login();
                AddUser();

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

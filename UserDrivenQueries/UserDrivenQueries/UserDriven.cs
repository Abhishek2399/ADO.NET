using System;
using System.Data.SqlClient;


namespace UserDrivenQueries
{

    class UserDriven
    {
        static SqlConnection con = new SqlConnection(); // connection object for establishing connection 

        static SqlCommand cmd = new SqlCommand(); // creating a command object to write and execute commands 
     
        static SqlDataReader sdr; // reader to read one record at a time

        #region Login
        public static void Login() // checking if the data is present 
        {
            // var to hold is user present or not
            Console.WriteLine("==================== Login User ====================");

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
        #endregion

        #region Show Users
        public static void ShowUsers()
        {
            Console.WriteLine("==================== Show Users ====================");
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
        #endregion

        #region Add User
        public static void AddUser()
        {
            Console.WriteLine("==================== Add User ====================");

            try
            {
                string uName, uPass;
                Console.WriteLine("Enter the User : ");
                uName = Console.ReadLine();

                Console.WriteLine("Enter the Password : ");
                uPass = Console.ReadLine();

                cmd.Connection = con;
                cmd.CommandText = "Insert into Users values(@uname, @upass)"; // better and safe way of injecting data 
                // inserting record 
                cmd.Parameters.AddWithValue("@uname", uName);
                cmd.Parameters.AddWithValue("@upass", uPass);

                int isInserted = cmd.ExecuteNonQuery(); // 1 -> successful execution, 0 -> UnSucessful Execution 
                cmd.Parameters.Clear();

                if (isInserted > 0)
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
        #endregion 

        #region Delete User
        public static void DelUser()
        {
            Console.WriteLine("==================== Delete User ====================");
            try
            {
                string uName;
                Console.WriteLine("Enter the User : ");
                uName = Console.ReadLine();

                cmd.Connection = con;
                cmd.CommandText = "Delete from Users where uid = @uname"; // better and safe way of injecting data 
                // inserting record 
                cmd.Parameters.AddWithValue("@uname", uName);

                int isDeleted = cmd.ExecuteNonQuery(); // 1 -> successful execution, 0 -> UnSucessful Execution 
                cmd.Parameters.Clear();
                if (isDeleted > 0)
                {
                    Console.WriteLine($"{uName} User Deleted Successfully");
                    return;
                }
                Console.WriteLine($"{uName} Not Found");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Update User Pass
        public static void UpdateUser()
        {
            Console.WriteLine("==================== Update User ====================");
            try
            {
                string uName, uPass;
                Console.WriteLine("Enter the User : ");
                uName = Console.ReadLine();

                Console.WriteLine("Enter New Password : ");
                uPass = Console.ReadLine();


                cmd.Connection = con;
                cmd.CommandText = "Update Users set pwd = @upass where uid = @uname;"; // better and safe way of injecting data 
                // inserting record 
                cmd.Parameters.AddWithValue("@uname", uName);
                cmd.Parameters.AddWithValue("@upass", uPass);


                int isUpdated = cmd.ExecuteNonQuery(); // 1 -> successful execution, 0 -> UnSucessful Execution 
                cmd.Parameters.Clear();
                if (isUpdated > 0)
                {
                    Console.WriteLine($"{uName} User Updated Successfully");
                    ShowUserByName(uName);
                    return;
                }
                Console.WriteLine($"{uName} Not Found");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Specific User Details By Name
        public static void ShowUserByName(string uName)
        {
            Console.WriteLine("==================== Show User By Name ====================");

            try
            {
                if (sdr != null) // check is reader is opened before 
                    sdr.Close(); // CLose any pre-existing reader
                //------------- <Sql Command initiation> ------------- 
                cmd.Connection = con; // connection between the command obj and the db conn
                cmd.CommandText = "Select * from Users where uid = @uname"; // Query we want to execute 

                cmd.Parameters.AddWithValue("@uname", uName);
                sdr = cmd.ExecuteReader(); // Executing the reading command as our command will send us block of data
                Console.WriteLine("-----------------------------------");
                // uid in the table is not primary key we can get multiple data, if it was primary key we would have got single record 
                if (sdr.HasRows)
                {
                    while (sdr.Read()) // .Read() will return true if data is present in the table 
                    {
                        Console.WriteLine($"User : {sdr.GetValue(0)}   Pass : {sdr.GetValue(1)}");
                        Console.WriteLine("-----------------------------------");
                    }
                }
                else
                {
                    Console.WriteLine($"{uName} Not Found");
                }
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sdr.Close();
            }
        }

        #endregion

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
                //ShowUsers();
                //DelUser();
                //UpdateUser();
                ShowUserByName("admin");
                //AddUser();
                
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

using System;
using System.Data.SqlClient;


namespace UserDrivenQueries
{
    class UserDriven
    {
        static void Main(string[] args)
        {
            //------------- <SqlConnection Establishment> ------------- 
            SqlConnection con = new SqlConnection("Data Source = NOOB; Initial Catalog = ADODay2; Integrated Security = true");
            // "Data Source" : Server where our db is present 
            // "Initial Catalog" : Name of the Db
            // "Integrated Security" : Windows Authentication / manual user name / password 

            con.Open();
            Console.WriteLine("Connection Successful");
            //---------------------<>----------------------------------


        }
    }
}

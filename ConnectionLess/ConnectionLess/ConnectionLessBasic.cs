using System;
using System.Data; // for dataset
using System.Data.SqlClient;


namespace ConnectionLess
{
    class ConnectionLessBasic
    {
        static SqlConnection con = new SqlConnection();
        static SqlCommand cmd = new SqlCommand();
        static SqlDataAdapter da = new SqlDataAdapter(); // to fill data in the data set
        public static DataSet ds = new DataSet(); // for locally storing data 


        public static void DisplaySpecificData()
        {
            cmd.Connection = con;
            cmd.CommandText = "select * from Users where uid = @uname";
            cmd.Parameters.AddWithValue("@uname", "Admin");

            da.SelectCommand = cmd;
            da.Fill(ds, "LocalUser");

            for (int i = 0; i < ds.Tables["LocalUser"].Rows.Count; i++)
            {
                Console.WriteLine(ds.Tables["LocatUser"].Rows[i][0]); // first row first col
                Console.WriteLine(ds.Tables["LocalUser"].Rows[i][1]); // first row second col
                Console.WriteLine("--------------------------");
            }


        }

        static void Main(string[] args)
        {
            con.ConnectionString = "Server=NOOB; Database = ADODay2; Trusted_connection=true";
            con.Open();
            
            cmd.Connection = con;
            cmd.CommandText = "select * from Users";
            
            SqlCommandBuilder cmdbuilder = new SqlCommandBuilder(da);// will generate the Queries for the adapter 

            da.SelectCommand = cmd; // get the data from the select command 
            da.Fill(ds); // fill the data set

            con.Close(); // closing the connection of the data base
            // even tho we can see the output which is because of the data set

            // data set is a collection of data tables this has rows,cols
            // data set have the ability of access random data throught the table unlike the reader where only oe record was available at one time 


            Console.WriteLine(ds.Tables[0].Rows[0][0]); // first row first col
            Console.WriteLine(ds.Tables[0].Rows[0][1]); // first row second col

            for(int i = 0; i< ds.Tables[0].Rows.Count; i++)
            {
                Console.WriteLine(ds.Tables[0].Rows[i][0]); // first row first col
                Console.WriteLine(ds.Tables[0].Rows[i][1]); // first row second col
                Console.WriteLine("--------------------------");
            }

            DataRow newr = ds.Tables[0].NewRow();

            newr[0] = "someone";
            newr[1] = "Someon123";

            ds.Tables[0].Rows.Add(newr); 



        }
    }
}

using System;
using System.Data; // for dataset
using System.Data.SqlClient;


namespace ConnectionLess
{
    class Program
    {
        static SqlConnection con = new SqlConnection();
        static SqlCommand cmd = new SqlCommand();
        static SqlDataAdapter adapt = new SqlDataAdapter(); // to fill data in the data set
        public static DataSet ds = new DataSet(); // for locally storing data 

        static void Main(string[] args)
        {
            con.ConnectionString = "Server=NOOB; Database = ADODay2; Trusted_connection=true";
            con.Open();
            
            cmd.Connection = con;
            cmd.CommandText = "select * from Users";

            adapt.SelectCommand = cmd; // get the data from the select command 
            adapt.Fill(ds); // fill the data set

            con.Close(); // closing the connection of the data base
            // even tho we can see the output which is because of the data set

            // data set is a collection of data tables this has rows,cols
            // data set have the ability of access random data throught the table unlike the reader where only oe record was available at one time 


            Console.WriteLine(ds.Tables[0].Rows[0][0]); // first row first col
            Console.WriteLine(ds.Tables[0].Rows[0][1]); // first row second col



            Console.WriteLine("Hello World!");
        }
    }
}

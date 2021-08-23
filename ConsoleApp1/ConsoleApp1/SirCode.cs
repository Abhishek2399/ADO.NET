using System;
using System.Data.SqlClient;



namespace ado1
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection condb = new SqlConnection("server=AEPL7-PC;database=CUSTOMERDB;trusted_connection=True");
            condb.Open();
            //SqlCommand cmd = new SqlCommand("select * from emps", condb);
            ////cmd.CommandText = "select * from emps";
            ////cmd.Connection = condb;



            //SqlDataReader red=cmd.ExecuteReader();



            //while(red.Read()==true)
            //{
            //    Console.WriteLine("employee code : " + red.GetValue(0));
            //    Console.WriteLine("employee Name : " + red.GetValue(1));
            //    Console.WriteLine("------------------------");
            //}




            // to insert record



            //SqlCommand insertcmd = new SqlCommand("insert into simp values(1,'ravi')");
            //insertcmd.Connection = condb;



            //insertcmd.ExecuteNonQuery();
            //Console.WriteLine("record inserted...");



            // to delete record



            //SqlCommand delcmd = new SqlCommand("delete from simp where rno =1");
            //delcmd.Connection = condb;



            //int r = delcmd.ExecuteNonQuery();



            //if (r == 0)
            //    Console.WriteLine("record not found");
            //else
            //    Console.WriteLine("record deleted...");



            // to update record



            SqlCommand upcmd = new SqlCommand("update simp set sname='newname' where rno =1");
            upcmd.Connection = condb;



            int r = upcmd.ExecuteNonQuery();



            if (r == 0)
                Console.WriteLine("record not found");
            else
                Console.WriteLine("record updated...");






            condb.Close();





        }
    }
}
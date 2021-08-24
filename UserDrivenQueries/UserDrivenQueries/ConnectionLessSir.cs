using System;
using System.Data.SqlClient;
using System.Data;



namespace disconnected1
{
    class Program
    {
        private static SqlConnection con = new SqlConnection();
        private static SqlCommand cmd = new SqlCommand();
        private static SqlDataAdapter da = new SqlDataAdapter();

        private static DataSet ds = new DataSet();



        static void Main(string[] args)
        {
            con.ConnectionString = "Server = NOOB; Database = ADODay2; Trusted_connection = true";
            con.Open();

            Console.WriteLine("Connection Successful");

            //cmd.CommandText = "select * from emps";
            //cmd.Connection = cn;



            //SqlCommandBuilder cmdb = new SqlCommandBuilder(da); 



            //da.SelectCommand = cmd;



            //da.Fill(ds, "emps");





            //for(int c=0;c<ds.Tables[0].Rows.Count;c++)
            //{ 
            //Console.WriteLine("employee id   : " + ds.Tables[0].Rows[c][0]);
            //Console.WriteLine("employee Name : " + ds.Tables[0].Rows[c][1]);
            //Console.WriteLine("Department id : " + ds.Tables[0].Rows[c][2]);
            //Console.WriteLine("Salary        : " + ds.Tables[0].Rows[c][3]);
            //}



            //DataRow newr = ds.Tables[0].NewRow();



            //newr[0] = 299;
            //newr[1] = "karthik";
            //newr[2] =100;
            //newr[3] = 2300;



            //ds.Tables[0].Rows.Add(newr);




            //da.Update(ds, "emps");   // update database 



            dispemp();



            cn.Close();




        }



        public static void showall()
        {



        }
        public static void dispemp()
        {
            int v;
            Console.WriteLine("enter employee id");
            v = Convert.ToInt32(Console.ReadLine());



            cn.Open();
            cmd.CommandText = "select * from emps where eid=@p";
            cmd.Connection = cn;
            cmd.Parameters.AddWithValue("@p", v);



            da.SelectCommand = cmd;
            da.Fill(ds, "emps");



            if (ds.Tables[0].Rows.Count != 0)
            {
                //for (int c = 0; c < ds.Tables[0].Rows.Count; c++)
                //{
                //    Console.WriteLine("employee id   : " + ds.Tables[0].Rows[c][0]);
                //    Console.WriteLine("employee Name : " + ds.Tables[0].Rows[c][1]);
                //    Console.WriteLine("Department id : " + ds.Tables[0].Rows[c][2]);
                //    Console.WriteLine("Salary        : " + ds.Tables[0].Rows[c][3]);
                //}




                foreach (DataRow r in ds.Tables[0].Rows)
                {



                    Console.WriteLine("employee id   : " + r[0]);
                    Console.WriteLine("employee Name : " + r[1]);
                    Console.WriteLine("Department id : " + r[2]);
                    Console.WriteLine("Salary        : " + r[3]);
                }
            }
            else
            {
                Console.WriteLine("record not found");
            }



            cn.Close();
        }
    }
}
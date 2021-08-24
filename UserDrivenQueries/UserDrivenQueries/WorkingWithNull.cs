using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;


namespace UserDrivenQueries
{
    class WorkingWithNull
    {
        static SqlConnection con = new SqlConnection();
        static SqlCommand cmd = new SqlCommand();
        static SqlDataReader sdr;

        public static void GetAllStuds()
        {
            try
            {
                if (sdr != null)
                    sdr.Close();
                cmd.Connection = con;
                cmd.CommandText = "Select * from students";
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    int rollno = sdr.IsDBNull(0) ? 0 : (int)sdr.GetValue(0); // replacing null values in the code with the "IsDBNull" function returns true if the value in the column is null
                    string name = sdr.IsDBNull(1) ? "Null" : (string)sdr.GetValue(1);
                    Console.WriteLine($"Name : {name}\nRoll No. : {rollno}\n");
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

        public static bool AddStudent()
        {
            try
            {
                string name;
                int rollNo;
                Console.WriteLine("Enter the name of the Student : ");
                name = Console.ReadLine();

                Console.WriteLine("Enter the Roll no. of the Student : ");
                rollNo = Convert.ToInt32(Console.ReadLine());

                cmd.Connection = con;
                cmd.CommandText = "Insert into Student values(@name, @rollno)";
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@rollno", rollNo);

                int isInserted = cmd.ExecuteNonQuery();
                if(isInserted > 0)
                {
                    Console.WriteLine("Student Successfully added");
                }
                else
                {
                    Console.WriteLine("Student Not Added");
                    return false;
                } 
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Parameters.Clear();
            }
            return true;

        }



        static void Main()
        {
            try
            {
                con.ConnectionString = "Server = NOOB; Database = ADODay2; Trusted_connection = true";
                con.Open();
                AddStudent();
                GetAllStuds();
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}

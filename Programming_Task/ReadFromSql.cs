using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Configuration;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Microsoft.VisualBasic.FileIO;

namespace Programming_Task
{
    class ReadFromSql : SqlHandlerBase
    { 
        public ReadFromSql()
        {
        }

        //Returns an answer to query1 from SQL according to the coutry the user selected.
        public void AnswerQuery_1(string countryName)
        {
            try
            {
                //sql connection object
                using (SqlConnection conn = CreateConnection())
                {
                    //retrieve the SQL Server instance version
                    string query = @"SELECT Confirmed , Last_Update
                                     FROM CovidData tb
                                     WHERE tb.Country_Region = @Country      
                                     AND tb.Last_Update >= '06/12/2021' AND tb.Last_Update < '17/12/2021' 
                                     ORDER BY tb.Last_Update";

                    // AND CAST(tb.Last_Update AS datetime) > DATEADD(day,-10, GETDATE()); --- cause an error

                    //define the SqlCommand object
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.Add("@Country", countryName);

                    //open connection
                    conn.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                           
                            string numberOfConfirmed = dr.GetString(0);
                            string date = dr.GetString(1);
                            Console.WriteLine(JsonConvert.SerializeObject(new { Date = date ,NumberOfConfirmed = numberOfConfirmed }));                   
                        }
                    }                     
                }
            }           
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        //Returns an answer to query2 from SQL
        public void AnswerQuery_2()
        {
            try
            {
                //sql connection object
                using (SqlConnection conn = CreateConnection())
                {
                    //retrieve the SQL Server instance version
                    string query = @"SELECT TOP 10 Country_Region, SUM(isnull(cast(Confirmed as float),0)) as confirmed,
                                     SUM(isnull(cast(Deaths as float),0))
                                     FROM CovidData 
                                     GROUP BY Country_Region
                                     ORDER BY confirmed DESC";

                    //define the SqlCommand object
                    SqlCommand cmd = new SqlCommand(query, conn);

                    //open connection
                    conn.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            string Country = dr.GetString(0);
                            double numberOfConfirmed = dr.GetDouble(1);
                            double numberOfDeath = dr.GetDouble(2);
                            Console.WriteLine(JsonConvert.SerializeObject(new { Country = Country, NumberOfConfirmed = numberOfConfirmed, numberOfDeath= numberOfDeath }));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

    }
}

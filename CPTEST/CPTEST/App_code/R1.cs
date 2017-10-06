using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CPTEST.App_code
{
    public class R1
    {
        SqlConnection con = new SqlConnection(@"Data Source=PEN-GM; Initial Catalog=Cane_Test; Integrated Security=true;");
        
        public double gethr(DateTime start, DateTime end) {

            DateTime dft = new DateTime (1858, 11, 18);
            double result;

            if (start == dft)
            {
                result = 0;

            }
            else {
                double dt = (end.Date - start.Date).TotalDays;
                int num = Convert.ToInt32(dt);
                //int total = num * 24;
                result = num;
            }

            return result;
        }

        public double[] getrate(int val) {

            double[] data = new double[2];

            if (val > 0 && val <=7 ){

                string penalty_rate = "";
                string fsc_retension = "";

                using (SqlCommand cmd99 = new SqlCommand("select penalty_rates, FSC_BC_RETENTION from penalty_rates where DELAY_DAYS = @Val"))
                {
                    SqlDataReader reader;
                    cmd99.CommandType = CommandType.Text;
                    cmd99.Connection = con;
                    con.Open();
                    cmd99.Parameters.AddWithValue("@Val", val);
                    reader = cmd99.ExecuteReader();
                    while (reader.Read())
                    {
                        penalty_rate = reader["penalty_rates"].ToString();
                        fsc_retension = reader["FSC_BC_RETENTION"].ToString();

                    }
                    reader.Close();
                    reader.Dispose();
                    con.Close();
                }
                if (Convert.ToInt32(penalty_rate) != 0) {
                    data[0] = Convert.ToDouble(penalty_rate) * 0.01;
                    data[1] = Convert.ToDouble(fsc_retension) * 0.01;
                }

            }
            else{

                string penalty_rate = "";
                string fsc_retension = "";
                int days = 7;
                using (SqlCommand cmd99 = new SqlCommand("select penalty_rates, FSC_BC_RETENTION from penalty_rates where DELAY_DAYS = @Val"))
                {
                    SqlDataReader reader;
                    cmd99.CommandType = CommandType.Text;
                    cmd99.Connection = con;
                    con.Open();
                    cmd99.Parameters.AddWithValue("@Val", days);
                    reader = cmd99.ExecuteReader();
                    while (reader.Read())
                    {
                        penalty_rate = reader["penalty_rates"].ToString();
                        fsc_retension = reader["FSC_BC_RETENTION"].ToString();

                    }
                    reader.Close();
                    reader.Dispose();
                    con.Close();
                }
                if (Convert.ToInt32(penalty_rate) != 0)
                {
                    data[0] = Convert.ToDouble(penalty_rate) * 0.01;
                    data[1] = Convert.ToDouble(fsc_retension) * 0.01;
                }
            }
            return data;
        }

        public double price(int season) {
            string str = "select delivery_price from price where price_season = @season";
            SqlCommand com = new SqlCommand(str, con);
            con.Open();
            com.Parameters.AddWithValue("@season", season);
            double price = Convert.ToDouble(com.ExecuteScalar());
            con.Close();
            return price;
        }
    }
}
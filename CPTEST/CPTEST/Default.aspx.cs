using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CPTEST.App_code;


namespace CPTEST
{
    public partial class Default : System.Web.UI.Page
    {

        SqlConnection con = new SqlConnection(@"Data Source=PEN-GM; Initial Catalog=Cane_Test; Integrated Security=true;");
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Double deli_price = 39.38;
            string next_cut = "2017 - 08 - 21";
            string before_cut = "2017 - 08 - 14";
            int season_no = 2017;
            string start_date = "2017 - 08 - 14";
            string data_date = "2017 - 08 - 15";

            string getrec = "select * from daily_transactions where LOAD_DATE = '2017 - 08 - 15' and REC_TYPE = 001 and truck_lorry_flag = 'T' and TOTAL_TONS != 0";

            SqlCommand cmd = new SqlCommand(getrec, con);
            cmd.CommandType = CommandType.Text;

            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string getrec = "select * from daily_transactions where LOAD_DATE = '2017 - 08 - 15' and REC_TYPE = 001 and truck_lorry_flag = 'T' and TOTAL_TONS != 0 and BURNT_FLAG = 2 and BURNT_DATE != '1858-11-18'";

            SqlCommand cmd = new SqlCommand(getrec, con);
            cmd.CommandType = CommandType.Text;

            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
            ViewState["dtt"] = dt;
            GridView1.DataSource = dt;
            GridView1.DataBind();

            string release_cut = "2017 - 08 - 21";

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            DateTime strt = new DateTime(2017, 11, 5);
            DateTime end = new DateTime(2017, 11, 7);
            string s = "2017-08-12 13:00:00.000";
            string p = "2017-08-14 18:00:00.000";
            DateTime s1 = Convert.ToDateTime(s);
            DateTime e1 = Convert.ToDateTime(p);
            R1 date = new R1();


            double[] data = date.getrate(9);

           // Label1.Text = date.gethr(s1, e1).ToString();

          //  Label1.Text = data[0].ToString();
            
            DataTable tablevals = (DataTable)ViewState["dtt"];

            for (int i = 0; i < tablevals.Rows.Count; i++)
            {
                string mill = tablevals.Rows[i][1].ToString().Trim();
                string sector = tablevals.Rows[i][2].ToString().Trim();
                string grower = tablevals.Rows[i][3].ToString().Trim();
                string ws = tablevals.Rows[i][4].ToString().Trim();
                string load = tablevals.Rows[i][18].ToString().Trim();
                string total_ton = tablevals.Rows[i][8].ToString().Trim();
                string season = tablevals.Rows[i][22].ToString().Trim();
                string burnt_flag = tablevals.Rows[i][5].ToString().Trim();
                string gang = tablevals.Rows[i][21].ToString().Trim();
                string district = tablevals.Rows[i][23].ToString().Trim();
                string burnt = tablevals.Rows[i][9].ToString().Trim();
                string mainline = tablevals.Rows[i][11].ToString().Trim();

                double[] ratedata = date.getrate(1);
                double penrate = ratedata[0];
                double deli_price = date.price(2017);

                double price = penrate * deli_price;
                double amount = Convert.ToDouble(total_ton) * deli_price;
                double amount1 = amount * penrate;
                SqlCommand sCommand;

                string strr = @"insert into bc_deduction_3wtd";
                strr += "(sector_no, gang_no, grower_no, quantity, price, amount, mill_no, date_advanced, reference_no, district_no, weekly_cutoff_date, canepay_process_flag, delay_days, season_no) ";
                strr += "Values ";
                strr += "(@sec,@gang,@grow,@quan,@price,@amt,@mill, @date, @ref, @dis, @week, @flag, @delay, @season)";

                sCommand = new SqlCommand(strr, con);
                sCommand.Parameters.AddWithValue("@sec", sector);
                sCommand.Parameters.AddWithValue("@gang", gang);
                sCommand.Parameters.AddWithValue("@grow", grower);
                sCommand.Parameters.AddWithValue("@quan", total_ton);
                sCommand.Parameters.AddWithValue("@price", price);
                sCommand.Parameters.AddWithValue("@amt", amount1);
                sCommand.Parameters.AddWithValue("@mill", mill);
                sCommand.Parameters.AddWithValue("@date", load);
                sCommand.Parameters.AddWithValue("@ref", ws);
                sCommand.Parameters.AddWithValue("@dis", district);
                sCommand.Parameters.AddWithValue("@week", sec);
                sCommand.Parameters.AddWithValue("@flag", "0");
                sCommand.Parameters.AddWithValue("@delay", sec);
                sCommand.Parameters.AddWithValue("@season", season);
                con.Open();
                sCommand.ExecuteNonQuery();
                con.Close();
            }

            
        }


    }
}
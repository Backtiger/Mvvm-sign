using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvvmsign.DBConnection
{
    public class DBCon
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-TKOPMES\\SQLEXPRESS; Initial Catalog=Sign; Integrated Security=True");


        int executeflag;

        public DataTable DataAdapter(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.Fill(dt);
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }

            return dt;
        }

        public int ExcuteNonquery(string query)
        {

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                executeflag = cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();

            }
            catch (Exception ex)
            {
                con.Close();
                System.Windows.Forms.MessageBox.Show(query);
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }

            return executeflag;

        }
    }
}

using Mvvmsign.DBConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consent2.Query
{
    class DalMakeChart
    {
        DBCon con = new DBCon();
        public DataTable SelectMackeChartList()
        {
            string query = @"SELECT MAKE_SEQ
                                  , CUSTOMER_SEQ
                                  , CUSTOMER_NAME
                                  , CHART_SEQ
                                  , CHART_NAME
                                  , CHART_PATH
                                  , USER_ID
                                  , USER_NAME
                                  , MAKE_DATE                          
                                  , REMARK
                               FROM MAKECHART";

            return con.DataAdapter(query);
        }

        public int InsertMackeChartList(string customerseq,string customername, string chartseq , string chartname,string chartpath ,string userid,string username,string remark)
        {
            string query = @"INSERT MAKECHART 
                               INTO (MAKE_SEQ
                                  , CUSTOMER_SEQ
                                  , CUSTOMER_NAME
                                  , CHART_SEQ
                                  , CHART_NAME
                                  , CHART_PATH
                                  , USER_ID
                                  , USER_NAME
                                  , MAKE_DATE                          
                                  , REMARK )
                             VALUES (NEXT VALUE FOR dbo.seq,'"+ customerseq + "','"+ customername + "','"+chartseq+"','"+ chartname + "','"+ chartpath + "','"+userid+"','"+ username + "','SELECT CONVERT(DATE,GETDATE())''"+ remark + "')";

            return con.ExcuteNonquery(query);
        }

        public int DeleteMakeChart(string Makeseq)
        {
            string query = @"DELETE FROM MAKECHART WHERE MAKE_SEQ ='" + Makeseq + "'";

            return con.ExcuteNonquery(query);
        }
    }
}

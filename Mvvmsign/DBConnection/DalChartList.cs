using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvvmsign.DBConnection
{
    class DalChartList
    {
        DBCon con = new DBCon();

        public DataTable SelectChartList()
        {
            string query = @"SELECT CHART_SEQ
                                  , CHART_NAME
                                  , CHART_MAKEDATE
                                  , CHART_PATH    
                                  , REMARK                                 
                               FROM SIGN.DBO.CHARTLIST";
            
            return con.DataAdapter(query);
        }

        public int InsertChartList(string ChartName, string Chartpath,string remark)
        {
            string query = @"INSERT INTO CHARTLIST 
                                  ( CHART_SEQ
                                  , CHART_NAME
                                  , CHART_MAKEDATE
                                  , CHART_PATH
                                  , REMARK ) 
                            VALUES (NEXT VALUE FOR dbo.seq, '"+ChartName+"',CONVERT(DATE,GETDATE()),'"+ Chartpath + "','"+ remark+"')";

            return con.ExcuteNonquery(query);
        }

        public int DeleteChartList(string Chartseq)
        {
            string query = @"DELETE FROM CHARTLIST WHERE CHART_SEQ ='" + Chartseq + "'";

            return con.ExcuteNonquery(query);
        }
    }
}

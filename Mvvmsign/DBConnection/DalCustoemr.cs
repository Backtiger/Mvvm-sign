using Mvvmsign.DBConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consent2.Query
{
    public class DalCustoemr
    {
        DBCon con = new DBCon();

        public DataTable SelectCustomer()
        {
            string query = @"SELECT CUSTOMER_SEQ
                                 , CUSTOMER_NAME
                                 , CUSTOMER_BIRTH
                                 , CUSTOMER_SEX
                                 , CUSTOMER_PHONE
                                 , CUSTOMER_ADDRESS
                                 , REMARK
                              FROM CUSTOMER";

            return con.DataAdapter(query);
        }

        public int InsertCustomer( string cusName, string Birthday , bool sex , string Phone ,string Address, string Remark)
        {
            string query = @"INSERT INTO CUSTOMER 
                                  ( CUSTOMER_SEQ
                                  , CUSTOMER_NAME
                                  , CUSTOMER_BIRTH
                                  , CUSTOMER_SEX
                                  , CUSTOMER_PHONE
                                  , CUSTOMER_ADDRESS
                                  , REMARK) 
                            VALUES (NEXT VALUE FOR dbo.seq,'" + cusName+"','"+Birthday+"','"+sex+"','"+Phone+"','"+Address+"','"+Remark+"')";


            return con.ExcuteNonquery(query);
        }

        public int DeleteCustomer(string custseq)
        {
            string query = @"DELETE FROM CUSTOMER WHERE CUSTOMER_SEQ ='"+ custseq + "'";

            return con.ExcuteNonquery(query);
        }



    }
}

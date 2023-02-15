using Mvvmsign.DBConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consent2.Query
{
    class DalUser
    {
        DBCon con = new DBCon();

        public DataTable SelectUser()
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

        public DataTable Login(string userid , string userpw)
        {
            string query = @"SELECT USER_ID
                                  , USER_PW                               
                               FROM [DBO].[USER]
                              WHERE USER_ID = '"+ userid +"'" +
                              " AND USER_PW = '"+userpw+"'";

            return con.DataAdapter(query);
        }

        public int InsertUser(string userid, string userpw,string username,string datetime)
        {
            string query = @"INSERT INTO [DBO].[USER]
                                  ( USER_ID
                                  , USER_PW   
                                  , USER_NAME
                                  , USER_MAKEDATE
                                  )
                             VALUES ( '" + userid + "','" + userpw + "','" + username + "','" + datetime + "' )"; 
                             

            return con.ExcuteNonquery(query);
        }
    }
}

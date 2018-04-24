using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Windows;

namespace WpfWithNet.dbCUID
{
    class AccessCUID
    {

        public DataTable dt;

        public DataTable Query(string strSql,string path)
        {
            string strPath= path;

            OleDbConnection cnn = new OleDbConnection();
            DataTable dt = new DataTable();

            cnn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strPath;
            cnn.Open();

            OleDbDataAdapter da = new OleDbDataAdapter(strSql, cnn);
            da.Fill(dt);
            da.Dispose();
            cnn.Close();
            return dt;
        }

        public void Delete(string strSql, string path)
        {
            string strPath = path;

            OleDbConnection cnn = new OleDbConnection()
            {
                ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strPath
            };
            cnn.Open();

            OleDbDataAdapter da = new OleDbDataAdapter(strSql, cnn);

            da.Dispose();
            cnn.Close();
        }

        public void Insert(string strSql, string path)
        {
            string strPath = path;

            OleDbConnection cnn = new OleDbConnection();

            cnn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strPath;
            cnn.Open();

            OleDbDataAdapter da = new OleDbDataAdapter(strSql, cnn);

            da.Dispose();
            cnn.Close();
        }

        public void Update(string strSql, string path)
        {
            string strPath = path;

            OleDbConnection cnn = new OleDbConnection();

            cnn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strPath;
            cnn.Open();

            //OleDbDataAdapter da = new OleDbDataAdapter(strSql, cnn);
            OleDbCommand comm = new OleDbCommand(strSql, cnn);

            comm.ExecuteNonQuery();


            //da.Dispose();
            cnn.Close();
        }
        //计数
        public int Count(string strSql, string path)
        {
            string strPath = path;

            OleDbConnection cnn = new OleDbConnection();

            cnn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strPath;
            cnn.Open();

            OleDbCommand comm = new OleDbCommand(strSql, cnn);
            int num=(int)comm.ExecuteScalar();
            cnn.Close();
            return num;
            
        }

        //多重计数
        public int[] Counts(string[] strSqls, string path)
        {
            string strPath = path;
            int[] nums=new int[strSqls.Count()];

            OleDbConnection cnn = new OleDbConnection();

            cnn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strPath;
            cnn.Open();
            int i = 0;
            foreach (string strSql in strSqls)
            {
                 OleDbCommand comm = new OleDbCommand(strSql, cnn);
                nums[i] = (int)comm.ExecuteScalar();
                i++;
            }

            cnn.Close();
            return nums;

        }
        //批量插入
        public void InsertByBatch(string[] sqls,string path)
        {
            OleDbConnection cnn = new OleDbConnection();

            cnn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path;
            cnn.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = cnn;
            cmd.Transaction = cnn.BeginTransaction();

            for (int n = 0; n < sqls.Length; n++)
            {
                string strsql = sqls[n].ToString();
                if (strsql.Trim().Length > 1)
                {
                    
                    cmd.CommandText = strsql;
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch(Exception ex)
                    {
                        //MessageBox.Show(n + ex.ToString());
                    }
                    
                }
            }
            cmd.Transaction.Commit();  //提交事务
           
        }
    }
}

using System.Data;
using System.Data.OleDb;

namespace WpfWithNet.dbCUID
{
    class ExcelCUID
    {
        public DataTable Query(string strSql, string path)
        {
            string strPath = path;

            OleDbConnection cnn = new OleDbConnection();
            DataTable dt = new DataTable();

            cnn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strPath+ ";Extended Properties='Excel 12.0;HDR=yes;IMEX=1'";
            cnn.Open();

            OleDbDataAdapter da = new OleDbDataAdapter(strSql, cnn);
            da.Fill(dt);
            da.Dispose();
            cnn.Close();
            return dt;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfWithNet.dbCUID;

namespace WpfWithNet.StoredProcedure
{
    class NormalClass
    {
        private string queryname;
        public string Queryname { get => queryname; set => queryname = value; }
        private string querystatement;
        public string Querystatement { get => querystatement; set => querystatement = value; }
        private int count;
        public int Count { get => count; set => count = value; }
        private DataTable dtResult;
        public DataTable DtResult { get => dtResult; set => dtResult = value; }
        private string path;
        public string Path { get => path; set => path = value; }

        

        public void CN(string UserName)
        {
            UseProcedure("国内",UserName);
        }

        public void Foreign(string UserName)
        {
            UseProcedure("涉外", UserName);
        }

        public void Todo(string UserName)
        {
            UseProcedure("可处理", UserName);
        }

        public void FirstVirsion(string UserName)
        {
            UseProcedure("初稿", UserName);
        }

        public void FirstVirsion_ThisMonth(string UserName)
        {
            UseProcedure("当月初稿", UserName);
        }

        public void Done_ThisMonth(string UserName)
        {
            UseProcedure("当月提交", UserName);
        }

        public void OA_Total(string UserName)
        {
            UseProcedure("OA总数", UserName);
        }

        public void OAin30(string UserName)
        {
            UseProcedure("30天内OA", UserName);
        }

        public void Recent(string UserName)
        {
            UseProcedure("最近提交", UserName);
        }

        public void Cases(string CaseID)
        {
            UseProcedure("ID查案件", CaseID);
        }

        public void Tasks(string TaskID)
        {
            UseProcedure("ID查任务", TaskID);
        }

        public void FirstVirsion_Group_ThisMonth()
        {
            UseProcedure("部门当月初稿");
        }

        public void Done_Group_ThisMonth()
        {
            UseProcedure("部门当月提交");
        }

       

        public void OverStock()
        {
            UseProcedure("积压案件");
        }

        public void OutofLimit()
        {
            UseProcedure("超期案件");
        }

        public void OutofLimitByHand(string name)
        {
            UseProcedure("2017年中以后超期案件",name);
        }
        public void NewApp_Total(string name)
        {
            UseProcedure("2017年中以后总案件", name);
        }

        public void NewAppTotalWeight(string name)
        {
            UseProcedure("2018年完成的新申请初稿", name);
        }

        public void OACount(string name)
        {
            UseProcedure("2018年完成的OA初稿", name);
        }


        public void Namelist()
        {
            UseProcedure("电子部人员");
        }


        private void UseProcedure(string proName)
        {
            string strPath = Path + "MyData\\dataset.accdb";
            DataTable dt = new DataTable();
            OleDbConnection cnn = new OleDbConnection();
            cnn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strPath;
            OleDbCommand cmd = new OleDbCommand(proName, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            dtResult = dt;
            count = dt.Rows.Count;
            da.Dispose();
            cnn.Close();

        }

        private void UseProcedure(string proName,string UserNameOrID)
        {
            string strPath = Path + "MyData\\dataset.accdb";
            DataTable dt = new DataTable();
            OleDbConnection cnn = new OleDbConnection();
            cnn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strPath;
            OleDbCommand cmd = new OleDbCommand(proName, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            OleDbParameter parName = new OleDbParameter("UserName", OleDbType.VarChar);
            parName.Value = UserNameOrID;
            cmd.Parameters.Add(parName);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            dtResult = dt;
            count = dt.Rows.Count;
            da.Dispose();
            cnn.Close();

        }

        
        public int[] numlist(string UserName)
        {
            int[] num=new int[6];
            string strPath = "C:\\WORK\\MyData\\dataset.accdb";
            DataTable dt = new DataTable();
            OleDbConnection cnn = new OleDbConnection();
            cnn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strPath;
            OleDbCommand cmd;
            OleDbDataAdapter da;
            string[] types = new string[] { "国内", "涉外", "可处理", "初稿", "OA总数", "30天内OA" };
            int i = 0;
            foreach(string type in types)
            {
                cmd = new OleDbCommand(type, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                OleDbParameter parName = new OleDbParameter("UserName", OleDbType.VarChar);
                parName.Value = UserName;
                cmd.Parameters.Add(parName);
                da = new OleDbDataAdapter(cmd);
                da.Fill(dt);            
                num[i] = dt.Rows.Count;
                i++;
                dt.Clear();
                cmd.Dispose();
                da.Dispose();
            }               
            
            cnn.Close();

            return num;

        }
        
        
    }
}

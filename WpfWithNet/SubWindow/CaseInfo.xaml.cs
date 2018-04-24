using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfWithNet.dbCUID;
using WpfWithNet.StoredProcedure;
using WpfWithNet.Models;
using System.Data;

namespace WpfWithNet.SubWindow
{
    /// <summary>
    /// Interaction logic for CaseInfo.xaml
    /// </summary>
    public partial class CaseInfo : Window
    {
        public string CaseID { get; set; }
        public string AttorneyNum { get; set; }
        public string CurrentDir;
        public bool IsAuthorized { get; set; }
        public bool IsFinished { get; set; }
        //public string[] LegalStatus { get; set; }
        public int i { get; set; }
        public CaseInfo()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            NormalClass nc = new NormalClass();
            nc.Path = CurrentDir;
            nc.Cases(CaseID);

            PatentCase pc = new PatentCase();
            pc = DTtoCaseInfo(nc.DtResult);
            this.DataContext = pc;

            lvwHistory.ItemsSource = nc.DtResult.DefaultView;

        }
//刷新案件历史
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string sql = "Select * from 案件任务 where";
            sql = sql + " (我方案号 like '%" + AttorneyNum + "%') order by 初稿时限 asc";

            AccessCUID acuid = new AccessCUID();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = acuid.Query(sql, "C:\\WORK\\MyData\\dataset.accdb");
            //MessageBox.Show(dt.Rows.Count.ToString());
            lvwHistory.ItemsSource = dt.DefaultView;
        }

        //把从数据库中查询所得到的数据表转换为PatentCase对象
        private PatentCase  DTtoCaseInfo(DataTable dt)
        {

            PatentCase pc = new PatentCase();
            DataRow dr = dt.Rows[0];
            pc.AttorneyNum = dr["我方文号"].ToString();
            pc.ClientNum = dr["客户文号"].ToString();
            pc.DocName = dr["开案名称"].ToString();
            pc.InventionName = dr["案件名称"].ToString();
            pc.Applicant = dr["客户名称"].ToString();
            pc.ApplicationNum = dr["客户名称"].ToString();
            pc.InternalState=dr["内部状态"].ToString();
            pc.TechField = dr["技术领域"].ToString();
            //List<TaskDetail> tasks = new List<TaskDetail>();
            //foreach (DataRow dr1 in dt.Rows)
            //{
            //    TaskDetail td = new TaskDetail()
            //    {

            //        CaseType = dr["任务名称"].ToString() + "：" + dr["任务属性"].ToString(),
            //        DispatchDate = (DateTime)dr["配案日"],

            //        TaskStatus = dr["代理人处理状态"].ToString()
            //    };
            //    try
            //    {
            //        if (dr["初稿期限(外)"].ToString() != "")
            //        {
            //            td.FirstVirsionDeadline = (DateTime)dr["初稿期限(外)"];
            //        }
            //        else
            //        {
            //            td.FirstVirsionDeadline = (DateTime)dr["初稿期限(内)"];
            //        }


            //    }
            //    catch
            //    {

            //    }

            //    try
            //    {
            //        if (dr["官方期限"].ToString() != "")
            //        {
            //            td.Deadline = (DateTime)dr["官方期限"];
            //        }
            //        else
            //        {
            //            td.Deadline = (DateTime)dr["定稿期限(内)"];
            //        }
            //    }
            //    catch
            //    {
            //    }
            //    try
            //    {
            //        td.FirstVirsionDoneDate = (DateTime)dr["初稿日"];
            //    }
            //    catch
            //    {
            //    }
            //    try
            //    {
            //        td.DoneDate = (DateTime)dr["完成日"];
            //    }
            //    catch
            //    {
            //    }
            //    tasks.Add(td);
            //}

            //pc.Tasks = tasks;

            return pc;
        }


    }
}

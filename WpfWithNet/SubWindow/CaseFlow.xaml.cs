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
using WpfWithNet.Models;
using System.Data;


namespace WpfWithNet.SubWindow
{
    /// <summary>
    /// Interaction logic for CaseFlow.xaml
    /// </summary>
    public partial class CaseFlow : Window
    {
        public string CaseID;
        public CaseFlow()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            AccessCUID acuid = new AccessCUID();
            string sql = "Select * from 案件任务,案件信息 where 案件任务.我方案号=案件信息.我方案号 and 案件任务.我方案号='"+CaseID+"' order by 初稿时限 asc"; 

            DataTable dt = new DataTable();

            dt = acuid.Query(sql, "C:\\WORK\\MyData\\数据集.accdb");
            lvwCaseFlow.ItemsSource = DTtoTaskDetail(dt);
            lvwCaseFlow.View = lvwCaseFlow.FindResource("tileView") as ViewBase;
            

        }


        //把从数据库中查询所得到的数据表转换为TaskDetails对象
        private List< TaskDetail >DTtoTaskDetail(DataTable dt)
        {
            List<TaskDetail> tds = new List<TaskDetail>();
            for (int i=0;i<dt.Rows.Count;i++)
            {
                DataRow dr = dt.Rows[i];
                TaskDetail td = new TaskDetail()
                {
                    AttorneyNum = dr["案件任务.我方案号"].ToString(),
                    ClientNum = dr["客户案号"].ToString(),
                    ClientName = dr["申请人"].ToString(),
                    //Applicant = dr["申请人"].ToString(),
                    DocName = dr["交底名称"].ToString(),
                    InventionName = dr["发明名称"].ToString(),
                    Inventor = dr["技术联系人"].ToString(),
                    IsAuthorized = (bool)dr["是否授权"],
                    IsRejected = (bool)dr["是否结案"],
                    FinishReason = dr["结案原因"].ToString(),
                    //CaseSource=dr["接案人"].ToString(),
                    ApplicationNum = dr["申请号"].ToString(),
                    CaseType = dr["案件类型"].ToString(),
                    IP = dr["IP"].ToString(),
                    //FirstVirsionDeadline=(DateTime)dr["初稿时限"],
                    //FirstVirsionDoneDate = (DateTime)dr["初稿时间"],
                    //Deadline = (DateTime)dr["完成时限"],
                    // DoneDate = (DateTime)dr["提交时间"],
                    Taskinfo = dr["备注"].ToString(),
                    Attorney = dr["代理人"].ToString(),
                    Weight = (double)dr["权值"],
                    TaskStatus = dr["案件状态"].ToString()
                };
                try
                {
                    td.FirstVirsionDeadline = (DateTime)dr["初稿时限"];
                    td.Deadline = (DateTime)dr["完成时限"];
                    td.FirstVirsionDoneDate = (DateTime)dr["初稿时间"];
                    td.DoneDate = (DateTime)dr["提交时间"];
                    td.FinishDate = (DateTime)dr["结案时间"];
                }
                catch
                {

                }
                tds.Add(td);
            }
           

            return tds;
        }

    }
}

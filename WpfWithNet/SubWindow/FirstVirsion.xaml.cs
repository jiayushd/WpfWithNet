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
using System.Data;
using WpfWithNet.dbCUID;
using WpfWithNet.Models;

namespace WpfWithNet.SubWindow
{
    /// <summary>
    /// Interaction logic for FirstVirsion.xaml
    /// </summary>
    public partial class FirstVirsion : Window
    {
        public DataTable dt;
        public string UserName;
        public string CurrentDir;
        public FirstVirsion()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {

            //DateTime startDate = (DateTime)dpStartDate.SelectedDate;
            //DateTime endDate = (DateTime)dpEndDate.SelectedDate;
            ////string[] members = new string[] { UserName };
            //string sql = "select 我方文号,客户名称,申请类型,任务名称,任务属性,任务标识,承办人,初稿日,完成日 from Tasks,Cases where Tasks.案件ID=Cases.案件ID and 任务名称 not in ('请求实审') and (承办人='" + UserName + "') and (初稿日 ";
            //QueryData qd = new QueryData();
            //qd.Query_Customized(UserName,qd.GetFilter_FirstVirsion(startDate, endDate));
            //WeightData wtdt = new WeightData();
            //wtdt = CalculateWeight2(startDate, endDate, sql);
            //Firstdg.ItemsSource = qd.DtResult.DefaultView;
            //tbTotalWeight.Text = wtdt.Totalweight.ToString();          

        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application xlsApp = new Microsoft.Office.Interop.Excel.Application();
            //Microsoft.Office.Interop.Excel.Workbooks xlsWorkBook = new List Microsoft.Office.Interop.Excel.Workbook();
            Microsoft.Office.Interop.Excel.Workbook xlsWorkBook;
            xlsWorkBook = xlsApp.Workbooks.Open("C:\\WORK\\MyData\\初稿.xls");

            for (int i = 2; i < dt.Rows.Count + 2; i++)
            {
                DataRow dr = dt.Rows[i - 2];
                DateTime firstVirsionDate;
                xlsWorkBook.ActiveSheet.Cells(i, 1).Value = UserName;
                xlsWorkBook.ActiveSheet.Cells(i, 2).Value = dr["我方案号"].ToString();
                xlsWorkBook.ActiveSheet.Cells(i, 2).Font.Name = "Times New Roman";
                xlsWorkBook.ActiveSheet.Cells(i, 3).Value = dr["案件类型"].ToString();
                firstVirsionDate = (DateTime)dr["初稿时间"];
                xlsWorkBook.ActiveSheet.Cells(i, 4).Value = firstVirsionDate.Month.ToString() + "月";
                xlsWorkBook.ActiveSheet.Cells(i, 4).Font.Name = "Times New Roman";
                xlsWorkBook.ActiveSheet.Cells(i, 5).Value = dr["备注"].ToString();
                xlsWorkBook.ActiveSheet.Cells(i, 6).Value = dr["权值"].ToString();
                xlsWorkBook.ActiveSheet.Cells(i, 6).Font.Name = "Times New Roman";
            }

            xlsApp.Visible = true;
        }

        private void btnQueryDone_Click(object sender, RoutedEventArgs e)
        {
            DateTime startDate =(DateTime) dpStartDate.SelectedDate;
            DateTime endDate = (DateTime)dpEndDate.SelectedDate;
            //string[] members = new string[] { UserName };
            string sql = "select 我方文号,客户名称,申请类型,任务名称,任务属性,任务标识,承办人,初稿日,完成日 from Tasks,Cases where Tasks.案件ID=Cases.案件ID and 任务名称 not in ('请求实审') and (承办人='" + UserName + "') and (完成日 ";

            WeightData wtdt = new WeightData();
            wtdt = CalculateWeight2(startDate, endDate, sql);
            Firstdg.ItemsSource = wtdt.ResultTable.DefaultView;
            tbTotalWeight.Text = wtdt.Totalweight.ToString();
        }

        private WeightData CalculateWeight2(DateTime startDate, DateTime endDate, string sql)
        {
            WeightData wtdt = new WeightData();
            wtdt.Totalweight = 0;


            sql = sql + "between #" + startDate + "# and #" + endDate + "#)";
            AccessCUID acuid = new AccessCUID();
            System.Data.DataTable dt = acuid.Query(sql, CurrentDir + "MyData\\dataset.accdb");
            dt.Columns.Add("权值", typeof(double));
            foreach (DataRow dr in dt.Rows)
            {
                string taskType = dr["申请类型"].ToString() + dr["任务名称"].ToString() + dr["任务属性"].ToString() + dr["任务标识"].ToString();
                double taskWeight = 0;
                switch (taskType)
                {
                    case "PCT国际申请新申请":
                        taskWeight = 1.2;
                        break;
                    case "PCT国际申请撰写":
                        taskWeight = 1.2;
                        break;
                    case "PCT国际申请改写":
                        taskWeight = 0.5;
                        break;
                    case "发明新申请":
                    case "发明新申请撰写":
                        taskWeight = 1;
                        break;
                    case "实用新型新申请":
                    case "实用新型新申请撰写":
                        taskWeight = 0.7;
                        break;
                    case "发明OA答复一通实质(S)":
                        taskWeight = 0.4;
                        break;
                    case "发明OA答复二通实质(S)":
                    case "实用新型OA答复一通实质(S)":
                        taskWeight = 0.2;
                        break;
                    case "发明OA答复三通实质(S)":
                    case "发明OA答复四通实质(S)":
                        taskWeight = 0.1;
                        break;
                    case "发明OA答复一通形式-非本人失误(XN)":
                    case "发明OA答复二通形式-非本人失误(XN)":
                    case "发明OA答复三通形式-非本人失误(XN)":
                    case "实用新型OA答复一通形式-非本人失误(XN)":
                    case "实用新型OA答复二通形式-非本人失误(XN)":
                        taskWeight = 0.1;
                        break;
                    default:
                        taskWeight = 0;
                        break;
                }
                dr["权值"] = taskWeight;


                wtdt.Totalweight = wtdt.Totalweight + taskWeight;

            }
            wtdt.ResultTable = dt;
            return wtdt;
        }

        private void btnGroupFirstVirsion_Click(object sender, RoutedEventArgs e)
        {
            DateTime startDate = (DateTime)dpStartDate.SelectedDate;
            DateTime endDate = (DateTime)dpEndDate.SelectedDate;
            //string[] members = new string[] { UserName };
            string members = "'舒丁', '熊文杰', '何茹玥', '刘赏源', '张杨', '王红红', '魏亮', '陈隆', '姚许', '虞凌霄', '郭小满', '罗美红', '吴黎丽' ";

            string sql = "select 我方文号,客户名称,申请类型,任务名称,任务属性,任务标识,承办人,初稿日,完成日 from Tasks,Cases where Tasks.案件ID=Cases.案件ID and 任务名称 not in ('请求实审') and (承办人 in (" + members + ")) and (初稿日 ";

            WeightData wtdt = new WeightData();
            wtdt = CalculateWeight2(startDate, endDate, sql);
            Firstdg.ItemsSource = wtdt.ResultTable.DefaultView;
            tbTotalWeight.Text = wtdt.Totalweight.ToString();
        }

        private void btnGroupDone_Click(object sender, RoutedEventArgs e)
        {
            DateTime startDate = (DateTime)dpStartDate.SelectedDate;
            DateTime endDate = (DateTime)dpEndDate.SelectedDate;
            //string[] members = new string[] { UserName };
            string members = "'舒丁', '熊文杰', '何茹玥', '刘赏源', '张杨', '王红红', '魏亮', '陈隆', '姚许', '虞凌霄', '郭小满', '罗美红', '吴黎丽' ";

            string sql = "select 我方文号,客户名称,申请类型,任务名称,任务属性,任务标识,承办人,初稿日,完成日 from Tasks,Cases where Tasks.案件ID=Cases.案件ID and 任务名称 not in ('请求实审') and (承办人 in (" + members + ")) and (完成日 ";

            WeightData wtdt = new WeightData();
            wtdt = CalculateWeight2(startDate, endDate, sql);
            Firstdg.ItemsSource = wtdt.ResultTable.DefaultView;
            tbTotalWeight.Text = wtdt.Totalweight.ToString();
        }
    }
}

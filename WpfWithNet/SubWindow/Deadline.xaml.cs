using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfWithNet.StoredProcedure;
using WpfWithNet.Models;

namespace WpfWithNet.SubWindow
{
    /// <summary>
    /// Deadline.xaml 的交互逻辑
    /// </summary>
    public partial class Deadline : Window
    {
        public string CurrentDir;
        public Deadline()
        {
            InitializeComponent();
        }

       
        private void namelist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem cbitem = new ComboBoxItem();
            cbitem =(ComboBoxItem) namelist.SelectedItem;
            //WindowWeightDetail wd = new WindowWeightDetail();
            int countsOfOutofLimitByHand;
            int countsOfNewApp_Total;


            WeightData wtdt = new WeightData();
            NormalClass nc = new NormalClass();
            nc.Path = CurrentDir;
            string name;
            name = cbitem.Content.ToString();
            name = name.Substring(2);
            //计算经手超期量
            nc.OutofLimitByHand(name);
            countsOfOutofLimitByHand = nc.Count;
            tbOutofLimitByHand.Text = nc.Count.ToString();
            double daysOfOutofLimit=0;
            foreach (DataRow dr in nc.DtResult.Rows)
            {

                if (!(dr["初稿日"] is DBNull) && (DateTime)dr["初稿日"] > (DateTime)dr["初稿期限(内)"] && (DateTime)dr["初稿期限(内)"] - (DateTime)dr["配案日"] > TimeSpan.FromDays(20))//初稿日没有
                {
                    daysOfOutofLimit += (double)dr["超期天数1"];              
                }

                if (!(dr["初稿日"] is DBNull) && (DateTime)dr["初稿日"] > (DateTime)dr["初稿期限(内)"] && (DateTime)dr["初稿期限(内)"] - (DateTime)dr["配案日"] <= TimeSpan.FromDays(20) && (DateTime)dr["初稿日"] - (DateTime)dr["配案日"] > TimeSpan.FromDays(20))//初稿日没有
                {
                    daysOfOutofLimit += (double)dr["超期天数4"];
                }

                if (dr["初稿日"] is DBNull && dr["完成日"] is DBNull && DateTime.Now.Date - (DateTime)dr["配案日"] > TimeSpan.FromDays(20) && DateTime.Now.Date > (DateTime)dr["初稿期限(内)"])//初稿日没有
                {
                    daysOfOutofLimit += (double)dr["超期天数3"];
                }

                if (dr["初稿日"] is DBNull && !(dr["完成日"] is DBNull) && (DateTime)dr["完成日"] > (DateTime)dr["定稿期限(内)"])//初稿日没有
                {
                    daysOfOutofLimit += (double)dr["超期天数2"];
                }
                
            }
            tbDaysOfOutofLimit.Text = daysOfOutofLimit.ToString();

                //计算总量
                nc.NewApp_Total(name);
            countsOfNewApp_Total = nc.Count;
            tbNewApp_Total.Text = nc.Count.ToString();

            //计算超期率
            double portionOfOutofLimit;
            portionOfOutofLimit = (double)countsOfOutofLimitByHand * 100 / countsOfNewApp_Total;
            portionOfOutofLimit = Math.Round(portionOfOutofLimit,1);
            string txt_portionOfOutofLimit;
            if (portionOfOutofLimit!=0)
            {
                txt_portionOfOutofLimit = portionOfOutofLimit.ToString() + "%";
                //txt_portionOfOutofLimit = txt_portionOfOutofLimit.Substring(0, 4) + "%";
            }
            else
            {
                txt_portionOfOutofLimit ="0%";
            }
            if (portionOfOutofLimit < 0.1)
            {
                tbPortionOfOutofLimit.Foreground = new SolidColorBrush(Colors.Green); 
            }              
            
            tbPortionOfOutofLimit.Text = txt_portionOfOutofLimit;

            double scoreofdone = 0;



            //计算初稿权值
            nc.NewAppTotalWeight(name);
            wtdt = CalculateWeight(nc.DtResult);
            tbNewAppTotalWeight.Text = wtdt.Totalweight.ToString();
            scoreofdone = wtdt.Totalweight * 10;

            //计算OA数量
            nc.OACount(name);
            tbOACount.Text = nc.Count.ToString();
            scoreofdone = scoreofdone + nc.Count * 3;

            tbScoreOfDone.Text = scoreofdone.ToString();

        }

        private void OutofLimitByHand_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            WindowWeightDetail wd = new WindowWeightDetail();
            NormalClass nc = new NormalClass();
            nc.Path = CurrentDir;
            nc.OutofLimit();
            WeightData wtdt = new WeightData();
            
            wd.Firstdg.ItemsSource = wtdt.ResultTable.DefaultView;
            wd.tbTotalWeight.Text = wtdt.Count.ToString();
            wd.Title = "部门超期案件列表";
            wd.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void tbNewApp_Total_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private WeightData CalculateWeight(System.Data.DataTable dt)
        {
            WeightData wtdt = new WeightData();
            wtdt.Totalweight = 0;

            dt.Columns.Add("权值", typeof(double));
            foreach (DataRow dr in dt.Rows)
            {
                string taskType = dr["申请类型"].ToString() + dr["任务名称"].ToString() + dr["任务属性"].ToString() + dr["任务标识"].ToString();
                double taskWeight = 0;
                switch (taskType)
                {
                    case "PCT国际申请撰写":
                        taskWeight = 1.2;
                        break;
                    case "PCT国际申请新申请":
                        taskWeight = 1.2;
                        break;
                    case "PCT国际申请改写":
                        taskWeight = 0.5;
                        break;
                    case "发明新申请撰写":
                    case "发明新申请":
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
            wtdt.Count = dt.Rows.Count;
            return wtdt;

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.IO;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfWithNet.dbCUID;

namespace WpfWithNet.SubWindow
{
    /// <summary>
    /// Interaction logic for EverydayAffair.xaml
    /// </summary>
    public partial class EverydayAffair : Window
    {
        public EverydayAffair()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AccessCUID acuid = new AccessCUID();
            lvwTransaction.ItemsSource = acuid.Query("select * from 事务表  order by 发起时间 desc", "C:\\WORK\\MyData\\dataset.accdb").DefaultView;
        }

        private void btnOpenFolder_Click(object sender, RoutedEventArgs e)
        {
            DataRowView obj = (DataRowView) lvwTransaction.SelectedItem;
            DataRow dr = obj.Row;
            DateTime sdate = (DateTime)dr["发起时间"];
            string tag = dr["标签"].ToString();
            string datePlusTag = tag + sdate.Year.ToString() + sdate.Month.ToString() + sdate.Day.ToString();
            if (Directory.Exists("C:\\WORK\\Transaction\\" + datePlusTag))
            {
                Process.Start("explorer.exe ", "C:\\WORK\\Transaction\\" + datePlusTag);
            }
            else
            {
                MessageBox.Show("文件夹不存在！");
            }
        }

        private void btnAddAffair_Click(object sender, RoutedEventArgs e)
        {
            AccessCUID acuid = new AccessCUID();
            string sql = "insert into 事务表(发起人,事项描述,发起时间,标签) values ('"+tbStarter.Text+"','"+ tbDescription.Text + "','"+DateTime.Now+"','"+tbTag.Text+"')";
            acuid.Update(sql, "C:\\WORK\\MyData\\dataset.accdb");

            DateTime now = DateTime.Now;
            string tagPlusDate=tbTag.Text+now.Year.ToString()+ now.Month.ToString() + now.Day.ToString();
            CreateFolder(tagPlusDate);
        }

        private void CreateFolder(string tagPlusDate)
        {
            Directory.CreateDirectory("C:\\WORK\\Transaction\\" + tagPlusDate);
            if (MessageBox.Show("文件夹已创建，是否打开并存入文件？","打开确认",MessageBoxButton.YesNo) ==MessageBoxResult.Yes)
            {
                Process.Start("explorer.exe ", "C:\\WORK\\Transaction\\" + tagPlusDate);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lvwTransaction.SelectedIndex<0)
            {
                MessageBox.Show("未选中任何项");
            }
            else
            {
                DataRowView obj = (DataRowView)lvwTransaction.SelectedItem;
                DataRow dr = obj.Row;
                //DateTime sdate = (DateTime)dr["发起时间"];
                string starter=dr["发起人"].ToString();
                string tag = dr["标签"].ToString();
                AccessCUID acuid = new AccessCUID();
                string sql = "delete from 事务表 where (发起人= '" + starter + "'and 标签 ='" + tag + "')";
                if (MessageBox.Show("是否删除选择的事务","确认删除",MessageBoxButton.YesNo)==MessageBoxResult.Yes)
                {
                    acuid.Update(sql, "C:\\WORK\\MyData\\dataset.accdb");
                    lvwTransaction.ItemsSource = acuid.Query("select * from 事务表 order by 发起时间 desc", "C:\\WORK\\MyData\\dataset.accdb").DefaultView;
                }
                
            }

        }
    }
}

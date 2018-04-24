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
using System.Data;

namespace WpfWithNet.SubWindow
{
    /// <summary>
    /// Interaction logic for Link.xaml
    /// </summary>
    public partial class Link : Window
    {
        private string clientFullName;
        public string ClientFullName
        {
            get { return clientFullName; }
            set { clientFullName = value; }
        }

        public Link()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string sql = "select * from 客户公司 order by 简称 asc";
            AccessCUID acuid = new AccessCUID();            
            cmbClientList.ItemsSource = acuid.Query(sql, "C:\\WORK\\MyData\\数据集.accdb").AsDataView();
            cmbClientList.DisplayMemberPath = "简称";
            lbClientName.Content = clientFullName;
        }

        private void btnLink_Click(object sender, RoutedEventArgs e)
        {
            string sql = "insert into 名称对应表 values ('"+clientFullName+"','"+cmbClientList.Text+"')";
            AccessCUID acuid = new AccessCUID();
            acuid.Update(sql, "C:\\WORK\\MyData\\数据集.accdb");
            MessageBox.Show("已关联");

        }
    }
}

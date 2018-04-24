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

namespace WpfWithNet.SubWindow
{
    /// <summary>
    /// Interaction logic for MaintainData.xaml
    /// </summary>
    public partial class MaintainData : Window
    {
        public MaintainData()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AccessCUID acuid = new AccessCUID();
            lvwContact.ItemsSource = acuid.Query("select * from 客户联系人", "C:\\WORK\\MyData\\数据集.accdb").DefaultView;
            lvwCompany.ItemsSource= acuid.Query("select * from 客户公司", "C:\\WORK\\MyData\\数据集.accdb").DefaultView;
        }
        //添加联系人
        private void btnAddContact_Click(object sender, RoutedEventArgs e)
        {
            MaintainPerson mtp = new MaintainPerson();
            mtp.Show();
        }
        //修改联系人
        private void btnModifyContact_Click(object sender, RoutedEventArgs e)
        {
            MaintainPerson mtp = new MaintainPerson();
            mtp.btnConfirm.Visibility = Visibility.Collapsed;
            mtp.btnConfirmModify.Visibility = Visibility.Visible;
            mtp.Show();
        }

        private void tbSearchContact_TextChanged(object sender, TextChangedEventArgs e)
        {
            AccessCUID acuid = new AccessCUID();
            lvwContact.ItemsSource = acuid.Query("select * from 客户联系人 where 姓名 like '%" + tbSearchContact.Text + "%'", "C:\\WORK\\MyData\\数据集.accdb").DefaultView;
        }
    }
}

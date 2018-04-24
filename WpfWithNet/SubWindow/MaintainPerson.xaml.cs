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
    /// Interaction logic for MaintainPerson.xaml
    /// </summary>
    public partial class MaintainPerson : Window
    {
        public MaintainPerson()
        {
            InitializeComponent();
        }
        //确认添加
        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tbName.Text!="")
                {
                    string sql = "insert into 客户联系人(姓名,座机,邮箱,公司,手机,部门) values ('"+tbName.Text+"','"+tbTel.Text+"','"+tbEmail.Text+"','"+tbCompany.Text+"','"+tbPhone.Text+"','"+tbDept.Text+"')";
                    AccessCUID acuid = new AccessCUID();
                    acuid.Update(sql, "C:\\WORK\\MyData\\数据集.accdb");
                    MessageBox.Show("已添加");
                }
                else
                {
                    MessageBox.Show("姓名不能为空！");
                }
            }
            catch
            {

            }

        }
    }
}

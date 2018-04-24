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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            checkpassword(); 
           
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void tbPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                checkpassword();
            }
            
        }

        private void checkpassword()
        {
            string sql = "select * from 用户 where 用户名='" + tbUser.Text + "' and 密码='" + tbPassword.Password + "'";
            string currentDir = "C:\\WORK\\";
            if (tbUser.Text!="舒丁")
            {
                currentDir = "D:\\";
            }
            AccessCUID acuid = new AccessCUID();
            try
            {
                if (acuid.Query(sql, currentDir + "MyData\\dataset.accdb").Rows.Count > 0)
                {
                    MainWindow workspace = new MainWindow();
                    workspace.UserName = tbUser.Text;
                    workspace.CurrentDir = currentDir;
                    workspace.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("用户名或密码错误！");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }
    }
}

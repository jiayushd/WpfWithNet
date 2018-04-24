using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using System.Data;
using System.Data.OleDb;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfWithNet.Models;
using WpfWithNet.dbCUID;
using WpfWithNet.StoredProcedure;

namespace WpfWithNet.SubWindow
{
    /// <summary>
    /// GeneralViewofMembers.xaml 的交互逻辑
    /// </summary>
    public partial class GeneralViewofMembers : Window
    {
        public List<GeneralView> gvMembers = new List<GeneralView>();
        public string CurrentDir;
        public GeneralViewofMembers()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
             dgGeneralViewofMembers.ItemsSource = gvMembers;
        }

        //国内
        private void ButtonDomesdic_Click(object sender, RoutedEventArgs e)
        {
            string UserName = ((GeneralView)((Button)sender).DataContext).name;
            tbIndicator.Text = UserName + ": 国内未完成新申请";
            NormalClass nc = new NormalClass();
            nc.Path = CurrentDir;
            nc.CN(UserName);
            dgDetailViewofMember.ItemsSource = nc.DtResult.DefaultView;
        }

        //涉外
        private void ButtonGlobal_Click(object sender, RoutedEventArgs e)
        {
            string UserName = ((GeneralView)((Button)sender).DataContext).name;
            tbIndicator.Text = UserName + ": 涉外未完成的新申请";
            NormalClass nc = new NormalClass();
            nc.Path = CurrentDir;
            nc.Foreign(UserName);
            dgDetailViewofMember.ItemsSource = nc.DtResult.DefaultView;
        }
        //可处理
        private void ButtonTodo_Click(object sender, RoutedEventArgs e)
        {
            string UserName = ((GeneralView)((Button)sender).DataContext).name;
            tbIndicator.Text = UserName + ": 可处理的新申请（包括涉外）";
            NormalClass nc = new NormalClass();
            nc.Path = CurrentDir;
            nc.Todo(UserName);
            dgDetailViewofMember.ItemsSource = nc.DtResult.DefaultView;
        }
        //初稿
        private void ButtonFirstVirsion_Click(object sender, RoutedEventArgs e)
        {
            string UserName = ((GeneralView)((Button)sender).DataContext).name;
            tbIndicator.Text = UserName + ": 已出初稿案件";
            NormalClass nc = new NormalClass();
            nc.Path = CurrentDir;
            nc.FirstVirsion(UserName);
            dgDetailViewofMember.ItemsSource = nc.DtResult.DefaultView;
        }
        //OA总数
        private void ButtonOAtotal_Click(object sender, RoutedEventArgs e)
        {
            string UserName = ((GeneralView)((Button)sender).DataContext).name;
            tbIndicator.Text = UserName + ": 全部OA";
            NormalClass nc = new NormalClass();
            nc.Path = CurrentDir;
            nc.OA_Total(UserName);
            dgDetailViewofMember.ItemsSource = nc.DtResult.DefaultView;
        }
        //30天内OA
        private void ButtonOAin30_Click(object sender, RoutedEventArgs e)
        {
            string UserName=((GeneralView) ((Button)sender).DataContext).name;
            tbIndicator.Text = UserName + ": 30天以内到期的OA";
            NormalClass nc = new NormalClass();
            nc.Path = CurrentDir;
            nc.OAin30(UserName);
            dgDetailViewofMember.ItemsSource = nc.DtResult.DefaultView;
        }
                
      
        //调整界面
        private void dgGeneralViewofMembers_MouseLeave(object sender, MouseEventArgs e)
        {
            MainGrid.ColumnDefinitions[0].Width = new GridLength(110, GridUnitType.Pixel); 
        }
        private void dgGeneralViewofMembers_MouseEnter(object sender, MouseEventArgs e)
        {
            MainGrid.ColumnDefinitions[0].Width = new GridLength(530, GridUnitType.Pixel);
        }
    }
}

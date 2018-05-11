using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Reflection;
using Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Core;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.OleDb;
using WpfWithNet.dbCUID;
using WpfWithNet.Models;
using Microsoft.VisualBasic;
using System.Diagnostics;
using WpfWithNet.SubWindow;
using WpfWithNet.StoredProcedure;


namespace WpfWithNet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public string UserName;
        public string CurrentDir;
        LinkList<string> operationRecord = new LinkList<string>();
        LinkList<string> operationName = new LinkList<string>();
        //ChromiumWebBrowser webBrowser = null;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gridSecondList.RowDefinitions[0].Height = new GridLength(80);
            dtpkStateDate.SelectedDate = DateTime.Now;
            
            gridTaskInfo.IsEnabled = false;
           
            showWeight();

        }

        private void showWeight()
        {
            WeightData wtdt = new WeightData();
            NormalClass nc = new NormalClass();
            nc.Path = CurrentDir;

            //计算个人初稿
            nc.FirstVirsion_ThisMonth(UserName);
            wtdt = CalculateWeight(nc.DtResult);
            tbkFirstVirsionTotalWeight.Text = wtdt.Totalweight.ToString();

            //计算个人递交
            nc.Done_ThisMonth(UserName);
            wtdt = CalculateWeight(nc.DtResult);
            tbkDoneWeight.Text = wtdt.Totalweight.ToString();

            //计算部门初稿
            nc.FirstVirsion_Group_ThisMonth();
            wtdt = CalculateWeight(nc.DtResult);
            tbkFirstVirsionGroupTotalWeight.Text = wtdt.Totalweight.ToString();

            //计算部门递交
            nc.Done_Group_ThisMonth();
            wtdt = CalculateWeight(nc.DtResult);
            tbkGroupDoneWeight.Text = wtdt.Totalweight.ToString();

            //计算积案
            nc.OverStock();
            wtdt = CalculateWeight(nc.DtResult);
            tbkOverStock.Text = wtdt.Count.ToString();

            //计算超期案件
            nc.OutofLimit();
            wtdt = CalculateWeight(nc.DtResult);
            tbkOutofLimit.Text = wtdt.Count.ToString();

        }

       

        private void Window_Closed(object sender, EventArgs e)
        {
            if (File.Exists(CurrentDir+"MyData\\邮件模板.txt"))
            {

                File.Delete(CurrentDir+"MyData\\邮件模板.txt");
            }
        }

        //----------------------主功能区：开始------------------------------------------------------------------------------------


        //数据查询
        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            FirstVirsion fv = new FirstVirsion();
            fv.UserName = UserName;
            fv.CurrentDir = CurrentDir;
            fv.Show();
        }

        //修改案件基本信息
        private void btnChaneCaseInfo_Click(object sender, RoutedEventArgs e)
        {
            CaseInfo cs = new CaseInfo();
            Abstract ab = (Abstract)lvw3.SelectedItem;
            cs.CaseID = ab.CaseID;
            cs.Show();

        }

        //日常事务
        private void btnEverydayAffair_Click(object sender, RoutedEventArgs e)
        {
            EverydayAffair ea = new EverydayAffair();
            ea.Show();
        }
        //部门案件状态
        private void btnGeneralViewofMembers_Click(object sender, RoutedEventArgs e)
        {
            string[] members = new string[] { "舒丁", "熊文杰", "何茹玥", "刘赏源", "张杨", "魏亮", "陈隆", "姚许", "虞凌霄", "郭小满", "罗美红", "王红红(离职)", "吴黎丽(离职)", "黄文勇", "陈善镇", "陈金普", "韩瑞", "郭健", "章雷" };
            NormalClass nc = new NormalClass();
            nc.Path = CurrentDir;
            nc.Namelist();
            //string[] members = new string[] { "方高明", "蔡丽妮", "万成", "易倩", "武志峰", "刘广", "景晓玲","唐德君"};
            GeneralView gv = new GeneralView();
            GeneralViewofMembers gvm = new GeneralViewofMembers();
            foreach (DataRow dr in nc.DtResult.Rows)
            {
                gv = GetGeneralView(dr);
                gvm.gvMembers.Add(gv);
            }
            gvm.CurrentDir = CurrentDir;
            gvm.Show();
        }

        public GeneralView GetGeneralView(DataRow dr)
        {
            GeneralView gv = new GeneralView();
            gv.name = dr["姓名"].ToString();
            gv.company = dr["分公司"].ToString();

            NormalClass nc = new NormalClass();
            int[] nums = nc.numlist(gv.name);

            gv.numDomesdic = nums[0];
            gv.numGlobal = nums[1];
            gv.numTodo = nums[2];
            gv.numFirstVirsion = nums[3];
            gv.numOAtotal = nums[4];
            gv.numOAin30 = nums[5];
            return gv;

        }
        //邮件模板
        private void btnEmail_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(CurrentDir+"MyData\\邮件模板.txt"))
            {
                File.Delete(CurrentDir+"MyData\\邮件模板.txt");

            }
            FileStream fs1 = new FileStream(CurrentDir+"MyData\\邮件模板.txt", FileMode.Create, FileAccess.Write);//创建写入文件 
            StreamWriter sw = new StreamWriter(fs1);

            sw.WriteLine("您好！");
            sw.WriteLine("关于题述专利申请，我方已经完成专利申请文件的撰写，请贵方对所附专利申请文件（技术内容是否表述无误，重要内容是否有疏漏或者不详尽之处）及下列案件信息进行确认或补充：");
            sw.WriteLine("我方案号：" + tbAttorneyNum.Text);
            sw.WriteLine("贵方案号：" + tbClientNum.Text);
            sw.WriteLine("交底名称：" + tbDocName.Text);
            sw.WriteLine("申请名称："+ tbApplicant.Text);
            sw.WriteLine("申请人：\n");
            sw.WriteLine("发明人：\n");
            sw.WriteLine("第一发明人身份证号：\n");
            sw.WriteLine("是否提前公开：(实用新型不要)");
            sw.WriteLine("是否同时提实审：(实用新型不要)");
            sw.WriteLine("是否同时提保密审查请求：\n");
            sw.WriteLine("");
            sw.WriteLine("为获得较早的申请日，请尽快回复。如有意见，请指出；如无意见也请回复确认。\n");
            sw.WriteLine("如有任何问题，请随时电话或邮件联系。\n");

            sw.Close();
            fs1.Close();


            Process.Start("explorer.exe ", CurrentDir+"MyData\\邮件模板.txt");


        }

        //----------------------主功能区：结束------------------------------------------------------------------------------------



        //选中一个案子
        public void lvw3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (lvw3.SelectedIndex != -1)
            {
                //设置界面变化
                gridTaskInfo.IsEnabled = true;               
                
                //获取案件信息                               
                Abstract ab = (Abstract)lvw3.SelectedItem;
                NormalClass nc = new NormalClass();
                nc.Path = CurrentDir;
                nc.Tasks(ab.TaskID);                              
                
                TaskDetail td = DTtoTaskDetail1(nc.DtResult);
                gridTaskInfo.DataContext = td;
                
                //获取文件信息
                if (!Directory.Exists(CurrentDir+"NewApplication\\" + tbAttorneyNum.Text))
                {
                    MessageBoxResult result = MessageBox.Show("文件夹不存在，是否创建？", "打开本地", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        Directory.CreateDirectory(CurrentDir+"NewApplication\\" + tbAttorneyNum.Text);
                        Directory.CreateDirectory(CurrentDir+"NewApplication\\" + tbAttorneyNum.Text + "\\技术交底");
                        Directory.CreateDirectory(CurrentDir+"NewApplication\\" + tbAttorneyNum.Text + "\\中间文件");
                        Directory.CreateDirectory(CurrentDir+"NewApplication\\" + tbAttorneyNum.Text + "\\定稿");
                        Directory.CreateDirectory(CurrentDir+"NewApplication\\" + tbAttorneyNum.Text + "\\对比文件");
                        wbLocalFiles.Source = new Uri(CurrentDir+"NewApplication\\" + tbAttorneyNum.Text);
                    }

                }
                else
                {
                    wbLocalFiles.Source = new Uri(CurrentDir+"NewApplication\\"+ tbAttorneyNum.Text);
                }
                               
            }

        }

        //双击打开案件详情
        private void lvw3_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CaseInfo cs = new CaseInfo();
            Abstract ab = (Abstract)lvw3.SelectedItem;
            cs.CurrentDir = CurrentDir;
            cs.CaseID = ab.CaseID;
            cs.Show();
            
        }

        //-------------------------------文件操作区：开始-------------------------------------------------------
        //打开本地文件夹
        private void openLocal_Click(object sender, RoutedEventArgs e)
        {
            if (tbAttorneyNum.Text == "")
            {
                MessageBox.Show("请先选择一个案子");
            }
            else
            {
                if (!Directory.Exists(CurrentDir+"NewApplication\\" + tbAttorneyNum.Text))
                {
                    MessageBoxResult result = MessageBox.Show("文件夹不存在，是否创建？", "打开本地", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        Directory.CreateDirectory(CurrentDir+"NewApplication\\" + tbAttorneyNum.Text);
                        Directory.CreateDirectory(CurrentDir+"NewApplication\\" + tbAttorneyNum.Text + "\\技术交底");
                        Directory.CreateDirectory(CurrentDir+"NewApplication\\" + tbAttorneyNum.Text + "\\中间文件");
                        Directory.CreateDirectory(CurrentDir+"NewApplication\\" + tbAttorneyNum.Text + "\\定稿");
                        Directory.CreateDirectory(CurrentDir+"NewApplication\\" + tbAttorneyNum.Text + "\\对比文件");
                        Process.Start("explorer.exe ", CurrentDir+"NewApplication\\" + tbAttorneyNum.Text);
                    }

                }
                else
                {
                    Process.Start("explorer.exe ", CurrentDir+"NewApplication\\" + tbAttorneyNum.Text);
                }
            }
        }
        List<FileInfo> fileInfos = new List<FileInfo>();
        //文件分组：所有文件
        private void btnCheckAllFiles_Click(object sender, RoutedEventArgs e)
        {
            //lvwFiles.ItemsSource = null;
            if (tbAttorneyNum.Text == "")
            {
                MessageBox.Show("请先选择一个案子");
            }
            else
            {
                string targetpath = CurrentDir+"NewApplication\\" + tbAttorneyNum.Text;
                string[] targetpaths = { targetpath };
                fileInfos.Clear();
                GetAllFiles(targetpaths);
                //lvwFiles.ItemsSource = fileInfos;

            }
        }

        //文件分组：技术交底
        private void btnSortbyTechDoc_Click(object sender, RoutedEventArgs e)
        {
            //lvwFiles.ItemsSource = null;
            if (tbAttorneyNum.Text == "")
            {
                MessageBox.Show("请先选择一个案子");
            }
            else
            {
                string targetpath = CurrentDir+"NewApplication\\" + tbAttorneyNum.Text + "\\技术交底";
                string[] targetpaths = { targetpath };
                fileInfos.Clear();
                GetAllFiles(targetpaths);
                //lvwFiles.ItemsSource = fileInfos;

            }
        }
        //文件分组：拖放保存技术交底
        private void btnSortbyTechDoc_Drop(object sender, DragEventArgs e)
        {
            String Path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            FileInfo fi = new FileInfo(Path);
            string fileName = fi.Name;

            string destPath = CurrentDir+"NewApplication\\" + tbAttorneyNum.Text + "\\技术交底\\" + fileName;
            File.Copy(fi.FullName, destPath);
            //File.Delete(fi.FullName);
        }
        //文件分组：对比文件
        private void btnSortbyCompDoc_Click(object sender, RoutedEventArgs e)
        {
            //lvwFiles.ItemsSource = null;
            if (tbAttorneyNum.Text == "")
            {
                MessageBox.Show("请先选择一个案子");
            }
            else
            {
                string targetpath = CurrentDir+"NewApplication\\" + tbAttorneyNum.Text + "\\对比文件";
                string[] targetpaths = { targetpath };
                fileInfos.Clear();
                GetAllFiles(targetpaths);
                //lvwFiles.ItemsSource = fileInfos;

            }
        }
        //文件分组：拖放保存对比文件
        private void btnSortbyCompDoc_Drop(object sender, DragEventArgs e)
        {
            String Path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            FileInfo fi = new FileInfo(Path);
            string fileName = fi.Name;

            string destPath = CurrentDir+"NewApplication\\" + tbAttorneyNum.Text + "\\对比文件\\" + fileName;
            File.Copy(fi.FullName, destPath);
        }
        //双击打开文件
        private void lvwFiles_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //FileInfo dir = (FileInfo)lvwFiles.SelectedItem;
            //Process.Start("explorer.exe ", dir.FullName);
        }

        //-------------------------------文件操作区：结束-------------------------------------------------------
        private void GetAllFiles(string[] targetpaths)
        {
            foreach (string dir in targetpaths)
            {
                if (Directory.Exists(dir))
                {
                    string[] files;
                    files = Directory.GetFiles(dir);
                    for (int i = 0; i < files.Length; i++)
                    {
                        fileInfos.Add(new FileInfo(files[i]));
                    }

                    GetAllFiles(Directory.GetDirectories(dir));
                }
                else
                {
                    MessageBox.Show("没有设置该类型的文件夹");
                }
            }
        }

       

        //-------------------------------个案操作功能区：开始-------------------------------------------------------

        
        //新建发明
        private void btnCreateApp_Click(object sender, RoutedEventArgs e)
        {
            if (tbAttorneyNum.Text == "")
            {
                MessageBox.Show("请先选择一个案子");
            }
            else
            {
                if (MessageBox.Show("是否创建新申请文件？", "确认", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        Microsoft.Office.Interop.Word.Application wd = new Microsoft.Office.Interop.Word.Application();
                        wd.Documents.Open(CurrentDir+"MyData\\大陆发明有图申请文件模板.doc");
                        wd.Visible = true;
                        if (tbClientNum.Text != "")
                        {
                            wd.ActiveDocument.SaveAs(CurrentDir+"NewApplication\\" + tbAttorneyNum.Text + "\\" + tbClientNum.Text + "_" + tbAttorneyNum.Text + "_V1.doc");
                        }
                        else
                        {
                            wd.ActiveDocument.SaveAs(CurrentDir+"NewApplication\\" + tbAttorneyNum.Text + "\\" + tbAttorneyNum.Text + "_V1.doc");

                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        //throw;
                    }
                    

                }
                else
                {
                    MessageBox.Show("已放弃创建新申请");
                }
            }

        }
        //新建新型
        private void btnCreateNewModel_Click(object sender, RoutedEventArgs e)
        {
            if (tbAttorneyNum.Text == "")
            {
                MessageBox.Show("请先选择一个案子");
            }
            else
            {
                if (MessageBox.Show("是否创建新申请文件？", "确认", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Microsoft.Office.Interop.Word.Application wd = new Microsoft.Office.Interop.Word.Application();
                    wd.Documents.Open(CurrentDir+"MyData\\大陆实用新型有图申请文件模板.doc");
                    wd.Visible = true;
                    if (tbClientNum.Text != "")
                    {
                        wd.ActiveDocument.SaveAs(CurrentDir+"NewApplication\\" + tbAttorneyNum.Text + "\\" + tbClientNum.Text + "_" + tbAttorneyNum.Text + "_V1.doc");
                    }
                    else
                    {
                        wd.ActiveDocument.SaveAs(CurrentDir+"NewApplication\\" + tbAttorneyNum.Text + "\\" + tbAttorneyNum.Text + "_V1.doc");

                    }

                }
                else
                {
                    MessageBox.Show("已放弃创建新申请");
                }
            }

        }
        //新建OA
        private void btnCreateOA_Click(object sender, RoutedEventArgs e)
        {
            if (tbAttorneyNum.Text == "")
            {//没有选择案子
                MessageBox.Show("请先选择一个案子");
            }
            else
            {
                if (tbCaseType.Text.Contains("OA"))
                {//是OA任务
                    if (MessageBox.Show("是否创建OA？", "确认", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {//用户确认要创建OA文件
                        Microsoft.Office.Interop.Word.Application wd = new Microsoft.Office.Interop.Word.Application();
                        wd.Documents.Open(CurrentDir+"MyData\\意见陈述书.doc");
                        wd.Visible = true;
                        string docFullName;
                        if (tbClientNum.Text != "")
                        {//有客户案号
                            docFullName = CurrentDir+"NewApplication\\" + tbAttorneyNum.Text + "\\" + tbClientNum.Text + "_" + tbAttorneyNum.Text + "_" + tbCaseType.Text + "_意见陈述书_V1.doc";
                        }
                        else
                        {//没有客户案号
                            docFullName = CurrentDir+"NewApplication\\" + tbAttorneyNum.Text + "\\" + tbAttorneyNum.Text + "_" + tbCaseType.Text + "_意见陈述书_V1.doc";
                        }
                        wd.ActiveDocument.SaveAs(docFullName);
                    }
                    else
                    {//用户放弃创建OA文件
                        MessageBox.Show("已放弃创建OA文件");
                    }
                }
                else
                {
                    MessageBox.Show("当前任务不是OA！");
                }

            }
        }
        
        
        //-------------------------------个案操作功能区：结束-------------------------------------------------------


        //--------------------------------搜索区----------------------------------------------------------------
        //单框搜索
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            lvw3.ItemsSource = null;
            rbtnCustom.IsChecked = true;


            //获得查询字符串
            string sql = "Select * from Tasks,Cases where Tasks.案件ID=Cases.案件ID ";
            sql = sql + "and (我方文号 like '%" + txtbSearchString.Text + "%'";
            sql = sql + "or 客户文号 like '%" + txtbSearchString.Text + "%'";
            sql = sql + "or 客户名称 like '%" + txtbSearchString.Text + "%'";
            sql = sql + "or 开案名称 like '%" + txtbSearchString.Text + "%'";
            sql = sql + "or 代理人处理状态 like '%" + txtbSearchString.Text + "%'";
            sql = sql + "or 承办人 like '%" + txtbSearchString.Text + "%'";
            sql = sql + "or 申请号 like '%" + txtbSearchString.Text + "%'";
            sql = sql + "or 任务名称 like '%" + txtbSearchString.Text + "%'";
            sql = sql + "or 案件备注 like '%" + txtbSearchString.Text + "%')";
            operationRecord.Append(sql);
            operationName.Append("自定义：" + txtbSearchString.Text);
           tbCustom.Text = "自定义(" + ShowList(sql, CurrentDir+"MyData\\dataset.accdb").ToString() + ")";
            //rbtnCustom.Content = "自定义(" + ShowList(sql, CurrentDir+"MyData\\dataset.accdb").ToString() + ")";
        }
        //高级搜索
        private void AdvanceSearch_Click(object sender, RoutedEventArgs e)
        {
            lvw3.ItemsSource = null;
            rbtnCustom.IsChecked = true;
            //获得查询字符串
            string sql = "Select * from Tasks,Cases where Tasks.案件ID=Cases.案件ID ";
            if (tbSearchCaseFileID.Text != "")
            {
                sql = sql + "and 我方文号 like '%" + tbSearchCaseFileID.Text + "%'";
            }
            if (tbSearchCustomSN.Text != "")
            {
                sql = sql + "and 客户文号 like '%" + tbSearchCustomSN.Text + "%'";
            }

            if (tbSearchApplicantTag.Text != "")
            {
                sql = sql + "and 客户名称 like '%" + tbSearchApplicantTag.Text + "%'";
            }

            if (tbSearchDocName.Text != "")
            {
                sql = sql + "and 开案名称 like '%" + tbSearchDocName.Text + "%'";
            }

            if (tbSearchStatus.Text != "")
            {
                sql = sql + "and 代理人处理状态 like '%" + tbSearchStatus.Text + "%'";
            }

            if (tbSearchAttorneyID.Text != "")
            {
                sql = sql + "and 承办人 like '%" + tbSearchAttorneyID.Text + "%'";
            }

            if (tbApplicationNum.Text != "")
            {
                sql = sql + "and 申请号 like '%" + tbApplicationNum.Text + "%'";
            }

            if (tbSearchTaskType.Text != "")
            {
                sql = sql + "and 任务名称 like '%" + tbSearchTaskType.Text + "%'";
            }

            if (tbSearchTaskInfo.Text != "")
            {
                sql = sql + "and 案件备注 like '%" + tbSearchTaskInfo.Text + "%'";
            }
            operationRecord.Append(sql);
            operationName.Append("高级搜索");
            tbCustom.Text = "自定义(" + ShowList(sql, CurrentDir + "MyData\\dataset.accdb").ToString() + ")";
            //rbtnCustom.Content = "自定义(" + ShowList(sql, CurrentDir+"MyData\\dataset.accdb").ToString() + ")";
        }
        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            gridSecondList.RowDefinitions[0].Height = new GridLength(240);
        }
        private void Expander_Collapsed(object sender, RoutedEventArgs e)
        {
            gridSecondList.RowDefinitions[0].Height = new GridLength(80);
        }

        //--------------------------------搜索区----------------------------------------------------------------


        //-------------------------------左侧按钮功能区：包括9个按钮：开始--------------------------------------------------
        //分类：国内
        private void rbtnCN_Checked(object sender, RoutedEventArgs e)
        {
            lvw3.ItemsSource = null;
            NormalClass nc = new NormalClass();
            nc.Path = CurrentDir;
            nc.CN(UserName);
            tbCN.Text = "国内(" + nc.Count.ToString() + ")";
            lvw3.ItemsSource = DTtoAbstracts1(nc.DtResult);
            lvw3.View = lvw3.FindResource("tileView") as ViewBase;

        }
        //分类：涉外
        private void rbtnForeign_Checked(object sender, RoutedEventArgs e)
        {
            lvw3.ItemsSource = null;
            NormalClass nc = new NormalClass();
            nc.Path = CurrentDir;
            nc.Foreign(UserName);
            tbForeign.Text = "涉外(" + nc.Count.ToString() + ")";
            lvw3.ItemsSource = DTtoAbstracts1(nc.DtResult);
            lvw3.View = lvw3.FindResource("tileView") as ViewBase;

        }
        //分类：可处理
        private void rbtnToDo_Checked(object sender, RoutedEventArgs e)
        {
            lvw3.ItemsSource = null;
            NormalClass nc = new NormalClass();
            nc.Path = CurrentDir;
            nc.Todo(UserName);
            tbToDo.Text = "可处理(" + nc.Count.ToString() + ")";
            lvw3.ItemsSource = DTtoAbstracts1(nc.DtResult);
            lvw3.View = lvw3.FindResource("tileView") as ViewBase;
        }
        //分类：初稿
        private void rbtnDone_Click(object sender, RoutedEventArgs e)
        {
            lvw3.ItemsSource = null;
            NormalClass nc = new NormalClass();
            nc.Path = CurrentDir;
            nc.FirstVirsion(UserName);
            tbDone.Text = "初稿(" + nc.Count.ToString() + ")";
            lvw3.ItemsSource = DTtoAbstracts1(nc.DtResult);
            lvw3.View = lvw3.FindResource("tileView") as ViewBase;
        }
        //分类：本月初稿统计
        private void rbtnCurrentDone_Click(object sender, RoutedEventArgs e)
        {
            lvw3.ItemsSource = null;
            NormalClass nc = new NormalClass();
            nc.Path = CurrentDir;
            nc.FirstVirsion_ThisMonth(UserName);
            tbCurrentDone.Text = "本月初稿(" + nc.Count.ToString() + ")";
            lvw3.ItemsSource = DTtoAbstracts1(nc.DtResult);
            lvw3.View = lvw3.FindResource("tileView") as ViewBase;
        }        
        //分类：OA总数
        private void rbtnOAtotal_Click(object sender, RoutedEventArgs e)
        {
            lvw3.ItemsSource = null;
            NormalClass nc = new NormalClass();
            nc.Path = CurrentDir;
            nc.OA_Total(UserName);
            tbOAtotal.Text = "全部OA(" + nc.Count.ToString() + ")";
            lvw3.ItemsSource = DTtoAbstracts1(nc.DtResult);
            lvw3.View = lvw3.FindResource("tileView") as ViewBase;
        }
        //分类：30天内OA
        private void rbtnOAin30_Click(object sender, RoutedEventArgs e)
        {
            lvw3.ItemsSource = null;
            NormalClass nc = new NormalClass();
            nc.Path = CurrentDir;
            nc.OAin30(UserName);
            tbOAin30.Text = "30天内OA(" + nc.Count.ToString() + ")";
            lvw3.ItemsSource = DTtoAbstracts1(nc.DtResult);
            lvw3.View = lvw3.FindResource("tileView") as ViewBase;

        }     
        //分类：最近提交
        private void rbtnRctAply_Checked(object sender, RoutedEventArgs e)
        {
            lvw3.ItemsSource = null;
            NormalClass nc = new NormalClass();
            nc.Path = CurrentDir;
            nc.Recent(UserName);
            tbRctAply.Text = "最近提交(" + nc.Count.ToString() + ")";
            lvw3.ItemsSource = DTtoAbstracts1(nc.DtResult);
            lvw3.View = lvw3.FindResource("tileView") as ViewBase;
        }

        //-------------------------------左侧按钮功能区：包括9个按钮：结束--------------------------------------------------


        //---------------------------辅助方法，被调用-------------------------------------------------

        //把从数据库中读出来的数据表转换为abstract序列
        private List<Abstract> DTtoAbstracts(System.Data.DataTable dt)
        {
            List<Abstract> abstracts = new List<Abstract>();

            foreach (DataRow dr in dt.Rows)
            {
                Abstract ab = new Abstract()
                {
                    AttorneyNum = dr["案件信息.我方案号"].ToString(),
                    CaseType = dr["案件类型"].ToString(),
                    Applicant = dr["申请人"].ToString(),
                    DocName = dr["交底名称"].ToString(),
                    CaseStatus = dr["案件状态"].ToString(),
                    Infosss = dr["备注"].ToString()
                };
                try
                {
                    ab.Deadline = (DateTime)dr["完成时限"];
                }
                catch
                { }
                abstracts.Add(ab);
            }

            return abstracts;
        }
        private List<Abstract> DTtoAbstracts1(System.Data.DataTable dt)
        {
            List<Abstract> abstracts = new List<Abstract>();

            foreach (DataRow dr in dt.Rows)
            {
                string taskname = dr["任务名称"].ToString();
                Abstract ab = new Abstract()
                {

                    AttorneyNum = dr["我方文号"].ToString(),
                    TaskID = dr["案件任务ID"].ToString(),
                    CaseID=dr["Tasks.案件ID"].ToString(),
                    CaseType = dr["申请类型"].ToString() + "：" + dr["任务名称"].ToString() + "：" + dr["任务属性"].ToString(),
                    Applicant = dr["客户名称"].ToString(),
                    DocName = dr["开案名称"].ToString(),
                    CaseStatus = dr["代理人处理状态"].ToString(),
                    Infosss = dr["案件备注"].ToString()
                };
                if (taskname == "OA答复" || taskname == "答复补正" || taskname == "驳回提复审（先请客户确认）")
                {
                    try
                    {
                        ab.Deadline = (DateTime)dr["官方期限"];
                    }
                    catch
                    { }
                }
                else
                {
                    try
                    {
                        ab.Deadline = (DateTime)dr["定稿期限(内)"];
                    }
                    catch
                    { }
                }

                abstracts.Add(ab);
            }

            return abstracts;
        }
        //把从数据库中查询所得到的数据表转换为TaskDetail对象
        private TaskDetail DTtoTaskDetail(System.Data.DataTable dt)
        {
            DataRow dr = dt.Rows[0];
            TaskDetail td = new TaskDetail()
            {
                AttorneyNum = dr["案件任务.我方案号"].ToString(),
                ClientNum = dr["客户案号"].ToString(),
                ClientName = dr["申请人"].ToString(),
                //Applicant = dr["申请人"].ToString(),
                DocName = dr["交底名称"].ToString(),
                InventionName = dr["发明名称"].ToString(),
                Inventor = dr["发明人"].ToString(),
                IsAuthorized = (bool)dr["是否授权"],
                IsRejected = (bool)dr["是否结案"],
                FinishReason = dr["结案原因"].ToString(),
                ApplicationID = dr["官方申请号"].ToString(),
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


            return td;
        }

        //把从数据库中查询所得到的数据表转换为TaskDetail对象
        private TaskDetail DTtoTaskDetail1(System.Data.DataTable dt)
        {
            DataRow dr = dt.Rows[0];
            TaskDetail td = new TaskDetail()
            {
                TaskID = dr["案件任务ID"].ToString(),
                
                AttorneyNum = dr["我方文号"].ToString(),
                ClientNum = dr["客户文号"].ToString(),
                ClientName = dr["客户名称"].ToString(),
                //Applicant = dr["申请人"].ToString(),
                DocName = dr["开案名称"].ToString(),

                ApplicationNum = dr["申请号"].ToString(),
                CaseType = dr["申请类型"].ToString() + "：" + dr["任务名称"].ToString() + "：" + dr["任务属性"].ToString(),

                Taskinfo = dr["案件备注"].ToString(),
                Attorney = dr["承办人"].ToString(),
                
                //Weight = (double)dr["权值"],
                TaskStatus = dr["代理人处理状态"].ToString()
            };
            try
            {
                if (dr["初稿期限(外)"].ToString() != "")
                {
                    td.FirstVirsionDeadline = (DateTime)dr["初稿期限(外)"];
                }
                else
                {
                    td.FirstVirsionDeadline = (DateTime)dr["初稿期限(内)"];
                }

                //td.FirstVirsionDeadline = (DateTime)dr["初稿期限(内)"];
                //td.Deadline = (DateTime)dr["定稿期限(内)"];
                //string taskname = dr["任务名称"].ToString();
                //if (taskname == "OA答复" || taskname == "答复补正" || taskname == "请求复审")
                //{
                //    try
                //    {
                //        td.Deadline = (DateTime)dr["官方期限"];
                //    }
                //    catch
                //    { }
                //}
                //else
                //{
                //    try
                //    {
                //        td.Deadline = (DateTime)dr["定稿期限(内)"];
                //    }
                //    catch
                //    { }
                //}


                //td.FinishDate = (DateTime)dr["结案时间"];
            }
            catch
            {

            }

            try
            {
                if (dr["官方期限"].ToString() != "")
                {
                    td.Deadline = (DateTime)dr["官方期限"];
                }
                else
                {
                    td.Deadline = (DateTime)dr["定稿期限(内)"];
                }
            }
            catch
            {
            }
            try
            {
                td.FirstVirsionDoneDate = (DateTime)dr["初稿日"];
            }
            catch
            {
            }
            try
            {
                td.DoneDate = (DateTime)dr["定稿日"];
            }
            catch
            {
            }

            return td;
        }
        
        //把数据显示在自定义的listview中
        private int ShowList(string sql, string path)
        {
            AccessCUID acuid = new AccessCUID();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = acuid.Query(sql, path);
            //lvw3.ItemsSource = DTtoAbstracts(dt);
            lvw3.ItemsSource = DTtoAbstracts1(dt);
            lvw3.View = lvw3.FindResource("tileView") as ViewBase;
            return dt.Rows.Count;
        }
        //更新信息
        private void UpdateInfo(string sql, string path)
        {
            AccessCUID acuid = new AccessCUID();
            acuid.Update(sql, path);
        }
              
        //上一次查询
        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (operationRecord.Pointer > 1)
            {
                operationRecord.Pointer -= 1;
                int pointer = operationRecord.Pointer;
                ShowList(operationRecord.GetNodeValue(pointer), CurrentDir+"MyData\\dataset.accdb");
                if (pointer > 0)
                {
                    lbCurrent.Content = operationName.GetNodeValue(pointer);

                }
                else
                {
                    lbCurrent.Content = "";
                }

            }
            else
                MessageBox.Show("已经到达第一项");
        }
        //下一个查询
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (operationRecord.Pointer < operationRecord.GetLength())
            {
                operationRecord.Pointer += 1;
                int pointer = operationRecord.Pointer;
                ShowList(operationRecord.GetNodeValue(pointer), CurrentDir+"MyData\\dataset.accdb");
                if (pointer < operationRecord.GetLength() + 1)
                {
                    lbCurrent.Content = operationName.GetNodeValue(pointer);

                }
                else
                {
                    lbCurrent.Content = "";
                }
            }
            else
                MessageBox.Show("已经到达最后一项");
        }

        
  
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {


            //string pathCases = CurrentDir+"MyData\\案件列表.xls";
            //string pathTasks = CurrentDir+"MyData\\任务列表.xls";
            //System.Data.DataTable dtCases = new System.Data.DataTable();
            //System.Data.DataTable dtTasks = new System.Data.DataTable();
            //AccessCUID acuid = new AccessCUID();
            //ExcelCUID exlcuid = new ExcelCUID();
            //acuid.Update("delete * from Tasks", CurrentDir+"MyData\\dataset.accdb");
            //acuid.Update("delete * from Cases", CurrentDir+"MyData\\dataset.accdb");
            //string sqlCases = "select * from [Sheet1$] order by 委案日期 desc";
            //string sqlTasks = "select * from [Sheet1$] order by 配案日 desc";
            ////string sqlTasks = "select * from [Sheet1$] where 承办人='" + UserName + "' order by 配案日 desc";
            //dtCases = exlcuid.Query(sqlCases, pathCases);
            //dtTasks = exlcuid.Query(sqlTasks, pathTasks);
            //UpdateAccess(dtCases, CurrentDir+"MyData\\dataset.accdb", "select * from Cases");
            //UpdateAccess(dtTasks, CurrentDir+"MyData\\dataset.accdb", "select * from Tasks");
            showWeight();
            MessageBox.Show("已刷新！");
        }

        public static void UpdateAccess(System.Data.DataTable temp, string strPath, string sql)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strPath);
            //try
            //{
                con.Open();
                OleDbDataAdapter Bada = new OleDbDataAdapter(sql, con);//建立一个DataAdapter对象
                OleDbCommandBuilder cb = new OleDbCommandBuilder(Bada);//这里的CommandBuilder对象一定不要忘了,一般就是写在DataAdapter定义的后面
                cb.QuotePrefix = "[";
                cb.QuoteSuffix = "]";
                DataSet ds = new DataSet();//建立DataSet对象
                Bada.Fill(ds, "demo");//填充DataSet                
                foreach (DataRow tempRow in temp.Rows)
                {

                    DataRow dr = ds.Tables["demo"].NewRow();
                    int num = tempRow.ItemArray.Count();
                    for (int i = 0; i < num; i++)
                    {
                        if (tempRow[i].ToString() == "")
                        {
                            tempRow[i] = DBNull.Value;
                        }
                    }

                    dr.ItemArray = tempRow.ItemArray;//行复制
                    //if (num != 13)
                    //{
                    //    string taskattribute = "";
                    //    taskattribute = dr["任务名称"].ToString() + dr["任务属性"].ToString();
                    //    switch (taskattribute)
                    //    {
                    //        case "新申请":
                    //            dr["权值"] = 1;
                    //            break;
                    //        case "OA答复一通":
                    //            dr["权值"] = 0.4;
                    //            break;
                    //        case "OA答复二通":
                    //            dr["权值"] = 0.2;
                    //            break;
                    //        case "OA答复三通":
                    //            dr["权值"] = 0.1;
                    //            break;
                    //        default:
                    //            dr["权值"] = 0;
                    //            break;
                    //    }
                    //}


                    ds.Tables["demo"].Rows.Add(dr);
                }

                Bada.Update(ds, "demo");//用DataAdapter的Update()方法进行数据库的更新
            }
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
            //finally
            //{
            //    con.Close();
            //}
        //}



        private void btnHJ_Click(object sender, RoutedEventArgs e)
        {
            btnHJ.Visibility = Visibility.Collapsed;
            btnLocal.Visibility = Visibility.Visible;
            wbLocalFiles.Visibility = Visibility.Hidden;
            this.Title = "华进系统";
            
            if (wbHJ.Source == null)
            {
                wbHJ.Source = new Uri("http://www.acip.vip");
            }
                              
            wbHJ.Visibility = Visibility.Visible;          

        }

        private void btnLocal_Click(object sender, RoutedEventArgs e)
        {
            btnHJ.Visibility = Visibility.Visible;
            btnLocal.Visibility = Visibility.Collapsed;
            wbHJ.Visibility = Visibility.Hidden;
            wbLocalFiles.Visibility = Visibility.Visible;
            this.Title = "案件助手";            
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (wbLocalFiles.CanGoBack)
            {
                wbLocalFiles.GoBack();
               
                if (wbLocalFiles.Source.ToString() == "file:///C:/WORK/NewApplication/" + tbAttorneyNum.Text)
                {
                    btnBack.IsEnabled = false;
                }
                else
                {
                    btnBack.IsEnabled = true;
                }
            }
        }

        private void btnForward_Click(object sender, RoutedEventArgs e)
        {
            if (wbLocalFiles.CanGoForward)
            {
                wbLocalFiles.GoForward();
                if (wbLocalFiles.Source.ToString() != "file:///C:/WORK/NewApplication/" + tbAttorneyNum.Text)
                {
                    btnBack.IsEnabled = true;
                }
                
            }
        }

        private void tbkFirstVirsionTotalWeight_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            WindowWeightDetail wd = new WindowWeightDetail();
            NormalClass nc = new NormalClass();
            nc.Path = CurrentDir;
            nc.FirstVirsion(UserName);
           
            WeightData wtdt = new WeightData();
            wtdt=CalculateWeight(nc.DtResult);
            wd.Firstdg.ItemsSource = wtdt.ResultTable.DefaultView;
            wd.tbTotalWeight.Text = wtdt.Totalweight.ToString();
            wd.Show();
        }
       

        private void tbkDoneWeight_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            WindowWeightDetail wd = new WindowWeightDetail();
            NormalClass nc = new NormalClass();
            nc.Path = CurrentDir;
            nc.Done_ThisMonth(UserName);
            WeightData wtdt = new WeightData();
            wtdt = CalculateWeight(nc.DtResult);
            wd.Firstdg.ItemsSource = wtdt.ResultTable.DefaultView;
            wd.tbTotalWeight.Text = wtdt.Totalweight.ToString();
            wd.Show();
        }

        private void tbkFirstVirsionGroupTotalWeight_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            WindowWeightDetail wd = new WindowWeightDetail();
            NormalClass nc = new NormalClass();
            nc.Path = CurrentDir;
            nc.FirstVirsion_Group_ThisMonth();
            WeightData wtdt = new WeightData();
            wtdt = CalculateWeight(nc.DtResult);
            wd.Firstdg.ItemsSource = wtdt.ResultTable.DefaultView;
            wd.tbTotalWeight.Text = wtdt.Totalweight.ToString();
            wd.Title = "部门初稿列表";
            wd.Show();
        }

        private void tbkGroupDoneWeight_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            WindowWeightDetail wd = new WindowWeightDetail();
            NormalClass nc = new NormalClass();
            nc.Path = CurrentDir;
            nc.Done_Group_ThisMonth();
            WeightData wtdt = new WeightData();
            wtdt = CalculateWeight(nc.DtResult);
            wd.Firstdg.ItemsSource = wtdt.ResultTable.DefaultView;
            wd.tbTotalWeight.Text = wtdt.Totalweight.ToString();
            wd.Title = "部门递交列表";
            wd.Show();
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

        private void tbkOverStock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            WindowWeightDetail wd = new WindowWeightDetail();
            NormalClass nc = new NormalClass();
            nc.Path = CurrentDir;
            nc.OverStock();
            WeightData wtdt = new WeightData();
            wtdt = CalculateWeight(nc.DtResult);
            wd.Firstdg.ItemsSource = wtdt.ResultTable.DefaultView;
            wd.tbTotalWeight.Text = wtdt.Count.ToString();
            wd.Title = "部门积压案件列表";
            wd.Show();
        }

        private void tbkOutofLimit_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            WindowWeightDetail wd = new WindowWeightDetail();
            NormalClass nc = new NormalClass();
            nc.Path = CurrentDir;
            nc.OutofLimit();
            WeightData wtdt = new WeightData();
            wtdt = CalculateWeight(nc.DtResult);
            wd.Firstdg.ItemsSource = wtdt.ResultTable.DefaultView;
            wd.tbTotalWeight.Text = wtdt.Count.ToString();
            wd.Title = "部门超期案件列表";
            wd.Show();
        }

        private void btnCareer_Click(object sender, RoutedEventArgs e)
        {
            Deadline dl = new Deadline();
            dl.CurrentDir = CurrentDir;
            dl.Show();
        }

       


        //---------------------------辅助方法，被调用-------------------------------------------------

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfWithNet.Models
{
    class TaskDetail
    {
        //任务ID
        public string TaskID { get; set; }
        //我方案号
        public string AttorneyNum { get; set; }
        //客户案号
        public string ClientNum { get; set; }
        //客户名称
        public string ClientName { get; set; }
        //交底名称
        public string DocName { get; set; }
        //发明名称
        public string InventionName { get; set; }
        //发明人
        public string Inventor { get; set; }
        //是否授权
        public bool IsAuthorized { get; set; }
        //是否结案
        public bool IsRejected { get; set; }
        //结案时间
        public DateTime FinishDate { get; set; }
        //结案原因
        public string FinishReason { get; set; }
        //接案人
        public string CaseSource { get; set; }
        //IP
        public string IP { get; set; }
        //申请号
        public string ApplicationNum { get; set; }
        //官方申请号
        public string ApplicationID { get; set; }
        //案件类型
        public string CaseType { get; set; }
        //申请人
        public string Applicant { get; set; }
        //初稿期限
        public DateTime FirstVirsionDeadline { get; set; }
        //初稿完成日期
        public DateTime FirstVirsionDoneDate { get; set; }
        //配案日
        public DateTime DispatchDate { get; set; }
        //完成期限
        private DateTime deadline;
        public DateTime Deadline
        {
            get { return deadline; }

            set { deadline = value; }
        }
        //完成日期
        public DateTime DoneDate { get; set; }
        //代理人
        public string Attorney { get; set; }
        //案件状态

        private string caseStatus;
        public string TaskStatus
        {
            get { return caseStatus; }
            set { caseStatus = value; }
        }
        //状态颜色，根据案件状态自动变换
        private SolidColorBrush statecolor;
        public SolidColorBrush StateColor
        {
            get
            {
                switch (caseStatus)
                {
                    case "已提交":
                    case "结案":
                        statecolor = new SolidColorBrush(Colors.LightGreen);
                        break;
                    case "客户确认中":
                        statecolor = new SolidColorBrush(Colors.SkyBlue);
                        break;
                    default:
                        statecolor = new SolidColorBrush(Colors.Red);
                        break;
                }
                return statecolor;
            }
            set
            {
                statecolor = value;
            }
        }

        //状态颜色1，根据案件状态自动变换
        private SolidColorBrush statecolor1;
        public SolidColorBrush StateColor1
        {
            get
            {
                switch (caseStatus)
                {
                    case "完成:完成":
                        statecolor1 = new SolidColorBrush(Colors.Green);
                        break;
                    case "返稿:客户确认中":
                    case "撰写:客户长期未确认":
                        statecolor1 = new SolidColorBrush(Colors.CornflowerBlue);
                        break;
                    case "送件:递交中":
                        statecolor1 = new SolidColorBrush(Colors.LightGreen);
                        break;
                    default:
                        statecolor1 = new SolidColorBrush(Colors.OrangeRed);
                        break;
                }
                return statecolor1;
            }
            set
            {
                statecolor1 = value;
            }
        }
        //剩余天数或超出天数
        private string daysleft;
        public string DaysLeft
        {
            get
            {
                if (caseStatus != "已提交")
                {
                    if (deadline > DateTime.Now.Date)
                    {
                        daysleft = "余" + (int)((Deadline - DateTime.Now.Date).Days) + "天";
                    }
                    else
                    {
                        daysleft = "超" + (int)((DateTime.Now.Date - Deadline).Days) + "天";
                    }
                }
                else
                {
                    daysleft = "";
                }
                return daysleft;
            }
            set
            {
                daysleft = value;
            }
        }
        //日期颜色，超期时为红，否则为绿
        private SolidColorBrush dayscolor;
        public SolidColorBrush DaysColor
        {
            get
            {
                if (caseStatus != "已提交")
                {
                    if (deadline > DateTime.Now.Date)
                    {
                        dayscolor = new SolidColorBrush(Colors.Green);
                    }
                    else
                    {
                        dayscolor = new SolidColorBrush(Colors.OrangeRed);
                    }
                }
                else
                {
                    dayscolor = new SolidColorBrush(Colors.White);
                }

                return dayscolor;
            }
            set
            {
                dayscolor = value;
            }
        }
        //权值
        public double Weight { get; set; }
        //备注
        public string Taskinfo { get; set; }
    }
}

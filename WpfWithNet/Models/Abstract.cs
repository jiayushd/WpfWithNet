using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfWithNet.Models
{
    class Abstract
    {
        //任务ID
        public string TaskID { get; set; }
        //案件ID
        public string CaseID { get; set; }
        //我方案号
        public string AttorneyNum { get; set; }
        //案件类型
        public string CaseType { get; set; }
        //申请人
        public string Applicant { get; set; }
        //交底名称
        public string DocName { get; set; }
        //完成期限
        private DateTime deadline;
        public DateTime Deadline
        {
            get { return deadline; }
                
            set { deadline = value; }
        }
        //案件状态
        private string caseStatus;
        public string CaseStatus
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
                        statecolor = new SolidColorBrush(Colors.Green);
                        break;
                    case "客户确认中":
                        statecolor = new SolidColorBrush(Colors.CornflowerBlue);
                        break;
                    default:
                        statecolor = new SolidColorBrush(Colors.OrangeRed);
                        break;
                }
                return statecolor;
            }
            set {
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
                    case "完成:结案":
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
        public ImageSource img { get; set; }

        //备注
        public string Infosss { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WpfWithNet.Models
{
    class PatentTask
    {
        //我方案号
        public string AttorneyNum { get; set; }
        //IP
        public string IP { get; set; }
        //案件类型
        public string CaseType { get; set; }

        //初稿完成日期
        public DateTime FirstVirsionDoneDate { get; set; }

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
        //权值
        public double Weight { get; set; }
        //备注
        public string Taskinfo { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfWithNet.Models
{
    class DetailView
    {
        //我方案号
        public string AttorneyNum { get; set; }
        //案件类型
        public string CaseType { get; set; }
        //申请人
        public string Applicant { get; set; }
        //开案日期
        public string StartDate { get; set; }
        //初稿日期
        public string FirstVirsionDate { get; set; }
        //完成日期
        public string DoneDate { get; set; }
    }
}

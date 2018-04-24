using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfWithNet.Models
{
    class PatentCase
    {
        public string AttorneyNum { get; set; }
        public string ClientNum { get; set; }
        public string Applicant { get; set; }
        public string ApplicationNum { get; set; }
        public string DocName { get; set; }
        public string InventionName { get; set; }
        public string Inventor { get; set; }
        public string CaseID { get; set; }
        public string InternalState { get; set; }
        public string TechField { get; set; }
        public string FinishReason { get; set; }
        //public List<TaskDetail> Tasks = new List<TaskDetail>();

    }
}

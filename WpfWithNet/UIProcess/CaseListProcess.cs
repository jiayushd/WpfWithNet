using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using WpfWithNet.Models;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfWithNet.UIProcess
{
    class CaseListProcess
    {
        //List<Abstract> abstracts = new List<Abstract>();

        //public void GetAbastracts(DataTable dt)
        //{
        //    DataRow dr;
            
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        dr = dt.Rows[i];
        //        Abstract abstractt = new Abstract();
        //        abstractt.Applicant = "-" + dr["申请人"];
        //        abstractt.AttorneyNum = dr["我方案号"];
        //        abstractt.CaseType = patentTasks[i].AttorneyNum;
        //        abstractt.DocName = "-" + patentTasks[i].AttorneyNum;
        //        if (patentTasks[i].TaskStatus == "未处理")
        //        {
        //            abstractt.StateColor = new SolidColorBrush(Colors.Red);
        //        }
        //        else
        //        {
        //            abstractt.StateColor = new SolidColorBrush(Colors.Black);
        //        }
        //        if (patentTasks[i].TaskType == "新申请")
        //        {
        //            abstractt.img = new BitmapImage(new Uri("Images/NewApp.png", UriKind.RelativeOrAbsolute));
        //        }
        //        else
        //        {
        //            abstractt.img = new BitmapImage(new Uri("Images/OA.png", UriKind.RelativeOrAbsolute));
        //        }
        //        TimeSpan ts = DateTime.Now.Date.Subtract(patentTasks[i].DoneDeadline);
        //        int daysleft = ts.Days;
        //        if (daysleft > 0)
        //        {
        //            abstractt.DaysLeft = "超" + Math.Abs(daysleft).ToString() + "天";
        //            abstractt.daysColor = Colors.Red;

        //        }
        //        else
        //        {
        //            abstractt.DaysLeft = "余" + Math.Abs(daysleft).ToString() + "天";
        //            abstractt.daysColor = Colors.Green;
        //        }


        //        //taskList.Applicant = "-" + caseFile.ApplicantTag;
        //        abstracts.Add(abstractt);
        //    }

        //}
    }
}

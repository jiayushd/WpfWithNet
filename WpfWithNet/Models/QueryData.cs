using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfWithNet.dbCUID;

namespace WpfWithNet.Models
{
    class QueryData
    {
        private string queryname;
        public string Queryname { get => queryname; set => queryname = value; }
        private string querystatement;
        public string Querystatement { get => querystatement; set => querystatement = value; }
        private int count;
        public int Count { get => count; set => count = value; }
        private DataTable dtResult;
        public DataTable DtResult { get => dtResult; set => dtResult = value; }

        public string GetAction_SelectAll()
        {
            string action= "Select * from Tasks,Cases where Tasks.案件ID=Cases.案件ID";
            return action;
        }
        //拼接SQL语句：不区分用户
        public string GetStatement(string action, string filter)
        {
            string sql = action;            
            sql = sql + filter;
            return sql;
        }
        //拼接SQL语句：区分用户
        public string GetStatement(string action,string username, string filter)
        {
            string sql= action;
            sql = sql + " and (承办人='" + username + "')";
            sql = sql+ filter;
            return sql;
        }
       
        //-------------------------------------------固定筛选条件：开始--------------------------------------------------------------------
        //筛选国内案的条件
        public string GetFilter_CN()
        {
            string filter = "";
            filter = filter + " and 任务名称 = '新申请'";
            filter = filter + " and (代理人处理状态 NOT IN ('完成:完成','完成:结案','不处理:不处理'))";
            //filter = filter + " order by [定稿期限(内)] asc";
            return filter;
        }
        //筛选涉外案的条件
        public string GetFilter_Foreign()
        {
            string filter = "";
            filter = filter + " and 申请类型 ='PCT国际申请'";
            filter = filter + " and (代理人处理状态 NOT IN ('完成:完成','完成:结案','不处理:不处理'))";
            //filter = filter + " order by [定稿期限(内)] asc";
            return filter;
        }
        //筛选可处理案件的条件
        public string GetFilter_Todo()
        {
            string filter = "";
            filter = filter + " and 任务名称 IN ('改写', '撰写','翻译' ,'新申请')";
            filter = filter + " and (代理人处理状态  IN ('撰写:未处理','撰写:撰写中'))";
            //filter = filter + " order by [定稿期限(内)] asc";
            return filter;
        }
        //筛选初稿的条件
        public string GetFilter_FirstVirsion()
        {
            string filter = "";
            filter = filter + " and 任务名称 = '新申请'";
            filter = filter + " and (代理人处理状态  IN ('返稿:客户确认中','撰写:客户长期未确认'))";
            //filter = filter + " order by [定稿期限(内)] asc";
            return filter;
        }
        public string GetFilter_FirstVirsion(DateTime startDate, DateTime endDate)
        {
            string filter = "";
            filter = filter + " and 任务名称 = '新申请'";
            filter = filter + " and (代理人处理状态  IN ('返稿:客户确认中','撰写:客户长期未确认'))";
            filter = filter + " and (初稿日 between #" + startDate + "# and #" + endDate + "#)";

            return filter;
        }

        //筛选当月初稿的条件
        public string GetFilter_FirstVirsion_ThisMonth()
        {
            DateTime now = DateTime.Now;
            DateTime startDate = new DateTime(now.Year, now.Month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            string filter = "";
            filter = filter + " and (初稿日 between #" + startDate + "# and #" + endDate + "#)";
            return filter;
        }
        //筛选OA总数的条件
        public string GetFilter_OAtotal()
        {
            string filter = "";
            filter = filter + " and 任务名称 IN ('OA答复','驳回提复审（先请客户确认）','答复补正')";
            filter = filter + " and (代理人处理状态 NOT IN ('完成:完成','完成:结案','不处理:不处理'))";            
            return filter;
        }
        //筛选30天内的OA的条件
        public string GetFilter_OAin30()
        {
            string filter = " and 官方期限 < CDate(now())+30";
            filter = filter + " and 任务名称 IN ('OA答复','驳回提复审（先请客户确认）','答复补正')";
            filter = filter + " and (代理人处理状态 NOT IN ('完成:完成','完成:结案','不处理:不处理'))";
            return filter;
        }
//-------------------------------------------固定筛选条件：结束--------------------------------------------------------------------

        
        //设置实例的各个参数    
        private void SetParameter()
        {
            string path = "C:\\WORK\\MyData\\数据集.accdb";
            AccessCUID acuid = new AccessCUID();
            dtResult = new DataTable();
            dtResult = acuid.Query(querystatement, path);
            count = dtResult.Rows.Count;

        }

        //查询国内案件
        public void Query_CN(string username)
        {
            queryname = "CN";
            querystatement = GetStatement(GetAction_SelectAll(), username, GetFilter_CN());
            SetParameter();
        }
        public void Query_CN(string username, string sortfield, bool isasc)
        {
            queryname = "CN";
            querystatement = GetStatement(GetAction_SelectAll(), username, GetFilter_CN());
            if (isasc)
                querystatement = querystatement + " order by " + sortfield + " asc";
            else
                querystatement = querystatement + " order by " + sortfield + " des";
            SetParameter();
        }


        //查询涉外案件
        public void Query_Foreign(string username)
        {
            queryname = "Foreign";
            querystatement = GetStatement(GetAction_SelectAll(), username,GetFilter_Foreign());
            SetParameter();
        }
        public void Query_Foreign(string username, string sortfield, bool isasc)
        {
            queryname = "Foreign";
            querystatement = GetStatement(GetAction_SelectAll(), username, GetFilter_Foreign());
            if (isasc)
                querystatement = querystatement + " order by " + sortfield + " asc";
            else
                querystatement = querystatement + " order by " + sortfield + " des";
            SetParameter();
        }

        //查询可处理案件
        public void Query_Todo(string username)
        {
            queryname = "Todo";
            querystatement = GetStatement(GetAction_SelectAll(), username,GetFilter_Todo());
            SetParameter();
        }
        public void Query_Todo(string username, string sortfield, bool isasc)
        {
            queryname = "Todo";
            querystatement = GetStatement(GetAction_SelectAll(), username, GetFilter_Todo());
            if (isasc)
                querystatement = querystatement + " order by " + sortfield + " asc";
            else
                querystatement = querystatement + " order by " + sortfield + " des";
            SetParameter();
        }
        //查询所有初稿
        public void Query_FirstVirsion()
        {
            queryname = "FirstVirsion";
            querystatement = GetStatement(GetAction_SelectAll(), GetFilter_FirstVirsion());
            SetParameter();
        }
        public void Query_FirstVirsion(string username)
        {
            queryname = "FirstVirsion";
            querystatement = GetStatement(GetAction_SelectAll(), username,GetFilter_FirstVirsion());
            SetParameter();
        }
        public void Query_FirstVirsion(string username, string sortfield, bool isasc)
        {
            queryname = "FirstVirsion";
            querystatement = GetStatement(GetAction_SelectAll(), username, GetFilter_FirstVirsion());
            if (isasc)
                querystatement = querystatement + " order by " + sortfield + " asc";
            else
                querystatement = querystatement + " order by " + sortfield + " des";
            SetParameter();
        }

        //查询当月初稿
        public void Query_FirstVirsion_ThisMonth(string username)
        {
            queryname = "FirstVirsion_ThisMonth";
            querystatement = GetStatement(GetAction_SelectAll(), username,GetFilter_FirstVirsion_ThisMonth());
            SetParameter();
        }
        public void Query_FirstVirsion_ThisMonth(string username, string sortfield, bool isasc)
        {
            queryname = "FirstVirsion_ThisMonth";
            querystatement = GetStatement(GetAction_SelectAll(), username, GetFilter_FirstVirsion_ThisMonth());
            if (isasc)
                querystatement = querystatement + " order by " + sortfield + " asc";
            else
                querystatement = querystatement + " order by " + sortfield + " des";
            SetParameter();
        }

        //查询所有OA
        public void Query_OAtotal(string username)
        {
            queryname = "OAtotal";
            querystatement = GetStatement(GetAction_SelectAll(), username,GetFilter_OAtotal());
            SetParameter();
        }
        public void Query_OAtotal(string username, string sortfield, bool isasc)
        {
            queryname = "OAtotal";
            querystatement = GetStatement(GetAction_SelectAll(), username, GetFilter_OAtotal());
            if (isasc)
                querystatement = querystatement + " order by " + sortfield + " asc";
            else
                querystatement = querystatement + " order by " + sortfield + " des";
            SetParameter();
        }
        //查询30天内OA
        public void Query_OAin30(string username)
        {
            queryname = "OAin30";
            querystatement = GetStatement(GetAction_SelectAll(),username, GetFilter_OAin30());
            SetParameter();
        }
        public void Query_OAin30(string username,string sortfield, bool isasc)
        {
            queryname = "OAin30";
            querystatement = GetStatement(GetAction_SelectAll(), username, GetFilter_OAin30());
            if(isasc)
                querystatement = querystatement + " order by " + sortfield + " asc";
            else
                querystatement = querystatement + " order by " + sortfield + " des";
            SetParameter();
        }

        public void Query_Customized(string username,string filter)
        {
            queryname = "Customized";
            querystatement = GetStatement(GetAction_SelectAll(), username, filter);
            SetParameter();
        }


    }
}

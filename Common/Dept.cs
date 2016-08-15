using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Workday.Common
{
    public interface IShowDepts
    {
        int DeptId { get; set; }

        string DeptName { get; set; }

        string ManagerName { get; set; }

        string ParentName { get; set; }

        DateTime CreateDate { get; set; }
    }

    public class Dept:IShowDepts
    {
        public int DeptId { get; set; }

        public string DeptName { get; set; }

        public int? Manager { get; set; }

        public string ManagerName { get; set; }

        public int? ParentDept { get; set; }

        public string ParentName { get; set; }

        public DateTime CreateDate { get; set; }

    }

    public class ParentList
    {
        public Dictionary<int, string> ParentDict = new Dictionary<int, string>();
        
        public int CurrentSelect { get; set; }
    }

    public class ManagerList
    {
        public Dictionary<string, int> ManagerDict = new Dictionary<string, int>();

        public int CurrentSelect { get; set; }
    }

    public class updateresult
    {
        public int resultid { get; set; }
        public string resultdesc { get; set; }
        public int conflictdeptid { get; set; }
    }

    public class deldeptresult
    {
        public int id { get; set; }
        public string desc { get; set; }
    }

}

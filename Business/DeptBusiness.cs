using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Workday.DataAccess;
using Workday.Common;
using System.Data.SqlClient;

namespace Workday.Business
{
    public class DeptBusiness
    {
        public static updateresult AddDept(Dept newdept,int conflictdeptid)
        {
            updateresult result=new updateresult();
            bool ifadd= DeptDataAccess.AddDept(newdept,conflictdeptid);
            if (ifadd)
            {
                result.resultid = 5;
                result.resultdesc = "add new dept successfully!";
                return result;
            }
            else
            {
                result.resultid = 6;
                result.resultdesc = "add new dept exception!";
                return result;
            }
        }
    
        public static List<IShowDepts> GetAllDepts()
        {
            //get all depts information for gridview binding
            return DeptDataAccess.IGetAllDepts();
        }

        public static ParentList GetParentListForUpdate(int i)
        {
            //get parent list dict and get rid of current dept for update dropdownlist binding
            ParentList parents = new ParentList();
            parents = DeptDataAccess.GetParentList();
            parents.CurrentSelect = DeptDataAccess.GetParent(i);
            List<int> ToRemove = new List<int>();
            foreach(int key in parents.ParentDict.Keys)
            {
                if (key == i)
                {
                    ToRemove.Add(key);
                }
            }
            foreach(int j in ToRemove)
            {
                parents.ParentDict.Remove(j);
            }

            return parents;
        }

        public static ParentList GetParentListForAdd()
        {
            return DeptDataAccess.GetParentList();
        }

        public static ManagerList GetManagerList(int i)
        {
            //get manager list dict for dropdownlist binding
            return DeptDataAccess.GetManagerList(i);
        }

        public static updateresult UpdateDeptValidate(Dept dept, string ifupdate)
        {
            updateresult result = new updateresult();
            //get all depts from db
            List<Dept> Depts = new List<Dept>();
            Depts = DeptDataAccess.GetAllDepts();

            // check if change,if no change, return -1. if add new, do no need check this.
            if (ifupdate == "update")
            {
                foreach (Dept d in Depts)
                {
                    if (d.DeptId == dept.DeptId)
                    {
                        if (d.DeptName == dept.DeptName & d.Manager == dept.Manager & d.ParentDept == dept.ParentDept)
                        {
                            // no change,return -1
                            result.resultid = -1;
                            result.resultdesc = "No Change";
                            return result;
                        }
                    }
                }
            }
            //check if new name is null, if null ,reture 0
            if (dept.DeptName == null || dept.DeptName == "")
            {
                result.resultid = 0;
                result.resultdesc = "Dept name could not be null";
                return result;
            }

            // if update, check if parent loop. if loop, return 1. if add new, do not need to check loop
            if (ifupdate == "update")
            {
                int deptid = dept.DeptId;
                if (dept.ParentDept != null)
                {
                    bool ifloop = CheckLoop(deptid, dept, Depts);
                    if (ifloop)
                    {
                        result.resultid = 1;
                        result.resultdesc = "parent loop. you could not set " + dept.ParentName + " as the parent of " + dept.DeptName + " !!";
                        return result;
                    }
                }
            }
            
            //check if name conflict and have the same parent.if do, return 2
            foreach (Dept d in Depts)
            {
                if (d.DeptName == dept.DeptName & d.DeptId != dept.DeptId)
                {//兄弟不能重名
                    if (d.ParentDept == dept.ParentDept)
                    {
                        result.resultid = 2;
                        result.resultdesc = dept.DeptName + " is the name of another dept and that dept has the same parent. You could not set!";
                        return result;
                    } //父子不能重名
                    else if ((d.ParentDept == dept.DeptId) || (d.DeptId == dept.ParentDept))
                    {
                        result.resultid = 2;
                        result.resultdesc = dept.DeptName + " is the name of its parent or son. You could not set!";
                        return result;
                    }

                }
            }
            //check if manager conflict. if conflict and not confirmed, return 3 and return conflictdeptid
            if (dept.ManagerName != "null")
            {
                foreach (Dept d in Depts)
                {
                    if (d.DeptId != dept.DeptId & d.Manager == dept.Manager)
                    {
                        result.resultid = 3;
                        result.resultdesc = dept.ManagerName + " is the manger of " + d.DeptName + "! Are you sure to assign he/her as the manger of  " + dept.DeptName + "?";
                        result.conflictdeptid = d.DeptId;
                        return result;
                    }
                }
            }

            //   if no loop, no null name, no name and manager confict and has change, then return 4 to nofify web call updatedept mothed to update db
            result.resultid = 4;
            return result;
        }

        public static updateresult UpdateDept(Dept dept,int conflictdeptid)
        {
            // update DB; or if conflict but confirmed, then update DB;
            updateresult result = new updateresult();
            //if =0, that means no manager conflict, so that you could update current dept only.
            if (conflictdeptid == 0)
            {
                if (DeptDataAccess.UpdateDept(dept))
                {
                    result.resultid = 5;
                    result.resultdesc = "update successfully!";
                    return result;
                }
                else
                {
                    result.resultid = 6;
                    result.resultdesc = "update exception!";
                    return result;
                }
            }
            else //if !=o, that means manager conflict, so that you should update both current and conflict depts in DB.
            {
                if (DeptDataAccess.UpdateDept(dept, conflictdeptid))
                {
                    result.resultid = 5;
                    result.resultdesc = "update successfully!";
                    return result;
                }
                else
                {
                    result.resultid = 6;
                    result.resultdesc = "update exception!";
                    return result;
                }
            }
        }

        //递归验证不存在上级部门循环
        private static bool CheckLoop (int deptid, Dept dept, List<Dept> depts)
        {
            foreach(Dept d in depts)
            {
                if (d.DeptId == dept.ParentDept)
                {
                    if (d.ParentDept == null)
                    {
                        //no loop, could set
                        return false;
                    }
                    else if (d.ParentDept == deptid)
                    {
                        //loop, could not set
                        return true;
                    }
                    else
                    {
                        //iterate to the grandfather （递归）
                        return CheckLoop(deptid, d, depts);
                    }
                }
            }
            //if this dept does not exist in db, then return ture. (this situation do not exist. the below code is just to avoid grammar error.
            return true;
        }

        public static deldeptresult deleteverify(int deptid)
        {
            deldeptresult result = new deldeptresult();
            result.id = 3;
            int usercount, subdeptcount;
            usercount = DeptDataAccess.GetUserCountForADept(deptid);
            subdeptcount = DeptDataAccess.GetSubDeptCountForADept(deptid);
            if (usercount != 0)
            {
                result.id = 1;
                result.desc = "this dept has users! You could not delete it!";
            }
            else if (subdeptcount != 0)
            {
                result.id = 2;
                result.desc = "this dept has sub depts! You could not delete it!";
            }
            else if(usercount==0 & subdeptcount == 0)
            {
                result.id = 0;
            }

            return result;
        }

        public static deldeptresult DeleteDept(int deptid)
        {
            deldeptresult result = new deldeptresult();
            if (DeptDataAccess.DeleteDept(deptid))
            {
                result.id = 4;
                result.desc = "delete successfully!";
                return result;
            }
            else
            {
                result.id = 5;
                result.desc = "delete exception!";
                return result;
            }
        }

        }
}

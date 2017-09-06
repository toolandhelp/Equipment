using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Equipment.DAL;
using Equipment.Model;

namespace Equipment.BLL
{
    public class BLL_Dept
    {
        #region dbContext
        public DbHelperEfSql<ts_Dept> dbContext { get; set; }
        public BLL_Dept()
        {
            dbContext = new DbHelperEfSql<ts_Dept>();
        }
        #endregion
        #region  查询 Search Entity

        //获取单条数据
        public ts_Dept GetObjectById(string deptId)
        {
            return dbContext.SearchBySingle(c => c.ID == deptId);
        }
        public ts_Dept GetObjectByCode(string deptCode)
        {
            return dbContext.SearchBySingle(c => c.Dept_Code == deptCode);
        }
        public IList<ts_Dept> GetEnabledListByParent(string parentID)
        {
            return dbContext.SearchListByCondition(c => c.Dept_FatherId == parentID, true, o => o.Dept_Order);
        }
        public IList<ts_Dept> GetEnabledList()
        {
            return dbContext.SearchListByCondition(c => true, true, o => o.Dept_Order);
        }

        public IList<ts_Dept> GetEnabledListBy2nodes()
        {
            return dbContext.SearchListByCondition_ex(c => true, o => o.Dept_FatherId == "1" || o.Dept_FatherId == "0", true, o => o.Dept_Order);
        }
        public IList<ts_Dept> GetEnabledList_Ex()
        {
            return dbContext.SearchListByCondition_ex(c => true, o => o.Dept_FatherId == "1", true, o => o.Dept_Order);
        }
        public IList<ts_Dept> GetEnabledListByFIdx(string sFatherid)
        {
            return dbContext.SearchListByCondition(c => c.Dept_FatherId == sFatherid, true, o => o.Dept_Order);
        }
        //public IList<v_link_dept> GetLinkDeptList(string parentID)
        //{
        //    var service = new DbHelperEfSql<v_link_dept>();
        //    return service.SearchListByCondition(c => c.Dept_FatherId == parentID, true, o => o.Dept_Order);
        //}

        //查询所有部门
        public IList<ts_Dept> GetListByAll()
        {
            return dbContext.SearchByAll();
        }
        #endregion

        public ts_Dept GetUserDept(string deptCode)
        {
            var deptNames = new List<ts_Dept>();
            var dept = dbContext.SearchBySingle(c => c.Dept_Code == deptCode);
            if (dept != null)
                deptNames.Add(dept);
            for (var i = 0; i < 1; i--)
            {
                dept = dbContext.SearchBySingle(c => c.ID == dept.Dept_FatherId);
                if (dept == null) break;
                deptNames.Add(dept);
            }
            return deptNames.Count > 2 ? deptNames[deptNames.Count - 3] : null;
        }
    }
}

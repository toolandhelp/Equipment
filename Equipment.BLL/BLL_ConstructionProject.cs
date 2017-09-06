
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Equipment.DAL;
using Equipment.Model;

namespace Equipment.BLL
{
    public class BLL_ConstructionProject
    {
        #region dbContext
        public DbHelperEfSql<td_ConstructionProject> dbContext { get; set; }
        public BLL_ConstructionProject()
        {
            dbContext = new DbHelperEfSql<td_ConstructionProject>();
        }
        #endregion

        #region 增改删 CUD Entity
        public bool AddOrUpdate(td_ConstructionProject entity)
        {
            if (entity.ID < 1)
            {
                dbContext.Add(entity);
            }
            return dbContext.Update(entity, c => c.ID == entity.ID);
        }
        public bool PhysicalDelete(int[] eIds)
        {
            return dbContext.PhysicalDeleteByCondition(c => eIds.Contains(c.ID));
        }

        #endregion
        #region  查询 Search Entity
        /// <summary>
        /// 根据Id获取单条数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public td_ConstructionProject GetObjectById(int iId)
        {
            return dbContext.SearchBySingle(c => c.ID == iId);
        }

        /// <summary>
        /// 获取所有_部门
        /// </summary>
        /// <returns></returns>
        public IList<V_ConstructionProject_Dept> GetObjectAll()
        {
            var service = new DbHelperEfSql<V_ConstructionProject_Dept>();
            return service.SearchByAll();
        }

        /// <summary>
        /// 根据Id获取单条数据...
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public V_ConstructionProject_Dept GetObjectDeptById(int iId)
        {
            var service = new DbHelperEfSql<V_ConstructionProject_Dept>();
            return service.SearchBySingle(c => c.ID == iId);
        }

        /// <summary>
        /// 根据查询对象获取多条记录
        /// </summary>
        /// <param name="conditionItem"></param>
        /// <returns></returns>
        public IList<td_ConstructionProject> GetListByCondition(ConditionModel conditionItem)
        {
            return dbContext.SearchListByCondition(conditionItem);
        }
        /// <summary>
        /// 分页获取记录
        /// </summary>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <param name="iTotalRecord"></param>
        /// <returns></returns>
        public IList<td_ConstructionProject> GetPageList(int iPageIndex, int iPageSize, ref int iTotalRecord)
        {
            return dbContext.SearchByPageCondition(true, c => c.ID, iPageIndex, iPageSize, ref iTotalRecord);
        }
        /// <summary>
        /// 根据查询对象分页获取记录
        /// </summary>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <param name="iTotalRecord"></param>
        /// <param name="conditionItem"></param>
        /// <returns></returns>
        public IList<V_ConstructionProject_Dept> GetPageListByCondition(int iPageIndex, int iPageSize, ref int iTotalRecord, ConditionModel conditionItem)
        {
            var service = new DbHelperEfSql<V_ConstructionProject_Dept>();
            return service.SearchByPageCondition(iPageIndex, iPageSize, ref iTotalRecord, conditionItem);
        }
        ///// <summary>
        ///// 获取所有在用记录
        ///// </summary>
        ///// <returns></returns>
        //public IList<td_Employee> GetListByAllEnabled()
        //{
        //    return dbContext.SearchListByCondition(c => c.status == (int)StageMode.Normal, true, c => c.eCode);
        //}
        ///// <summary>
        ///// 获取所有记录
        ///// </summary>
        ///// <returns></returns>
        //public IList<td_Employee> GetListByAll()
        //{
        //    return dbContext.SearchByAll();
        //}
        ///// <summary>
        ///// 获取所有记录
        ///// </summary>
        ///// <returns></returns>
        ////public IList<v_User> GetViewListByAll()
        ////{
        ////    var service = new DbHelperEfSql<v_User>();
        ////    return service.SearchByAll();
        ////}
        #endregion

    }
}

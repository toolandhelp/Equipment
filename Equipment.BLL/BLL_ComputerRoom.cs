using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Equipment.Model;
using Equipment.DAL;

namespace Equipment.BLL
{
    public class BLL_ComputerRoom
    {
        #region dbContext
        public DbHelperEfSql<td_ComputerRoom> dbContext { get; set; }
        public BLL_ComputerRoom()
        {
            dbContext = new DbHelperEfSql<td_ComputerRoom>();
        }
        #endregion

        #region 增改删 CUD Entity

        public bool AddImport(List<td_ComputerRoom_1> list)
        {
            var service = new SpecialService();
            return service.AddImportRoom(list);
        }
        public bool AddImport_sl(List<td_ComputerRoom_sbsl_1> list)
        {
            var service = new SpecialService();
            return service.AddImportRoom_sl(list);
        }
        public bool AddOrUpdate(td_ComputerRoom entity)
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

        public bool PhysicalDelete1(int[] eIds)
        {
            var service = new DbHelperEfSql<td_ComputerRoom_1>();
            return service.PhysicalDeleteByCondition(c => eIds.Contains(c.ID));
        }
        #endregion

        #region  查询 Search Entity
        /// <summary>
        /// 根据Id获取单条数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public td_ComputerRoom GetObjectById(int iId)
        {
            return dbContext.SearchBySingle(c => c.ID == iId);
        }

        /// <summary>
        /// 根据Id获取单条数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public V_ComputerRoom_Dept GetObjectDeptById(int iId)
        {
            var service = new DbHelperEfSql<V_ComputerRoom_Dept>();
            return service.SearchBySingle(c => c.ID == iId);
        }

        /// <summary>
        /// 根据查询对象获取多条记录
        /// </summary>
        /// <param name="conditionItem"></param>
        /// <returns></returns>
        public IList<td_ComputerRoom> GetListByCondition(ConditionModel conditionItem)
        {
            return dbContext.SearchListByCondition(conditionItem);
        }

        /// <summary>
        /// 获取所有_部门
        /// </summary>
        /// <returns></returns>
        public IList<V_ComputerRoom_Dept> GetObjectAll()
        {
            var service = new DbHelperEfSql<V_ComputerRoom_Dept>();
            return service.SearchByAll();
        }

        /// <summary>
        /// 分页获取记录
        /// </summary>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <param name="iTotalRecord"></param>
        /// <returns></returns>
        public IList<td_ComputerRoom> GetPageList(int iPageIndex, int iPageSize, ref int iTotalRecord)
        {
            return dbContext.SearchByPageCondition(true, c => c.ID, iPageIndex, iPageSize, ref iTotalRecord);
        }
        #endregion

        /// <summary>
        /// 根据查询对象分页获取记录
        /// </summary>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <param name="iTotalRecord"></param>
        /// <param name="conditionItem"></param>
        /// <returns></returns>
        public IList<V_ComputerRoom_Dept> GetPageListByCondition(int iPageIndex, int iPageSize, ref int iTotalRecord, ConditionModel conditionItem)
        {
            var service = new DbHelperEfSql<V_ComputerRoom_Dept>();
            return service.SearchByPageCondition(iPageIndex, iPageSize, ref iTotalRecord, conditionItem);
        }


        /// <summary>
        /// 根据查询对象分页获取记录111
        /// </summary>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <param name="iTotalRecord"></param>
        /// <param name="conditionItem"></param>
        /// <returns></returns>
        public IList<td_ComputerRoom_1> GetPageListByCondition1(int iPageIndex, int iPageSize, ref int iTotalRecord, ConditionModel conditionItem)
        {
            var service = new DbHelperEfSql<td_ComputerRoom_1>();
            return service.SearchByPageCondition(iPageIndex, iPageSize, ref iTotalRecord, conditionItem);
        }
    }
}

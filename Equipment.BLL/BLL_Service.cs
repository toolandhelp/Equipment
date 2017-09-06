using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Equipment.DAL;
using Equipment.Model;

namespace Equipment.BLL
{
    public class BLL_Service
    {
        #region dbContext
        public DbHelperEfSql<td_Service_1> dbContext { get; set; }
        public BLL_Service()
        {
            dbContext = new DbHelperEfSql<td_Service_1>();
        }
        #endregion

        #region 增改删 CUD Entity

        public bool AddImport_1(List<td_Service_1> list)
        {
            var service = new SpecialService();
            return service.AddImportService_1(list);
        }
        public bool AddImport_2(List<td_Service_2> list)
        {
            var service = new SpecialService();
            return service.AddImportService_2(list);
        }
        public bool AddOrUpdate1(td_Service_1 entity)
        {
            if (entity.ID < 1)
            {
                dbContext.Add(entity);
            }
            return dbContext.Update(entity, c => c.ID == entity.ID);
        }

        public bool AddOrUpdate2(td_Service_2 entity)
        {
            var service = new DbHelperEfSql<td_Service_2>();
            if (entity.ID < 1)
            {
                service.Add(entity);
            }
            return service.Update(entity, c => c.ID == entity.ID);
        }


        public bool PhysicalDelete(int[] eIds,int iCode)
        {
            if (iCode == 0)
            {
                return dbContext.PhysicalDeleteByCondition(c => eIds.Contains(c.ID));
            }
            else
            {
                var service = new DbHelperEfSql<td_Service_2>();
                return service.PhysicalDeleteByCondition(c => eIds.Contains(c.ID));
            }
        }

   
        #endregion

        #region  查询 Search Entity
        /// <summary>
        /// 根据Id获取单条数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        //public td_Service_1 GetObjectById(int iId)
        //{
        //    return dbContext.SearchBySingle(c => c.ID == iId);
        //}

        /// <summary>
        /// 根据Id获取单条数据(物理)
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public td_Service_1 GetObjectSer1ById(int iId)
        {
            var service = new DbHelperEfSql<td_Service_1>();
            return service.SearchBySingle(c => c.ID == iId);
        }
        /// <summary>
        /// 根据Id获取单条数据(虚拟)
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public td_Service_2 GetObjectSer2ById(int iId)
        {
            var service = new DbHelperEfSql<td_Service_2>();
            return service.SearchBySingle(c => c.ID == iId);
        }

        /// <summary>
        /// 根据查询对象获取多条记录
        /// </summary>
        /// <param name="conditionItem"></param>
        /// <returns></returns>
        public IList<td_Service_1> GetListByCondition(ConditionModel conditionItem)
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
        public IList<td_Service_1> GetPageList(int iPageIndex, int iPageSize, ref int iTotalRecord)
        {
            return dbContext.SearchByPageCondition(true, c => c.ID, iPageIndex, iPageSize, ref iTotalRecord);
        }
        #endregion

        /// <summary>
        /// 根据查询对象分页获取记录(1=物理服务器)
        /// </summary>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <param name="iTotalRecord"></param>
        /// <param name="conditionItem"></param>
        /// <returns></returns>
        public IList<td_Service_1> GetPageList1ByCondition(int iPageIndex, int iPageSize, ref int iTotalRecord, ConditionModel conditionItem)
        {
            var service = new DbHelperEfSql<td_Service_1>();
            return service.SearchByPageCondition(iPageIndex, iPageSize, ref iTotalRecord, conditionItem);
        }

        /// <summary>
        /// 根据查询对象分页获取记录(1=虚拟服务器)
        /// </summary>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <param name="iTotalRecord"></param>
        /// <param name="conditionItem"></param>
        /// <returns></returns>
        public IList<td_Service_2> GetPageList2ByCondition(int iPageIndex, int iPageSize, ref int iTotalRecord, ConditionModel conditionItem)
        {
            var service = new DbHelperEfSql<td_Service_2>();
            return service.SearchByPageCondition(iPageIndex, iPageSize, ref iTotalRecord, conditionItem);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Equipment.DAL;
using Equipment.Model;

namespace Equipment.BLL
{
    public class BLL_Menu
    {
        #region dbContext
        public DbHelperEfSql<td_Menu> dbContext { get; set; }
        public BLL_Menu()
        {
            dbContext = new DbHelperEfSql<td_Menu>();
        }
        #endregion

        #region 增改删 CUD Entity

        public bool AddOrUpdate(td_Menu entity)
        {
            if (entity.menu_Id < 1)
            {
                dbContext.Add(entity);
            }
            return dbContext.Update(entity, c => c.menu_Id == entity.menu_Id);
        }
        public bool PhysicalDelete(int[] eIds)
        {
            return dbContext.PhysicalDeleteByCondition(c => eIds.Contains(c.menu_Id));
        }

        #endregion

        #region  查询 Search Entity
        /// <summary>
        /// 根据Id获取单条数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public td_Menu GetObjectById(int iId)
        {
            return dbContext.SearchBySingle(c => c.menu_Id == iId);
        }

        public IList<td_Menu> GetListAll()
        {
            return dbContext.SearchByAll();
        }
        #endregion
    }
}

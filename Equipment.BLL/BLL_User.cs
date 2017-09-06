using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Equipment.Model;
using Equipment.DAL;


namespace Equipment.BLL
{
    public class BLL_User
    {
        #region dbContext
        public DbHelperEfSql<td_User> dbContext { get; set; }
        public BLL_User()
        {
            dbContext = new DbHelperEfSql<td_User>();
        }
        #endregion


        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool AddAccount(td_User entity)
        {
            return dbContext.Add(entity);
        }

        /// <summary>
        /// 根据user_num获取单条数据
        /// </summary>
        /// <param name="user_num"></param>
        /// <returns></returns>
        public td_User GetObjectByUserID(int iUserID)
        {
            return dbContext.SearchBySingle(c => c.user_Id == iUserID);
        }

        /// <summary>
        /// 根据账号获取用户
        /// </summary>
        /// <param name="user_num"></param>
        /// <returns></returns>
        public td_User GetObjectByUserAccount(string sUserName)
        {
            return dbContext.SearchBySingle(c => c.user_Name == sUserName);
        }



        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public td_User GetObjectByUser(string userName, string pwd)
        {
            return dbContext.SearchBySingle(c => c.user_Name == userName && c.user_Pwd == pwd);
        }

        public IList<td_User> GetVListByAll()
        {
            var service = new DbHelperEfSql<td_User>();
            return service.SearchByAll();
        }

        public IList<td_User> GetViewListByAll()
        {
            var service = new DbHelperEfSql<td_User>();
            return service.SearchByAll();
        }

    }
}
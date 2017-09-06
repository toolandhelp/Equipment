using Equipment.BLL;
using Equipment.CommonLib;
using Equipment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Utits
{
    #region 导入Excel 存放
    public const string UploadExcelPath = "ImpExcel";
    #endregion
    #region 当前登录用户信息
    public static td_User CurrentUser
    {
        get
        {
            td_User item = null;
            try
            {
                //var cBll = new BLL_User();
                //item = cBll.GetObjectByUserID(139);
                if (HttpContext.Current.Session["EQUI_USERID"] != null)
                {
                    var usersBll = new BLL_User();
                    var temp = HashEncrypt.DecryptQueryString(HttpContext.Current.Session["EQUI_USERID"].ToString());
                    if (RegexValidate.IsInt(temp))
                    {
                        item = usersBll.GetObjectByUserID(int.Parse(temp));
                        return item;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageLog.AddLog("Utits.CurrentUser异常：" + ex.Message);
            }
            return item;
        }
    }
    #endregion

    /// <summary>
    /// 判断是否登录
    /// 1防止session名称修改和sessionState形式存储判别
    /// 2一项目多种身份登录形式
    /// </summary>
    public static bool IsLogin
    {
        get { return CurrentUser != null; }
    }
}
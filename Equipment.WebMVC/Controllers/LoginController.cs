using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Equipment.CommonLib;
using Equipment.Model;
using Equipment.BLL;

namespace Equipment.WebMVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Account()
        {
            return View();
        }

        public JsonResult LoginSystem()
        {
            #region 后台系统登录
            string UserName = RequestParameters.Pstring("UserName");
            string Password = RequestParameters.Pstring("Password");

            if (UserName.Length <= 0)
            {
                var sReturnModel = new ReturnMessageModel();
                sReturnModel.ErrorType = 2;
                sReturnModel.MessageContent = "用户名不能为空.";
                return Json(sReturnModel);
            }
            if (Password.Length <= 0)
            {
                var sReturnModel = new ReturnMessageModel();
                sReturnModel.ErrorType = 2;
                sReturnModel.MessageContent = "密码不能为空.";
                return Json(sReturnModel);
            }
          //  string retVal = "";
            #region
            try
            {
                var usersBll = new BLL_User();
                var item = usersBll.GetObjectByUser(UserName, HashEncrypt.BgPassWord(Password));
                if (item != null)
                {
                    if (item.user_Sign == (int)StageMode.Normal)
                    {
                        Session["EQUI_USERID"] = HashEncrypt.EncryptQueryString(item.user_Id.ToString());

                        var sReturnModel = new ReturnMessageModel();
                        sReturnModel.ErrorType = 1;
                        sReturnModel.MessageContent = "登录成功.";
                        return Json(sReturnModel);
                    }
                    else
                    {
                        var sReturnModel = new ReturnMessageModel();
                        sReturnModel.ErrorType = 2;
                        sReturnModel.MessageContent = "该账号已停用.";
                        return Json(sReturnModel);
                    }
                      //  retVal = "该账号已停用."; //登录成功
                }
                else
                {
                    var sReturnModel = new ReturnMessageModel();
                    sReturnModel.ErrorType = 2;
                    sReturnModel.MessageContent = "账号或密码错误.";
                    return Json(sReturnModel);
                }
            }
            catch (Exception ex)
            {
                var sReturnModel = new ReturnMessageModel();
                sReturnModel.ErrorType = 2;
                sReturnModel.MessageContent = "系统异常:" + ex.Message;
                return Json(sReturnModel);
              //  retVal = "1系统异常:" ;
            }
            #endregion

            //if (retVal == "1")
            //{
            //    var sReturnModel = new ReturnMessageModel();
            //    sReturnModel.ErrorType = 1;
            //    sReturnModel.MessageContent = "登录成功.";
            //    return Json(sReturnModel);
            //}
            //else
            //{
            //    var sReturnModel = new ReturnMessageModel();
            //    sReturnModel.ErrorType = 2;
            //    sReturnModel.MessageContent = retVal;
            //    return Json(sReturnModel);
            //}
            #endregion
        }

        public JsonResult LoginSystemAESCryp()
        {
            #region 后台系统登录
            string UserName = RequestParameters.Pstring("UserName");
            string Password = RequestParameters.Pstring("Password");

            if (UserName.Length <= 0)
            {
                var sReturnModel = new ReturnMessageModel();
                sReturnModel.ErrorType = 2;
                sReturnModel.MessageContent = "用户名不能为空.";
                return Json(sReturnModel);
            }
            if (Password.Length <= 0)
            {
                var sReturnModel = new ReturnMessageModel();
                sReturnModel.ErrorType = 2;
                sReturnModel.MessageContent = "密码不能为空.";
                return Json(sReturnModel);
            }
            //  string retVal = "";
            #region
            try
            {
                var usersBll = new BLL_User();
                var item = usersBll.GetObjectByUserAccount(UserName);//, HashEncrypt.BgPassWord(Password));
                if (item != null && new AESCrypt().Decrypt(item.user_Pwd) == Password)
                {
                    if (item.user_Sign == (int)StageMode.Normal)
                    {
                        Session["EQUI_USERID"] = HashEncrypt.EncryptQueryString(item.user_Id.ToString());

                        var sReturnModel = new ReturnMessageModel();
                        sReturnModel.ErrorType = 1;
                        sReturnModel.MessageContent = "登录成功.";
                        return Json(sReturnModel);
                    }
                    else
                    {
                        var sReturnModel = new ReturnMessageModel();
                        sReturnModel.ErrorType = 2;
                        sReturnModel.MessageContent = "该账号已停用.";
                        return Json(sReturnModel);
                    }
                    //  retVal = "该账号已停用."; //登录成功
                }
                else
                {
                    var sReturnModel = new ReturnMessageModel();
                    sReturnModel.ErrorType = 2;
                    sReturnModel.MessageContent = "账号或密码错误.";
                    return Json(sReturnModel);
                }
            }
            catch (Exception ex)
            {
                var sReturnModel = new ReturnMessageModel();
                sReturnModel.ErrorType = 2;
                sReturnModel.MessageContent = "系统异常:" + ex.Message;
                return Json(sReturnModel);
                //  retVal = "1系统异常:" ;
            }
            #endregion

            //if (retVal == "1")
            //{
            //    var sReturnModel = new ReturnMessageModel();
            //    sReturnModel.ErrorType = 1;
            //    sReturnModel.MessageContent = "登录成功.";
            //    return Json(sReturnModel);
            //}
            //else
            //{
            //    var sReturnModel = new ReturnMessageModel();
            //    sReturnModel.ErrorType = 2;
            //    sReturnModel.MessageContent = retVal;
            //    return Json(sReturnModel);
            //}
            #endregion
        }

        public JsonResult SystemAccount()
        {
            var sReturnModel = new ReturnMessageModel();

            string UserName = RequestParameters.Pstring("UserName");
            string Password = RequestParameters.Pstring("Password");
            string newPwd = new AESCrypt().Encrypt(Password);

            td_User userInfo = new td_User();

            //userInfo.user_Id = 141;
            userInfo.user_Name = UserName;
            userInfo.user_Pwd = newPwd;
            userInfo.user_Sign = 1;
            //userInfo.user_TrueName = "test";
            //userInfo.user_Purview = 1;
            //userInfo.user_Dept = "hlw";

            try
            {
                var usersBll = new BLL_User();
                var item = usersBll.GetObjectByUserAccount(UserName);//, HashEncrypt.BgPassWord(Password));
                if (item == null)
                {
                    if (usersBll.AddAccount(userInfo))
                    {
                        sReturnModel.ErrorType = 1;
                        sReturnModel.MessageContent = "注册成功.";
                    }
                    else
                    {
                        sReturnModel.ErrorType = 2;
                        sReturnModel.MessageContent = "注册失败.";
                    }
                    return Json(sReturnModel);
                }
                else
                {
                    sReturnModel.ErrorType = 2;
                    sReturnModel.MessageContent = "账号名称已存在.";
                    return Json(sReturnModel);
                }
            }
            catch (Exception ex)
            {
                sReturnModel.ErrorType = 2;
                sReturnModel.MessageContent = "系统异常:" + ex.Message;
                return Json(sReturnModel);
            }
        }

    }
}
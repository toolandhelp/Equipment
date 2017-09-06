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
    public class SecurityController : Controller
    {
        // GET: Security
        public ActionResult Index()
        {
            if (!Utits.IsLogin)
            {
                return RedirectPermanent("/Login/Index");
            }
            return View();
        }
        public ActionResult Index1()
        {
            if (!Utits.IsLogin)
            {
                return RedirectPermanent("/Login/Index");
            }
            return View();
        }

        public ActionResult Edit()
        {
            if (!Utits.IsLogin)
            {
                return RedirectPermanent("/Login/Index");
            }
            return View();
        }

        public JsonResult AddOrUpdate()
        {
            var sReturnModel = new ReturnMessageModel();
            #region 登录验证
            if (!Utits.IsLogin)
            {
                sReturnModel.ErrorType = 3;
                sReturnModel.MessageContent = "登录状态已失效.";
                return Json(sReturnModel);
            }
            #endregion
            int id = RequestParameters.Pint("ID");
            string DeptId = RequestParameters.Pstring("Dept_Id");
            string bswz = RequestParameters.Pstring("bswz");
            int fhq = RequestParameters.Pint("fhq");
            int rqjc = RequestParameters.Pint("rqjc");
            int VPNsb = RequestParameters.Pint("VPNsb");
            string VPNsbxh = RequestParameters.Pstring("VPNsbxh");
            string VPNsbpp = RequestParameters.Pstring("VPNsbpp");
            int zmaqfhsb = RequestParameters.Pint("zmaqfhsb");
            string zmaqfhsbxh = RequestParameters.Pstring("zmaqfhsbxh");
            string zmaqfhsbpp = RequestParameters.Pstring("zmaqfhsbpp");
            int mmj = RequestParameters.Pint("mmj");
            int jsmb = RequestParameters.Pint("jsmb");
            int yjsjsb = RequestParameters.Pint("yjsjsb");
            string yjsjsbxh = RequestParameters.Pstring("yjsjsbxh");
            string yjsjsbpp = RequestParameters.Pstring("yjsjsbpp");
            string sfrzwg = RequestParameters.Pstring("sfrzwg");
            int yyfzjh = RequestParameters.Pint("yyfzjh");
            int xhgrq = RequestParameters.Pint("xhgrq");
            int swxwgl = RequestParameters.Pint("wljk");
            int wljk = RequestParameters.Pint("wljk");
            int fwqjkpt = RequestParameters.Pint("fwqjkpt");
            int ywglpt = RequestParameters.Pint("ywglpt");
            int wyfcg = RequestParameters.Pint("wyfcg");
            int zdjkASM = RequestParameters.Pint("zdjkASM");

            if (bswz.Length < 1)
            {
                sReturnModel.ErrorType = 0;
                sReturnModel.MessageContent = "操作失败:部署位置不能为空.";
                return Json(sReturnModel);
            }

            //if (sfrzwg.GetType() != typeof(int)) {
            //    sReturnModel.ErrorType = 0;
            //    sReturnModel.MessageContent = "操作失败:身份认证网关为数量.";
            //    return Json(sReturnModel);
            //}

             var currentUser = Utits.CurrentUser;

            #region 检查参数(暂不做处理)
            //if (ECode.Length < 1)
            //{
            //    sReturnModel.ErrorType = 0;
            //    sReturnModel.MessageContent = "操作失败:员工号不能为空.";
            //    return Json(sReturnModel);
            //}
            //if (WCode.Length < 1)
            //{
            //    sReturnModel.ErrorType = 0;
            //    sReturnModel.MessageContent = "操作失败:考勤号不能为空.";
            //    return Json(sReturnModel);
            //}
            //if (Name.Length < 1)
            //{
            //    sReturnModel.ErrorType = 0;
            //    sReturnModel.MessageContent = "操作失败:姓名不能为空.";
            //    return Json(sReturnModel);
            //}
            //if (Dept.Length < 1)
            //{
            //    sReturnModel.ErrorType = 0;
            //    sReturnModel.MessageContent = "操作失败:部门不能为空.";
            //    return Json(sReturnModel);
            //}
            //if (Login.Length < 1)
            //{
            //    sReturnModel.ErrorType = 0;
            //    sReturnModel.MessageContent = "操作失败:登录名不能为空.";
            //    return Json(sReturnModel);
            //}
            //if (Role == Guid.Empty)
            //{
            //    sReturnModel.ErrorType = 0;
            //    sReturnModel.MessageContent = "操作失败:角色不能为空.";
            //    return Json(sReturnModel);
            //}
            //if (id < 1 && Password.Length < 1)
            //{
            //    sReturnModel.ErrorType = 0;
            //    sReturnModel.MessageContent = "操作失败:密码不能为空.";
            //    return Json(sReturnModel);
            //}
            #endregion

            var cBll = new BLL_Security();
            td_Security model;
            if (id > 0)
            {
                model = cBll.GetObjectById(id);
                if (model == null)
                {
                    sReturnModel.ErrorType = 0;
                    sReturnModel.MessageContent = "操作失败.";
                    return Json(sReturnModel);
                }
            }

            model = new td_Security();
            model.ID = id;
            model.OperatPid = currentUser.user_Id;
            model.OperatTime = DateTime.Now;

            model.Dept_Id = DeptId;
            model.bswz = bswz;
            model.fhq = fhq;
            model.rqjc = rqjc;
            model.VPNsb = VPNsb;
            model.VPNsbxh = VPNsbxh;
            model.VPNsbpp = VPNsbpp;
            model.zmaqfhsb = zmaqfhsb;
            model.zmaqfhsbxh = zmaqfhsbxh;
            model.zmaqfhsbpp = zmaqfhsbpp;
            model.mmj = mmj;
            model.jsmb = jsmb;
            model.yjsjsb = yjsjsb;
            model.yjsjsbxh = yjsjsbxh;
            model.yjsjsbpp = yjsjsbpp;
            model.sfrzwg =sfrzwg; 
            model.yyfzjh = yyfzjh;
            model.xhgrq = xhgrq;
            model.swxwgl = swxwgl;
            model.wljk = wljk;
            model.fwqjkpt = fwqjkpt;
            model.ywglpt = ywglpt;
            model.wyfcg = wyfcg;
            model.zdjkASM = zdjkASM;

            if (cBll.AddOrUpdate(model))
            {
                sReturnModel.ErrorType = 1;
                sReturnModel.MessageContent = "操作成功.";
            }
            else
            {
                sReturnModel.ErrorType = 0;
                sReturnModel.MessageContent = "操作失败.";
            }
            return Json(sReturnModel);
        }

        public JsonResult LoadPageList()
        {
            var sReturnModel = new ReturnListModel();
            #region 登录验证
            var currentUser = Utits.CurrentUser;
            if (currentUser == null)
            {
                sReturnModel.ErrorType = 3;
                sReturnModel.MessageContent = "登录状态已失效.";
                return Json(sReturnModel);
            }
            #endregion
            #region 参数处理
            #region 分页参数
            int page = RequestParameters.Pint("page");
            int size = RequestParameters.Pint("size");
            if (page < 1 || size < 1)
            {
                sReturnModel.ErrorType = 0;
                sReturnModel.MessageContent = "参数错误.";
                return Json(sReturnModel);
            }
            #endregion
            var condition = new ConditionModel();
            #region 查询参数
            var whereList = new List<WhereCondition>();
            //部门名称
            string Search = RequestParameters.Pstring("SearchDept");
            if (Search.Length > 0)
            {
                var where1Condition = new WhereCondition();
                where1Condition.FieldName = "Dept_Name";
                where1Condition.FieldOperator = EnumOper.Contains;
                where1Condition.FieldValue = Search;
                whereList.Add(where1Condition);
            }
            condition.WhereList = whereList;
            #endregion
            #region 排序参数
            var orderList = new List<OrderCondition>();
            var orderCondition = new OrderCondition();
            orderCondition.Ascending = true;
            orderCondition.FiledOrder = "OperatTime";
            orderList.Add(orderCondition);
            condition.OrderList = orderList;
            #endregion
            #endregion
            int iTotalRecord = 0, iPageIndex = page - 1, iPageSize = size;
            var cBll = new BLL_Security();
            var list = cBll.GetPageListByCondition(iPageIndex, iPageSize, ref iTotalRecord, condition);
            iPageSize = iPageSize == 0 ? iTotalRecord : iPageSize;
            int pageCount = iTotalRecord % iPageSize == 0 ? iTotalRecord / iPageSize : iTotalRecord / iPageSize + 1;

            sReturnModel.ErrorType = 1;
            sReturnModel.CurrentPage = page;
            sReturnModel.PageSize = iPageSize;
            sReturnModel.TotalRecord = iTotalRecord;
            sReturnModel.PageCount = pageCount;
            if (list != null)
                sReturnModel.Data = from a in list
                                    select new
                                    {
                                        a.ID,
                                        a.Dept_Name,
                                        a.bswz,
                                        a.fhq,
                                        a.rqjc,
                                        a.VPNsb,
                                        a.zmaqfhsb,
                                        a.mmj,
                                        a.jsmb,
                                        a.yjsjsb,
                                    };
            return Json(sReturnModel);
        }

        /// <summary>
        /// 1
        /// </summary>
        /// <returns></returns>
        public JsonResult LoadPageList1()
        {
            var sReturnModel = new ReturnListModel();
            #region 登录验证
            var currentUser = Utits.CurrentUser;
            if (currentUser == null)
            {
                sReturnModel.ErrorType = 3;
                sReturnModel.MessageContent = "登录状态已失效.";
                return Json(sReturnModel);
            }
            #endregion
            #region 参数处理
            #region 分页参数
            int page = RequestParameters.Pint("page");
            int size = RequestParameters.Pint("size");
            if (page < 1 || size < 1)
            {
                sReturnModel.ErrorType = 0;
                sReturnModel.MessageContent = "参数错误.";
                return Json(sReturnModel);
            }
            #endregion
            var condition = new ConditionModel();
            #region 查询参数
            var whereList = new List<WhereCondition>();
            //部门名称
            string Search = RequestParameters.Pstring("SearchDept");
            if (Search.Length > 0)
            {
                var where1Condition = new WhereCondition();
                where1Condition.FieldName = "Dept_Name";
                where1Condition.FieldOperator = EnumOper.Contains;
                where1Condition.FieldValue = Search;
                whereList.Add(where1Condition);
            }
            condition.WhereList = whereList;
            #endregion
            #region 排序参数
            var orderList = new List<OrderCondition>();
            var orderCondition = new OrderCondition();
            orderCondition.Ascending = true;
            orderCondition.FiledOrder = "OperatTime";
            orderList.Add(orderCondition);
            condition.OrderList = orderList;
            #endregion
            #endregion
            int iTotalRecord = 0, iPageIndex = page - 1, iPageSize = size;
            var cBll = new BLL_Security();
            var list = cBll.GetPageListByCondition1(iPageIndex, iPageSize, ref iTotalRecord, condition);
            iPageSize = iPageSize == 0 ? iTotalRecord : iPageSize;
            int pageCount = iTotalRecord % iPageSize == 0 ? iTotalRecord / iPageSize : iTotalRecord / iPageSize + 1;

            sReturnModel.ErrorType = 1;
            sReturnModel.CurrentPage = page;
            sReturnModel.PageSize = iPageSize;
            sReturnModel.TotalRecord = iTotalRecord;
            sReturnModel.PageCount = pageCount;
            if (list != null)
                sReturnModel.Data = from a in list
                                    select new
                                    {
                                        a.ID,
                                        a.Dept_Name,
                                        a.bswz,
                                        a.dyxt,
                                        a.sfjrzwww,
                                        a.sbglryxm,
                                        a.lxfs
                                    };
            return Json(sReturnModel);
        }
        public JsonResult InitSingle()
        {
            var sReturnModel = new ReturnDetailModel();
            #region 登录验证
            if (!Utits.IsLogin)
            {
                sReturnModel.ErrorType = 3;
                sReturnModel.MessageContent = "登录状态已失效.";
                return Json(sReturnModel);
            }
            #endregion
            int id = RequestParameters.Pint("ID");
            if (id < 1)
            {
                sReturnModel.ErrorType = 0;
                sReturnModel.MessageContent = "参数错误.";
                return Json(sReturnModel);
            }
            var cBll = new BLL_Security();
            sReturnModel.ErrorType = 1;
            sReturnModel.Entity = cBll.GetObjectDeptById(id);
            return Json(sReturnModel);
        }

        public JsonResult ListPhyDel()
        {
            var sReturnModel = new ReturnMessageModel();
            #region 登录验证
            if (!Utits.IsLogin)
            {
                sReturnModel.ErrorType = 3;
                sReturnModel.MessageContent = "登录状态已失效.";
                return Json(sReturnModel);
            }
            #endregion
            var ids = RequestParameters.Pstring("ids");
            if (ids.Length < 1)
            {
                sReturnModel.ErrorType = 0;
                sReturnModel.MessageContent = "参数错误.";
                return Json(sReturnModel);
            }
            var tempArry = ids.Split(',');
            List<int> delIds = new List<int>();
            foreach (var s in tempArry)
            {
                if (RegexValidate.IsInt(s))
                {
                    delIds.Add(int.Parse(s));
                }
            }
            if (!delIds.Any())
            {
                sReturnModel.ErrorType = 0;
                sReturnModel.MessageContent = "参数格式错误.";
                return Json(sReturnModel);
            }
            var cBll = new BLL_Security();

            if (cBll.PhysicalDelete(delIds.ToArray()))
            {
                sReturnModel.ErrorType = 1;
                sReturnModel.MessageContent = "操作成功.";
            }
            else
            {
                sReturnModel.ErrorType = 0;
                sReturnModel.MessageContent = "操作失败.";
            }
            return Json(sReturnModel);
        }

        /// <summary>
        /// 111
        /// </summary>
        /// <returns></returns>
        public JsonResult ListPhyDel1()
        {
            var sReturnModel = new ReturnMessageModel();
            #region 登录验证
            if (!Utits.IsLogin)
            {
                sReturnModel.ErrorType = 3;
                sReturnModel.MessageContent = "登录状态已失效.";
                return Json(sReturnModel);
            }
            #endregion
            var ids = RequestParameters.Pstring("ids");
            if (ids.Length < 1)
            {
                sReturnModel.ErrorType = 0;
                sReturnModel.MessageContent = "参数错误.";
                return Json(sReturnModel);
            }
            var tempArry = ids.Split(',');
            List<int> delIds = new List<int>();
            foreach (var s in tempArry)
            {
                if (RegexValidate.IsInt(s))
                {
                    delIds.Add(int.Parse(s));
                }
            }
            if (!delIds.Any())
            {
                sReturnModel.ErrorType = 0;
                sReturnModel.MessageContent = "参数格式错误.";
                return Json(sReturnModel);
            }
            var cBll = new BLL_Security();

            if (cBll.PhysicalDelete1(delIds.ToArray()))
            {
                sReturnModel.ErrorType = 1;
                sReturnModel.MessageContent = "操作成功.";
            }
            else
            {
                sReturnModel.ErrorType = 0;
                sReturnModel.MessageContent = "操作失败.";
            }
            return Json(sReturnModel);
        }

    }
}

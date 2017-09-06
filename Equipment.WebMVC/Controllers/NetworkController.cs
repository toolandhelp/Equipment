using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Equipment.BLL;
using Equipment.CommonLib;
using Equipment.Model;

namespace Equipment.WebMVC.Controllers
{
    public class NetworkController : Controller
    {
        // GET: Network
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
            var cBll = new BLL_Network();
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
                                        a.scjhj,
                                        a.ecjhj,
                                        a.lyq,
                                        a.gdj,
                                        a.llfzjh,
                                        a.gx,
                                        a.wxjrd,
                                        a.wxzj ,
                                        a.flwck
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
            var cBll = new BLL_Network();
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
                                        a.scjhj,
                                        a.ecjhj,
                                        a.lyq,
                                        a.gdj,
                                        a.llfzjh,
                                        a.gx,
                                        a.wxjrd,
                                        a.wxzj,
                                        a.flwck
                                    };
            return Json(sReturnModel);
        }
        /// <summary>
        /// 保存信息
        /// </summary>
        public JsonResult AddOrUpdate()
        {
            var sReturnModel = new ReturnMessageModel();
            //#region 登录验证
            //if (!Utits.IsLogin)
            //{
            //    sReturnModel.ErrorType = 3;
            //    sReturnModel.MessageContent = "登录状态已失效.";
            //    return Json(sReturnModel);
            //}
            //#endregion
            int id = RequestParameters.Pint("ID");
            string DeptId = RequestParameters.Pstring("Dept_Id");
            int scjhj = RequestParameters.Pint("scjhj");
            string scjhjxh = RequestParameters.Pstring("scjhjxh");
            string scjhjpp = RequestParameters.Pstring("scjhjpp");
            int ecjhj = RequestParameters.Pint("ecjhj");
            string ecjhjxh = RequestParameters.Pstring("ecjhjxh");
            string ecjhjpp = RequestParameters.Pstring("ecjhjpp");
            int lyq = RequestParameters.Pint("lyq");
            string lyqxh = RequestParameters.Pstring("lyqxh");
            string lyqpp = RequestParameters.Pstring("lyqpp");
            int gdj = RequestParameters.Pint("gdj");
            string gdjxh = RequestParameters.Pstring("gdjxh");
            string gdjpp = RequestParameters.Pstring("gdjpp");
            int llfzjh = RequestParameters.Pint("llfzjh");
            string llfzjhxh = RequestParameters.Pstring("llfzjhxh");
            string llfzjhpp = RequestParameters.Pstring("llfzjhpp");
            int wxzj = RequestParameters.Pint("wxzj");
            string wxzjxh = RequestParameters.Pstring("wxzjxh");
            string wxzjpp = RequestParameters.Pstring("wxzjpp");
            int gx = RequestParameters.Pint("gx");
            int wxjrd = RequestParameters.Pint("wxjrd");
            string flwck = RequestParameters.Pstring("flwck");
            string jrcwwwIPdzd = RequestParameters.Pstring("jrcwwwIPdzd");

            if (jrcwwwIPdzd.Length < 1)
            {
                sReturnModel.ErrorType = 0;
                sReturnModel.MessageContent = "操作失败:接入政务外网IP地址端不能为空.";
                return Json(sReturnModel);
            }

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

            var cBll = new BLL_Network();
            td_Network model;
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

            model = new td_Network();
            model.ID = id;
            model.OperatPid = currentUser.user_Id;
            model.OperatTime = DateTime.Now;

            model.Dept_Id = DeptId;
            model.scjhj = scjhj;
            model.scjhjxh = scjhjxh;
            model.scjhjpp = scjhjpp;
            model.ecjhj = ecjhj;
            model.ecjhjxh = ecjhjxh;
            model.ecjhjpp = ecjhjpp;
            model.lyq = lyq;
            model.lyqxh = lyqxh;
            model.lyqpp = lyqpp;
            model.gdj = gdj;
            model.gdjxh = gdjxh;
            model.gdjpp = gdjpp;
            model.llfzjh = llfzjh;
            model.llfzjhxh = llfzjhxh;
            model.llfzjhpp = llfzjhpp;
            model.wxzj = wxzj;
            model.wxzjxh = wxzjxh;
            model.wxzjpp = wxzjpp;
            model.gx = gx;
            model.wxjrd = wxjrd;
            model.flwck = flwck;
            model.jrcwwwIpdzd = jrcwwwIPdzd;

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
            var cBll = new BLL_Network();
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
            var cBll = new BLL_Network();
     
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
        /// 1
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
            var cBll = new BLL_Network();

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
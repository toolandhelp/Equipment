
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
    public class ConstructionProjectController : Controller
    {
        // GET: ConstructionProject
        public ActionResult Index()
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

        //添加或修改
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
            string xmys = RequestParameters.Pstring("xmys");
            string zjly = RequestParameters.Pstring("zjly");
            string xmfzr = RequestParameters.Pstring("xmfzr");
            string fzrdwdh = RequestParameters.Pstring("fzrdwdh");
            int fzrsj = RequestParameters.Pint("fzrsj");
            string xmlxr = RequestParameters.Pstring("xmlxr");
            string lxrdwdh = RequestParameters.Pstring("lxrdwdh");
            int lxrsj = RequestParameters.Pint("lxrsj");

            if (DeptId.Length < 1)
            {
                sReturnModel.ErrorType = 0;
                sReturnModel.MessageContent = "操作失败:资源提供部门不能为空.";
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

            var cBll = new BLL_ConstructionProject();
            td_ConstructionProject model;
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

            model = new td_ConstructionProject();
            model.ID = id;
            model.OperatPid = currentUser.user_Id;
            model.OperatTime = DateTime.Now;

            model.Dept_Id = DeptId;
            model.xmys = xmys;
            model.zjly = zjly;
            model.xmfzr = xmfzr;
            model.fzrdwdh = fzrdwdh;
            model.fzrsj = fzrsj;
            model.xmlxr =xmlxr;
            model.lxrsj = lxrsj;
            model.lxrdwdh = lxrdwdh;

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

        //加载数据集
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
            var cBll = new BLL_ConstructionProject();
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
                                        a.xmys,
                                        a.zjly,
                                        a.xmfzr,
                                        a.fzrdwdh,
                                        a.fzrsj,
                                        a.xmlxr,
                                        a.lxrdwdh,
                                        a.lxrsj
                                    };
            return Json(sReturnModel);
        }

        //单一信息
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
            var cBll = new BLL_ConstructionProject();
            sReturnModel.ErrorType = 1;
            sReturnModel.Entity = cBll.GetObjectDeptById(id);
            return Json(sReturnModel);
        }

        //删除
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
            var cBll = new BLL_ConstructionProject();

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

    }
}
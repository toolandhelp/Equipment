using Equipment.BLL;
using Equipment.CommonLib;
using Equipment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Equipment.WebMVC.Controllers
{
    public class ServiceController : Controller
    {
        // GET: Service
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 物理服务器
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit1()
        {
            return View();
        }

        /// <summary>
        /// 虚拟服务器
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit2()
        {
            return View();
        }

        /// <summary>
        /// 物理服务器
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadPageList1()
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
            //var whereList = new List<WhereCondition>();
            ////部门名称
            //string Search = RequestParameters.Pstring("SearchDept");
            //if (Search.Length > 0)
            //{
            //    var where1Condition = new WhereCondition();
            //    where1Condition.FieldName = "Dept_Name";
            //    where1Condition.FieldOperator = EnumOper.Contains;
            //    where1Condition.FieldValue = Search;
            //    whereList.Add(where1Condition);
            //}
            //condition.WhereList = whereList;
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
            var cBll = new BLL_Service();
            var list = cBll.GetPageList1ByCondition(iPageIndex, iPageSize, ref iTotalRecord, condition);
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
                                        a.fwqbh,
                                        a.xh,
                                        gmrq = a.gmrq.ToString("yyyy/MM/dd"),
                                        gbrq = a.gbrq.ToString("yyyy/MM/dd"),
                                        a.cpbb,
                                        a.xlh,
                                        a.wlwz,
                                        a.jgwz,
                                        a.zycd,
                                    };
            return Json(sReturnModel);
        }

        /// <summary>
        /// 虚拟服务器
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadPageList2()
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
            //var whereList = new List<WhereCondition>();
            ////部门名称
            //string Search = RequestParameters.Pstring("SearchDept");
            //if (Search.Length > 0)
            //{
            //    var where1Condition = new WhereCondition();
            //    where1Condition.FieldName = "Dept_Name";
            //    where1Condition.FieldOperator = EnumOper.Contains;
            //    where1Condition.FieldValue = Search;
            //    whereList.Add(where1Condition);
            //}
            //condition.WhereList = whereList;
            #endregion
            #region 排序参数
            var orderList = new List<OrderCondition>();
            var orderCondition = new OrderCondition();
            orderCondition.Ascending = true;
            orderCondition.FiledOrder = "ID";
            orderList.Add(orderCondition);
            condition.OrderList = orderList;
            #endregion
            #endregion
            int iTotalRecord = 0, iPageIndex = page - 1, iPageSize = size;
            var cBll = new BLL_Service();
            var list = cBll.GetPageList2ByCondition(iPageIndex, iPageSize, ref iTotalRecord, condition);
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
                                        a.xnjm,
                                        a.xtmc,
                                        tjsj = a.tjsj.ToString("yyyy/MM/dd"),
                                        a.CPU,
                                        a.nc,
                                        a.ypkj,
                                        a.czxt,
                                        a.IPdz,

                                    };
            return Json(sReturnModel);
        }

        /// <summary>
        /// 物理服务器
        /// </summary>
        public ActionResult ListPhyDel()
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
            var idcode = RequestParameters.Pint("Idcode");
            if (idcode != 1 && idcode != 0)
            {
                sReturnModel.ErrorType = 0;
                sReturnModel.MessageContent = "参数错误.";
                return Json(sReturnModel);
            }

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
            var cBll = new BLL_Service();

            if (cBll.PhysicalDelete(delIds.ToArray(), idcode))
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

        public ActionResult InitSingle()
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

            var idcode = RequestParameters.Pint("Code");
            if (idcode != 1 && idcode != 0)
            {
                sReturnModel.ErrorType = 0;
                sReturnModel.MessageContent = "参数错误.";
                return Json(sReturnModel);
            }
            int id = RequestParameters.Pint("ID");
            if (id < 1)
            {
                sReturnModel.ErrorType = 0;
                sReturnModel.MessageContent = "参数错误.";
                return Json(sReturnModel);
            }
            var cBll = new BLL_Service();
            sReturnModel.ErrorType = 1;
            if (idcode == 0)
            {
                sReturnModel.Entity = cBll.GetObjectSer1ById(id);
            }
            else
            {
                sReturnModel.Entity = cBll.GetObjectSer2ById(id);
            }

            return Json(sReturnModel);
        }


        public JsonResult AddOrUpdate1()
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
            //string DeptId = RequestParameters.Pstring("Dept_Id");
            //int jg = RequestParameters.Pint("jg");
            //int kt = RequestParameters.Pint("kt");
            //int sxt = RequestParameters.Pint("sxt");
            //int mhq = RequestParameters.Pint("mhq");
            //int UPS = RequestParameters.Pint("UPS");
            //int fhm = RequestParameters.Pint("fhm");
            //int fhdb = RequestParameters.Pint("fhdb");
            //int qtmhzz = RequestParameters.Pint("qtmhzz");
            //int fljsb = RequestParameters.Pint("fljsb");
            //int fjdsb = RequestParameters.Pint("fjdsb");
            //int fdcgr = RequestParameters.Pint("fdcgr");
            //int fcsb = RequestParameters.Pint("fcsb");
            //int fwq = RequestParameters.Pint("fwq");

            string fwqbh = RequestParameters.Pstring("fwqbh");
            string xh = RequestParameters.Pstring("xh");
            string gmrq = RequestParameters.Pstring("gmrq");
            string cpbh = RequestParameters.Pstring("cpbh");
            string xlh = RequestParameters.Pstring("xlh");
            string gbrq = RequestParameters.Pstring("gbrq");
            string wlwz = RequestParameters.Pstring("wlwz");
            string jgwz = RequestParameters.Pstring("jgwz");
            string gdzcbh = RequestParameters.Pstring("gdzcbh");
            string sjkxt = RequestParameters.Pstring("sjkxt");
            string IPdz = RequestParameters.Pstring("IPdz");
            string ym = RequestParameters.Pstring("ym");
            string wg = RequestParameters.Pstring("wg");
            string jqm = RequestParameters.Pstring("jqm");
            string zyywyy = RequestParameters.Pstring("zyywyy");
            string czxt = RequestParameters.Pstring("czxt");
            string CPU = RequestParameters.Pstring("CPU");
            string nc = RequestParameters.Pstring("nc");
            string yp = RequestParameters.Pstring("yp");
            string sjywsj = RequestParameters.Pstring("sjywsj");
            string sfrb = RequestParameters.Pstring("sfrb");
            string zycd = RequestParameters.Pstring("zycd");
            string glyhm = RequestParameters.Pstring("glyhm");
            string glmm = RequestParameters.Pstring("glmm");
            string gsyhm = RequestParameters.Pstring("gsyhm");
            string gsmm = RequestParameters.Pstring("gsmm");
            string xlhbbg = RequestParameters.Pstring("xlhbbg");
            string sfjh = RequestParameters.Pstring("sfjh");
            string jhm = RequestParameters.Pstring("jhm");
            string jhsl = RequestParameters.Pstring("jhsl");
            string sfzbrj = RequestParameters.Pstring("sfzbrj");

            //if (fwq < 1)
            //{
            //    sReturnModel.ErrorType = 0;
            //    sReturnModel.MessageContent = "操作失败:服务器数量不能小于1台.";
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

            var cBll = new BLL_Service();
            td_Service_1 model;
            if (id > 0)
            {
                model = cBll.GetObjectSer1ById(id);
                if (model == null)
                {
                    sReturnModel.ErrorType = 0;
                    sReturnModel.MessageContent = "操作失败.";
                    return Json(sReturnModel);
                }
            }

            model = new td_Service_1();
            model.ID = id;
            model.OperatPid = currentUser.user_Id;
            model.OperatTime = DateTime.Now;

            //model.Dept_Id = DeptId;

            model.fwqbh = fwqbh;
            model.xh = xh;
            model.gmrq = Convert.ToDateTime(gmrq);
            model.cpbb = cpbh;
            model.xlh = xlh;
            model.gbrq = Convert.ToDateTime(gbrq);
            model.wlwz = wlwz;
            model.jgwz = jgwz;
            model.gdzcbh = gdzcbh;
            model.sjkxt = sjkxt;
            model.IPdz = IPdz;
            model.ym = ym;
            model.wg = wg;
            model.jqm = jqm;
            model.zyywyy = zyywyy;
            model.czxt = czxt;
            model.CPU = CPU;
            model.nc = nc;
            model.yp = yp;
            model.ywsj = sjywsj;
            model.sfrb = sfrb;
            model.zycd = zycd;
            model.glyhm = glyhm;
            model.glmm = glyhm;
            model.yhm1 = gsyhm;
            model.mm1 = gsmm;
            model.xlhbbgl = xlhbbg;
            model.sfjh = sfjh;
            model.jhm = jhm;
            model.sykyxjhsl = Convert.ToInt32(jhsl);
            model.sfzb = sfzbrj;

            model.Dept_Name = "";


            if (cBll.AddOrUpdate1(model))
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
        public JsonResult AddOrUpdate2()
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

            string bm = RequestParameters.Pstring("bm");
            string ipdz = RequestParameters.Pstring("ipdz");
            string zjsj = RequestParameters.Pstring("zjsj");
            string xnjm = RequestParameters.Pstring("xnjm");
            string xtmc = RequestParameters.Pstring("xtmc");
            string bz = RequestParameters.Pstring("bz");
            string CPU = RequestParameters.Pstring("CPU");
            string nc = RequestParameters.Pstring("nc");
            string ypkj = RequestParameters.Pstring("ypkj");
            string czxt = RequestParameters.Pstring("czxt");
            string glyzh = RequestParameters.Pstring("glyzh");
            string glymm = RequestParameters.Pstring("glymm");
            string whryzh = RequestParameters.Pstring("whryzh");
            string whrymm = RequestParameters.Pstring("whrymm");
            string sjzh = RequestParameters.Pstring("sjzh");
            string sjmm = RequestParameters.Pstring("sjmm");


            //if (fwq < 1)
            //{
            //    sReturnModel.ErrorType = 0;
            //    sReturnModel.MessageContent = "操作失败:服务器数量不能小于1台.";
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

            var cBll = new BLL_Service();
            td_Service_2 model;
            if (id > 0)
            {
                model = cBll.GetObjectSer2ById(id);
                if (model == null)
                {
                    sReturnModel.ErrorType = 0;
                    sReturnModel.MessageContent = "操作失败.";
                    return Json(sReturnModel);
                }
            }

            model = new td_Service_2();
            model.ID = id;

            model.Dept_Name = bm;
            model.IPdz = ipdz;
            model.tjsj = Convert.ToDateTime(zjsj);
            model.xnjm = xnjm;
            model.xtmc = xtmc;
            model.bz = bz;
            model.CPU = CPU;
            model.nc = nc;
            model.ypkj = ypkj;
            model.czxt = czxt;
            model.glyhm = glyzh;
            model.glmm = glymm;
            model.whyhm1 = whryzh;
            model.whmm1 = whrymm;
            model.sjyhm = sjzh;
            model.sjmm = sjmm;

            if (cBll.AddOrUpdate2(model))
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
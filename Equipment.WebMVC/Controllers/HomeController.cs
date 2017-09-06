using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Equipment.BLL;
using Equipment.Model;
using System.Text;

namespace Equipment.WebMVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (!Utits.IsLogin)
            {
                return RedirectPermanent("/Login/Index");
            }

            StringBuilder sb = new StringBuilder();
            var currentUser = Utits.CurrentUser;
            ViewBag.LogPerson = currentUser.user_TrueName;

            var cBll = new BLL_Menu();
            var list = cBll.GetListAll().Where(o => o.menu_Stage != 0).ToList();
            GetLeftAuthMenu(ref sb, list);
            ViewBag.LeftAuthMenu = sb.ToString();
            return View();
        }

        public void GetLeftAuthMenu(ref StringBuilder sb,IList<td_Menu> menus)
        {
            if (menus.Count > 0)
            {
                for (int i = 0; i < menus.Count; i++)
                {
                    sb.Append("<a href =\"javascript:void(0);\" class=\"menu-link " + menus[i].menu_Image + "\" urldata=" + menus[i].menu_Link + ">" + menus[i].menu_Name + "</a>");
                }
            }
            else
            {
                sb.Append("0");
            }
        }

        public ActionResult Home()
        {
            //if (!Utits.IsLogin)
            //{
            //    return RedirectPermanent("/Login/Index");
            //}
            //var currentUser = Utits.CurrentUser;
            //ViewBag.LogPerson = currentUser.user_TrueName;
            ViewBag.LogPerson = "管理员";// currentUser.user_TrueName;

            var cNetBll = new BLL_Network();
            var cRoomBll = new BLL_ComputerRoom();
            var cTerBll = new BLL_Terminal();
            var cSecuBll = new BLL_Security();
            var cCproBll = new BLL_ConstructionProject();
            var cResBll = new BLL_ResourceCatalog();
            var cGSoft = new BLL_GenuineSoftware();


            var Netlist = cNetBll.GetObjectAll();
            var Roomlist = cNetBll.GetObjectAll();
            var Terlist = cTerBll.GetObjectAll();
            var Seculist = cSecuBll.GetObjectAll();
            var Cprolist = cCproBll.GetObjectAll();
            var Reslist = cResBll.GetObjectAll();
            var GSoftlist = cGSoft.GetObjectAll();

            ViewBag.jhj = Netlist.Sum(c => c.scjhj);
            ViewBag.gx = Netlist.Sum(c => c.gx);
            ViewBag.wxjrd = Netlist.Sum(c => c.wxjrd);

            ViewBag.jfsl = Roomlist.Count();

            ViewBag.zd = Terlist.Sum(c => c.zdsl);
            ViewBag.jrww = Terlist.Sum(c => c.jrzwwwsl);
            ViewBag.az360 = Terlist.Sum(c => c.az360tysl);

            ViewBag.fwq = Seculist.Sum(c=>c.fhq);
            ViewBag.VPN = Seculist.Sum(c=>c.VPNsb);

            ViewBag.xxm = Cprolist.Count();

            ViewBag.zyml = Reslist.Sum(c => c.zymlsl);
            ViewBag.ysj = Reslist.Sum(c => c.ysjzyl);
            ViewBag.gxzy = Reslist.Sum(c=>c.gxcs);
            ViewBag.fwzy = Reslist.Sum(c=>c.fwl);

            ViewBag.czxt = GSoftlist.Count();

            return View();
        }



        /// <summary>
        /// 左边菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult LeftAuthMenu()
        {
            if (Utits.IsLogin)
            {
                var cBll = new BLL_Menu();
                var list = cBll.GetListAll().Where(o => o.menu_Stage != 0);
                if (null != list)
                {
                    return Json(list.Select(itemNode => new TreeModel
                    {
                        Id = itemNode.menu_Code,
                        Pid = itemNode.menu_FatherId,
                        Title = itemNode.menu_Name,
                        Target = itemNode.menu_Target,
                        iconSkin = itemNode.menu_Image,
                        Url = itemNode.menu_Link
                    }));
                }
            }
            return Json("");
        }

        public ActionResult Home_Noajax()
        {
            return View();
        }


        /// <summary>
        /// 接入外网数量排行
        /// </summary>
        /// <returns></returns>
        public JsonResult JrwwsjphApi()
        {
            var sReturnModel = new ReturnDetailModel();

            var cBll = new BLL_Terminal();
            var list = cBll.GetObjectAll();
            list = list.OrderByDescending(o => o.jrzwwwsl).Take(10).ToList();

            sReturnModel.ErrorType = 1;
            if (list != null)
            {
                sReturnModel.Entity = from a in list
                                      select new
                                      {
                                          jrwwsjphAxisData = a.Dept_Name,
                                          jrwwsjphseriesData = a.jrzwwwsl
                                      };
            }
            return Json(sReturnModel);
        }

        /// <summary>
        /// 终端数量排行
        /// </summary>
        /// <returns></returns>
        public JsonResult ZdsjphApi()
        {
            var sReturnModel = new ReturnDetailModel();

            var cBll = new BLL_Terminal();
            var list = cBll.GetObjectAll();
            list = list.OrderByDescending(o => o.zdsl).Take(10).ToList();

            sReturnModel.ErrorType = 1;
            if (list != null)
            {
                sReturnModel.Entity = from a in list
                                      select new
                                      {
                                          zdsjphAxisData = a.Dept_Name,
                                          zdsjphseriesData = a.zdsl
                                      };
            }
            return Json(sReturnModel);
        }


        /// <summary>
        /// 360软件排行
        /// </summary>
        /// <returns></returns>
        public JsonResult Software360Api()
        {
            var sReturnModel = new ReturnDetailModel();

            var cBll = new BLL_Terminal();
            var list = cBll.GetObjectAll();
            list = list.OrderByDescending(o => o.az360tysl).Take(10).ToList();

            sReturnModel.ErrorType = 1;
            if (list != null)
            {
                sReturnModel.Entity = from a in list
                                      select new
                                      {
                                          name = a.Dept_Name,
                                          value = a.az360tysl
                                      };
            }
            return Json(sReturnModel);
        }


        public JsonResult ZYBMApi()
        {
            var sReturnModel = new ReturnDetailModel();

            var cBll = new BLL_ResourceCatalog();
            var list = cBll.GetObjectAll();
            list = list.OrderByDescending(o => o.ID).Distinct().Take(10).ToList();

            sReturnModel.ErrorType = 1;
            if (list != null)
            {
                sReturnModel.Entity = from a in list
                                      select new
                                      {
                                          name = a.Dept_Name,
                                          value = a.zymlsl
                                      };
            }
            return Json(sReturnModel);
        }

        #region 滚动
        /// <summary>
        /// 网络情况（光纤）
        /// </summary>
        /// <returns></returns>
        public JsonResult WlApi()
        {
            var sReturnModel = new ReturnDetailModel();

            var cBll = new BLL_Network();
            var list = cBll.GetObjectAll();
            list = list.OrderByDescending(o => o.gx).Take(5).ToList();

            sReturnModel.ErrorType = 1;
            if (list != null)
            {
                sReturnModel.Entity = from a in list
                                      select new
                                      {
                                          wlAxisData = a.Dept_Name,
                                          wlseriesData = a.gx
                                      };
            }
            return Json(sReturnModel);
        }

        /// <summary>
        /// 机房情况（部门/间）
        /// </summary>
        /// <returns></returns>
        public JsonResult JFApi()
        {
            var sReturnModel = new ReturnDetailModel();

            var cBll = new BLL_ComputerRoom();
            var list = cBll.GetObjectAll();
            list = list.OrderByDescending(o => o.ID).Distinct().ToList();

            sReturnModel.ErrorType = 1;
            if (list != null)
            {
                sReturnModel.Entity = from a in list
                                      select new
                                      {
                                          name = a.Dept_Name,
                                          value = a.fwq
                                      };
            }
            return Json(sReturnModel);

        }

        /// <summary>
        /// 终端情况
        /// </summary>
        /// <returns></returns>
        public JsonResult ZDApi()
        {
            var sReturnModel = new ReturnDetailModel();

            var cBll = new BLL_Terminal();
            var list = cBll.GetObjectAll();
            list = list.OrderByDescending(o => o.zdsl).Take(5).ToList();

            sReturnModel.ErrorType = 1;
            if (list != null)
            {
                sReturnModel.Entity = from a in list
                                      select new
                                      {
                                          name = a.Dept_Name,
                                          value = a.zdsl
                                      };
            }
            return Json(sReturnModel);
        }

        /// <summary>
        /// 安全设备
        /// </summary>
        /// <returns></returns>
        public JsonResult AQApi()
        {
            var sReturnModel = new ReturnDetailModel();

            var cBll = new BLL_Security();
            var list = cBll.GetObjectAll();
            list = list.OrderByDescending(o => o.ID).Distinct().ToList();

            sReturnModel.ErrorType = 1;
            if (list != null)
            {
                sReturnModel.Entity = from a in list
                                      select new
                                      {
                                          name = a.Dept_Name,
                                          value = a.yjsjsb
                                      };
            }
            return Json(sReturnModel);
        }

        /// <summary>
        /// 建设项目——项目预算
        /// </summary>
        /// <returns></returns>
        public JsonResult JSApi()
        {
            var sReturnModel = new ReturnDetailModel();

            var cBll = new BLL_ConstructionProject();
            var list = cBll.GetObjectAll();
            list = list.OrderByDescending(o => o.ID).Distinct().ToList();

            sReturnModel.ErrorType = 1;
            if (list != null)
            {
                sReturnModel.Entity = from a in list
                                      select new
                                      {
                                          name = a.Dept_Name,
                                          value = a.xmys
                                      };
            }
            return Json(sReturnModel);
        }

        /// <summary>
        /// 资源编码——资源编码数量
        /// </summary>
        /// <returns></returns>
        public JsonResult ZYApi()
        {
            var sReturnModel = new ReturnDetailModel();

            var cBll = new BLL_ResourceCatalog();
            var list = cBll.GetObjectAll();
            list = list.OrderByDescending(o => o.ID).Distinct().ToList();

            sReturnModel.ErrorType = 1;
            if (list != null)
            {
                sReturnModel.Entity = from a in list
                                      select new
                                      {
                                          name = a.Dept_Name,
                                          value = a.zymlsl
                                      };
            }
            return Json(sReturnModel);
        }

        /// <summary>
        /// 正版软件
        /// </summary>
        public JsonResult ZBApi()
        {
            var sReturnModel = new ReturnDetailModel();

            var cBll = new BLL_GenuineSoftware();
            var list = cBll.GetObjectAll();
            list = list.OrderByDescending(o => o.ID).Distinct().ToList();

            sReturnModel.ErrorType = 1;
            if (list != null)
            {
                sReturnModel.Entity = from a in list
                                      select new
                                      {
                                          name = a.Dept_Name,
                                          value = a.czxtlx.Count()
                                      };
            }
            return Json(sReturnModel);
        }

        #endregion
    }
}
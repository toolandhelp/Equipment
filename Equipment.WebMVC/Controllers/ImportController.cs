using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Equipment.Model;
using Equipment.BLL;
using Equipment.CommonLib;
using NPOI.XSSF.UserModel;


namespace Equipment.WebMVC.Controllers
{
    public class ImportController : Controller
    {
        // GET: Import
        //public ActionResult Index()
        //{
        //    return View();
        //}

        /// <summary>
        /// 网络情况
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public JsonResult ImportNetExcelFile(HttpPostedFileBase file)
        {
            // string filePath = RequestParameters.Pstring("filePath");
            if (!Utits.IsLogin)
            {
                return Json(new { jsonrpc = 2.0, error = new { code = 102, message = "登录状态已失效." } });
            }
            string filePathName = string.Empty;
            string localPath = Server.MapPath(string.Format("/{0}/NetWorkData/", Utits.UploadExcelPath));
            if (Request.Files.Count == 0)
            {
                return Json(new { jsonrpc = 2.0, error = new { code = 100, message = "文件上传失败." } });
            }

            //  string ex = Path.GetExtension(file.FileName);
            filePathName = DateTime.Now.ToString("yyyyMMddHHssmm") + "_" + file.FileName;
            if (!System.IO.Directory.Exists(localPath))
            {
                System.IO.Directory.CreateDirectory(localPath);
            }
            file.SaveAs(localPath + filePathName);

            #region 信息  
            try
            {
                var currentUser = Utits.CurrentUser;
                var OpUserId = currentUser.user_Id;
                var Opdate = System.DateTime.Now;

                var cBll = new BLL_Network();
                List<td_Network_1> list = new List<td_Network_1>();

                using (FileStream fsfile = new FileStream(localPath + filePathName, FileMode.Open, FileAccess.Read))
                {
                    XSSFWorkbook workbook = new XSSFWorkbook(fsfile);
                    NPOI.SS.UserModel.ISheet sheet = workbook.GetSheetAt(0); //第一个工作表
                    ///Excel 第一行是标题，不需要导入数据库的
                    for (int i = 1; i <= sheet.LastRowNum; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        td_Network_1 network = new td_Network_1();

                        if (row.GetCell(0) == null)
                        {
                            network.Dept_Name = "";
                        }
                        else
                        {
                            network.Dept_Name = row.GetCell(0).StringCellValue;
                        }

                        if (row.GetCell(1) == null)
                        {
                            network.bgdd = "";
                        }
                        else
                        {
                            network.bgdd = row.GetCell(1).StringCellValue;
                        }

                        if (row.GetCell(2) == null)
                        {
                            network.lxdh = "";
                        }
                        else
                        {
                            network.lxdh = row.GetCell(2).StringCellValue;
                        }

                        if (row.GetCell(3) == null)
                        {
                            network.jrzwwwzdsl = 0;
                        }
                        else
                        {
                            network.jrzwwwzdsl = Convert.ToInt32(row.GetCell(3).NumericCellValue);
                        }

                        if (row.GetCell(4) == null)
                        {
                            network.jrgwwzdsl = 0;
                        }
                        else
                        {
                            network.jrgwwzdsl = Convert.ToInt32(row.GetCell(4).NumericCellValue);
                        }

                        if (row.GetCell(5) == null)
                        {
                            network.jrzwwwIPdzd = "";
                        }
                        else
                        {
                            network.jrzwwwIPdzd = row.GetCell(5).StringCellValue;
                        }

                        if (row.GetCell(6) == null)
                        {
                            network.scjhj = 0;
                        }
                        else
                        {
                            network.scjhj = Convert.ToInt32(row.GetCell(6).NumericCellValue);
                        }

                        if (row.GetCell(7) == null)
                        {
                            network.ecjhj = 0;
                        }
                        else
                        {
                            network.ecjhj = Convert.ToInt32(row.GetCell(7).NumericCellValue);
                        }

                        if (row.GetCell(8) == null)
                        {
                            network.lyq = 0;
                        }
                        else
                        {
                            network.lyq = Convert.ToInt32(row.GetCell(8).NumericCellValue);
                        }

                        if (row.GetCell(9) == null)
                        {
                            network.gdj = 0;
                        }
                        else
                        {
                            network.gdj = Convert.ToInt32(row.GetCell(9).NumericCellValue);
                        }

                        if (row.GetCell(10) == null)
                        {
                            network.llfzjh = 0;
                        }
                        else
                        {
                            network.llfzjh = Convert.ToInt32(row.GetCell(10).NumericCellValue);
                        }

                        if (row.GetCell(11) == null)
                        {
                            network.gx = 0;
                        }
                        else
                        {
                            network.gx = Convert.ToInt32(row.GetCell(11).NumericCellValue);
                        }

                        if (row.GetCell(12) == null)
                        {
                            network.wxjrd = 0;
                        }
                        else
                        {
                            network.wxjrd = Convert.ToInt32(row.GetCell(12).NumericCellValue);
                        }

                        if (row.GetCell(13) == null)
                        {
                            network.wxzj = 0;
                        }
                        else
                        {
                            network.wxzj = Convert.ToInt32(row.GetCell(13).NumericCellValue);
                        }

                        if (row.GetCell(14) == null)
                        {
                            network.flwck = "";
                        }
                        else
                        {
                            network.flwck = row.GetCell(14).StringCellValue;
                        }
                        network.OperatTime = Opdate;
                        network.OperatPid = OpUserId;

                        list.Add(network);
                    }

                    if (cBll.AddImportNet(list))
                    {
                        return Json(new { jsonrpc = 2.0, message = "导入成功." });
                    }
                    else
                    {
                        return Json(new { jsonrpc = 2.0, error = new { code = 100, message = "导入失败." } });
                    }

                }
            }
            catch (Exception e)
            {
                MessageLog.AddLog(string.Format("UpDataProcess(数据导入)异常:{0}", e.Message));
            }
            return Json(new { jsonrpc = 2.0, error = new { code = 100, message = "系统错误.（请确保数据完整）" } });
            #endregion

        }


        /// <summary>
        /// 正版软件情况
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public JsonResult ImportSoftwareExcelFile(HttpPostedFileBase file)
        {
            // string filePath = RequestParameters.Pstring("filePath");
            if (!Utits.IsLogin)
            {
                return Json(new { jsonrpc = 2.0, error = new { code = 102, message = "登录状态已失效." } });
            }
            string filePathName = string.Empty;
            string localPath = Server.MapPath(string.Format("/{0}/SoftwareData/", Utits.UploadExcelPath));
            if (Request.Files.Count == 0)
            {
                return Json(new { jsonrpc = 2.0, error = new { code = 100, message = "文件上传失败." } });
            }

            //  string ex = Path.GetExtension(file.FileName);
            filePathName = DateTime.Now.ToString("yyyyMMddHHssmm") + "_" + file.FileName;
            if (!System.IO.Directory.Exists(localPath))
            {
                System.IO.Directory.CreateDirectory(localPath);
            }
            file.SaveAs(localPath + filePathName);

            #region 信息  
            try
            {
                var currentUser = Utits.CurrentUser;
                var OpUserId = currentUser.user_Id;
                var Opdate = System.DateTime.Now;

                var cBll = new BLL_GenuineSoftware();
                List<td_GenuineSoftware_1> list = new List<td_GenuineSoftware_1>();

                using (FileStream fsfile = new FileStream(localPath + filePathName, FileMode.Open, FileAccess.Read))
                {
                    XSSFWorkbook workbook = new XSSFWorkbook(fsfile);
                    NPOI.SS.UserModel.ISheet sheet = workbook.GetSheetAt(0); //第一个工作表
                    ///Excel 第一行是标题，不需要导入数据库的
                    for (int i = 1; i <= sheet.LastRowNum; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        td_GenuineSoftware_1 network = new td_GenuineSoftware_1();

                        if (row.GetCell(0) == null)
                        {
                            network.Dept_Name = "";
                        }
                        else
                        {
                            network.Dept_Name = row.GetCell(0).StringCellValue;
                        }

                        if (row.GetCell(1) == null)
                        {
                            network.tsj = 0;
                        }
                        else
                        {
                            network.tsj = Convert.ToInt32(row.GetCell(1).NumericCellValue);
                        }

                        if (row.GetCell(2) == null)
                        {
                            network.bjb = 0;
                        }
                        else
                        {
                            network.bjb = Convert.ToInt32(row.GetCell(2).NumericCellValue);
                        }

                        if (row.GetCell(3) == null)
                        {
                            network.bjrqzwww = 0;
                        }
                        else
                        {
                            network.bjrqzwww = Convert.ToInt32(row.GetCell(3).NumericCellValue);
                        }

                        if (row.GetCell(4) == null)
                        {
                            network.jrzwwwsl = 0;
                        }
                        else
                        {
                            network.jrzwwwsl = Convert.ToInt32(row.GetCell(4).NumericCellValue);
                        }

                        if (row.GetCell(5) == null)
                        {
                            network.az260tqsl = 0;
                        }
                        else
                        {
                            network.az260tqsl = Convert.ToInt32(row.GetCell(5).NumericCellValue);
                        }

                        if (row.GetCell(6) == null)
                        {
                            network.sfdbd = "";
                        }
                        else
                        {
                            network.sfdbd = row.GetCell(6).StringCellValue;
                        }

                        if (row.GetCell(7) == null)
                        {
                            network.czxtlx = "";
                        }
                        else
                        {
                            network.czxtlx = row.GetCell(7).StringCellValue;
                        }

                        if (row.GetCell(8) == null)
                        {
                            network.yzczxt = "";
                        }
                        else
                        {
                            network.yzczxt = row.GetCell(8).StringCellValue;
                        }

                        if (row.GetCell(9) == null)
                        {
                            network.yysqs1 = 0;
                        }
                        else
                        {
                            network.yysqs1 = Convert.ToInt32(row.GetCell(9).NumericCellValue);
                        }

                        if (row.GetCell(10) == null)
                        {
                            network.bgrjlx = "";
                        }
                        else
                        {
                            network.bgrjlx = row.GetCell(10).StringCellValue;
                        }

                        if (row.GetCell(11) == null)
                        {
                            network.sftytg1 = "";
                        }
                        else
                        {
                            network.sftytg1 = row.GetCell(11).StringCellValue;
                        }

                        if (row.GetCell(12) == null)
                        {
                            network.yysqs = 0;
                        }
                        else
                        {
                            network.yysqs = Convert.ToInt32(row.GetCell(12).NumericCellValue);
                        }

                        if (row.GetCell(13) == null)
                        {
                            network.sftytg = "";
                        }
                        else
                        {
                            network.sftytg = row.GetCell(13).StringCellValue;
                        }

                        if (row.GetCell(14) == null)
                        {
                            network.tgsl = 0;
                        }
                        else
                        {
                            network.tgsl = Convert.ToInt32(row.GetCell(14).NumericCellValue);
                        }
                        network.OperatTime = Opdate;
                        network.OperatPid = OpUserId;

                        list.Add(network);
                    }

                    if (cBll.AddImport(list))
                    {
                        return Json(new { jsonrpc = 2.0, message = "导入成功." });
                    }
                    else
                    {
                        return Json(new { jsonrpc = 2.0, error = new { code = 100, message = "导入失败." } });
                    }

                }
            }
            catch (Exception e)
            {
                MessageLog.AddLog(string.Format("UpDataProcess(数据导入)异常:{0}", e.Message));
            }
            return Json(new { jsonrpc = 2.0, error = new { code = 100, message = "系统错误.（请确保数据完整）" } });
            #endregion

        }

        /// <summary>
        /// 机房
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public JsonResult ImportRoomExcelFile(HttpPostedFileBase file)
        {
            // string filePath = RequestParameters.Pstring("filePath");
            if (!Utits.IsLogin)
            {
                return Json(new { jsonrpc = 2.0, error = new { code = 102, message = "登录状态已失效." } });
            }
            string filePathName = string.Empty;
            string localPath = Server.MapPath(string.Format("/{0}/RoomData/", Utits.UploadExcelPath));
            if (Request.Files.Count == 0)
            {
                return Json(new { jsonrpc = 2.0, error = new { code = 100, message = "文件上传失败." } });
            }

            //  string ex = Path.GetExtension(file.FileName);
            filePathName = DateTime.Now.ToString("yyyyMMddHHssmm") + "_" + file.FileName;
            if (!System.IO.Directory.Exists(localPath))
            {
                System.IO.Directory.CreateDirectory(localPath);
            }
            file.SaveAs(localPath + filePathName);

            #region 信息  
            try
            {
                var currentUser = Utits.CurrentUser;
                var OpUserId = currentUser.user_Id;
                var Opdate = System.DateTime.Now;

                var cBll = new BLL_ComputerRoom();
                List<td_ComputerRoom_1> list = new List<td_ComputerRoom_1>();
                List<td_ComputerRoom_sbsl_1> list_sbsl = new List<td_ComputerRoom_sbsl_1>();

                using (FileStream fsfile = new FileStream(localPath + filePathName, FileMode.Open, FileAccess.Read))
                {
                    XSSFWorkbook workbook = new XSSFWorkbook(fsfile);
                    NPOI.SS.UserModel.ISheet sheet = workbook.GetSheetAt(0); //第一个工作表
                    ISheet sheet_sbsl = workbook.GetSheetAt(1);
                    ///Excel 第一行是标题，不需要导入数据库的
                    for (int i = 1; i <= sheet.LastRowNum; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        td_ComputerRoom_1 Room = new td_ComputerRoom_1();

                        if (row.GetCell(0) == null)
                        {
                            Room.Dept_Name = "";
                        }
                        else
                        {
                            Room.Dept_Name = row.GetCell(0).StringCellValue;
                        }

                        if (row.GetCell(1) == null)
                        {
                            Room.jfwlwz = "";
                        }
                        else
                        {
                            Room.jfwlwz = row.GetCell(1).StringCellValue;
                        }

                        if (row.GetCell(2) == null)
                        {
                            Room.mj = 0;
                        }
                        else
                        {
                            Room.mj = Convert.ToInt32(row.GetCell(2).NumericCellValue);
                        }

                        if (row.GetCell(3) == null)
                        {
                            Room.jflx = "";
                        }
                        else
                        {
                            Room.jflx = row.GetCell(3).StringCellValue;
                        }

                        if (row.GetCell(4) == null)
                        {
                            Room.lsgx = "";
                        }
                        else
                        {
                            Room.lsgx= row.GetCell(4).StringCellValue;
                        }

                        if (row.GetCell(5) == null)
                        {
                            Room.sfjrczww = "";
                        }
                        else
                        {
                            Room.sfjrczww = row.GetCell(5).StringCellValue;
                        }

                        if (row.GetCell(6) == null)
                        {
                            Room.jfglyxm = "";
                        }
                        else
                        {
                            Room.jfglyxm = row.GetCell(6).StringCellValue;
                        }

                        if (row.GetCell(7) == null)
                        {
                            Room.lxfs= "";
                        }
                        else
                        {
                            Room.lxfs = row.GetCell(7).NumericCellValue.ToString();
                        }

                        Room.OperatTime = Opdate;
                        Room.OperatPid = OpUserId;

                        list.Add(Room);
                    }

                    for (int i = 1; i <= sheet_sbsl.LastRowNum; i++)
                    {
                        IRow row = sheet_sbsl.GetRow(i);
                        td_ComputerRoom_sbsl_1 Room_sl = new td_ComputerRoom_sbsl_1();

                        if (row.GetCell(0) == null)
                        {
                            Room_sl.Dept_Name = "";
                        }
                        else
                        {
                            Room_sl.Dept_Name = row.GetCell(0).StringCellValue;
                        }


                        if (row.GetCell(1) == null)
                        {
                            Room_sl.sbmc = "";
                        }
                        else
                        {
                            Room_sl.sbmc = row.GetCell(1).StringCellValue;
                        }

                        if (row.GetCell(2) == null)
                        {
                            Room_sl.pp = "";
                        }
                        else
                        {
                            Room_sl.pp = row.GetCell(2).StringCellValue;
                        }

                        if (row.GetCell(2) == null)
                        {
                            Room_sl.pp = "";
                        }
                        else
                        {
                            Room_sl.pp = row.GetCell(2).StringCellValue;
                        }

                        if (row.GetCell(3) == null)
                        {
                            Room_sl.xh = "";
                        }
                        else
                        {
                            Room_sl.xh = row.GetCell(3).StringCellValue;
                        }

                        if (row.GetCell(4) == null)
                        {
                            Room_sl.sl = 0;
                        }
                        else
                        {
                            Room_sl.sl = Convert.ToInt32(row.GetCell(4).NumericCellValue);
                        }

                        //Room_sl.OperatTime = Opdate;
                        //Room_sl.OperatPid = OpUserId;

                        list_sbsl.Add(Room_sl);
                    }

                    if (cBll.AddImport(list)&&cBll.AddImport_sl(list_sbsl))
                    {
                        return Json(new { jsonrpc = 2.0, message = "导入成功." });
                    }
                    else
                    {
                        return Json(new { jsonrpc = 2.0, error = new { code = 100, message = "导入失败." } });
                    }

                }
            }
            catch (Exception e)
            {
                MessageLog.AddLog(string.Format("UpDataProcess(数据导入)异常:{0}", e.Message));
            }
            return Json(new { jsonrpc = 2.0, error = new { code = 100, message = "系统错误.（请确保数据完整）" } });
            #endregion

        }

        /// <summary>
        /// 安全设备
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public JsonResult ImportSecurityExcelFile(HttpPostedFileBase file)
        {
            // string filePath = RequestParameters.Pstring("filePath");
            if (!Utits.IsLogin)
            {
                return Json(new { jsonrpc = 2.0, error = new { code = 102, message = "登录状态已失效." } });
            }
            string filePathName = string.Empty;
            string localPath = Server.MapPath(string.Format("/{0}/SecurityData/", Utits.UploadExcelPath));
            if (Request.Files.Count == 0)
            {
                return Json(new { jsonrpc = 2.0, error = new { code = 100, message = "文件上传失败." } });
            }

            //  string ex = Path.GetExtension(file.FileName);
            filePathName = DateTime.Now.ToString("yyyyMMddHHssmm") + "_" + file.FileName;
            if (!System.IO.Directory.Exists(localPath))
            {
                System.IO.Directory.CreateDirectory(localPath);
            }
            file.SaveAs(localPath + filePathName);

            #region 信息  
            try
            {
                var currentUser = Utits.CurrentUser;
                var OpUserId = currentUser.user_Id;
                var Opdate = System.DateTime.Now;
                var mdate = DateTime.Parse("1999/09/09");

                var cBll = new BLL_Security();
                List<td_Security_1> list = new List<td_Security_1>();
                List<td_Security_xxsl_1> list_xxsl = new List<td_Security_xxsl_1>();

                using (FileStream fsfile = new FileStream(localPath + filePathName, FileMode.Open, FileAccess.Read))
                {
                    XSSFWorkbook workbook = new XSSFWorkbook(fsfile);
                   // HSSFWorkbook Hworkbook = new HSSFWorkbook(fsfile);
                    NPOI.SS.UserModel.ISheet sheet = workbook.GetSheetAt(0); //第一个工作表
                    //NPOI.SS.UserModel.ISheet sheet = workbook.GetSheetAt(1); //第二个工作表
                    ISheet sheet_xxsl = workbook.GetSheet("设备信息");
                   // string nam = workbook.GetSheetName(1);
                    ///Excel 第一行是标题，不需要导入数据库的
                    for (int i = 1; i <= sheet.LastRowNum; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        td_Security_1 security = new td_Security_1();

                        if (row.GetCell(0) == null)
                        {
                            security.Dept_Name = "";
                        }
                        else
                        {
                            security.Dept_Name = row.GetCell(0).StringCellValue;
                        }

                        if (row.GetCell(1) == null)
                        {
                            security.bswz = "";
                        }
                        else
                        {
                            security.bswz = row.GetCell(1).StringCellValue;
                        }

                        if (row.GetCell(2) == null)
                        {
                            security.dyxt = "";
                        }
                        else
                        {
                            security.dyxt = row.GetCell(2).StringCellValue;
                        }

                        if (row.GetCell(3) == null)
                        {
                            security.sfjrzwww = "";
                        }
                        else
                        {
                            security.sfjrzwww = row.GetCell(3).StringCellValue;
                        }

                        if (row.GetCell(4) == null)
                        {
                            security.sbglryxm = "";
                        }
                        else
                        {
                            security.sbglryxm = row.GetCell(4).StringCellValue;
                        }

                        if (row.GetCell(5) == null)
                        {
                            security.lxfs = 0;
                        }
                        else
                        {
                            security.lxfs = Convert.ToInt32(row.GetCell(5).NumericCellValue);
                        }

                       
                        security.OperatTime = Opdate;
                        security.OperatPid = OpUserId;

                        list.Add(security);
                    }

                    for (int i = 1; i <= sheet_xxsl.LastRowNum; i++)
                    {
                        IRow row = sheet_xxsl.GetRow(i);
                        td_Security_xxsl_1 security_sl = new td_Security_xxsl_1();

                        if (row.GetCell(0) == null)
                        {
                            security_sl.Dept_Name = "";
                        }
                        else
                        {
                            security_sl.Dept_Name = row.GetCell(0).StringCellValue;
                        }


                        if (row.GetCell(1) == null)
                        {
                            security_sl.aqsbhrjmc = "";
                        }
                        else
                        {
                            security_sl.aqsbhrjmc = row.GetCell(1).StringCellValue;
                        }

                        if (row.GetCell(2) == null)
                        {
                            security_sl.sfbs = "";
                        }
                        else
                        {
                            security_sl.sfbs = row.GetCell(2).StringCellValue;
                        }

                        if (row.GetCell(3) == null)
                        {
                            security_sl.sl = 0;
                        }
                        else
                        {
                            security_sl.sl = Convert.ToInt32(row.GetCell(3).NumericCellValue);
                        }

                        if (row.GetCell(4) == null)
                        {
                            security_sl.pp = "";
                        }
                        else
                        {
                            security_sl.pp = row.GetCell(4).StringCellValue;
                        }

                        if (row.GetCell(5) == null)
                        {
                            security_sl.xh ="";
                        }
                        else
                        {
                            security_sl.xh = row.GetCell(5).StringCellValue;
                        }
                        if (row.GetCell(6) == null)
                        {
                            security_sl.gmrq = mdate;
                        }
                        else
                        {
                            security_sl.gmrq = row.GetCell(6).DateCellValue;
                        }
                        if (row.GetCell(7) == null)
                        {
                            security_sl.gmjq =0;
                        }
                        else
                        {
                            security_sl.gmjq = Convert.ToInt32(row.GetCell(7).NumericCellValue);
                        }

                        //Room_sl.OperatTime = Opdate;
                        //Room_sl.OperatPid = OpUserId;

                        list_xxsl.Add(security_sl);
                    }

                    if (cBll.AddImport(list) && cBll.AddImport_sl(list_xxsl))
                    {
                        return Json(new { jsonrpc = 2.0, message = "导入成功." });
                    }
                    else
                    {
                        return Json(new { jsonrpc = 2.0, error = new { code = 100, message = "导入失败." } });
                    }

                }
            }
            catch (Exception e)
            {
                MessageLog.AddLog(string.Format("UpDataProcess(数据导入)异常:{0}", e.Message));
            }
            return Json(new { jsonrpc = 2.0, error = new { code = 100, message = "系统错误.（请确保数据完整）" } });
            #endregion

        }


        /// <summary>
        /// 服务器
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public JsonResult ImportServiceExcelFile(HttpPostedFileBase file)
        {
            // string filePath = RequestParameters.Pstring("filePath");
            if (!Utits.IsLogin)
            {
                return Json(new { jsonrpc = 2.0, error = new { code = 102, message = "登录状态已失效." } });
            }
            string filePathName = string.Empty;
            string localPath = Server.MapPath(string.Format("/{0}/ServiceData/", Utits.UploadExcelPath));
            if (Request.Files.Count == 0)
            {
                return Json(new { jsonrpc = 2.0, error = new { code = 100, message = "文件上传失败." } });
            }

            //  string ex = Path.GetExtension(file.FileName);
            filePathName = DateTime.Now.ToString("yyyyMMddHHssmm") + "_" + file.FileName;
            if (!System.IO.Directory.Exists(localPath))
            {
                System.IO.Directory.CreateDirectory(localPath);
            }
            file.SaveAs(localPath + filePathName);

            #region 信息  
            try
            {
                var currentUser = Utits.CurrentUser;
                var OpUserId = currentUser.user_Id;
                var Opdate = System.DateTime.Now;
                var mdate = DateTime.Parse("1999/09/09");

                var cBll = new BLL_Service();
                List<td_Service_1> list = new List<td_Service_1>();
                List<td_Service_2> list_2 = new List<td_Service_2>();

                using (FileStream fsfile = new FileStream(localPath + filePathName, FileMode.Open, FileAccess.Read))
                {
                    XSSFWorkbook workbook = new XSSFWorkbook(fsfile);
                    ISheet sheet_1 = workbook.GetSheetAt(0); //第一个工作表
                    ISheet sheet_2 = workbook.GetSheetAt(1);
                    ///Excel 第一行是标题，不需要导入数据库的
                    for (int i = 1; i <= sheet_1.LastRowNum; i++)
                    {
                        IRow row = sheet_1.GetRow(i);
                        td_Service_1 service1 = new td_Service_1();

                        //第一列不管

                        if (row.GetCell(1) == null)
                        {
                            service1.fwqbh = "";
                        }
                        else
                        {
                            service1.fwqbh = row.GetCell(1).StringCellValue;
                        }

                        if (row.GetCell(2) == null)
                        {
                            service1.xh = "";
                        }
                        else
                        {
                            service1.xh = row.GetCell(2).StringCellValue;
                        }

                        if (row.GetCell(3) == null)
                        {
                            service1.gmrq = mdate;
                        }
                        else
                        {
                            service1.gmrq = row.GetCell(3).DateCellValue;
                        }

                        if (row.GetCell(4) == null)
                        {
                            service1.gbrq = mdate;
                        }
                        else
                        {
                            service1.gbrq = row.GetCell(4).DateCellValue;
                        }

                        if (row.GetCell(5) == null)
                        {
                            service1.cpbb = "";
                        }
                        else
                        {
                            service1.cpbb = row.GetCell(5).StringCellValue;
                        }

                        if (row.GetCell(6) == null)
                        {
                            service1.xlh = "";
                        }
                        else
                        {
                            service1.xlh = row.GetCell(6).NumericCellValue.ToString();
                        }

                        if (row.GetCell(7) == null)
                        {
                            service1.gdzcbh = "";
                        }
                        else
                        {
                            service1.gdzcbh = row.GetCell(7).StringCellValue;
                        }

                        if (row.GetCell(8) == null)
                        {
                            service1.wlwz = "";
                        }
                        else
                        {
                            service1.wlwz = row.GetCell(8).StringCellValue;
                        }

                        if (row.GetCell(9) == null)
                        {
                            service1.jgwz = "";
                        }
                        else
                        {
                            service1.jgwz = row.GetCell(9).StringCellValue;
                        }

                        if (row.GetCell(10) == null)
                        {
                            service1.IPdz = "";
                        }
                        else
                        {
                            service1.IPdz = row.GetCell(10).StringCellValue;
                        }

                        if (row.GetCell(11) == null)
                        {
                            service1.ym = "";
                        }
                        else
                        {
                            service1.ym = row.GetCell(11).StringCellValue;
                        }
                        if (row.GetCell(12) == null)
                        {
                            service1.wg = "";
                        }
                        else
                        {
                            service1.wg = row.GetCell(12).StringCellValue;
                        }
                        if (row.GetCell(13) == null)
                        {
                            service1.jqm = "";
                        }
                        else
                        {
                            service1.jqm = row.GetCell(13).StringCellValue;
                        }

                        if (row.GetCell(14) == null)
                        {
                            service1.zyywyy = "";
                        }
                        else
                        {
                            service1.zyywyy = row.GetCell(14).StringCellValue;
                        }

                        if (row.GetCell(15) == null)
                        {
                            service1.czxt = "";
                        }
                        else
                        {
                            service1.czxt = row.GetCell(15).StringCellValue;
                        }

                        if (row.GetCell(16) == null)
                        {
                            service1.sjkxt = "";
                        }
                        else
                        {
                            service1.sjkxt = row.GetCell(16).StringCellValue;
                        }

                        if (row.GetCell(17) == null)
                        {
                            service1.CPU = "";
                        }
                        else
                        {
                            service1.CPU = row.GetCell(17).StringCellValue;
                        }

                        if (row.GetCell(18) == null)
                        {
                            service1.nc = "";
                        }
                        else
                        {
                            service1.nc = row.GetCell(18).StringCellValue;
                        }

                        if (row.GetCell(19) == null)
                        {
                            service1.yp = "";
                        }
                        else
                        {
                            service1.yp = row.GetCell(19).StringCellValue;
                        }
                        if (row.GetCell(20) == null)
                        {
                            service1.ywsj = "";
                        }
                        else
                        {
                            service1.ywsj = row.GetCell(20).StringCellValue;
                        }
                        if (row.GetCell(21) == null)
                        {
                            service1.sfrb = "";
                        }
                        else
                        {
                            service1.sfrb = row.GetCell(21).StringCellValue;
                        }
                        if (row.GetCell(22) == null)
                        {
                            service1.zycd = "";
                        }
                        else
                        {
                            service1.zycd = row.GetCell(22).StringCellValue;
                        }
                        if (row.GetCell(23) == null)
                        {
                            service1.glyhm = "";
                        }
                        else
                        {
                            service1.glyhm = row.GetCell(23).StringCellValue;
                        }
                        if (row.GetCell(24) == null)
                        {
                            service1.glmm = "";
                        }
                        else
                        {
                            service1.glmm = row.GetCell(24).StringCellValue;
                        }

                        if (row.GetCell(25) == null)
                        {
                            service1.yhm1 = "";
                        }
                        else
                        {
                            service1.yhm1 = row.GetCell(25).StringCellValue;
                        }
                        if (row.GetCell(26) == null)
                        {
                            service1.mm1 = "";
                        }
                        else
                        {
                            service1.mm1 = row.GetCell(26).StringCellValue;
                        }
                        if (row.GetCell(27) == null)
                        {
                            service1.xlhbbgl = "";
                        }
                        else
                        {
                            service1.xlhbbgl = row.GetCell(27).StringCellValue;
                        }
                        if (row.GetCell(28) == null)
                        {
                            service1.sfjh = "";
                        }
                        else
                        {
                            service1.sfjh = row.GetCell(28).StringCellValue;
                        }
                        if (row.GetCell(29) == null)
                        {
                            service1.jhm = "";
                        }
                        else
                        {
                            service1.jhm = row.GetCell(29).StringCellValue;
                        }
                        if (row.GetCell(30) == null)
                        {
                            service1.sykyxjhsl = 0;
                        }
                        else
                        {
                            service1.sykyxjhsl = Convert.ToInt32(row.GetCell(30).NumericCellValue);
                        }
                        if (row.GetCell(31) == null)
                        {
                            service1.sfzb = "";
                        }
                        else
                        {
                            service1.sfzb = row.GetCell(31).StringCellValue;
                        }

                        service1.Dept_Name = "";

                        service1.OperatTime = Opdate;
                        service1.OperatPid = OpUserId;

                        list.Add(service1);
                    }

                    for (int i = 1; i <= sheet_2.LastRowNum; i++)
                    {
                        IRow row = sheet_2.GetRow(i);
                        td_Service_2 service2 = new td_Service_2();

                       


                        if (row.GetCell(1) == null)
                        {
                            service2.IPdz = "";
                        }
                        else
                        {
                            service2.IPdz = row.GetCell(1).StringCellValue;
                        }

                        if (row.GetCell(2) == null || row.GetCell(2).ToString() == "")
                        {
                            service2.tjsj = mdate;
                        }
                        else
                        {
                            service2.tjsj = row.GetCell(2).DateCellValue;
                        }

                        if (row.GetCell(3) == null)
                        {
                            service2.xnjm = "";
                        }
                        else
                        {
                            service2.xnjm = row.GetCell(3).StringCellValue;
                        }

                        if (row.GetCell(4) == null)
                        {
                            service2.Dept_Name = "";
                        }
                        else
                        {
                            service2.Dept_Name = row.GetCell(4).StringCellValue;
                        }

                        if (row.GetCell(5) == null)
                        {
                            service2.xtmc = "";
                        }
                        else
                        {
                            service2.xtmc = row.GetCell(5).StringCellValue;
                        }
                        if (row.GetCell(6) == null)
                        {
                            service2.bz = "";
                        }
                        else
                        {
                            service2.bz = row.GetCell(6).StringCellValue;
                        }
                        if (row.GetCell(7) == null)
                        {
                            service2.CPU ="";
                        }
                        else
                        {
                            service2.CPU =row.GetCell(7).StringCellValue;
                        }
                        if (row.GetCell(8) == null)
                        {
                            service2.nc = "";
                        }
                        else
                        {
                            service2.nc = row.GetCell(8).StringCellValue;
                        }
                        if (row.GetCell(9) == null)
                        {
                            service2.ypkj = "";
                        }
                        else
                        {
                            service2.ypkj = row.GetCell(9).StringCellValue;
                        }
                        if (row.GetCell(10) == null)
                        {
                            service2.czxt = "";
                        }
                        else
                        {
                            service2.czxt = row.GetCell(10).StringCellValue;
                        }
                        if (row.GetCell(11) == null)
                        {
                            service2.glyhm = "";
                        }
                        else
                        {
                            service2.glyhm = row.GetCell(11).StringCellValue;
                        }
                        if (row.GetCell(12) == null)
                        {
                            service2.glmm = "";
                        }
                        else
                        {
                            service2.glmm = row.GetCell(12).StringCellValue;
                        }

                        if (row.GetCell(13) == null)
                        {
                            service2.whyhm1 = "";
                        }
                        else
                        {
                            service2.whyhm1 = row.GetCell(13).StringCellValue;
                        }
                        if (row.GetCell(14) == null)
                        {
                            service2.whmm1 = "";
                        }
                        else
                        {
                            service2.whmm1 = row.GetCell(14).StringCellValue;
                        }
                        if (row.GetCell(15) == null)
                        {
                            service2.sjyhm = "";
                        }
                        else
                        {
                            service2.sjyhm = row.GetCell(15).StringCellValue;
                        }
                        if (row.GetCell(16) == null)
                        {
                            service2.sjmm = "";
                        }
                        else
                        {
                            service2.sjmm = row.GetCell(16).StringCellValue;
                        }
                     
                        //Room_sl.OperatTime = Opdate;
                        //Room_sl.OperatPid = OpUserId;

                        list_2.Add(service2);
                    }

                    if (cBll.AddImport_1(list) && cBll.AddImport_2(list_2))
                    {
                        return Json(new { jsonrpc = 2.0, message = "导入成功." });
                    }
                    else
                    {
                        return Json(new { jsonrpc = 2.0, error = new { code = 100, message = "导入失败." } });
                    }

                }
            }
            catch (Exception e)
            {
                MessageLog.AddLog(string.Format("UpDataProcess(数据导入)异常:{0}", e.Message));
            }
            return Json(new { jsonrpc = 2.0, error = new { code = 100, message = "系统错误.（请确保数据完整）" } });
            #endregion

        }

    }
}
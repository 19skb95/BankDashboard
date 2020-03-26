using BankDashboard.Common;
using BankDashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static BankDashboard.Common.ViewModelClass;

namespace BankDashboard.Controllers
{
    public class DashboardController : Controller
    {

        #region-------------------  Dashboard--------------------------------------------
        public ActionResult index(double? id)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("LogIn", "LogIn");
            }
            try
            {
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Something went wrong.";
            }
            return View();
        }
        #endregion---------------------------------------------------------------------

        #region--------------------BIN List--------------------------------------------
        public ActionResult BNMst(double? id, string ed)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("LogIn", "LogIn");
            }
            if (((tbl_UserDetail)Session["User"]).GroupPages.Contains("BinList"))
            {
                try
                {
                    if (id != null)
                    {
                        var x = MvcHelper.GetMasterBIN(Convert.ToInt64(id));
                        if (!string.IsNullOrEmpty(ed) && ed.Equals("2"))
                        {
                            x.ApproveStat = string.Empty;
                        }
                        ViewBag.obj = x;
                        ViewBag.hid = id;

                    }

                    ViewBag.list = MvcHelper.GetMasterBINList();
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Something went wrong.";
                }
                return View();
            }
            else
            {
                return RedirectToAction("Error", "ErrorPage");
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BNMst(tbl_MasterBINList obj, string hid)
        {

            if (Session["User"] == null)
            {
                return RedirectToAction("LogIn", "LogIn");
            }
            if (((tbl_UserDetail)Session["User"]).GroupPages.Contains("BinList"))
            {
                try
                {
                    new MvcHelper((tbl_UserDetail)Session["User"]);
                    if (string.IsNullOrEmpty(hid))
                    {
                        MvcHelper.SaveMasterBINList(obj);
                    }
                    else
                    {
                        MvcHelper.EditMasterBINList(obj, Convert.ToInt64(hid));
                    }
                    TempData["Success"] = "Data saved successfully.";
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Something went wrong.";
                }
                return RedirectToAction("BNMst");
            }
            else
            {
                return RedirectToAction("Error", "ErrorPage");
            }

        }

        #endregion-----------------------------------------------------------------------

        #region--------------------Device List--------------------------------------------
        public ActionResult DeviceList(double? id, string ed)
        {
            if (((tbl_UserDetail)Session["User"]).GroupPages.Contains("DeviceList"))
            {
                if (Session["User"] == null)
                {
                    return RedirectToAction("LogIn", "LogIn");
                }
                try
                {
                    if (id != null)
                    {
                        var x = MvcHelper.GetMasterDevice(Convert.ToInt64(id));
                        if (!string.IsNullOrEmpty(ed) && ed.Equals("2"))
                        {
                            x.ApproveStat = string.Empty;
                        }
                        ViewBag.obj = x;
                        ViewBag.hid = id;
                    }

                    ViewBag.list = MvcHelper.GetMasterDeviceList();
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Something went wrong.";
                }
                return View();
            }
            else
            {
                return RedirectToAction("Error", "ErrorPage");
            }


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeviceList(tbl_MasterDeviceList obj, string hid)
        {

            if (Session["User"] == null)
            {
                return RedirectToAction("LogIn", "LogIn");
            }
            if (((tbl_UserDetail)Session["User"]).GroupPages.Contains("DeviceList"))
            {
                try
                {
                    new MvcHelper((tbl_UserDetail)Session["User"]);
                    if (string.IsNullOrEmpty(hid))
                    {
                        MvcHelper.SaveMasterDeviceList(obj);
                    }
                    else
                    {
                        MvcHelper.EditMasterDeviceList(obj, Convert.ToInt64(hid));
                    }
                    TempData["Success"] = "Data saved successfully.";
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Something went wrong.";
                }
                return RedirectToAction("DeviceList");
            }
            else
            {
                return RedirectToAction("Error", "ErrorPage");
            }

        }
        #endregion-----------------------------------------------------------------------

        #region--------------------GL Account List--------------------------------------------
        public ActionResult GLAList(double? id, string ed)
        {

            if (Session["User"] == null)
            {
                return RedirectToAction("LogIn", "LogIn");
            }
            if (((tbl_UserDetail)Session["User"]).GroupPages.Contains("GLAList"))
            {
                try
                {
                    if (id != null)
                    {

                        var x = MvcHelper.GetGLAccount(Convert.ToInt64(id));
                        if (!string.IsNullOrEmpty(ed) && ed.Equals("2"))
                        {
                            x.ApproveStat = string.Empty;
                        }
                        ViewBag.obj = x;
                        ViewBag.hid = id;
                    }
                    ViewBag.termiallist = MvcHelper.GetTerminalsIdList();
                    ViewBag.list = MvcHelper.GetGLAccountList();
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Something went wrong.";
                }

                return View();
            }
            else
            {
                return RedirectToAction("Error", "ErrorPage");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GLAList(tbl_GLAccountList obj, string hid)
        {

            if (Session["User"] == null)
            {
                return RedirectToAction("LogIn", "LogIn");
            }
            try
            {
                new MvcHelper((tbl_UserDetail)Session["User"]);
                if (string.IsNullOrEmpty(hid))
                {
                    MvcHelper.SaveGLAccount(obj);
                }
                else
                {
                    MvcHelper.EditGLAccount(obj, Convert.ToInt64(hid));
                }
                TempData["Success"] = "Data saved successfully.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Something went wrong.";
            }

            return RedirectToAction("GLAList");
        }
        #endregion-----------------------------------------------------------------------

        #region--------------------Error Code List--------------------------------------------
        public ActionResult ErrorCodeList(double? id, string ed)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("LogIn", "LogIn");
            }
            if (((tbl_UserDetail)Session["User"]).GroupPages.Contains("ErrorCodeList"))
            {
                try
                {
                    if (id != null)
                    {
                        var x = MvcHelper.Errcode(Convert.ToInt64(id));
                        if (!string.IsNullOrEmpty(ed) && ed.Equals("2"))
                        {
                            x.ApproveStat = string.Empty;
                        }
                        ViewBag.obj = x;
                        ViewBag.hid = id;
                    }

                    ViewBag.list = MvcHelper.ErrcodeList();
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Something went wrong.";
                }
                return View();
            }
            else
            {
                return RedirectToAction("Error", "ErrorPage");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ErrorCode(tbl_MasterErrorCode obj, string hid)
        {

            if (Session["User"] == null)
            {
                return RedirectToAction("LogIn", "LogIn");
            }
            if (((tbl_UserDetail)Session["User"]).GroupPages.Contains("ErrorCodeList"))
            {
                try
                {
                    new MvcHelper((tbl_UserDetail)Session["User"]);
                    if (string.IsNullOrEmpty(hid))
                    {
                        MvcHelper.SaveErrCode(obj);
                    }
                    else
                    {
                        MvcHelper.EditErrCode(obj, Convert.ToInt64(hid));
                    }
                    TempData["Success"] = "Data saved successfully.";
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Something went wrong.";
                }

                return RedirectToAction("ErrorCodeList");
            }
            else
            {
                return RedirectToAction("Error", "ErrorPage");
            }
        }

        #endregion-----------------------------------------------------------------------

        #region--------------------Bot Master--------------------------------------------
        public ActionResult BotMst(double? id, string ed)
        {

            if (Session["User"] == null)
            {
                return RedirectToAction("LogIn", "LogIn");
            }

            if (((tbl_UserDetail)Session["User"]).GroupPages.Contains("BotMaster"))
            {
                try
                {
                    if (id != null)
                    {
                        var x = MvcHelper.BotObj(Convert.ToInt64(id));
                        if (!string.IsNullOrEmpty(ed) && ed.Equals("2"))
                        {
                            x.ApproveStat = string.Empty;
                        }
                        ViewBag.obj = x;
                        ViewBag.hid = id;
                    }

                    ViewBag.list = MvcHelper.BotList();
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Something went wrong.";
                }
                return View();
            }
            else
            {
                return RedirectToAction("Error", "ErrorPage");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BotMst(tbl_BotMaster obj, string hid)
        {
            new MvcHelper((tbl_UserDetail)Session["User"]);
            if (Session["User"] == null)
            {
                return RedirectToAction("LogIn", "LogIn");
            }
            if (((tbl_UserDetail)Session["User"]).GroupPages.Contains("BotMaster"))
            {
                try
                {
                    if (string.IsNullOrEmpty(hid))
                    {
                        MvcHelper.SaveBot(obj);
                    }
                    else
                    {
                        MvcHelper.EditBot(obj, Convert.ToInt64(hid));
                    }
                    TempData["Success"] = "Data saved successfully.";
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Something went wrong.";
                }

                return RedirectToAction("BotMst");
            }
            else
            {
                return RedirectToAction("Error", "ErrorPage");
            }
        }

        #endregion-----------------------------------------------------------------------

        #region--------------------Balance --------------------------------------------
        public ActionResult Balance()
        {

            if (Session["User"] == null)
            {
                return RedirectToAction("LogIn", "LogIn");
            }
            if (((tbl_UserDetail)Session["User"]).GroupPages.Contains("Balance"))
            {
                try
                {
                    if (TempData["List"] != null && TempData["obj"] != null)
                    {
                        ViewBag.list = TempData["List"];
                        ViewBag.obj = TempData["obj"];
                    }

                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Something went wrong.";
                }
                return View();
            }
            else
            {
                return RedirectToAction("Error", "ErrorPage");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Balance(ViewModelClass.BalancingFormModel obj, string download)
        {

            if (Session["User"] == null)
            {
                return RedirectToAction("LogIn", "LogIn");
            }
            if (((tbl_UserDetail)Session["User"]).GroupPages.Contains("Balance"))
            {
                try
                {
                    TempData["List"] = MvcHelper.GetBalanceList(obj.fromdate, obj.todate, obj.terminalId, obj.GlAccoungt);
                    TempData["obj"] = obj;
                    List<ViewModelClass.ViewBalancingModel> transactionlist = new List<ViewModelClass.ViewBalancingModel>();
                    transactionlist = MvcHelper.GetBalanceList(obj.fromdate, obj.todate, obj.terminalId, obj.GlAccoungt);


                    if (!string.IsNullOrEmpty(download))
                    {
                        string sheetname = "BalanceReport" + DateTime.Now;
                        FormattoExcel(transactionlist, sheetname);
                    }
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Something went wrong.";
                }
                return RedirectToAction("Balance");
            }
            else
            {
                return RedirectToAction("Error", "ErrorPage");
            }
        }

        void FormattoExcel(List<ViewModelClass.ViewBalancingModel> transactionlist, string sheetname)
        {
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.ClearContent();
            System.Web.HttpContext.Current.Response.ClearHeaders();
            System.Web.HttpContext.Current.Response.Buffer = true;
            System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
            System.Web.HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + sheetname + ".xls");

            System.Web.HttpContext.Current.Response.Charset = "utf-8";
            System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            //sets font
            System.Web.HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            System.Web.HttpContext.Current.Response.Write("<BR><BR><BR>");
            System.Web.HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " + "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
              "style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");

            System.Web.HttpContext.Current.Response.Write("<Td>");
            System.Web.HttpContext.Current.Response.Write("<B>");
            System.Web.HttpContext.Current.Response.Write("Terminal ID");
            System.Web.HttpContext.Current.Response.Write("</B>");
            System.Web.HttpContext.Current.Response.Write("</Td>");

            System.Web.HttpContext.Current.Response.Write("<Td>");
            System.Web.HttpContext.Current.Response.Write("<B>");
            System.Web.HttpContext.Current.Response.Write("Device ID");
            System.Web.HttpContext.Current.Response.Write("</B>");
            System.Web.HttpContext.Current.Response.Write("</Td>");

            System.Web.HttpContext.Current.Response.Write("<Td>");
            System.Web.HttpContext.Current.Response.Write("<B>");
            System.Web.HttpContext.Current.Response.Write("Terminal Type");
            System.Web.HttpContext.Current.Response.Write("</B>");
            System.Web.HttpContext.Current.Response.Write("</Td>");

            System.Web.HttpContext.Current.Response.Write("<Td>");
            System.Web.HttpContext.Current.Response.Write("<B>");
            System.Web.HttpContext.Current.Response.Write("GLAccount No");
            System.Web.HttpContext.Current.Response.Write("</B>");
            System.Web.HttpContext.Current.Response.Write("</Td>");

            System.Web.HttpContext.Current.Response.Write("<Td>");
            System.Web.HttpContext.Current.Response.Write("<B>");
            System.Web.HttpContext.Current.Response.Write("Trasanction Type");
            System.Web.HttpContext.Current.Response.Write("</B>");
            System.Web.HttpContext.Current.Response.Write("</Td>");

            System.Web.HttpContext.Current.Response.Write("<Td>");
            System.Web.HttpContext.Current.Response.Write("<B>");
            System.Web.HttpContext.Current.Response.Write("Transaction TS");
            System.Web.HttpContext.Current.Response.Write("</B>");
            System.Web.HttpContext.Current.Response.Write("</Td>");

            System.Web.HttpContext.Current.Response.Write("<Td>");
            System.Web.HttpContext.Current.Response.Write("<B>");
            System.Web.HttpContext.Current.Response.Write("Amount From T24");
            System.Web.HttpContext.Current.Response.Write("</B>");
            System.Web.HttpContext.Current.Response.Write("</Td>");

            System.Web.HttpContext.Current.Response.Write("<Td>");
            System.Web.HttpContext.Current.Response.Write("<B>");
            System.Web.HttpContext.Current.Response.Write("Amont From EJ");
            System.Web.HttpContext.Current.Response.Write("</B>");
            System.Web.HttpContext.Current.Response.Write("</Td>");

            System.Web.HttpContext.Current.Response.Write("<Td>");
            System.Web.HttpContext.Current.Response.Write("<B>");
            System.Web.HttpContext.Current.Response.Write("Replenishment");
            System.Web.HttpContext.Current.Response.Write("</B>");
            System.Web.HttpContext.Current.Response.Write("</Td>");


            foreach (ViewBalancingModel listData in transactionlist)
            {
                System.Web.HttpContext.Current.Response.Write("<TR>");

                System.Web.HttpContext.Current.Response.Write("<Td>");
                System.Web.HttpContext.Current.Response.Write(listData.terminalID);
                System.Web.HttpContext.Current.Response.Write("</Td>");

                System.Web.HttpContext.Current.Response.Write("<Td>");
                System.Web.HttpContext.Current.Response.Write(listData.DeviceID);
                System.Web.HttpContext.Current.Response.Write("</Td>");

                System.Web.HttpContext.Current.Response.Write("<Td>");
                System.Web.HttpContext.Current.Response.Write(listData.TerminalType);
                System.Web.HttpContext.Current.Response.Write("</Td>");

                System.Web.HttpContext.Current.Response.Write("<Td>");
                System.Web.HttpContext.Current.Response.Write(listData.GLAccountNo);
                System.Web.HttpContext.Current.Response.Write("</Td>");

                System.Web.HttpContext.Current.Response.Write("<Td>");
                System.Web.HttpContext.Current.Response.Write(listData.TrasanctionType);
                System.Web.HttpContext.Current.Response.Write("</Td>");

                System.Web.HttpContext.Current.Response.Write("<Td>");
                System.Web.HttpContext.Current.Response.Write(listData.Transaction_Ts);
                System.Web.HttpContext.Current.Response.Write("</Td>");

                System.Web.HttpContext.Current.Response.Write("<Td>");
                System.Web.HttpContext.Current.Response.Write(listData.AmountFromT24);
                System.Web.HttpContext.Current.Response.Write("</Td>");

                System.Web.HttpContext.Current.Response.Write("<Td>");
                System.Web.HttpContext.Current.Response.Write(listData.AmontFromEJ);
                System.Web.HttpContext.Current.Response.Write("</Td>");

                System.Web.HttpContext.Current.Response.Write("<Td>");
                System.Web.HttpContext.Current.Response.Write(listData.Replenishment);
                System.Web.HttpContext.Current.Response.Write("</Td>");

                System.Web.HttpContext.Current.Response.Write("<TR>");
            }

            System.Web.HttpContext.Current.Response.Write("</Table>");
            System.Web.HttpContext.Current.Response.Write("</font>");
            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.End();
            System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        #endregion-----------------------------------------------------------------------

        #region--------------------Profile --------------------------------------------

        public ActionResult ProfilePage(double? id, string ed)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("LogIn", "LogIn");
            }

            if (((tbl_UserDetail)Session["User"]).GroupPages.Contains("Profile"))
            {
                try
                {
                    if (id != null)
                    {
                        var x = MvcHelper.GetGroupRoleMappingMaster(Convert.ToInt32(id));

                        if (!string.IsNullOrEmpty(ed) && ed.Equals("2"))
                        {
                            x.ApproveStat = string.Empty;
                        }

                        ViewBag.obj = x;
                        ViewBag.hid = id;
                    }

                }

                catch (Exception ex)
                {
                    TempData["Error"] = "Something went wrong.";
                }

                ViewBag.groupList = MvcHelper.GetGroupRoleMappingMasterList();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProfilePage(tbl_GroupRoleMappingMaster obj, string hid)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("LogIn", "LogIn");
            }
            try
            {
                if (!string.IsNullOrEmpty(hid))
                {
                    new MvcHelper((tbl_UserDetail)Session["User"]);
                    tbl_GroupRoleMappingMaster tbl = new tbl_GroupRoleMappingMaster()
                    {

                        IsActive = true,
                        GroupName = obj.GroupName,
                        PageName = obj.PageName,
                        EntryBy = ((tbl_UserDetail)Session["user"]).UserGroup.ToString(),
                    };
                    MvcHelper.EditGroupRoleMappingMaster(obj, Convert.ToInt32(hid));
                }
            }

            catch
            {
                TempData["Error"] = "Something went wrong.";
            }
            ViewBag.groupList = MvcHelper.GetGroupRoleMappingMasterList();
            return View();
        }


        #endregion-----------------------------------------------------------------------
    }
}
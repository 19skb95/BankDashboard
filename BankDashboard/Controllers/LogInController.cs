using BankDashboard.Common;
using BankDashboard.Models;
using BankMuscat;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankDashboard.Controllers
{
    public class LogInController : Controller
    {
        // GET: LogIn
        public ActionResult LogIn()
        {
            
            string ErrorMsg = string.Empty; string Action = string.Empty; string cntrlr = string.Empty, groupname = string.Empty;
            if (Session["User"] != null)
            {
                var userSession = ((tbl_UserDetail)Session["User"]);
                if (userSession.UserGroup.ToLower().Equals(Constants.UserGroups.Admin.ToLower()))
                {
                    Action = "index"; cntrlr = "Admin";
                }
                else if (userSession.UserGroup.ToLower().Equals(Constants.UserGroups.ParameterManager.ToLower()))
                {
                    Action = "index"; cntrlr = "Admin";
                }
                else if (userSession.UserGroup.ToLower().Equals(Constants.UserGroups.Checker.ToLower()) || userSession.UserGroup.ToLower().Equals(Constants.UserGroups.Maker.ToLower()))
                {
                    Action = "index"; cntrlr = "User";
                }
                return RedirectToAction(Action, cntrlr);
            }
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(string uname, string pwd)

        {

            pwd = pwd.Replace("\"", string.Empty);
            bool logincheck = false;
            string DecryptKey = Convert.ToString(ConfigurationManager.AppSettings["DecryptKey"]);
            pwd = MvcHelper.CodeDecrypt(pwd.Trim(), DecryptKey.Trim());
            string ErrorMsg = string.Empty; string Action = string.Empty; string cntrlr = string.Empty, groupname = string.Empty, pages = string.Empty;

            tbl_UserDetail user = new tbl_UserDetail();
            UserTransactionData utobj = new UserTransactionData();
            ADManager AdObj = new ADManager();
            logincheck = AdObj.ChcekLogin(uname, pwd, ref groupname);

            //user = MvcHelper.GetUser(uname, pwd, ref pages);
            //if (user != null)
            //{
            //    logincheck = true;
            //    groupname = user.UserGroup;
            //}
            if (logincheck)
            {

                if (!string.IsNullOrEmpty(groupname))
                {
                    user = new tbl_UserDetail()
                    {
                        AccountName = uname,
                        UserGroup = groupname,
                    };
                    user.GroupPages = MvcHelper.GetGroupPages(groupname);
                    //int check = utobj.CheckMachineAvailable(System.Net.Dns.GetHostEntry(Request.UserHostAddress).HostName);
                    //if (check == 1)
                    //{
                        //Session["Machine"] = System.Net.Dns.GetHostEntry(Request.UserHostAddress).HostName;                        
                        MvcHelper.SaveUser(user);
                        Action = "index"; cntrlr = "Dashboard";
                   // }
                    //else
                    //{
                    //    ErrorMsg = "Sorry...!!! Multiple user login is not allowed.";
                    //}
                }
                else
                {
                    ErrorMsg = "User type is not authorized for the dashboard...!";
                }
            }
            else
            {
                ErrorMsg = "Wrong credential...!";
            }
            if (!string.IsNullOrEmpty(ErrorMsg))
            {
                TempData["invalidmsg"] = ErrorMsg;
                return RedirectToAction("LogIn");
            }
            else
            {
                Session["User"] = user;
                return RedirectToAction(Action, cntrlr);
            }
        }

        public JsonResult EncryptPassword(string pwd)
        {

            string EncryptKey = Convert.ToString(ConfigurationManager.AppSettings["DecryptKey"]);
            string encrypteddata = MvcHelper.CodeEncrypt(pwd.Trim(), EncryptKey.Trim());
            return Json(encrypteddata, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            //UserTransactionData utobj = new UserTransactionData();
            //utobj.MachineLogout(Session["Machine"].ToString());
            Session.Abandon();
            return RedirectToAction("LogIn");
        }
    }
}
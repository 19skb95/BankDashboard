using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankDashboard.Models;
using BankMuscat;

using BankDashboard.Common;

namespace BankDashboard.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewReportUserWise(DateTime? startDate, DateTime? endDate, string ddlUserList, string terminaldId, string status, string reportType, string download)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("LogIn", "LogIn");
            }
            //if (!((tbl_UserDetail)Session["User"]).UserGroup.Equals(Constants.UserGroups.ParameterManager))
            if (((tbl_UserDetail)Session["User"]).GroupPages.Contains("Report"))
            {
                try
                {
                    tbl_UserDetail user = (tbl_UserDetail)Session["User"];
                    List<UserDetails> transactionlist = new List<UserDetails>();
                    UserTransactionData userTransactionData = new UserTransactionData();
                    if (!string.IsNullOrEmpty(reportType) && reportType.Trim() == "Maximum Dispute Raising Machine")
                    {
                        ViewBag.MaxDisputeMachine = userTransactionData.GetMaxDisputedMachine(startDate, endDate, reportType, user.AccountName, user.UserGroup);
                    }
                    else if (!string.IsNullOrEmpty(reportType) && reportType.Trim() == "Maximum Complaint Raising Customer")
                    {
                        var maxdispute = userTransactionData.GetMaxDisputedCustomer(startDate, endDate, reportType, user.AccountName, user.UserGroup);
                        MvcHelper.DecryptMaxDisputeObj(ref maxdispute);
                        ViewBag.MaxDisputeCustomer = maxdispute;
                    }
                    else if (!string.IsNullOrEmpty(reportType) && reportType.Trim() == "User Wise Transactions")
                    {
                        ddlUserList = (ddlUserList.Contains(',') ? ddlUserList.Split(',')[0] : ddlUserList);
                        transactionlist = ViewBag.ReportData = userTransactionData.GetTransactionDataList(startDate, endDate, terminaldId, status, reportType, ddlUserList, user.UserGroup);
                    }
                    else
                    {
                        transactionlist = ViewBag.ReportData = userTransactionData.GetTransactionDataList(startDate, endDate, terminaldId, status, reportType, user.AccountName, user.UserGroup);
                    }
                    if (!string.IsNullOrEmpty(download))
                    {
                        string sheetname = "Report" + DateTime.Now;
                        FormattoExcel(transactionlist, sheetname);
                    }
                    if (!string.IsNullOrEmpty(ddlUserList) && ddlUserList != "0")
                    {
                        tbl_UserDetail objuser = MvcHelper.UserDetail(ddlUserList.Trim());
                        ddlUserList = objuser.AccountName + "," + objuser.AccountName;
                    }
                    ViewBag.SearchDetail = new ViewModelClass.ViewReportSeachDataModel()
                    {
                        endDate = (endDate != null ? (Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy")) : ""),
                        startDate = (startDate != null ? (Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy")) : ""),
                        terminaldId = terminaldId,
                        reportType = reportType,
                        ddlUserList = ddlUserList,
                        status = status
                    };
                    ViewBag.UserList = MvcHelper.UserList();
                }
                catch (Exception ex)
                {

                }
                return View();
            }
            else
            {
                Session.Abandon();
                return RedirectToAction("Error", "ErrorPage");
            }
        }

        public ActionResult NewViewTransaction(string Comment, string UserAction, string CommentHold, string ID, string SolvedBy, string EjID, string feedbackid, string SubmitEnable, string back, string Submit)
        {

            if (Session["User"] == null)
            {
                return RedirectToAction("LogIn", "LogIn");
            }
            if (((tbl_UserDetail)Session["User"]).GroupPages.Contains("Report"))
            {
                try
                {
                    ViewModelClass.ViewTransactionModel transactionData = new ViewModelClass.ViewTransactionModel();
                    var searchobj = (ViewModelClass.ViewReportSeachDataModel)TempData["SearchDetail"];
                    if (!string.IsNullOrEmpty(SolvedBy))
                        TempData["Sovedby"] = SolvedBy.Trim();


                    List<string> T24ImageUrls = new List<string>();
                    List<string> WeCareImageUrls = new List<string>();
                    tbl_UserDetail user = (tbl_UserDetail)Session["User"];
                    if (!string.IsNullOrEmpty(back))            //Back Button Clicked...............................
                    {
                        if (user.UserGroup != Constants.UserGroups.Admin)
                        {
                            string solvedby = Convert.ToString(TempData["Sovedby"]);
                            if (SubmitEnable.ToLower() == "y")
                            {
                                solvedby = TempData["Sovedby"].ToString();

                                UserTransactionData userTransactionData1 = new UserTransactionData();
                                if (solvedby.Trim().ToLower() == "reactive")
                                    userTransactionData1.MakerOrCheckerBack(feedbackid, solvedby, user.UserGroup);
                                else if (!string.IsNullOrEmpty(EjID) && EjID != "0")
                                    userTransactionData1.MakerOrCheckerBack(EjID, solvedby, user.UserGroup);
                            }
                        }

                        return RedirectToAction("NewReportUserWise", new
                        {
                            startDate = searchobj.startDate,
                            endDate = searchobj.endDate,
                            ddlUserList = searchobj.ddlUserList,
                            terminaldId = searchobj.terminaldId,
                            status = searchobj.status,
                            reportType = searchobj.reportType,
                            searchobj
                        });
                    }
                    else if (!string.IsNullOrEmpty(Submit) && (!user.UserGroup.Equals(Constants.UserGroups.Admin)))         //Save user input....................................
                    {
                        string solvedby = Convert.ToString(TempData["Sovedby"]);

                        if (!string.IsNullOrEmpty(solvedby))
                        {
                            if (solvedby.ToLower() == "reactive")
                                MvcHelper.SaveUserInput(long.Parse(feedbackid), solvedby, Comment, UserAction, CommentHold, user.UserGroup);
                            else
                                MvcHelper.SaveUserInput(long.Parse(EjID), solvedby, Comment, UserAction, CommentHold, user.UserGroup);
                        }
                        return RedirectToAction("NewReportUserWise", new
                        {
                            startDate = searchobj.startDate,
                            endDate = searchobj.endDate,
                            ddlUserList = searchobj.ddlUserList,
                            terminaldId = searchobj.terminaldId,
                            status = searchobj.status,
                            reportType = searchobj.reportType,
                            searchobj
                        });
                    }
                    else
                    {
                        UserTransactionData userTransactionData = new UserTransactionData();
                        transactionData = MvcHelper.GetTransactionData(ID, SolvedBy, user.AccountName, user.UserGroup);
                        transactionData.T24ImageUrls = userTransactionData.GetT24ImageUrls(transactionData.EJID.ToString());
                        transactionData.WeCareImageUrls = userTransactionData.GetWCImageUrls(transactionData.FeedBackId);
                        ViewBag.obj = transactionData;
                        return View();
                    }
                }
                catch (Exception ex)
                {
                    return View();
                }
            }
            else
            {
                Session.Abandon();
                return RedirectToAction("Error", "ErrorPage");
            }
        }


        void FormattoExcel(List<UserDetails> p, string sname)
        {
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.ClearContent();
            System.Web.HttpContext.Current.Response.ClearHeaders();
            System.Web.HttpContext.Current.Response.Buffer = true;
            System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
            System.Web.HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + sname + ".xls");

            System.Web.HttpContext.Current.Response.Charset = "utf-8";
            System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            //sets font
            System.Web.HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            System.Web.HttpContext.Current.Response.Write("<BR><BR><BR>");
            System.Web.HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " + "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
              "style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");
            //System.Web.HttpContext.Current.Response.Write("<Td>");
            //System.Web.HttpContext.Current.Response.Write("<B>");
            //System.Web.HttpContext.Current.Response.Write("EJID");
            //System.Web.HttpContext.Current.Response.Write("</B>");
            //System.Web.HttpContext.Current.Response.Write("</Td>");

            System.Web.HttpContext.Current.Response.Write("<Td>");
            System.Web.HttpContext.Current.Response.Write("<B>");
            System.Web.HttpContext.Current.Response.Write("Terminal Id");
            System.Web.HttpContext.Current.Response.Write("</B>");
            System.Web.HttpContext.Current.Response.Write("</Td>");

            System.Web.HttpContext.Current.Response.Write("<Td>");
            System.Web.HttpContext.Current.Response.Write("<B>");
            System.Web.HttpContext.Current.Response.Write("Error Code");
            System.Web.HttpContext.Current.Response.Write("</B>");
            System.Web.HttpContext.Current.Response.Write("</Td>");

            //System.Web.HttpContext.Current.Response.Write("<Td>");
            //System.Web.HttpContext.Current.Response.Write("<B>");
            //System.Web.HttpContext.Current.Response.Write("Account Number");
            //System.Web.HttpContext.Current.Response.Write("</B>");
            //System.Web.HttpContext.Current.Response.Write("</Td>");

            //System.Web.HttpContext.Current.Response.Write("<Td>");
            //System.Web.HttpContext.Current.Response.Write("<B>");
            //System.Web.HttpContext.Current.Response.Write("Card Number");
            //System.Web.HttpContext.Current.Response.Write("</B>");
            //System.Web.HttpContext.Current.Response.Write("</Td>");

            System.Web.HttpContext.Current.Response.Write("<Td>");
            System.Web.HttpContext.Current.Response.Write("<B>");
            System.Web.HttpContext.Current.Response.Write("Amount");
            System.Web.HttpContext.Current.Response.Write("</B>");
            System.Web.HttpContext.Current.Response.Write("</Td>");

            System.Web.HttpContext.Current.Response.Write("<Td>");
            System.Web.HttpContext.Current.Response.Write("<B>");
            System.Web.HttpContext.Current.Response.Write("Counterfeit Amount");
            System.Web.HttpContext.Current.Response.Write("</B>");
            System.Web.HttpContext.Current.Response.Write("</Td>");

            System.Web.HttpContext.Current.Response.Write("<Td>");
            System.Web.HttpContext.Current.Response.Write("<B>");
            System.Web.HttpContext.Current.Response.Write("Cash Retracted Amount");
            System.Web.HttpContext.Current.Response.Write("</B>");
            System.Web.HttpContext.Current.Response.Write("</Td>");

            System.Web.HttpContext.Current.Response.Write("<Td>");
            System.Web.HttpContext.Current.Response.Write("<B>");
            System.Web.HttpContext.Current.Response.Write("Transaction Date & Time");
            System.Web.HttpContext.Current.Response.Write("</B>");
            System.Web.HttpContext.Current.Response.Write("</Td>");

            System.Web.HttpContext.Current.Response.Write("<Td>");
            System.Web.HttpContext.Current.Response.Write("<B>");
            System.Web.HttpContext.Current.Response.Write("Bot Process Start Time");
            System.Web.HttpContext.Current.Response.Write("</B>");
            System.Web.HttpContext.Current.Response.Write("</Td>");

            System.Web.HttpContext.Current.Response.Write("<Td>");
            System.Web.HttpContext.Current.Response.Write("<B>");
            System.Web.HttpContext.Current.Response.Write("Bot Process End Time");
            System.Web.HttpContext.Current.Response.Write("</B>");
            System.Web.HttpContext.Current.Response.Write("</Td>");

            System.Web.HttpContext.Current.Response.Write("<Td>");
            System.Web.HttpContext.Current.Response.Write("<B>");
            System.Web.HttpContext.Current.Response.Write("Bot Status");
            System.Web.HttpContext.Current.Response.Write("</B>");
            System.Web.HttpContext.Current.Response.Write("</Td>");

            System.Web.HttpContext.Current.Response.Write("<Td>");
            System.Web.HttpContext.Current.Response.Write("<B>");
            System.Web.HttpContext.Current.Response.Write("Bot Remarks");
            System.Web.HttpContext.Current.Response.Write("</B>");
            System.Web.HttpContext.Current.Response.Write("</Td>");

            System.Web.HttpContext.Current.Response.Write("<Td>");
            System.Web.HttpContext.Current.Response.Write("<B>");
            System.Web.HttpContext.Current.Response.Write("User Action");
            System.Web.HttpContext.Current.Response.Write("</B>");
            System.Web.HttpContext.Current.Response.Write("</Td>");

            System.Web.HttpContext.Current.Response.Write("<Td>");
            System.Web.HttpContext.Current.Response.Write("<B>");
            System.Web.HttpContext.Current.Response.Write("User Recommendation");
            System.Web.HttpContext.Current.Response.Write("</B>");
            System.Web.HttpContext.Current.Response.Write("</Td>");

            System.Web.HttpContext.Current.Response.Write("<Td>");
            System.Web.HttpContext.Current.Response.Write("<B>");
            System.Web.HttpContext.Current.Response.Write("Repeated Claim No");
            System.Web.HttpContext.Current.Response.Write("</B>");
            System.Web.HttpContext.Current.Response.Write("</Td>");

            System.Web.HttpContext.Current.Response.Write("<Td>");
            System.Web.HttpContext.Current.Response.Write("<B>");
            System.Web.HttpContext.Current.Response.Write("Maker Input");
            System.Web.HttpContext.Current.Response.Write("</B>");
            System.Web.HttpContext.Current.Response.Write("</Td>");

            System.Web.HttpContext.Current.Response.Write("<Td>");
            System.Web.HttpContext.Current.Response.Write("<B>");
            System.Web.HttpContext.Current.Response.Write("Checker input");
            System.Web.HttpContext.Current.Response.Write("</B>");
            System.Web.HttpContext.Current.Response.Write("</Td>");

            System.Web.HttpContext.Current.Response.Write("<Td>");
            System.Web.HttpContext.Current.Response.Write("<B>");
            System.Web.HttpContext.Current.Response.Write("Final Status");
            System.Web.HttpContext.Current.Response.Write("</B>");
            System.Web.HttpContext.Current.Response.Write("</Td>");

            System.Web.HttpContext.Current.Response.Write("<Td>");
            System.Web.HttpContext.Current.Response.Write("<B>");
            System.Web.HttpContext.Current.Response.Write("Solved By");
            System.Web.HttpContext.Current.Response.Write("</B>");
            System.Web.HttpContext.Current.Response.Write("</Td>");
            System.Web.HttpContext.Current.Response.Write("</TR>");

            foreach (UserDetails pdata in p)
            {
                System.Web.HttpContext.Current.Response.Write("<TR>");
                //System.Web.HttpContext.Current.Response.Write("<Td>");

                //System.Web.HttpContext.Current.Response.Write(pdata.EJID);

                //System.Web.HttpContext.Current.Response.Write("</Td>");

                System.Web.HttpContext.Current.Response.Write("<Td>");

                System.Web.HttpContext.Current.Response.Write(pdata.TerminalID);

                System.Web.HttpContext.Current.Response.Write("</Td>");

                System.Web.HttpContext.Current.Response.Write("<Td>");

                System.Web.HttpContext.Current.Response.Write(pdata.ErrorCode);

                System.Web.HttpContext.Current.Response.Write("</Td>");

                //System.Web.HttpContext.Current.Response.Write("<Td>");

                //System.Web.HttpContext.Current.Response.Write(pdata.AccountNo);

                //System.Web.HttpContext.Current.Response.Write("</Td>");

                //System.Web.HttpContext.Current.Response.Write("<Td>");

                //System.Web.HttpContext.Current.Response.Write(pdata.CardNo);

                //System.Web.HttpContext.Current.Response.Write("</Td>");

                System.Web.HttpContext.Current.Response.Write("<Td>");

                System.Web.HttpContext.Current.Response.Write(pdata.Amount);

                System.Web.HttpContext.Current.Response.Write("</Td>");

                System.Web.HttpContext.Current.Response.Write("<Td>");

                System.Web.HttpContext.Current.Response.Write(pdata.Counterfeit_Amount);

                System.Web.HttpContext.Current.Response.Write("</Td>");

                System.Web.HttpContext.Current.Response.Write("<Td>");

                System.Web.HttpContext.Current.Response.Write(pdata.Cash_Retracted_Amount);

                System.Web.HttpContext.Current.Response.Write("</Td>");

                System.Web.HttpContext.Current.Response.Write("<Td>");

                System.Web.HttpContext.Current.Response.Write(pdata.TransactionTime);

                System.Web.HttpContext.Current.Response.Write("</Td>");

                System.Web.HttpContext.Current.Response.Write("<Td>");

                System.Web.HttpContext.Current.Response.Write(pdata.BotProcessStartTime);

                System.Web.HttpContext.Current.Response.Write("</Td>");

                System.Web.HttpContext.Current.Response.Write("<Td>");

                System.Web.HttpContext.Current.Response.Write(pdata.BotProcessEndTime);

                System.Web.HttpContext.Current.Response.Write("</Td>");

                System.Web.HttpContext.Current.Response.Write("<Td>");

                System.Web.HttpContext.Current.Response.Write(pdata.BotStatus);

                System.Web.HttpContext.Current.Response.Write("</Td>");

                System.Web.HttpContext.Current.Response.Write("<Td>");

                System.Web.HttpContext.Current.Response.Write(pdata.BotRemarks);

                System.Web.HttpContext.Current.Response.Write("</Td>");

                System.Web.HttpContext.Current.Response.Write("<Td>");

                System.Web.HttpContext.Current.Response.Write(pdata.UserAction);

                System.Web.HttpContext.Current.Response.Write("</Td>");

                System.Web.HttpContext.Current.Response.Write("<Td>");

                System.Web.HttpContext.Current.Response.Write(pdata.UserRecommendation);

                System.Web.HttpContext.Current.Response.Write("</Td>");

                System.Web.HttpContext.Current.Response.Write("<Td>");

                System.Web.HttpContext.Current.Response.Write(pdata.RepeatedClaimNo);

                System.Web.HttpContext.Current.Response.Write("</Td>");


                System.Web.HttpContext.Current.Response.Write("<Td>");

                System.Web.HttpContext.Current.Response.Write(pdata.MakerStatus);

                System.Web.HttpContext.Current.Response.Write("</Td>");

                System.Web.HttpContext.Current.Response.Write("<Td>");

                System.Web.HttpContext.Current.Response.Write(pdata.CheckerStatus);

                System.Web.HttpContext.Current.Response.Write("</Td>");

                System.Web.HttpContext.Current.Response.Write("<Td>");

                System.Web.HttpContext.Current.Response.Write(pdata.CaseStatus);

                System.Web.HttpContext.Current.Response.Write("</Td>");

                System.Web.HttpContext.Current.Response.Write("<Td>");

                System.Web.HttpContext.Current.Response.Write(pdata.SolvedBy);

                System.Web.HttpContext.Current.Response.Write("</Td>");
                System.Web.HttpContext.Current.Response.Write("</TR>");

            }
            System.Web.HttpContext.Current.Response.Write("</Table>");
            System.Web.HttpContext.Current.Response.Write("</font>");
            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.End();
            System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
        }


    }
}
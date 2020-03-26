using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BankDashboard.Common;
using static BankDashboard.Common.ViewModelClass;
using BankDashboard.Models;
using System.Net.Mail;

using System.Globalization;
using BankMuscat;
using System.Security.Cryptography;
using System.Text;
using System.Configuration;


namespace BankDashboard.Common
{
    public class MvcHelper
    {
        static tbl_UserDetail SessionObj = new tbl_UserDetail();
        public MvcHelper(tbl_UserDetail user)
        {
            SessionObj = user;
        }

        #region-------------------------------------------- UserProfilePage--------------------------------
        
        public static List<tbl_GroupRoleMappingMaster> GetGroupRoleMappingMasterList()

        {
            BNKModel db = new BNKModel();
            return db.tbl_GroupRoleMappingMaster.Where(x=>!x.GroupName.Trim().Contains(Constants.UserGroups.ApplicationUserManagement)).ToList();
        }


        public static tbl_GroupRoleMappingMaster GetGroupRoleMappingMaster(int id)
        {
            using (BNKModel db = new BNKModel())
            {
                return db.tbl_GroupRoleMappingMaster.Where(x => x.ID == id).FirstOrDefault();
            }

        }

        public static void EditGroupRoleMappingMaster(tbl_GroupRoleMappingMaster obj, double id)
        {
            using (BNKModel db = new BNKModel())
            {
                tbl_GroupRoleMappingMaster editobj = db.tbl_GroupRoleMappingMaster.Where(x => x.ID == id).FirstOrDefault();
                if (!string.IsNullOrEmpty(obj.ApproveStat) && obj.ApproveStat.Equals(Constants.ModifyStatus))
                {
                    editobj.PageName = obj.PageName;

                    editobj.UpdateBy = SessionObj.AccountName;

                    editobj.ApproveStat = Constants.ApproveModifyStatus;

                    editobj.UpdateTime = DateTime.Now;

                    editobj.IsActive = true;

                    db.SaveChanges();
                }
                else

                {
                    editobj.PageName = obj.PageName;
                    editobj.IsActive = false;
                    editobj.UpdateBy = SessionObj.AccountName;
                    editobj.ApproveStat = Constants.ModifyStatus;
                    editobj.UpdateTime = DateTime.Now;
                    db.SaveChanges();
                }

            }
        }
        #endregion
        #region--------------------BIN List--------------------------------------------
        public static tbl_MasterBINList GetMasterBIN(double id)
        {
            BNKModel db = new BNKModel();
            return db.tbl_MasterBINList.Where(x => x.ID == id).FirstOrDefault();
        }
        public static List<tbl_MasterBINList> GetMasterBINList()
        {
            BNKModel db = new BNKModel();
            return db.tbl_MasterBINList.ToList();
        }
        public static void SaveMasterBINList(tbl_MasterBINList obj)
        {
            BNKModel db = new BNKModel();
            tbl_MasterBINList tblobj = new tbl_MasterBINList();
            tblobj.BIN = obj.BIN;
            tblobj.SUBBIN = obj.SUBBIN;
            tblobj.ProductName = obj.ProductName;
            tblobj.IsActive = false;
            tblobj.EntryBy = SessionObj.AccountName;
            //
            tblobj.ApproveStat = Constants.SaveStatus;
            tblobj.EntryTime = DateTime.Now;
            //
            db.tbl_MasterBINList.Add(tblobj);
            db.SaveChanges();

        }
        public static void EditMasterBINList(tbl_MasterBINList obj, double id)
        {
            BNKModel db = new BNKModel();
            tbl_MasterBINList editobj = db.tbl_MasterBINList.Where(x => x.ID == id).FirstOrDefault();
            if (!string.IsNullOrEmpty(obj.ApproveStat) && obj.ApproveStat.Equals(Constants.SaveStatus))
            {
                editobj.BIN = obj.BIN;
                editobj.SUBBIN = obj.SUBBIN;
                editobj.ProductName = obj.ProductName;
                editobj.IsActive = obj.IsActive;
                //
                editobj.ApproveStat = Constants.ApproveSaveStatus;
                editobj.UpdateBy = SessionObj.AccountName;
                editobj.IsActive = true;
                //
                editobj.EntryTime = DateTime.Now;
            }
            else if (!string.IsNullOrEmpty(obj.ApproveStat) && obj.ApproveStat.Equals(Constants.ModifyStatus))
            {
                editobj.BIN = obj.BIN;
                editobj.SUBBIN = obj.SUBBIN;
                editobj.ProductName = obj.ProductName;
                // editobj.IsActive = obj.IsActive;
                //
                editobj.ApproveStat = Constants.ApproveModifyStatus;
                editobj.UpdateBy = SessionObj.AccountName;
                editobj.IsActive = true;
                //
                editobj.UpdateTime = DateTime.Now;
            }
            else
            {
                editobj.BIN = obj.BIN;
                editobj.SUBBIN = obj.SUBBIN;
                editobj.ProductName = obj.ProductName;
                editobj.IsActive = obj.IsActive;
                //
                editobj.ApproveStat = Constants.ModifyStatus;
                editobj.UpdateBy = SessionObj.AccountName;
                editobj.IsActive = false;
                //
                editobj.UpdateTime = DateTime.Now;
            }
            db.SaveChanges();

        }
        #endregion-----------------------------------------------------------------------

        #region--------------------Device List--------------------------------------------
        public static tbl_MasterDeviceList GetMasterDevice(double id)
        {
            BNKModel db = new BNKModel();
            return db.tbl_MasterDeviceList.Where(x => x.ID == id).FirstOrDefault();
        }
        public static List<tbl_MasterDeviceList> GetMasterDeviceList()
        {
            BNKModel db = new BNKModel();
            return db.tbl_MasterDeviceList.ToList();
        }
        public static void SaveMasterDeviceList(tbl_MasterDeviceList obj)
        {
            BNKModel db = new BNKModel();
            tbl_MasterDeviceList tblobj = new tbl_MasterDeviceList();
            tblobj = obj;
            //
            //tblobj.EntryBy = SessionObj.AccountName;
            tblobj.ApproveStat = Constants.SaveStatus;
            tblobj.EntryBy = SessionObj.AccountName;
            tblobj.IsActive = false;
            //
            tblobj.UpdateTime = DateTime.Now;
            db.tbl_MasterDeviceList.Add(tblobj);
            db.SaveChanges();

        }
        public static void EditMasterDeviceList(tbl_MasterDeviceList obj, double id)
        {
            BNKModel db = new BNKModel();
            tbl_MasterDeviceList editobj = db.tbl_MasterDeviceList.Where(x => x.ID == id).FirstOrDefault();
            if (!string.IsNullOrEmpty(obj.ApproveStat) && obj.ApproveStat.Equals(Constants.SaveStatus))
            {
                editobj.ModelNumber = obj.ModelNumber;
                editobj.SerialNumber = obj.SerialNumber;
                editobj.Make = obj.Make;
                editobj.TerminalID = obj.TerminalID;
                editobj.TerminalType = obj.TerminalType;
                editobj.DeviceId = obj.DeviceId;
                editobj.DeviceLocation = obj.DeviceLocation;
                editobj.CustodianAlertMobile = obj.CustodianAlertMobile;
                editobj.CustodianAlertEmail = obj.CustodianAlertEmail;
                editobj.Custodian = obj.Custodian;

                //editobj.IsActive = obj.IsActive;
                //
                editobj.UpdateBy = SessionObj.AccountName;
                editobj.ApproveStat = Constants.ApproveSaveStatus;
                editobj.IsActive = true;
                //
                editobj.EntryTime = DateTime.Now;
            }
            else if (!string.IsNullOrEmpty(obj.ApproveStat) && obj.ApproveStat.Equals(Constants.ModifyStatus))
            {
                editobj.ModelNumber = obj.ModelNumber;
                editobj.SerialNumber = obj.SerialNumber;
                editobj.Make = obj.Make;
                editobj.TerminalID = obj.TerminalID;
                editobj.TerminalType = obj.TerminalType;
                editobj.DeviceId = obj.DeviceId;
                editobj.DeviceLocation = obj.DeviceLocation;
                editobj.CustodianAlertMobile = obj.CustodianAlertMobile;
                editobj.CustodianAlertEmail = obj.CustodianAlertEmail;
                editobj.Custodian = obj.Custodian;

                //editobj.IsActive = obj.IsActive;
                //
                editobj.UpdateBy = SessionObj.AccountName;
                editobj.ApproveStat = Constants.ApproveModifyStatus;
                editobj.IsActive = true;
                //
                editobj.UpdateTime = DateTime.Now;
            }
            else
            {
                editobj.ModelNumber = obj.ModelNumber;
                editobj.SerialNumber = obj.SerialNumber;
                editobj.Make = obj.Make;
                editobj.TerminalID = obj.TerminalID;
                editobj.TerminalType = obj.TerminalType;
                editobj.DeviceId = obj.DeviceId;
                editobj.DeviceLocation = obj.DeviceLocation;
                editobj.CustodianAlertMobile = obj.CustodianAlertMobile;
                editobj.CustodianAlertEmail = obj.CustodianAlertEmail;
                editobj.Custodian = obj.Custodian;

                //editobj.IsActive = obj.IsActive;
                //
                editobj.UpdateBy = SessionObj.AccountName;
                editobj.ApproveStat = Constants.ModifyStatus;
                editobj.IsActive = false;
                //
                editobj.UpdateTime = DateTime.Now;
            }
            db.SaveChanges();

        }
        #endregion-----------------------------------------------------------------------

        #region--------------------GL Account List--------------------------------------------
        public static tbl_GLAccountList GetGLAccount(double id)
        {
            BNKModel db = new BNKModel();
            return db.tbl_GLAccountList.Where(x => x.ID == id).FirstOrDefault();
        }
        public static List<tbl_GLAccountList> GetGLAccountList()
        {
            BNKModel db = new BNKModel();
            return db.tbl_GLAccountList.ToList();
        }
        public static void SaveGLAccount(tbl_GLAccountList obj)
        {
            BNKModel db = new BNKModel();
            tbl_GLAccountList tblobj = new tbl_GLAccountList();
            tblobj = obj;
            //
            tblobj.EntryBy = SessionObj.AccountName;
            tblobj.ApproveStat = Constants.SaveStatus;
            tblobj.IsActive = false;
            //
            tblobj.EntryTime = DateTime.Now;
            db.tbl_GLAccountList.Add(tblobj);
            db.SaveChanges();

        }
        public static void EditGLAccount(tbl_GLAccountList obj, double id)
        {
            BNKModel db = new BNKModel();
            tbl_GLAccountList editobj = db.tbl_GLAccountList.Where(x => x.ID == id).FirstOrDefault();
            if (!string.IsNullOrEmpty(obj.ApproveStat) && obj.ApproveStat.Equals(Constants.SaveStatus))
            {
                editobj.TerminalID = obj.TerminalID;
                editobj.TerminalType = obj.TerminalType;
                editobj.GLNumber = obj.GLNumber;
                //editobj.IsActive = obj.IsActive;

                editobj.UpdateBy = SessionObj.AccountName;
                editobj.ApproveStat = Constants.ApproveSaveStatus;
                editobj.IsActive = true;

                //
                editobj.UpdateTime = DateTime.Now;
            }
            else if (!string.IsNullOrEmpty(obj.ApproveStat) && obj.ApproveStat.Equals(Constants.ModifyStatus))
            {
                editobj.TerminalID = obj.TerminalID;
                editobj.TerminalType = obj.TerminalType;
                editobj.GLNumber = obj.GLNumber;
                //editobj.IsActive = obj.IsActive;

                //
                editobj.UpdateBy = SessionObj.AccountName;
                editobj.ApproveStat = Constants.ApproveModifyStatus;
                editobj.IsActive = true;
                //
                editobj.UpdateTime = DateTime.Now;
            }

            else
            {
                editobj.TerminalID = obj.TerminalID;
                editobj.TerminalType = obj.TerminalType;
                editobj.GLNumber = obj.GLNumber;
                editobj.IsActive = obj.IsActive;

                //
                editobj.UpdateBy = SessionObj.AccountName;
                editobj.ApproveStat = Constants.ModifyStatus;
                editobj.IsActive = false;
                //
                editobj.UpdateTime = DateTime.Now;
            }
            db.SaveChanges();

        }
        public static List<string> GetTerminalsIdList()
        {
            BNKModel db = new BNKModel();
            return db.tbl_MasterDeviceList.Where(x => x.IsActive == true).Select(x => x.TerminalID).ToList();
        }
        #endregion-----------------------------------------------------------------------

        #region--------------------Error Code List--------------------------------------------
        public static tbl_MasterErrorCode Errcode(double id)
        {
            BNKModel db = new BNKModel();
            return db.tbl_MasterErrorCode.Where(x => x.ID == id).FirstOrDefault();
        }
        public static List<tbl_MasterErrorCode> ErrcodeList()
        {
            BNKModel db = new BNKModel();
            return db.tbl_MasterErrorCode.ToList();
        }
        public static void SaveErrCode(tbl_MasterErrorCode obj)
        {
            BNKModel db = new BNKModel();
            tbl_MasterErrorCode tblobj = new tbl_MasterErrorCode();
            tblobj.Error_Code = obj.Error_Code;
            tblobj.Error_Description = obj.Error_Description;
            tblobj.Machine_Type = obj.Machine_Type;
            tblobj.Make = obj.Make;
            // tblobj.IsActive = obj.IsActive;


            //
            tblobj.EntryBy = SessionObj.AccountName;
            tblobj.ApproveStat = Constants.SaveStatus;
            tblobj.IsActive = false;
            //
            tblobj.EntryTime = DateTime.Now;
            db.tbl_MasterErrorCode.Add(tblobj);
            db.SaveChanges();

        }
        public static void EditErrCode(tbl_MasterErrorCode obj, double id)
        {
            BNKModel db = new BNKModel();
            tbl_MasterErrorCode tblobj = db.tbl_MasterErrorCode.Where(x => x.ID == id).FirstOrDefault();
            if (!string.IsNullOrEmpty(obj.ApproveStat) && obj.ApproveStat.Equals(Constants.SaveStatus))
            {
                tblobj.Error_Code = obj.Error_Code;
                tblobj.Error_Description = obj.Error_Description;
                tblobj.Machine_Type = obj.Machine_Type;
                tblobj.Make = obj.Make;
                // tblobj.IsActive = obj.IsActive;

                //
                tblobj.UpdateBy = SessionObj.AccountName;
                tblobj.ApproveStat = Constants.ApproveSaveStatus;
                tblobj.IsActive = true;
                //
                tblobj.UpdateTime = DateTime.Now;
            }
            else if (!string.IsNullOrEmpty(obj.ApproveStat) && obj.ApproveStat.Equals(Constants.ModifyStatus))
            {
                tblobj.Error_Code = obj.Error_Code;
                tblobj.Error_Description = obj.Error_Description;
                tblobj.Machine_Type = obj.Machine_Type;
                tblobj.Make = obj.Make;
                // tblobj.IsActive = obj.IsActive;

                //
                tblobj.UpdateBy = SessionObj.AccountName;
                tblobj.ApproveStat = Constants.ApproveModifyStatus;
                tblobj.IsActive = true;
                //
                tblobj.UpdateTime = DateTime.Now;
            }
            else
            {
                tblobj.Error_Code = obj.Error_Code;
                tblobj.Error_Description = obj.Error_Description;
                tblobj.Machine_Type = obj.Machine_Type;
                tblobj.Make = obj.Make;
                //tblobj.IsActive = obj.IsActive;

                //
                tblobj.UpdateBy = SessionObj.AccountName;
                tblobj.ApproveStat = Constants.ModifyStatus;
                tblobj.IsActive = false;
                //
                tblobj.UpdateTime = DateTime.Now;
            }
            db.SaveChanges();

        }
        #endregion-----------------------------------------------------------------------

        #region--------------------Balance--------------------------------------------

        public static List<ViewModelClass.ViewBalancingModel> GetBalanceList(string frmdate, string todate, string terminalid, string glaccount)
        {
            BNKModel db = new BNKModel();
            List<ViewModelClass.ViewBalancingModel> Listmodel = new List<ViewBalancingModel>();
            Listmodel = GetListFromGlAmountHstry(db);
            Listmodel = GetListFromEJLogHistory(db, Listmodel);
            if ((!string.IsNullOrEmpty(frmdate) && !string.IsNullOrEmpty(frmdate)))
            {
                DateTime fdt = Convert.ToDateTime(frmdate), tdt = Convert.ToDateTime(todate);
                if (!string.IsNullOrEmpty(terminalid) && !string.IsNullOrEmpty(glaccount))
                {
                    Listmodel = Listmodel.Where(x => x.Transaction_Ts >= fdt && x.Transaction_Ts <= tdt
                    && x.terminalID == terminalid.Trim() && x.GLAccountNo == glaccount.Trim()).ToList();
                }
                else if (!string.IsNullOrEmpty(terminalid))
                {
                    Listmodel = Listmodel.Where(x => x.Transaction_Ts >= fdt && x.Transaction_Ts <= tdt
                  && x.terminalID == terminalid.Trim()).ToList();
                }
                else if (!string.IsNullOrEmpty(glaccount))
                {
                    Listmodel = Listmodel.Where(x => x.Transaction_Ts >= fdt && x.Transaction_Ts <= tdt && x.GLAccountNo == glaccount.Trim()).ToList();
                }
                else
                {
                    Listmodel = Listmodel.Where(x => x.Transaction_Ts >= fdt && x.Transaction_Ts <= tdt).ToList();
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(terminalid) && !string.IsNullOrEmpty(glaccount))
                {
                    Listmodel = Listmodel.Where(x => x.terminalID == terminalid.Trim() && x.GLAccountNo == glaccount.Trim()).ToList();
                }
                else if (!string.IsNullOrEmpty(terminalid))
                {
                    Listmodel = Listmodel.Where(x => x.terminalID == terminalid.Trim()).ToList();

                }
                else
                {
                    Listmodel = Listmodel.Where(x => x.GLAccountNo == glaccount.Trim()).ToList();
                }

            }


            return Listmodel;
        }
        public static List<ViewModelClass.ViewBalancingModel> GetListFromGlAmountHstry(BNKModel db)
        {
            var list = (from a in db.tbl_GLAmountHistory
                        select new ViewModelClass.ViewBalancingModel()

                        {
                            terminalID = a.TerminalID,
                            GLAccountNo = a.GLNumber,
                            TerminalType = a.TerminalType,
                            AmountFromT24 = a.TotalApproveAmount,
                            Transaction_Ts = a.Transaction_TS,
                            //TrasanctionType = a.TerminalType.Contains("FFM") ? (a.TerminalType == "FFMATM" ? "Withdraw" : "Deposit") : (a.TerminalType == "ATM" ? "Withdraw" : "Deposit"),
                            TrasanctionType = (a.TerminalType.ToUpper().Contains("ATM") ? "Withdraw" : "Deposit")
                        }).ToList();
            return list;
        }
        public static List<ViewModelClass.ViewBalancingModel> GetListFromEJLogHistory(BNKModel db, List<ViewModelClass.ViewBalancingModel> list)
        {
            var reslist = (from a in list
                           join b in db.tbl_EJLogsHistory
            on a.terminalID equals b.TerminalID
                           where a.TrasanctionType.ToLower() == b.TransactionType.ToLower()

                           select new ViewModelClass.ViewBalancingModel()
                           {
                               terminalID = a.terminalID,
                               GLAccountNo = a.GLAccountNo,
                               TerminalType = a.TerminalType,
                               AmountFromT24 = a.AmountFromT24,
                               Transaction_Ts = a.Transaction_Ts,
                               AmontFromEJ = b.TotalApproveAmount,
                               DeviceID = b.DeviceId,
                               TrasanctionType = a.TrasanctionType,
                               Replenishment = b.Replenishment
                           }
                         ).ToList();

            return reslist;

        }
        #endregion-----------------------------------------------------------------------

        #region--------------------BOT Master List--------------------------------------------
        public static tbl_BotMaster BotObj(double id)
        {
            BNKModel db = new BNKModel();
            return db.tbl_BotMaster.Where(x => x.ID == id).FirstOrDefault();
        }
        public static List<tbl_BotMaster> BotList()
        {
            BNKModel db = new BNKModel();
            return db.tbl_BotMaster.ToList();
        }
        public static void SaveBot(tbl_BotMaster obj)
        {
            BNKModel db = new BNKModel();
            tbl_BotMaster tblobj = new tbl_BotMaster();
            tblobj.MachineName = obj.MachineName;
            tblobj.IsActive = obj.IsActive;


            //
            tblobj.EntryBy = SessionObj.AccountName;
            tblobj.ApproveStat = Constants.SaveStatus;
            //
            tblobj.EntryTime = DateTime.Now;
            db.tbl_BotMaster.Add(tblobj);
            db.SaveChanges();

        }
        public static void EditBot(tbl_BotMaster obj, double id)
        {
            BNKModel db = new BNKModel();
            tbl_BotMaster tblobj = db.tbl_BotMaster.Where(x => x.ID == id).FirstOrDefault();
            if (!string.IsNullOrEmpty(obj.ApproveStat) && obj.ApproveStat.Equals(Constants.SaveStatus))
            {
                tblobj.MachineName = obj.MachineName;
                tblobj.IsActive = obj.IsActive;
                //
                tblobj.UpdateBy = SessionObj.AccountName;
                tblobj.ApproveStat = Constants.ApproveSaveStatus;
                //
                tblobj.UpdateTime = DateTime.Now;
            }
            else if (!string.IsNullOrEmpty(obj.ApproveStat) && obj.ApproveStat.Equals(Constants.ModifyStatus))
            {
                tblobj.MachineName = obj.MachineName;
                tblobj.IsActive = obj.IsActive;
                //
                tblobj.UpdateBy = SessionObj.AccountName;
                tblobj.ApproveStat = Constants.ApproveModifyStatus;
                //
                tblobj.UpdateTime = DateTime.Now;
            }
            else
            {
                tblobj.MachineName = obj.MachineName;
                tblobj.IsActive = obj.IsActive;
                //
                tblobj.UpdateBy = SessionObj.AccountName;
                tblobj.ApproveStat = Constants.ModifyStatus;
                //
                tblobj.UpdateTime = DateTime.Now;
            }


            db.SaveChanges();

        }
        #endregion-----------------------------------------------------------------------
        public static tbl_UserDetail GetUser(string uname, string pwd, ref string pages)
        {
            using (BNKModel db = new BNKModel())
            {
                var obj = db.tbl_UserDetail.Where(x => x.AccountName.Equals(uname) && x.PWD.Equals(pwd)).FirstOrDefault();
                if (obj != null)
                {
                    pages = GetGroupPages(obj.UserGroup.Trim());
                }

                return obj;
            }

        }
        public static string GetGroupPages(string group)
        {
            using (BNKModel db = new BNKModel())
            {               
                var obj = db.tbl_GroupRoleMappingMaster.Where(x => x.GroupName.Trim().Contains(group)&&x.IsActive==true).FirstOrDefault();
                if (obj != null)
                {
                    return ((string.IsNullOrEmpty(obj.PageName)) ? "" : obj.PageName);
                }
                else
                {
                    return "";
                }
               
            }
        }
        #region--------------------View Transaction--------------------------------------------
        public static ViewTransactionModel GetTransactionData(string ID, string SolvedBy, string userid, string utype)
        {
            BNKModel db = new BNKModel();
            //List<tbl_EJData> Ejobj = db.tbl_EJData.Where(x => x.RecordTS == TransactionTs.Date && x.AccountNumber == AccountNo && x.CardNumber == CardNo).ToList();
            //List<tbl_DisputeData> DisputeObj = db.tbl_DisputeData.Where(x => x.Incident_Date == TransactionTs.Date && x.AccountNumber == AccountNo && x.CardNumber == CardNo).ToList();
            UserTransactionData userTransactionData = new UserTransactionData();
            ViewTransaction v = userTransactionData.GetViewTransactionDetail(ID, SolvedBy, userid, utype);
            ViewModelClass.ViewTransactionModel Vobj = new ViewModelClass.ViewTransactionModel();
            string decryptKey = Convert.ToString(ConfigurationManager.AppSettings["DecryptKey"]);
            try
            {
                Vobj.FeedBackId = v.FeedBackId;
                Vobj.CustomerName = v.CustomerName;
                Vobj.RegMobileNumber = (!string.IsNullOrEmpty(v.RegMobileNumber) ? (CodeDecrypt(v.RegMobileNumber, decryptKey)) : string.Empty);
                Vobj.EJID = long.Parse(string.IsNullOrEmpty(v.EJID) ? "0" : v.EJID);
                Vobj.Recordts = v.Recordts;
                Vobj.AccountNumber = (!string.IsNullOrEmpty(v.AccountNumber) ? (CodeDecrypt(v.AccountNumber, decryptKey)) : string.Empty);
                Vobj.CardNumber = (!string.IsNullOrEmpty(v.CardNumber) ? (CodeDecrypt(v.CardNumber, decryptKey)) : string.Empty);
                Vobj.RegEmailId = (!string.IsNullOrEmpty(v.RegEmailId) ? (CodeDecrypt(v.RegEmailId, decryptKey)) : string.Empty);
                Vobj.Amount = v.Amount;
                Vobj.Counterfeit_Amount =v.Counterfeit_Amount;
                Vobj.Cash_Retracted_Amount =v.Cash_Retracted_Amount;
                Vobj.TerminalID = v.TerminalID;
                Vobj.DeviceId = v.DeviceId;
                Vobj.BotStatus = v.BotStatus;
                Vobj.User_Action = v.User_Action;
                Vobj.User_Recommendation = v.User_Recommendation;
                Vobj.Maker_Input = v.Maker_Input;
                Vobj.Checker_Input = v.Checker_Input;
                Vobj.MakerComment = v.MakerComment;
                Vobj.MakerComment_Hold = v.MakerComment_Hold;
                Vobj.TransactionType = v.TransactionType;
                Vobj.IsLocked = v.IsLocked;
                Vobj.SubmitEnable = v.SubmitEnable;


                if (!string.IsNullOrEmpty(Vobj.FeedBackId) || !string.IsNullOrEmpty(Vobj.TerminalID))
                {

                    var BalanceAmountList = GetBalanceList(string.Empty, string.Empty, Vobj.TerminalID, string.Empty);

                    if (BalanceAmountList.Count > 1)
                    {
                        ViewBalancingModel AtmBalaceModel = BalanceAmountList.Where(x => x.TerminalType.ToUpper().Contains("ATM")).FirstOrDefault();
                        ViewBalancingModel CdmBalaceModel = BalanceAmountList.Where(x => x.TerminalType.ToUpper().Contains("CDM")).FirstOrDefault();
                        Vobj.ATMBalancingAmount = ((Convert.ToInt64(AtmBalaceModel.AmontFromEJ)) - (Convert.ToInt64(AtmBalaceModel.AmountFromT24))).ToString();
                        Vobj.CDMBalancingAmount = ((Convert.ToInt64(CdmBalaceModel.AmontFromEJ)) - (Convert.ToInt64(CdmBalaceModel.AmountFromT24))).ToString();
                    }
                    else
                    {
                        foreach (var item in BalanceAmountList)
                        {
                            if (item.TerminalType.ToUpper().Contains("ATM"))
                                Vobj.ATMBalancingAmount = ((Convert.ToInt64(item.AmontFromEJ)) - (Convert.ToInt64(item.AmountFromT24))).ToString();
                            else
                                Vobj.CDMBalancingAmount = ((Convert.ToInt64(item.AmontFromEJ)) - (Convert.ToInt64(item.AmountFromT24))).ToString();
                        }
                    }
                }
                return Vobj;
            }
            catch (Exception ex)
            {
                return Vobj = new ViewTransactionModel();
            }


        }

        public static void Encrypalldata()
        {
            string decryptKey = Convert.ToString(ConfigurationManager.AppSettings["DecryptKey"]);

            BNKModel db = new BNKModel();
            var ejlist = db.tbl_EJData.ToList();
            foreach (var item in ejlist)
            {
                //item.AccountNumber = (!string.IsNullOrEmpty(item.AccountNumber) ? (CodeEncrypt(item.AccountNumber, decryptKey)) : string.Empty);
                item.CardNumber = (!string.IsNullOrEmpty(item.CardNumber) ? (CodeEncrypt(item.CardNumber, decryptKey)) : string.Empty);
            }
            db.SaveChanges();
            var diputelist = db.tbl_DisputeData.ToList();
            foreach (var item in diputelist)
            {
                item.AccountNumber = (!string.IsNullOrEmpty(item.AccountNumber) ? (CodeEncrypt(item.AccountNumber, decryptKey)) : string.Empty);
                item.CardNumber = (!string.IsNullOrEmpty(item.CardNumber) ? (CodeEncrypt(item.CardNumber, decryptKey)) : string.Empty);
                item.RegEmailId = (!string.IsNullOrEmpty(item.RegEmailId) ? (CodeEncrypt(item.RegEmailId, decryptKey)) : string.Empty);
                item.RegMobileNumber = (!string.IsNullOrEmpty(item.RegMobileNumber) ? (CodeEncrypt(item.RegMobileNumber, decryptKey)) : string.Empty);
            }
            db.SaveChanges();
        }
        public static string CodeEncrypt(string str, string key)
        {
            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(str);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        public static string CodeDecrypt(string input, string key)
        {
            //try
            //{
            if (!string.IsNullOrEmpty(input) && !string.IsNullOrEmpty(key))
            {
                byte[] inputArray = Convert.FromBase64String(input);
                TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
                tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
                tripleDES.Mode = CipherMode.ECB;
                tripleDES.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = tripleDES.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
                tripleDES.Clear();
                return UTF8Encoding.UTF8.GetString(resultArray);
            }
            else
            {
                return string.Empty;
            }
            // }
            //catch(Exception ex)
            //{
            //    return string.Empty;
            //}

        }
        #endregion-----------------------------------------------------------------------

        #region-----------------------------Save UserInput--------------------------------
        public static void SaveUserInput(long Id, string solvedby, string comment, string useraction, string commenthold, string usertype)
        {
            BNKModel db = new BNKModel();
            if (solvedby.Trim().ToLower() == "reactive")
            {
                var objDispute = db.tbl_DisputeData.Where(x => x.FeedbackId == Id).FirstOrDefault();
                if (usertype.Trim().ToLower().Equals(Constants.UserGroups.Maker.ToLower()))
                {
                    if (useraction.ToLower() == "hold")
                    {
                        objDispute.Maker_Input = useraction;
                        objDispute.MakerComment_Hold = commenthold;
                        objDispute.MakerHold_Time = DateTime.Now;
                    }
                    else
                    {
                        objDispute.Maker_Input = useraction;
                        objDispute.MakerComment = comment;
                        objDispute.MakerProcess_EndTime = DateTime.Now;

                    }
                }
                else
                {
                    if (useraction.ToLower() == "hold")
                    {
                        objDispute.Checker_Input = useraction;
                        objDispute.CheckerComment_Hold = commenthold;
                        objDispute.CheckerHold_Time = DateTime.Now;
                    }
                    else
                    {
                        objDispute.Checker_Input = useraction;
                        objDispute.CheckerComment = comment;
                        objDispute.CheckerProcess_EndTime = DateTime.Now;
                        objDispute.Final_Status = "Open";
                    }
                }
            }
            else
            {
                var objEjData = db.tbl_EJData.Where(x => x.EJID == Id).FirstOrDefault();
                if (usertype.Trim().ToLower().Equals(Constants.UserGroups.Maker.ToLower()))
                {
                    if (useraction.ToLower() == "hold")
                    {
                        objEjData.Maker_Input = useraction;
                        objEjData.MakerComment_Hold = commenthold;
                        objEjData.MakerHold_Time = DateTime.Now;
                    }
                    else
                    {
                        objEjData.Maker_Input = useraction;
                        objEjData.MakerComment = comment;
                        objEjData.MakerProcess_EndTime = DateTime.Now;
                    }



                }
                else
                {
                    if (useraction.ToLower() == "hold")
                    {
                        objEjData.Checker_Input = useraction;
                        objEjData.CheckerComment_Hold = commenthold;
                        objEjData.CheckerHold_Time = DateTime.Now;
                    }
                    else
                    {
                        objEjData.Checker_Input = useraction;
                        objEjData.CheckerComment = comment;
                        objEjData.CheckerProcess_EndTime = DateTime.Now;
                    }

                }
            }
            db.SaveChanges();

        }

        #endregion-----------------------------------------------------------------------

        #region--------------------ManageList--------------------------------------------
        public static tbl_UserDetail UserDetail(string acoountname)
        {
            BNKModel db = new BNKModel();
            return db.tbl_UserDetail.Where(x => x.AccountName == acoountname).FirstOrDefault();
        }
        public static List<tbl_UserDetail> UserList()
        {
            BNKModel db = new BNKModel();
            return db.tbl_UserDetail.Where(x => !x.UserGroup.Equals(Constants.UserGroups.Admin)).ToList();
        }
        public static void SaveUser(tbl_UserDetail obj)
        {
            //string DashboardURL = Convert.ToString(ConfigurationManager.AppSettings["DashboardURL"]);

            using (BNKModel db = new BNKModel())
            {
                var Check = db.tbl_UserDetail.Where(x => x.AccountName.Equals(obj.AccountName)).FirstOrDefault();
                if (Check == null)
                {
                    db.tbl_UserDetail.Add(obj);
                    db.SaveChanges();
                }
            }
        }
        public static int CheckIfUserExist(string val, string fieldname)
        {
            return 0;
        }
        public static void EditUser(tbl_UserDetail obj, double id)
        {
            BNKModel db = new BNKModel();
            tbl_UserDetail tblobj = db.tbl_UserDetail.Where(x => x.ID == id).FirstOrDefault();
            db.SaveChanges();
        }

        #endregion-----------------------------------------------------------------------

        #region-----------------------------Max Dispute Customer Decrypt--------------------------------
        public static void DecryptMaxDisputeObj(ref MaxComplaintCustomer maxdispute)
        {
            string decryptKey = Convert.ToString(ConfigurationManager.AppSettings["DecryptKey"]);

            maxdispute.AccountNo = (string.IsNullOrEmpty(maxdispute.AccountNo) ? "" : CodeDecrypt(maxdispute.AccountNo, decryptKey));
            maxdispute.CardNo = (string.IsNullOrEmpty(maxdispute.CardNo) ? "" : CodeDecrypt(maxdispute.CardNo, decryptKey));
            maxdispute.Mobile = (string.IsNullOrEmpty(maxdispute.Mobile) ? "" : CodeDecrypt(maxdispute.Mobile, decryptKey));
            maxdispute.Email = (string.IsNullOrEmpty(maxdispute.Email) ? "" : CodeDecrypt(maxdispute.Email, decryptKey));
        }

        #endregion-----------------------------------------------------------------------
    }
}
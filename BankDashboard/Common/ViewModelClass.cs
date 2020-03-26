using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankDashboard.Common
{
    public class ViewModelClass
    {
        public class ViewtblMasterBIN
        {
            public string ProductName { get; set; }
            public string BIN { get; set; }
            public string SUBBIN { get; set; }
            public bool Check { get; set; }
        }
        public class ViewReportSeachDataModel
        {
            public string startDate { get; set; }
            public string endDate { get; set; }
            public string terminaldId { get; set; }
            public string status { get; set; }
            public string reportType { get; set; }
            public string ddlUserList { get; set; }
        }



        public class ViewBalancingModel
        {
            public string terminalID { get; set; }
            public string DeviceID { get; set; }
            public string TerminalType { get; set; }
            public string GLAccountNo { get; set; }
            public string TrasanctionType { get; set; }
            public DateTime? Transaction_Ts { get; set; }
            public string AmountFromT24 { get; set; }
            public string AmontFromEJ { get; set; }
            public string Replenishment { get; set; }
        }
        public class BalancingFormModel
        {
            public string fromdate { get; set; }
            public string todate { get; set; }
            public string terminalId { get; set; }
            public string GlAccoungt { get; set; }
           
        }

        public class ViewTransactionModel
        {
            public string CustomerName { get; set; }
            public double EJID { get; set; }
            public string FeedBackId { get; set; }
            public string RegMobileNumber { get; set; }
            public string RegEmailId { get; set; }
            public string CardNumber { get; set; }
            public string AccountNumber { get; set; }
            public string TransactionType { get; set; }
            public DateTime? Recordts { get; set; }
            public string Amount { get; set; }

            public string Counterfeit_Amount { get; set; }
            public string Cash_Retracted_Amount { get; set; }
            public string TerminalID { get; set; }
            public string DeviceId { get; set; }
            public string User_Action { get; set; }
            public string User_Recommendation { get; set; }
            public string RepeatedClaimNo { get; set; }
            public string Maker_Input { get; set; }
            public string Checker_Input { get; set; }
            public string BotStatus { get; set; }
            public string MakerComment { get; set; }
            public string MakerComment_Hold { get; set; }
            public string CheckerComment { get; set; }
            public string CheckerComment_Hold { get; set; }
            public string ATMBalancingAmount { get; set; }
            public string CDMBalancingAmount { get; set; }
            public List<string> T24ImageUrls { get; set; }
            public List<string> WeCareImageUrls { get; set; }
            public string IsLocked { get; set; }
            public string SubmitEnable { get; set; }
        }

        public class selectPages
        {
            public string PageName { get; set; }
            public string groupname { get; set; }
            public string groupdesc { get; set; }
        }

        //public class ViewUserDetail
        //{
        //    public string UserName { get; set; }
        //    public string GroupName { get; set; }
        //    public string GroupPages { get; set; }
        //}
    }
}
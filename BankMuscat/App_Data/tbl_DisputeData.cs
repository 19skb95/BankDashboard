//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BankMuscat.App_Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_DisputeData
    {
        public long ID { get; set; }
        public long FeedbackId { get; set; }
        public string CustomerName { get; set; }
        public string RegMobileNumber { get; set; }
        public string RegEmailId { get; set; }
        public string CIF_Number { get; set; }
        public string AccountNumber { get; set; }
        public string CardNumber { get; set; }
        public string Comment_Eng { get; set; }
        public Nullable<System.DateTime> Incident_Date { get; set; }
        public string Amount { get; set; }
        public string Device_Number { get; set; }
        public string Device_Type { get; set; }
        public string TransactionType { get; set; }
        public Nullable<System.DateTime> EntryTime { get; set; }
        public Nullable<int> BotIdEntry { get; set; }
        public Nullable<int> BotIdProcess { get; set; }
        public Nullable<System.DateTime> BotProcess_StartTime { get; set; }
        public Nullable<System.DateTime> BotProcess_EndTime { get; set; }
        public string Bot_Status { get; set; }
        public string Bot_Remarks { get; set; }
        public string User_Action { get; set; }
        public string User_Recommendation { get; set; }
        public Nullable<long> EJID { get; set; }
        public string Maker_Input { get; set; }
        public string Checker_Input { get; set; }
        public Nullable<int> BotIdWC { get; set; }
        public Nullable<System.DateTime> BotWCProcess_StartTime { get; set; }
        public Nullable<System.DateTime> BotWCProcess_EndTime { get; set; }
        public string BotWC_Status { get; set; }
        public string Final_Status { get; set; }
        public string SolvedBy { get; set; }
        public Nullable<int> MakerUserId { get; set; }
        public Nullable<System.DateTime> MakerProcess_StartTime { get; set; }
        public Nullable<System.DateTime> MakerProcess_EndTime { get; set; }
        public Nullable<int> CheckerUserId { get; set; }
        public Nullable<System.DateTime> CheckerProcess_StartTime { get; set; }
        public Nullable<System.DateTime> CheckerProcess_EndTime { get; set; }
        public string MakerComment { get; set; }
        public string CheckerComment { get; set; }
        public Nullable<System.DateTime> MakerHold_Time { get; set; }
        public string MakerComment_Hold { get; set; }
        public Nullable<System.DateTime> CheckerHold_Time { get; set; }
        public string CheckerComment_Hold { get; set; }
    }
}

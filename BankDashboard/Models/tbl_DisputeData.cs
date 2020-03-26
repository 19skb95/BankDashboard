namespace BankDashboard.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_DisputeData
    {
        public long ID { get; set; }

        public long FeedbackId { get; set; }

        [StringLength(250)]
        public string CustomerName { get; set; }

        [StringLength(500)]
        public string RegMobileNumber { get; set; }

        [StringLength(500)]
        public string RegEmailId { get; set; }

        [StringLength(20)]
        public string CIF_Number { get; set; }

        [StringLength(500)]
        public string AccountNumber { get; set; }

        [StringLength(500)]
        public string CardNumber { get; set; }

        [StringLength(500)]
        public string Comment_Eng { get; set; }

        public DateTime? Incident_Date { get; set; }

        [StringLength(20)]
        public string Amount { get; set; }

        [StringLength(20)]
        public string Device_Number { get; set; }

        [StringLength(20)]
        public string Device_Type { get; set; }

        [StringLength(100)]
        public string TransactionType { get; set; }

        public DateTime? EntryTime { get; set; }

        public int? BotIdEntry { get; set; }

        public int? BotIdProcess { get; set; }

        public DateTime? BotProcess_StartTime { get; set; }

        public DateTime? BotProcess_EndTime { get; set; }

        [StringLength(20)]
        public string Bot_Status { get; set; }

        [StringLength(200)]
        public string Bot_Remarks { get; set; }

        [StringLength(20)]
        public string User_Action { get; set; }

        [StringLength(250)]
        public string User_Recommendation { get; set; }

        public long? EJID { get; set; }

        [StringLength(20)]
        public string Maker_Input { get; set; }

        [StringLength(20)]
        public string Checker_Input { get; set; }

        public int? BotIdWC { get; set; }

        public DateTime? BotWCProcess_StartTime { get; set; }

        public DateTime? BotWCProcess_EndTime { get; set; }

        [StringLength(20)]
        public string BotWC_Status { get; set; }

        [StringLength(20)]
        public string Final_Status { get; set; }

        [StringLength(20)]
        public string SolvedBy { get; set; }

        public string MakerUserId { get; set; }

        public DateTime? MakerProcess_StartTime { get; set; }

        public DateTime? MakerProcess_EndTime { get; set; }

        public string CheckerUserId { get; set; }

        public DateTime? CheckerProcess_StartTime { get; set; }

        public DateTime? CheckerProcess_EndTime { get; set; }

        [StringLength(500)]
        public string MakerComment { get; set; }

        [StringLength(500)]
        public string CheckerComment { get; set; }

        public DateTime? MakerHold_Time { get; set; }

        [StringLength(500)]
        public string MakerComment_Hold { get; set; }

        public DateTime? CheckerHold_Time { get; set; }

        [StringLength(500)]
        public string CheckerComment_Hold { get; set; }
    }
}

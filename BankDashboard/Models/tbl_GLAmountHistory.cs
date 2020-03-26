namespace BankDashboard.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_GLAmountHistory
    {
        public long ID { get; set; }

        [StringLength(20)]
        public string GLNumber { get; set; }

        [StringLength(20)]
        public string TerminalID { get; set; }

        [StringLength(10)]
        public string TerminalType { get; set; }

        public DateTime? Transaction_TS { get; set; }

        [StringLength(20)]
        public string TotalApproveAmount { get; set; }

        [StringLength(10)]
        public string CountApproveTrans { get; set; }

        public int? BotId { get; set; }

        public DateTime? BotEntry_TS { get; set; }
    }
}

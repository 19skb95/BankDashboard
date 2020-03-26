namespace BankDashboard.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_EJLogsHistory
    {
        public long ID { get; set; }

        [StringLength(20)]
        public string TerminalID { get; set; }

        [StringLength(20)]
        public string DeviceId { get; set; }

        public DateTime? Transaction_TS { get; set; }

        [StringLength(100)]
        public string TransactionType { get; set; }

        [StringLength(20)]
        public string TotalAmount { get; set; }

        [StringLength(20)]
        public string CountTotalTrans { get; set; }

        [StringLength(20)]
        public string TotalApproveAmount { get; set; }

        [StringLength(20)]
        public string CountApproveTrans { get; set; }

        [StringLength(20)]
        public string TotalErroneousAmount { get; set; }

        [StringLength(50)]
        public string CountErroneousTrans { get; set; }

        [StringLength(20)]
        public string BalancingAmount { get; set; }

        public int? BotId { get; set; }

        public DateTime? BotEntry_TS { get; set; }

        [StringLength(10)]
        public string Replenishment { get; set; }
    }
}

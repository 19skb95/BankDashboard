namespace BankDashboard.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GLAccountList_Production
    {
        [Key]
        [Column(Order = 0)]
        public long ID { get; set; }

        [StringLength(20)]
        public string TerminalID { get; set; }

        [StringLength(10)]
        public string TerminalType { get; set; }

        [StringLength(20)]
        public string GLNumber { get; set; }

        public DateTime? EntryTime { get; set; }

        [StringLength(100)]
        public string EntryBy { get; set; }

        public DateTime? UpdateTime { get; set; }

        [StringLength(100)]
        public string UpdateBy { get; set; }

        [Key]
        [Column(Order = 1)]
        public bool IsActive { get; set; }
    }
}

namespace BankDashboard.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_MasterErrorCode
    {
        public long ID { get; set; }

        [StringLength(10)]
        public string Make { get; set; }

        [StringLength(10)]
        public string Machine_Type { get; set; }

        [StringLength(255)]
        public string Error_Code { get; set; }

        [StringLength(255)]
        public string Error_Description { get; set; }

        public DateTime? EntryTime { get; set; }

        [StringLength(100)]
        public string EntryBy { get; set; }

        public DateTime? UpdateTime { get; set; }

        [StringLength(100)]
        public string UpdateBy { get; set; }

        public bool IsActive { get; set; }

        [StringLength(10)]
        public string ApproveStat { get; set; }
    }
}

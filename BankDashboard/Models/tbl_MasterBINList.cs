namespace BankDashboard.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_MasterBINList
    {
        public long ID { get; set; }

        [StringLength(200)]
        public string ProductName { get; set; }

        [StringLength(10)]
        public string BIN { get; set; }

        [StringLength(10)]
        public string SUBBIN { get; set; }

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

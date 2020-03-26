using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankDashboard.Models
{
    public partial class tbl_GroupRoleMappingMaster
    {
        public int ID { get; set; }
        public string GroupName { get; set; }      
        public DateTime? EntryTime { get; set; }
        public string EntryBy { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string UpdateBy { get; set; }
        public bool IsActive { get; set; }
        public string ApproveStat { get; set; }
        public string PageName { get; set; }


    }

}
namespace BankDashboard.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_UserDetail
    {
        public int ID { get; set; }

        public string AccountName { get; set; }

        public string UserGroup { get; set; }

        public string PWD { get; set; }
        public string GroupPages { get; set; }

    }
}

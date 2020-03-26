namespace BankDashboard.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Configuration;
    using System.Security.Cryptography;
    using System.Text;

    public partial class BNKModel : DbContext
    {
        public BNKModel()
            : base(GetSqlConnection())
        {
        }

        public virtual DbSet<tbl_BotMaster> tbl_BotMaster { get; set; }
        public virtual DbSet<tbl_DisputeData> tbl_DisputeData { get; set; }
        public virtual DbSet<tbl_EJData> tbl_EJData { get; set; }
        public virtual DbSet<tbl_EJLogsHistory> tbl_EJLogsHistory { get; set; }
        public virtual DbSet<tbl_GLAccountList> tbl_GLAccountList { get; set; }
        public virtual DbSet<tbl_GLAmountHistory> tbl_GLAmountHistory { get; set; }
        public virtual DbSet<tbl_MasterBINList> tbl_MasterBINList { get; set; }
        public virtual DbSet<tbl_MasterDeviceList> tbl_MasterDeviceList { get; set; }
        public virtual DbSet<tbl_MasterErrorCode> tbl_MasterErrorCode { get; set; }
        public virtual DbSet<tbl_UserDetail> tbl_UserDetail { get; set; }
        public virtual DbSet<GLAccountList_Production> GLAccountList_Production { get; set; }
        public virtual DbSet<MasterDeviceList_Production> MasterDeviceList_Production { get; set; }
        public virtual DbSet<tbl_MasterBINList_Production> tbl_MasterBINList_Production { get; set; }

        public virtual DbSet<tbl_GroupRoleMappingMaster> tbl_GroupRoleMappingMaster { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
        public static string GetSqlConnection()
        {
            string x = ConfigurationManager.AppSettings["getstr"].ToString();
            byte[] inputArray = Convert.FromBase64String(x);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes("sblw-3hn8-sqoy19");
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            //string sty= UTF8Encoding.UTF8.GetString(resultArray);
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
}

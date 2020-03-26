using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankDashboard.Common
{
    public class Constants
    {
        readonly public static string SaveStatus = "0000";
        readonly public static string ApproveSaveStatus = "00001";
        readonly public static string ModifyStatus = "0005";
        readonly public static string ApproveModifyStatus = "0006";

        public static class UserGroups
        {
            readonly public static string Admin = "RPA-IT Support Team";
            readonly public static string Checker = "RPA-Recon Checker";
            readonly public static string Maker = "RPA-Recon Maker";
            readonly public static string ParameterManager = "Parameter Management Team";
            readonly public static string ApplicationUserManagement = "Application User Management";
        }
    }
}
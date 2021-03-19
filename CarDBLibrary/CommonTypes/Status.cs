using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarDBLibrary.CommonTypes
{
    public static class Status
    {
        public const string Active = "Active";
        public const string Deactive = "Deactive";
        public const string Updating = "Updating";
        private static bool CheckStatus(string standartStatus, string checkedStatus) 
        {
            ValidStatus(checkedStatus);
            return standartStatus == checkedStatus;
        }
        public static bool IsActive(string status)
        {
            return CheckStatus(Status.Active, status);
        }
        public static bool IsDeactive(string status)
        {
            return CheckStatus(Status.Deactive, status);
        }
        public static bool IsUpdating(string status)
        {
            return CheckStatus(Status.Updating, status);
        }
        private static void ValidStatus(string status)
        {
            if (!new string[] 
            {
                Status.Active,
                Status.Deactive,
                Status.Updating
            }.Contains(status))
            {
                throw new Exception("Неизвестный статус " + status);
            }
        }
       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace imidro.Models
{
    public class Result
    {
        public int Status { get; set; }
        public string Discription { get; set; }

        public string ErrorText { get; set; }

        public string Title { get; set; }
        public string Response { get; set; }
        public string ErrorCode { get; set; }

        public Result(int status, string discription, string error, string title, string errorCode)
        {
            Status = status;
            Discription = discription;
            ErrorText = error;
            Title = title;
            ErrorCode = errorCode;
        }


        public enum StatusList
        {
            ok,
            failed,
            FolderNotFound,
            inaccess,
            connectionError,
            unauthorized
        }


        public static class DiscriptionList
        {
            public static string Ok = "عملیات شما با موفقیت انجام شد .";
            public static string failed = "  متاسفانه عملیات اجرا نشد لطفا دوباره امتحان کنید .";
            public static string FolderNotFound = " متاسفانه فولدر یافت نشد .";
            public static string inaccess = " متاسفانه شما به این صفحه دسترسی ندارید .";
            public static string connectionError = " متاسفانه مشکلی در اتصال بوجود امده.";
            public static string unauthorized = " توکن شما منقضی شده است.";
        }

        public static class TitleList
        {
            public const string ok = "ok";
            public const string failed = "failed";
            public const string FolderNotFound = "FolderNotFound";
            public const string inaccess = "inaccess";
            public const string connectionError = "connectionError";
            public const string unauthorized = "unauthorized";
        }

        public static string GenerateResult(string val)
        {
            string returnValue = "";
            switch (val)
            {
                case TitleList.ok:
                    returnValue = DiscriptionList.Ok;
                    break;

                case TitleList.failed:
                    returnValue = DiscriptionList.failed;
                    break;

                case TitleList.FolderNotFound:
                    returnValue = DiscriptionList.FolderNotFound;
                    break;
                case TitleList.inaccess:
                    returnValue = DiscriptionList.inaccess;
                    break;
                case TitleList.connectionError:
                    returnValue = DiscriptionList.connectionError;
                    break;
                case TitleList.unauthorized:
                    returnValue = DiscriptionList.unauthorized;
                    break;
                default:
                    break;
            }

            return returnValue;
        }

        public static string GenerateMessage(bool val)
        {
            string returnValue = "";
            switch (val)
            {
                case true:
                    returnValue = DiscriptionList.Ok;
                    break;

                case false:
                    returnValue = DiscriptionList.failed;
                    break;
            }

            return returnValue;
        }

    }
}

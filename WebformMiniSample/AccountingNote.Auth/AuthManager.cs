﻿using AccountingNote.DBSource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AccountingNote.Auth
{
    /// <summary>
    /// 負責處理登入的元件
    /// </summary>
    public class AuthManager
    {
        /// <summary>
        /// 檢查目前是否登入
        /// </summary>
        /// <returns></returns>
        public static bool IsLogined()
        {

            if (HttpContext.Current.Session["UserLoginInfo"] == null)
                return false;
            else
                return true;
        }
        /// <summary>
        /// 取得以登入的使用者資訊(若沒有登入則回傳null)
        /// </summary>
        /// <returns></returns>
        public static UserInfoModel GetCurrentUser()
        {
            string account = HttpContext.Current.Session["UserLoginInfo"] as string;

            if (account == null)
                return null;

            //DataRow dr = UserInfoManger.GetUserInfoByAccount(account);
            var userInfo = UserInfoManger.GetUserInfoByAccount(account);

            if (userInfo == null)
            {
                HttpContext.Current.Session["UserLoginInfo"] = null;
                return null;
            }

            UserInfoModel model = new UserInfoModel();
            model.ID = userInfo.ID;
            model.Account = userInfo.Account;
            model.Name = userInfo.Name;
            model.Email = userInfo.Email;
            model.MobilePhone = userInfo.MobilePhone;

            return model;
        }
        /// <summary>
        /// 清除登入(登出)
        /// </summary>
        public static void Logout()
        {
            HttpContext.Current.Session["UserLoginInfo"] = null;
        }
        /// <summary>
        /// 嘗試登入
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public static bool TryLogin(string account, string pwd, out string errorMsg)
        {
            //check empty
            if (string.IsNullOrWhiteSpace(account) || string.IsNullOrWhiteSpace(pwd))
            {
                errorMsg = "帳號及密碼為必填";
                return false;
            }
            //read db and check
            var userInfo = UserInfoManger.GetUserInfoByAccount(account);

            //check null
            if (userInfo == null)
            {
                errorMsg = $"{account}不存在";
                return false;
            }

            //check account / pwd
            if (string.Compare(userInfo.Account, account, true) == 0 &&
                string.Compare(userInfo.PWD, pwd, false) == 0)
            {
                HttpContext.Current.Session["UserLoginInfo"] = userInfo.Account;
                errorMsg = string.Empty;
                return true;
            }
            else
            {
                errorMsg = "登入失敗，請檢查帳號及密碼";
                return false;

            }
        }
    }
}

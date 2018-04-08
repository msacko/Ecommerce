using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Takadam.Models;

namespace Takadam.Tools
{
    public class Authentication
    {

        #region [ PUBLIC PROPERTIES ]

        public static Boolean IsAuthenticated
        {
            get
            {
                if (UserIdentity != null)
                {
                    return UserIdentity.IsAuthenticated;
                }
                return false;
            }
        }

        public static String UserName
        {
            get
            {
                if (UserIdentity != null)
                {
                    return UserIdentity.Name;
                }
                return "";
            }
        }

        public static int UserId
        {
            get
            {

                String[] userIdPersId = UserData.Split('|');
                return Int32.Parse(userIdPersId[0]);

            }
        }

        public static string Token
        {
            get
            {
                String[] userIdPersId = UserData.Split('|');
                if (userIdPersId.Length > 1)
                {
                    return (userIdPersId[1]);
                }
                return string.Empty;
            }
        }

        public static string LastName
        {
            get
            {
                String[] userIdPersId = UserData.Split('|');
                if (userIdPersId.Length > 2)
                {
                    return userIdPersId[2];
                }
                return string.Empty;
            }
        }

        public static Boolean IsAdmin
        {
            get
            {
                if (!IsAuthenticated)
                {
                    return false;
                }
                String[] userIdPersId = UserData.Split('|');
                if (userIdPersId.Length > 3)
                {
                    if (Int32.Parse(userIdPersId[3]) == 1)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        private static DateTime? _dateFrom = null;
        public static int GetMinFromStartSession
        {
            get
            {
                if (_dateFrom == null)
                {
                    _dateFrom = FormAutTicket.IssueDate;
                }
                DateTime dateNow = DateTime.Now;
                TimeSpan ts = dateNow - (DateTime)((object)(_dateFrom));

                return ts.Minutes;
            }
        }

        public static void ResetMinFromStartSession(Boolean pSetToNull = false)
        {
            if (pSetToNull) _dateFrom = null;
            else _dateFrom = DateTime.Now;
        }

        #endregion
        #region [ PRIVATE PROPERTIES ]

        private static FormsAuthenticationTicket FormAutTicket
        {
            get
            {
                if (UserIdentity == null) return null;
                return UserIdentity.Ticket;
            }
        }

        private static FormsIdentity UserIdentity
        {
            get
            {
                if (HttpContext.Current.User != null && HttpContext.Current.User.Identity.AuthenticationType == "Forms")
                {
                    return (FormsIdentity)HttpContext.Current.User.Identity;
                }
                return null;
            }
        }

        private static String UserData
        {
            get
            {
                if (FormAutTicket == null) return "";
                return FormAutTicket.UserData.ToString();
            }
        }

        #endregion

        #region [ METHODS ]

        public static Member CurrentUser
        {
            get
            {
                if (HttpContext.Current.Session["CurrentUser"] == null)
                {
                    //var m = member.GetById(UserId);
                    Member m = null;
                    HttpContext.Current.Session["CurrentUser"] = m;
                    return m;
                }
                return (Member)HttpContext.Current.Session["CurrentUser"];
            }
        }

        public static Member LogOn(String pUsername, String pPsw)
        {

            //member userObj = member.GetByUsernameAndPassword(pUsername, pPsw);
            Member userObj = null;

            //The locationID is required
            if (userObj != null)
            {
                HttpContext.Current.Session.RemoveAll();
                int userId = userObj.id;
                int admin = 1;
                string token = Guid.NewGuid().ToString();
                FormsAuthenticationTicket tkt = new FormsAuthenticationTicket(1, pUsername, DateTime.Now, DateTime.Now.AddYears(2), false, userId + "|" + token + "|" + DateTime.Now + "|" + admin, FormsAuthentication.FormsCookiePath);
                //Encrypt the ticket.
                String encTicket = FormsAuthentication.Encrypt(tkt);

                //Create the cookie.
                HttpCookie httpCoo = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                httpCoo.Expires = DateTime.Now.AddYears(1);
                //jme added for security
                httpCoo.HttpOnly = true;
                HttpContext.Current.Response.Cookies.Add(httpCoo);

                return userObj;
            }
            return userObj;
        }

        public static void LogOff()
        {
            FormsAuthentication.SignOut();
            HttpContext.Current.Session.Abandon();

            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            cookie1.HttpOnly = true;
            HttpContext.Current.Response.Cookies.Add(cookie1);

            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            HttpContext.Current.Response.Cache.SetNoStore();
            HttpContext.Current.Response.AppendHeader("Pragma", "no-cache");

            // clear session cookie (not necessary for your current problem but i would recommend you do it anyway) 
            HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId", "");
            cookie2.HttpOnly = true;
            cookie2.Expires = DateTime.Now.AddYears(-1);
            HttpContext.Current.Response.Cookies.Add(cookie2);
        }
        /*
        public static DataTable GetAuthenticationReport(DateTime? pDateToDisplayed)
        {
            //using (JTIGlobalEntities db = new JTIGlobalEntities()) {
            //IQueryable<DataTable> dt = db.ExecuteStoreQuery<DataTable>("GetReportLogAuthentication", pDateToDisplayed).AsQueryable();
            SqlMng db = new SqlMng();
            db.SetSPCmd("[GetReportLogAuthentication]");
            db.AddInputParameter("@DateToDisplayed", SqlDbType.DateTime, pDateToDisplayed);

            return db.GetDataTable();
            //}
        }
        
        */

        /*
        #region [ DIGEST ]

        public static String GetPasswordToStored(String pPassword, String pSaltValue)
        {
            if (pSaltValue == null || pSaltValue.Length < 15) return "";
            return GenerateDigest(pPassword, pSaltValue, pSaltValue.Substring(0, 15));
        }

        public static String GenerateDigest(String pUserName, String pNonce)
        {
            return GenerateDigest(pUserName, pNonce, ConfigMng.GetRequiredSetting("SSO_SecretKey"));
        }

        public static String GenerateDigest(String pUserName, String pNonce, String pSecretKey)
        {

            //String secret = "aksdjskajd99230";
            System.Text.ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] secretByte = encoding.GetBytes(pSecretKey);

            String userAndNonce = pUserName + pNonce;

            HMACSHA1 hmacsha1 = new HMACSHA1(secretByte);
            byte[] userNameBytes = encoding.GetBytes(userAndNonce);
            byte[] digestBytes = hmacsha1.ComputeHash(userNameBytes);

            return Convert.ToBase64String(digestBytes);

            //"https://demo.service-now.com?user=jme&nonce=12345678901234567890&hash=" + digestBytes
        }
        */
        public static String GetRandomNonceValue()
        {
            String val = "";
            Random r = new Random();
            for (int i = 0; i < 20; i++)
            {
                val += r.Next(0, 9).ToString();
            }
            return val;
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Workday.Common;
using Workday.DataAccess;

namespace Workday.Business
{
    public class UserBusiness
    {
        public static User GetUserById(int userId)
        {
            return UserDataAccess.GetUserById(userId);
        }

        public static User GetUserByName(string name)
        {
            return UserDataAccess.GetUserByName(name);
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static User AddUser(User user)
        {
            return UserDataAccess.AddUser(user);
        }

        /// <summary>
        /// 验证用户，用于登录
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public static User Verify(string name, string password, out string errorMsg,out string isAdmin,out bool isFLogin)
        {
            errorMsg = null;
            isAdmin = null;
            isFLogin = false;
            password = User.GenerateHash(password);
            User user = GetUserByName(name);

            if (user == null)
            {
                errorMsg = "invalid account";
            }
            else if (user.Password != password)
            {
                user = null;
                errorMsg = "password error";
            }
            else if (user.Status != UserStatus.Normal)
            {
                user = null;
                errorMsg = "this user has been disabled!";
            }
            else if (user.LoginDate == null)
            {
                isFLogin = true;
            }

            if (user != null)
            {
                if (user.IsAdmin == UserIsAdmin.IsAdmin)
                {
                    isAdmin = user.IsAdmin.ToString();
                }
            }

            return user;
        }

        public static AllUser GetAllUsers(AllUser users)
        {
            return UserDataAccess.GetAllUser(users);
        }

        public static bool ChangeUserStatus(int userid,int currentstatus)
        {
            return UserDataAccess.ChangeUserStatus(userid, currentstatus);
        }

        public static bool ChangeDefaultPassword(User user)
        {
            return UserDataAccess.ChangeDefaultPassword(user);
        }

        public static Dictionary<int, string> GetSecretQuestion()
        {
            Dictionary<int, string> questionlist = new Dictionary<int, string>();
            SecretQuestion temp = new SecretQuestion();
            temp=UserDataAccess.GetSecretQuestion();
            questionlist = temp.QuestionList;
            return questionlist;
        }
    }

}

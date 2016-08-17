using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Security.Cryptography;

namespace Workday.Common
{
    // to show users info on gridview
    public interface IShowUsers
    {
         int UserId { get; set; }

         string Email { get; set; }

         string UserName { get; set; }

         UserStatus Status { get; set; }

         string StatusString { get; set; }

         int DeptId { get; set; }

         string DeptName { get; set; }

         UserIsAdmin IsAdmin { get; set; }

         DateTime CreateDate { get; set; }
    }

    public class User
    {
        public int UserId { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int DeptId { get; set; }

        public UserStatus Status { get; set; }

        public UserIsAdmin IsAdmin { get; set; }

        public DateTime CreateDate { get; set; }

        public Nullable<DateTime> LoginDate { get; set; }

        public int SecretQuestion { get; set; }

        public string SecretAnswer { get; set; }

        public static string GenerateHash(string value)
        {
           
            var salt = System.Text.Encoding.UTF8.GetBytes("qiuxt");
            var password = System.Text.Encoding.UTF8.GetBytes(value);
            var hmacSHA1 = new HMACSHA1(salt);
            var saltedHash = hmacSHA1.ComputeHash(password);
            string result = Convert.ToBase64String(saltedHash);
            return result;

        }

     }


    public class AllUser
    {
        public DataSet AllUserSet { get; set; }
    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Workday.Web
{
    public partial class Timesheet1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = @"D:\personal\My Activity\2016 Seattle life\20160514 study coding\Workday1\Web\636050514245992797.png";
            //string path = @"D:\personal\My Activity\2016 Seattle life\20160514 study coding\Workday1\Web\636050562721935462.png";
            
            Byte[] imgcontent = System.IO.File.ReadAllBytes(path);
            string base64string = Convert.ToBase64String(imgcontent, 0, imgcontent.Length);
            string imageurl = @"data:image/png;base64," + base64string;
            Image1.Visible = true;
            Image1.ImageUrl = imageurl;
        }

        // 需要标识为WebMethod  
        [System.Web.Services.WebMethod]
        // 注意，要让前台调用的方法，一定要是public和static的  
        public static string Hello(string name)
        {
            return "Hello:" + name;
        }
    }
    }

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Workday.Business;

namespace Workday.Web
{
    public partial class TimeSheet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //just for debug, reader from db and show image.
            //Common.TimeSheet newsheet = new Common.TimeSheet();
            //newsheet.TimeSheetId = new Guid("3EA9B686-B3DA-4C4A-B758-1EE28FCFC544");
            //Byte[] imgcontent = TimeSheetBusiness.GetStartImageByID(newsheet);
            //string base64string = Convert.ToBase64String(imgcontent, 0, imgcontent.Length);
            //string imageurl = @"data:image/png;base64," + base64string;
            //capresult.ImageUrl = imageurl;

            //just for debug. reponse front end javascript upload method ajax request.
            //if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            //{
            //    string image = Request.Form["image"];
            //    string filename = DateTime.Now.ToString();
            //    if (image != null & image != "")
            //    {
            //        var parts = image.Split(new char[] { ',' }, 2);
            //        var bytes = Convert.FromBase64String(parts[1]);
            //        var path = HttpContext.Current.Server.MapPath(string.Format("~/{0}.png", filename));
            //        System.IO.File.WriteAllBytes(path, bytes);
            //        //Byte[] imgcontent = System.IO.File.ReadAllBytes(path);
            //        //string base64string = Convert.ToBase64String(imgcontent, 0, imgcontent.Length);
            //        //string imageurl = @"data:image/png;base64," + base64string;
            //        //Response.Write(imageurl);
            //        //capresult.ImageUrl = imageurl;

            //    }
            //}
        }

        [WebMethod(EnableSession = true)]
        public static string Upload(string base64)
        {
            var parts = base64.Split(new char[] { ',' }, 2);
            var bytes = Convert.FromBase64String(parts[1]);
            DateTime now = DateTime.Now;
            string Today = now.ToString("MM/dd/yyyy");
            String Time = now.ToString("HH:mm:ss");
            string IP = HttpContext.Current.Request.UserHostAddress;
            bool ifaddtime = false;

            if (HttpContext.Current.Session["UserID"] != null)
            {
                int userid = Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString());
                Common.TimeSheet newsheet = new Common.TimeSheet();
                newsheet.UserId = userid;
                newsheet.Date = Today;
                newsheet.StartTime = Time;
                newsheet.StartIp = IP;
                newsheet.StartImage = bytes;
                newsheet.TimeSheetId = Guid.NewGuid();

                ifaddtime = TimeSheetBusiness.AddTimeSheet(newsheet);

                if (ifaddtime)
                {
                    Byte[] imgcontent = TimeSheetBusiness.GetStartImageByID(newsheet);
                    string base64string = Convert.ToBase64String(imgcontent, 0, imgcontent.Length);
                    string imageurl = @"data:image/png;base64," + base64string;
                    return imageurl;
                }
            }
            return "Fail";

            //just for debug, write image to file
            //var path = HttpContext.Current.Server.MapPath(string.Format("~/{0}.png", now.ToString("MM-dd-yyyy-HH-mm-ss")));
            //System.IO.File.WriteAllBytes(path, bytes);
            //return "success!";

        }

        //just for debug
        [WebMethod]
        public static string hello(string a)
        {
            return "you are: "+a;
        }
    }
}
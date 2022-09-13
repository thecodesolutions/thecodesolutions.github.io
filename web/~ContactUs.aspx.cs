using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.IO;
using System.Net.Mail;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;


public partial class ContactUs : System.Web.UI.Page
{
	static string tempDirPathGlobal = "";
	
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string SendMailtoAdmin(string name, string email, string message)
    {

        var oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        int success = 0;
        try
        {
            string URL = HttpContext.Current.Request.UserHostAddress;
            // if (URL.Contains("http://enabling.systems") || URL.Contains("http://www.enabling.systems"))
            {
                string ip = GetIPAddress();
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                mail.From = new MailAddress(ConfigurationManager.AppSettings["FROM"], name);


                mail.Subject = "Enabling Systems Website";
                mail.Body = message + "<br/><br/>Sender: " + name + " &lt;" + email + "&gt; " + "<br/><br/> User IP: " + ip;
                mail.To.Add(ConfigurationManager.AppSettings["ApproachMeEamil"]);
                mail.ReplyTo = new MailAddress(email, name);
                mail.Bcc.Add(ConfigurationManager.AppSettings["BCC"]);
                mail.Priority = MailPriority.High;
                mail.IsBodyHtml = true;

                Amazon.SES.Manager.Send(mail);

                Object o = new
                {
                    success = success
                };
                return oSerializer.Serialize(o);
            }
            //else
            //{
            //    return "";
            //}
        }
        catch (Exception e)
        {
            success = 0;
            throw e;
        }
    }
	[WebMethod]
	[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
	public static string UploadCv()
        {
            var oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            int success = 0;
            try
            {

                var file = Request.Files[0];
                var fileName = file.FileName;
                if (!Request.Files.AllKeys.Any() || file == null || file.ContentLength < 1) return "error";
                string message = "Please find the attached document";
                string name = "Enabling System Website";
                string email = "info@enabling.systems";
                var config = ConfigurationManager.AppSettings;
                string defaultPath = Server.MapPath(config["BUILDS_TEMP_DIRECTORY"]);
                Int32 timeStamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

                // Temp directory path with timestamp for unique path for each build
                string tempPath = Path.Combine(defaultPath, timeStamp.ToString());
                string tempDirPath = savetoLocal(file);
                //mail
                string ip = GetIPAddress();
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

                mail.From = new MailAddress(ConfigurationManager.AppSettings["FROM"], name);
                mail.Subject = "Enabling Systems Website";
                mail.Body = message + "<br/><br/> User IP: " + ip;
                mail.To.Add(ConfigurationManager.AppSettings["ApproachMeEamil"]);
                mail.ReplyTo = new MailAddress(ConfigurationManager.AppSettings["FROM"], name);
                mail.Bcc.Add(ConfigurationManager.AppSettings["BCC"]);
                mail.Priority = MailPriority.High;
                mail.IsBodyHtml = true;
                mail.Attachments.Add(new System.Net.Mail.Attachment(tempDirPath));
                //send
                Amazon.SES.Manager.Send(mail);
                //System.IO.Directory.Delete(tempDirPathGlobal, true);
                Object o = new
                {
                    success = success
                };
                return oSerializer.Serialize(o);
            }
            catch (Exception e)
            {
                success = 0;
                throw e;
            }
        }
        public static string savetoLocal(HttpPostedFileBase file)
        {
            var config = ConfigurationManager.AppSettings;
            string defaultPath = config["BUILDS_TEMP_DIRECTORY"];
            Int32 timeStamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            string tempPath = Path.Combine(defaultPath, timeStamp.ToString());
            string mappedTempPath = Server.MapPath(tempPath);
            if (!System.IO.Directory.Exists(mappedTempPath))
            {
                System.IO.Directory.CreateDirectory(mappedTempPath);
            }
            string buildPath = Path.Combine(tempPath, file.FileName.Replace(" ", ""));
            buildPath = Server.MapPath(buildPath);
            file.SaveAs(buildPath);
            tempDirPathGlobal = mappedTempPath;
            return buildPath;
        }
		
    protected static string GetIPAddress()
    {
        System.Web.HttpContext context = System.Web.HttpContext.Current;
        string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (!string.IsNullOrEmpty(ipAddress))
        {
            string[] addresses = ipAddress.Split(',');
            if (addresses.Length != 0)
            {
                return addresses[0];
            }
        }

        return context.Request.ServerVariables["REMOTE_ADDR"];
    }

}
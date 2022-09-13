using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class ContactUs : System.Web.UI.Page
{
    static string tempDirPathGlobal = "";

    protected void Page_Load(object sender, EventArgs e)
    {
    }
    [WebMethod]
    public static string UploadCv()
    {
        try
        {
            HttpPostedFile file = HttpContext.Current.Request.Files[0];
            string fileName = file.FileName;
            WriteLog(fileName, "Log.txt");
            if (HttpContext.Current.Request.Files.AllKeys.Length > 0 || file == null || file.ContentLength < 1) return "error";
            string message = "Please find the attached document";
            string name = "Enabling System Website";
            string email = "info@enabling.systems";
            NameValueCollection config = ConfigurationManager.AppSettings;
            string defaultPath = HttpContext.Current.Server.MapPath(config["BUILDS_TEMP_DIRECTORY"]);
            Int32 timeStamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            // Temp directory path with timestamp for unique path for each build
            string tempPath = Path.Combine(defaultPath, timeStamp.ToString());
            //string tempDirPath = savetoLocal(file);
            //string defaultPath = config["BUILDS_TEMP_DIRECTORY"];
            //Int32 timeStamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            //string tempPath = Path.Combine(defaultPath, timeStamp.ToString());
            string mappedTempPath = HttpContext.Current.Server.MapPath(tempPath);
            if (!System.IO.Directory.Exists(mappedTempPath))
            {
                System.IO.Directory.CreateDirectory(mappedTempPath);
            }
            string buildPath = Path.Combine(tempPath, file.FileName.Replace(" ", ""));
            buildPath = HttpContext.Current.Server.MapPath(buildPath);
            file.SaveAs(buildPath);
            tempDirPathGlobal = mappedTempPath;
            string tempDirPath = buildPath;
            // Mail
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

            return "Ok";
        }
        catch (Exception e)
        {
            return e.InnerException.ToString();
        }
    }
    public static string savetoLocal(HttpPostedFile file)
    {
        try
        {

            NameValueCollection config = ConfigurationManager.AppSettings;
            string defaultPath = config["BUILDS_TEMP_DIRECTORY"];
            Int32 timeStamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            string tempPath = Path.Combine(defaultPath, timeStamp.ToString());
            string mappedTempPath = HttpContext.Current.Server.MapPath(tempPath);
            if (!System.IO.Directory.Exists(mappedTempPath))
            {
                System.IO.Directory.CreateDirectory(mappedTempPath);
            }
            string buildPath = Path.Combine(tempPath, file.FileName.Replace(" ", ""));
            buildPath = HttpContext.Current.Server.MapPath(buildPath);
            file.SaveAs(buildPath);
            tempDirPathGlobal = mappedTempPath;
            return buildPath;
        }
        catch (Exception e)
        {
            
            return e.InnerException.ToString();
        }

    }

    public static void WriteLog(string Data, string LogFile)
    {
        try
        {
            NameValueCollection config = ConfigurationManager.AppSettings;
            if (Data != "Thread was being aborted.")
            {
                LogFile = DateTime.Now.Date.ToString("MM-dd-yy") + " " + LogFile;
                string path = HttpContext.Current.Server.MapPath(config["BUILDS_TEMP_DIRECTORY"]) + "\\Log";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string filepath = System.Web.HttpContext.Current.Server.MapPath(".") + "\\Log" + "\\" + LogFile;
                System.IO.File.AppendAllText(filepath, DateTime.Now + ": " + Data + Environment.NewLine);
            }
        }
        catch
        {
        }
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
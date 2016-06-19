using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication6
{
    class Program
    {
        static void Main(string[] args)
        {
         MailMessage myHtmlMessage; 
        SmtpClient mySmtpClient; 

        
        myHtmlMessage = new MailMessage();

        myHtmlMessage.From = new MailAddress("hariom.kuntal@ge.com", "Santosh Thakur");
        //myHtmlMessage.To.Add("asim.bera@citiustech.com");
        //myHtmlMessage.CC.Add("uday.gadamsetty@citiustech.com");
        //myHtmlMessage.Bcc.Add("hariom.kuntal@citiustech.com");
        myHtmlMessage.To.Add("hariom.kuntal@ge.com");
        //myHtmlMessage.To.Add("pravin.mohite@citiustech.com");
   
        myHtmlMessage.Subject = "URGENT: Onsite Requirement";
        myHtmlMessage.Body = ReadFile();

        //myHtmlMessage.Attachments.Add(AddInlineAttachment("AprilFool.jpg"));

        myHtmlMessage.Priority = System.Net.Mail.MailPriority.High;
        myHtmlMessage.IsBodyHtml = true;

        mySmtpClient = new SmtpClient("AlphaUR.e2k.ad.ge.com", 995);
        //mySmtpClient.Credentials = new NetworkCredential("hariom.kuntal@ge.com", "####");


        mySmtpClient.EnableSsl = false;

       mySmtpClient.Send(myHtmlMessage);

        
        }

        private static Attachment AddInlineAttachment(string FileName)
        {
            Attachment attchmnt = new Attachment(@"C:\Hariom\Personal\DotNetExperiments\TeamWorkManagement\ConsoleApplication6\ConsoleApplication6\ConsoleApplication6\" + FileName);
            attchmnt.ContentDisposition.Inline = true;
            attchmnt.ContentId = FileName.Substring(FileName.LastIndexOf("\\") + 1).Replace(".", "");
            return attchmnt;
        }

        private static string ReadFile()
        {
            StringBuilder strContents = new StringBuilder();
        StreamReader objReader;
        objReader = new StreamReader(@"C:\Hariom\Personal\DotNetExperiments\TeamWorkManagement\ConsoleApplication6\ConsoleApplication6\ConsoleApplication6\Test.htm");
        strContents.Append(objReader.ReadToEnd());
        objReader.Close();


        return strContents.ToString();
        }
    }
}

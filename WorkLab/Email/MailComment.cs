using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace WorkLab
{
    class sendComment
    {

        public void sendcomment(string email, string date)
        {
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "server120.web-hosting.com";
            client.EnableSsl = true;
            client.Timeout = 200000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("noreply@worklab.online", "*removed-for-security-purposes");

            MailMessage mm = new MailMessage("noreply@worklab.online", email, "Comment - WorkLab", "teacher_comment");
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            client.Send(mm);
        }


    }
}

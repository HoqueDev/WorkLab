using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace WorkLab
{
    class sMail
    {

        public void sendmail(string email, string date)
        {
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "	malh.pw";
            client.EnableSsl = true;
            client.Timeout = 200000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("worklab-noreply@malh.pw", "i^^Z%!Q2BHSp");

            MailMessage mm = new MailMessage("worklab-noreply@malh.pw", email, "Assigned Homework - WorkLab", "Homework has been set due for " + date + ", please login to WorkLab and complete your assignment.");
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            client.Send(mm);
        }


    }
}

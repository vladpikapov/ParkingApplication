using ServerPart.Data.Models.ParkingModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ServerPart.Logic.Managers
{
    public class MailManager
    {
        private static int _randomCode;

        public bool CheckVerifyCode(int userCode)
        {
            return _randomCode == userCode;
        }

        public void SendDeleteOrderToMail(string mail, Order order)
        {
            string fromaddr = "pikpikapoc@gmail.com";
            string toaddr = mail;
            string password = "20000430n";

            MailMessage msg = new MailMessage();
            msg.Subject = "";
            msg.From = new MailAddress(fromaddr);
            msg.Body = $"Ваш заказ <b>был удален</b>: " +
                $"<br><b>Адрес парковки:</b> {order.Parking.Address}" +
                $"<br><b>Начало:</b> {order.OrderStartDate}" +
                $"<br><b>Конец:</b> {order.OrderEndDate}" +
                $"<br><b>Номер машины:</b> {order.Account.CarNumber}";
            msg.IsBodyHtml = true;
            msg.To.Add(new MailAddress(toaddr));
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            NetworkCredential nc = new NetworkCredential(fromaddr, password);
            smtp.Credentials = nc;
            smtp.Send(msg);
        }

        public void SendOrderToMail(string mail, Order order)
        {
            string fromaddr = "pikpikapoc@gmail.com";
            string toaddr = mail;
            string password = "20000430n";

            MailMessage msg = new MailMessage();
            msg.Subject = "";
            msg.From = new MailAddress(fromaddr);
            msg.Body = $"Ваш заказ: " +
                $"<br><b>Адрес парковки:</b> {order.Parking.Address}" +
                $"<br><b>Начало:</b> {order.OrderStartDate}" +
                $"<br><b>Конец:</b> {order.OrderEndDate}" +
                $"<br><b>Номер машины:</b> {order.Account.CarNumber}";
            msg.IsBodyHtml = true;
            msg.To.Add(new MailAddress(toaddr));
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            NetworkCredential nc = new NetworkCredential(fromaddr, password);
            smtp.Credentials = nc;
            smtp.Send(msg);
        }


        public void SendCodeToMail(string mail)
        {

            string fromaddr = "pikpikapoc@gmail.com";
            string toaddr = mail;
            string password = "20000430n";


            var random = new Random();
            _randomCode = random.Next(100000, 999999);

            MailMessage msg = new MailMessage();
            msg.Subject = "";
            msg.From = new MailAddress(fromaddr);
            msg.Body = $"Код подтверждения: <b>{_randomCode}<b>";
            msg.IsBodyHtml = true;
            msg.To.Add(new MailAddress(toaddr));
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            NetworkCredential nc = new NetworkCredential(fromaddr, password);
            smtp.Credentials = nc;
            smtp.Send(msg);
        }
    }
}

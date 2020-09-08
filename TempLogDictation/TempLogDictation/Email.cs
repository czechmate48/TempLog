using System;
using System.Net;
using System.Net.Mail;

public class Email
{
    private SmtpClient client;
    private MailMessage msg;

    private string server { get; set; }
    private int port { get; set; }
    private string username { get; set; }
    private string password { get; set; }
    private string sender_email_address { get; set; }
    private string sender_name { get; set; }
    private string recipient_email_address { get; set; }
    public string subject { get; set; }
    public string message { get; set; }

    public Email() { } //Needed for initiliazation

    public Email(string server, int port, string username, string password, string sender_email_address,
        string sender_name, string recipient_email_address, string subject, string message)
    {
        this.server = server;
        this.port = port;
        this.username = username;
        this.password = password;
        this.sender_email_address = sender_email_address;
        this.sender_name = sender_name;
        this.recipient_email_address = recipient_email_address;
        this.subject = subject;
        this.message = message;

        client = Setup_Client(server, port, username, password);
        msg = Setup_Message(sender_email_address, sender_name, recipient_email_address, message, subject);
    }

    private SmtpClient Setup_Client(string server, int port, string username, string password)
    {
        SmtpClient client = new SmtpClient(server);
        client.Port = port;
        client.EnableSsl = true;
        client.Credentials = new NetworkCredential(username, password);
        return client;
    }

    private MailMessage Setup_Message(string sender_email_address, string sender_name, string recipient_email_address, string message, string subject)
    {
        MailAddress from = new MailAddress(sender_email_address, sender_name);
        MailAddress to = new MailAddress(recipient_email_address);
        MailMessage msg = new MailMessage(from, to);
        msg.Subject = subject;
        msg.Body = message;
        msg.IsBodyHtml = true;
        msg.Priority = MailPriority.Normal;
        msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        return msg;
    }

    public void Update()
    {
        client = Setup_Client(server, port, username, password);
        msg = Setup_Message(sender_email_address, sender_name, recipient_email_address, message, subject);
    }

    public void Send()
    {
        client.SendAsync(msg, "sending...");
    }
}

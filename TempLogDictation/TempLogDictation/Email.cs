using System;
using System.Net;
using System.Net.Mail;

public class Email
{
    /// <summary>
    /// Creates an email message from a specified email account. 
    /// Email message will be sent from the "Sender_email_address" to the "Recipient_email_address" 
    /// </summary>

    private SmtpClient client;
    private MailMessage msg;

    private string Server { get; set; }
    private int Port { get; set; }
    private string Username { get; set; }
    private string Password { get; set; }
    private string Sender_email_address { get; set; }
    private string Sender_name { get; set; }
    private string Recipient_email_address { get; set; }
    public string Subject { get; set; }
    public string Message { get; set; }

    public Email() { }

    public Email(string server, int port, string username, string password, string sender_email_address,
        string sender_name, string recipient_email_address, string subject, string message)
    {
        this.Server = server;
        this.Port = port;
        this.Username = username;
        this.Password = password;
        this.Sender_email_address = sender_email_address;
        this.Sender_name = sender_name;
        this.Recipient_email_address = recipient_email_address;
        this.Subject = subject;
        this.Message = message;

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
        client = Setup_Client(Server, Port, Username, Password);
        msg = Setup_Message(Sender_email_address, Sender_name, Recipient_email_address, Message, Subject);
    }

    public void Send()
    {
        client.SendAsync(msg, "sending...");
    }
}

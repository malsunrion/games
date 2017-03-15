using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class LogHistory : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SendLogHistory()
	{
		MailMessage mail = new MailMessage();
		//Attachment attachment = new System.Net.Mail.Attachment(Application.dataPath+"/screenshoot.png");
		mail.From = new MailAddress("arcaneparkinfo@gmail.com");
		//mail.To.Add ("support@arcanegaming.asia");  
		mail.To.Add ("mikha_malsunrionjr@yahoo.co.id");
		mail.Subject = "Log History";
		mail.Body = SlotMachine.log;
		//mail.Attachments.Add (attachment);

		SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
		smtpServer.Port = 587;
		//smtpServer.Credentials = new System.Net.NetworkCredential("vejhe1994@gmail.com", "ronlodge") as ICredentialsByHost;
		smtpServer.Credentials = new System.Net.NetworkCredential("arcaneparkinfo@gmail.com", "arcaneparkparkpark") as ICredentialsByHost;
		smtpServer.EnableSsl = true;
		ServicePointManager.ServerCertificateValidationCallback = 
			delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) 
		{ return true; };
		smtpServer.Send(mail);
		Debug.Log("success");
	}
}

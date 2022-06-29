using Epam.Rd.Application.Interfaces;
using System;
using System.Net.Mail;

namespace Epam.Rd.Application.Services
{
	public class EmailService : IEmail
	{
		public void SendEmail(MailMessage mailMessage)
		{
			Console.WriteLine("Email отправлен");
		}
	}
}
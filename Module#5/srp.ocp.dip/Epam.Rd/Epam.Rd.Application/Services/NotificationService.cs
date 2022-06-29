using System.Net.Mail;
using Epam.Rd.Application.Interfaces;
using Epam.Rd.Application.Models;

namespace Epam.Rd.Application.Services
{
    public class NotificationService : INotificationService
	{
		//Данный класс содержал в себе методы, имеющие разное назначение, что нарушало SRP
		private readonly IEmail _emailService;
		private readonly IStorage _fileStorage;

		public NotificationService(IEmail emailService, IStorage fileStorage)
		{
			_emailService = emailService;
			_fileStorage = fileStorage;
		}

		public void SendNotification(Notification notification)
		{
			var email = new MailMessage
			{
				From = new MailAddress("rd.net@epam.com", ".NET УЦ, г. Ижевск"),
				To = {string.Join(',', notification.To)},
				Body = notification.Body,
				Subject = notification.Subject
			};

			_emailService.SendEmail(email);
		}

		public void SaveAndSendNotification(Notification notification)
		{
			_fileStorage.Save(notification);
			SendNotification(notification);
		}

		
	}
}
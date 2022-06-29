using Epam.Rd.Application.Models;
using Epam.Rd.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epam.Rd.Application.Services
{
    internal class NotificationsCreator : ICreateNotification
    {
		//Вынес методы, относящиеся к созданию уведомлений в отдельный класс, тем самым решив проблемы SRP и OCP
		private const string BodyFooter = "С уважением, команда .NET УЦ, г. Ижевск";
		private const string ContactInformation = "rd.net@epam.com";

		public Notification CreateNotificationModuleCompleted(User student, User mentor, string moduleName)
		{
			var subject = $"Модуль {moduleName} был успешно завершен";
			var message = $"Информируем вас о том, что студент {student.FullName} успешно завершил изучение модуля {moduleName} {DateTime.UtcNow:dd.MM.yyyy HH:mm}";
			var users = new[] { student, mentor };
			var body = GetBody(message, users);
			var notification = new Notification
			{
				Subject = subject,
				Body = body,
				To = users
			};

			return notification;
		}

		public Notification CreateNotificationModuleIsOverdue(User student, User mentor, string moduleName, DateTime deadline)
		{
			var subject = $"Модуль {moduleName} не завершен вовремя";
			var message = $"Информируем вас о том, что студент {student.FullName} не завершил модуль {moduleName} в установленный срок {deadline:dd.MM.yyyy HH:mm}";
			var users = new[] { student, mentor };
			var body = GetBody(message, users);
			var notification = new Notification
			{
				Subject = subject,
				Body = body,
				To = users
			};

			return notification;
		}

		public Notification CreateNotificationNewModuleActivated(User student, User mentor, string moduleName, DateTime deadline)
		{
			var subject = $"Модуль {moduleName} добавлен в план обучения";
			var message = $"Информируем вас о том, что в учебном плане студента {student.FullName} был активирован новый модуль {moduleName}, срок выполнения: {deadline:dd.MM.yyyy HH:mm}";
			var users = new[] { student, mentor };
			var body = GetBody(message, users);
			var notification = new Notification
			{
				Subject = subject,
				Body = body,
				To = users
			};

			return notification;
		}

		public Notification CreateNotificationForAdminErrorInModuleFound(User coordinator, string moduleName, string description)
		{
			var subject = $"Обнаружена ошибка в модуле {moduleName}";
			var body = $"В модуле {moduleName} была обнаружена ошибка.<br/>Детали ошибки:<br/>{description}";
			var notification = new Notification
			{
				Subject = subject,
				Body = body,
				To = new[] { coordinator }
			};

			return notification;
		}

		public Notification CreateNotificationForAdminModuleMaterialsAreInaccessible(User coordinator, string moduleName)
		{
			var subject = $"Материалы модуля {moduleName} недоступны";
			var body = $"Материалы модуля {moduleName} в настоящее время недоступны.";
			var notification = new Notification
			{
				Subject = subject,
				Body = body,
				To = new[] { coordinator }
			};

			return notification;
		}

		private string GetBody(string message, IEnumerable<User> to)
		{
			var bodyBuilder = new StringBuilder();
			bodyBuilder.Append($"Добрый день, {string.Join(", ", to.Select(u => u.FullName))}.");
			bodyBuilder.Append("<br/>");
			bodyBuilder.Append("<br/>");
			bodyBuilder.Append(message);
			bodyBuilder.Append("<br/>");
			bodyBuilder.Append("<br/>");
			bodyBuilder.Append(BodyFooter);
			bodyBuilder.Append("<br/>");
			bodyBuilder.Append(ContactInformation);

			return bodyBuilder.ToString();
		}
	}
}

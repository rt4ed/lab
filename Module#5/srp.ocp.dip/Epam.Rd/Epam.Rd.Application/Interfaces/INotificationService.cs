using Epam.Rd.Application.Models;

namespace Epam.Rd.Application.Interfaces
{
    public interface INotificationService
	{
		//Разделил интерфейс, INotificationService теперь отвечает только за отправку и сохраниение уведомлений, тем самым исправив проблему SRP
		void SendNotification(Notification notification);
		void SaveAndSendNotification(Notification notification);
	}
}
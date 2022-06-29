using System;
using Epam.Rd.Application.Models;

namespace Epam.Rd.Application.Interfaces
{
    internal interface ICreateNotification
    {
        //Данный интерфейс отвечает за создание уведомлений
        Notification CreateNotificationModuleCompleted(User student, User mentor, string moduleName);
        Notification CreateNotificationModuleIsOverdue(User student, User mentor, string moduleName, DateTime deadline);
        Notification CreateNotificationNewModuleActivated(User student, User mentor, string moduleName, DateTime deadline);
        Notification CreateNotificationForAdminErrorInModuleFound(User coordinator, string moduleName, string description);
        Notification CreateNotificationForAdminModuleMaterialsAreInaccessible(User coordinator, string moduleName);
    }
}

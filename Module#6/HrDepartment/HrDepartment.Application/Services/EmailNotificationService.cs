using System.Threading.Tasks;
using HrDepartment.Application.Interfaces;
using HrDepartment.Domain.Entities;
using HrDepartment.Infrastructure.Interfaces;

namespace HrDepartment.Application.Services
{
	public class EmailNotificationService : INotificationService
	{
		private readonly IEmailSender _emailSender;

		public EmailNotificationService(IEmailSender emailSender)
		{
			_emailSender = emailSender;
		}

		public async Task NotifyRockStarFoundAsync(JobSeeker jobSeeker)
		{
			var subject = "Rock star found!";
			var body = $"Hello! There is rock star! To view details, please follow the <a href=\"https://hr-department.com/jobseekers/{jobSeeker.Id}\">link</a>";
			await _emailSender.SendEmailAsync(null, new []{"ceo@company.com"}, subject, body);
		}
	}
}

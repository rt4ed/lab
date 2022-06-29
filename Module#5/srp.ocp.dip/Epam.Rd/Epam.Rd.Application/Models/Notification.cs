using System.Collections.Generic;

namespace Epam.Rd.Application.Models
{
	public record Notification
	{
		public string Subject { get; init; }
		public string Body { get; init; }
		public ICollection<User> To { get; init; }
	}
}
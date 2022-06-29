namespace Epam.Rd.Application.Models
{
	public record User
	{
		public string FullName { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
	}
}
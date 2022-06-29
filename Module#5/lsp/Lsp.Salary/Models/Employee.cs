using System;

namespace Lsp.Salary.Models
{
	public class Employee
	{
		public string FullName { get; set; }
		public DateTime BirthDate { get; set; }
		public int YearExperience { get; set; }
		public decimal multiplier { get; set; }
		public virtual decimal Bonus
		{
			get { return YearExperience * multiplier; }
		}
	}
}
using System.Collections.Generic;

namespace Lsp.Salary.Models
{
	public class Manager : Employee
	{
		public ICollection<Employee> Subordinates { get; set; }
		public override decimal Bonus
		{
			get { return base.Bonus + Subordinates.Count * multiplier * 0.3m; }
		}
	}
}
using Lsp.Salary.Models;

namespace Lsp.Salary.Services
{
	public class SalaryService
	{
		public decimal CalculateBonus(Employee employee, decimal basicRate)
		{
			employee.multiplier = basicRate;

			return employee.Bonus;
		}
	}
}
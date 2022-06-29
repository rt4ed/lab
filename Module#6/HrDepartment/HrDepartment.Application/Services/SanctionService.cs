using System;
using System.Threading.Tasks;
using HrDepartment.Application.Interfaces;

namespace HrDepartment.Application.Services
{
	public class SanctionService : ISanctionService
	{
		public Task<bool> IsInSanctionsListAsync(string lastName, string firstName, string middleName, DateTime birthDate)
		{
			throw new NotImplementedException();
		}
	}
}

using System;

namespace HrDepartment.Domain.Enums
{
	[Flags]
	public enum BadHabits
	{
		None = 0,
		Smoking = 1,
		Alcoholism = 2,
		Drugs = 4
	}
}

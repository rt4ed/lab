using System;
using System.Threading.Tasks;
using HrDepartment.Application.Interfaces;
using HrDepartment.Domain.Entities;
using HrDepartment.Domain.Enums;

namespace HrDepartment.Application.Services
{
	public class JobSeekerRateService : IRateService<JobSeeker>
	{
		private readonly ISanctionService _sanctionService;

		public JobSeekerRateService(ISanctionService sanctionService)
		{
			_sanctionService = sanctionService ?? throw new ArgumentNullException(nameof(sanctionService));
		}

		public async Task<int> CalculateJobSeekerRatingAsync(JobSeeker jobSeeker)
		{
			var rating = 0;

			if (await _sanctionService.IsInSanctionsListAsync(jobSeeker.LastName, jobSeeker.FirstName, jobSeeker.MiddleName, jobSeeker.BirthDate))
			{
				return 0;
			}

			rating += CalculateBirthDateRating(jobSeeker.BirthDate);
			rating += CalculateEducationRating(jobSeeker.Education);
			rating += CalculateExperienceRating(jobSeeker.Experience);
			rating += CalculateHabitsRating(jobSeeker.BadHabits);

			return rating > 100
				? 100
				: rating < 0
					? 0
					: rating;
		}

		private int CalculateBirthDateRating(DateTime birthDate)
		{
			var currentYear = DateTime.Today.Year;

			return birthDate.Year + 18 <= currentYear && currentYear < birthDate.Year + 65
				? 10
				: 0;
		}

		private int CalculateEducationRating(EducationLevel educationLevel)
		{
			return educationLevel switch
			{
				EducationLevel.None => 0,
				EducationLevel.School => 5,
				EducationLevel.College => 15,
				EducationLevel.University => 35,
				_ => throw new ArgumentOutOfRangeException(nameof(educationLevel), educationLevel, null)
			};
		}

		private int CalculateExperienceRating(double workExperienceInYears)
		{
			if (workExperienceInYears < 1)
				return 5;
			if (workExperienceInYears < 3)
				return 10;
			if (workExperienceInYears < 5)
				return 15;
			if (workExperienceInYears < 10)
				return 25;

			return 40;
		}

		private int CalculateHabitsRating(BadHabits badHabits)
		{
			var rating = 10;
			if (badHabits == BadHabits.None)
				return rating;

			if (badHabits.HasFlag(BadHabits.Smoking))
				rating -= 5;

			if (badHabits.HasFlag(BadHabits.Alcoholism))
				rating -= 15;

			if (badHabits.HasFlag(BadHabits.Drugs))
				rating -= 45;

			return rating;
		}
	}
}

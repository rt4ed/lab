using NUnit.Framework;
using System;
using System.Reflection;
using System.Threading.Tasks;
using HrDepartment.Domain.Entities;
using HrDepartment.Application.Interfaces;
using HrDepartment.Domain.Enums;
using HrDepartment.Application.Services;
using Moq;

namespace HrDepartment.Application.Tests
{
    [TestFixture]
    public class JobSeekerRateServiceTests
    {
        private Mock<ISanctionService> _sanctionServiceMock;
        
        [SetUp]
        public void SetUp()
        {
            _sanctionServiceMock = new Mock<ISanctionService>();
        }

        private JobSeekerRateService GetJobSeekerRateService()
        {
            return new JobSeekerRateService(_sanctionServiceMock.Object);
        }


        [Test]
        public void Ctor_SanctionServiceIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new JobSeekerRateService(null));
        }

        
        
        [TestCase("2003, 9, 1", 45)]
        [TestCase("2002, 8, 2",45)]
        [TestCase("1957, 7, 3", 45)]
        [TestCase("1958, 6, 4", 45)]
        public void CalculateJobSeekerRating_AgeOfJobSeekerBetween16and65_Add10Rating(DateTime birthDate, int expectedResult)
        {
            var jobSeekerRateService = GetJobSeekerRateService();
            var jobSeeker = JobSeekersExamples.AverageJobSeeker();
            jobSeeker.BirthDate = birthDate;

            var testMethod = jobSeekerRateService.CalculateJobSeekerRatingAsync(jobSeeker);
            if (testMethod.Status == TaskStatus.Created)
                testMethod.Start();
            testMethod.Wait();
            var ratingAfterChangeBirthday = testMethod.Result;

            Assert.AreEqual(expectedResult, ratingAfterChangeBirthday); 
        }


        [TestCase("2004, 1, 1",35)]
        [TestCase("2005, 2, 3",35)]
        [TestCase("1955, 1, 1",35)]
        [TestCase("1956, 4, 5",35)]
        public void CalculateBirthDateRating_AgeLessThan_18_or_AgeMoreOrEqual_65_Add0Rating(DateTime birthDate, int expectedResult)
        {
            var jobSeekerRateService = GetJobSeekerRateService();
            var jobSeeker = JobSeekersExamples.AverageJobSeeker();
            jobSeeker.BirthDate = birthDate;

            var testMethod = jobSeekerRateService.CalculateJobSeekerRatingAsync(jobSeeker);
            if (testMethod.Status == TaskStatus.Created)
                testMethod.Start();
            testMethod.Wait();
            var ratingAfterChangeBirthday = testMethod.Result;

            Assert.AreEqual(expectedResult, ratingAfterChangeBirthday);
        }

        [TestCase(EducationLevel.None,30)]
        [TestCase(EducationLevel.School, 35)]
        [TestCase(EducationLevel.College, 45)]
        [TestCase(EducationLevel.University, 65)]
        public void CalculateEducationRating_DifferentLevelsOfEducation_AddEducationRating(EducationLevel educationLevel, int expectedResult)
        {
            var jobSeekerRateService = GetJobSeekerRateService();
            var jobSeeker = JobSeekersExamples.AverageJobSeeker();
            jobSeeker.Education = educationLevel;

            var testMethod = jobSeekerRateService.CalculateJobSeekerRatingAsync(jobSeeker);
            if (testMethod.Status == TaskStatus.Created)
                testMethod.Start();
            testMethod.Wait();
            var ratingAfterChangeEducation = testMethod.Result;

            Assert.AreEqual(expectedResult, ratingAfterChangeEducation);
        }

        [Test]
        public void CalculateEducationRating_OutOfRange_ThrowsArgumentOutOfRangeException()
        {
            var jobSeekerRateService = GetJobSeekerRateService();
            var jobSeeker = JobSeekersExamples.AverageJobSeeker();
            jobSeeker.Education = (EducationLevel)int.MaxValue;

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => jobSeekerRateService.CalculateJobSeekerRatingAsync(jobSeeker));
        }

        [TestCase(0.1, 40)]
        [TestCase(1, 45)]
        [TestCase(2.9, 45)]
        [TestCase(3, 50)]
        [TestCase(4.9, 50)]
        [TestCase(5, 60)]
        [TestCase(9.9, 60)]
        [TestCase(10, 75)]
        [TestCase(21, 75)]
        public void CalculateExperienceRating_DifferentExperience_AddExperienceRating(double experience,
            int expectedResult)
        {
            var jobSeekerRateService = GetJobSeekerRateService();
            var jobSeeker = JobSeekersExamples.AverageJobSeeker();
            jobSeeker.Experience = experience;

            var testMethod = jobSeekerRateService.CalculateJobSeekerRatingAsync(jobSeeker);
            if (testMethod.Status == TaskStatus.Created)
                testMethod.Start();
            testMethod.Wait();
            var ratingAfterChangeExperience = testMethod.Result;

            Assert.AreEqual(expectedResult, ratingAfterChangeExperience);
        }

        [TestCase(BadHabits.None,45)]
        [TestCase(BadHabits.Smoking, 40)]
        [TestCase(BadHabits.Alcoholism, 30)]
        [TestCase(BadHabits.Drugs, 0)]
        [TestCase(BadHabits.Drugs | BadHabits.Smoking, 0)]
        [TestCase(BadHabits.Alcoholism | BadHabits.Smoking, 25)]
        [TestCase(BadHabits.Alcoholism | BadHabits.Smoking | BadHabits.Drugs, 0)]
        public void CalculateHabitsRating_DifferentHabits_AddHabitsRating(BadHabits habits, int expectedResult)
        {
            var jobSeekerRateService = GetJobSeekerRateService();
            var jobSeeker = JobSeekersExamples.AverageJobSeeker();
            jobSeeker.BadHabits = habits;

            var testMethod = jobSeekerRateService.CalculateJobSeekerRatingAsync(jobSeeker);
            if (testMethod.Status == TaskStatus.Created)
                testMethod.Start();
            testMethod.Wait();
            var ratingAfterChangeHabits = testMethod.Result;

            Assert.AreEqual(expectedResult, ratingAfterChangeHabits);
        }

        [Test]
        public async Task CalculateJobSeekerRating_SeekerInBlackList_Return0()
        {
            var jobSeekerRateService = GetJobSeekerRateService();
            var jobSeeker = JobSeekersExamples.AverageJobSeeker();
            
            _sanctionServiceMock.Setup(x => x.IsInSanctionsListAsync(jobSeeker.LastName, jobSeeker.FirstName, jobSeeker.MiddleName, jobSeeker.BirthDate))
                .ReturnsAsync(true);

            var totalRating = await jobSeekerRateService.CalculateJobSeekerRatingAsync(jobSeeker);

            Assert.AreEqual(0, totalRating);
        }

    }
}
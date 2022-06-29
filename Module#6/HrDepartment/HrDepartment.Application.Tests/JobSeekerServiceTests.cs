using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using HrDepartment.Application.Interfaces;
using HrDepartment.Infrastructure.Interfaces;
using Moq;
using NUnit.Framework;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using HrDepartment.Domain.Entities;
using HrDepartment.Domain.Enums;
using HrDepartment.Application.Services;

namespace HrDepartment.Application.Tests
{
    [TestFixture]
    class JobSeekerServiceTests
    {
        private Mock<IStorage> _storageMock;
        private Mock<IRateService<JobSeeker>> _rateServiceMock;
        private Mock<INotificationService> _notificationServiceMock;
        private Mock<IMapper> _mapperMock;


        [SetUp]
        public void SetUp()
        {
            _storageMock = new Mock<IStorage>();
            _rateServiceMock = new Mock<IRateService<JobSeeker>>();
            _notificationServiceMock = new Mock<INotificationService>();
            _mapperMock = new Mock<IMapper>();
        }

        private JobSeekerService GetJobSeekerService()
        {
            return new JobSeekerService(_storageMock.Object, _rateServiceMock.Object, _mapperMock.Object,
                _notificationServiceMock.Object);
        }


        [TestCase(1)]
        [TestCase(95)]
        [TestCase(99)]
        [TestCase(101)]
        [TestCase(110)]
        public async Task RateJobSeekerAsync_RatingLessThan100_ReturnRating(int expectedRating)
        {
            var jobSeekerService = GetJobSeekerService();
            var jobSeeker = new JobSeeker();

            _storageMock.Setup(s => s.GetByIdAsync<JobSeeker, int>(JobSeekersExamples.AverageJobSeeker().Id))
                .ReturnsAsync(jobSeeker);
                

            _rateServiceMock.Setup(s => s.CalculateJobSeekerRatingAsync(jobSeeker))
                .ReturnsAsync(expectedRating);


            var rating = await jobSeekerService.RateJobSeekerAsync(JobSeekersExamples.AverageJobSeeker().Id);
            _notificationServiceMock.Verify(s => s.NotifyRockStarFoundAsync(jobSeeker),
                Times.Never);

            Assert.AreEqual(expectedRating, rating);
        }

        
        [Test]
        public async Task RateJobSeekerAsync_RatingEqual100_NotifyRockStar()
        {
            var jobSeekerService = GetJobSeekerService();
            var jobSeeker = new JobSeeker();
            var expectedRating = 100;

            _storageMock.Setup(s => s.GetByIdAsync<JobSeeker, int>(JobSeekersExamples.AverageJobSeeker().Id))
                .ReturnsAsync(jobSeeker);
            
            _rateServiceMock.Setup(s => s.CalculateJobSeekerRatingAsync(jobSeeker))
                .ReturnsAsync(expectedRating);


            var rating = await jobSeekerService.RateJobSeekerAsync(JobSeekersExamples.AverageJobSeeker().Id);
            _notificationServiceMock.Verify(s => s.NotifyRockStarFoundAsync(jobSeeker),
                Times.Once);

            Assert.AreEqual(expectedRating, rating);
        }





    }
}

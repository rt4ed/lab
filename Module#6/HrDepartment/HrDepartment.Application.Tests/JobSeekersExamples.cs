using System;
using HrDepartment.Domain.Enums;
using HrDepartment.Domain.Entities;
using System.Collections.Generic;
using System.Text;

namespace HrDepartment.Application.Tests
{
    class JobSeekersExamples
    {
        public static JobSeeker AverageJobSeeker()//total rating 45
            => new JobSeeker()
            {
                Id = 0,
                BadHabits = BadHabits.None,//10
                BirthDate = new DateTime(1996, 1, 12),//10
                Education = EducationLevel.College,//15
                Experience = 1,//10
                FirstName = "Anton",
                LastName = "Ivanov",
                MiddleName = "Petrovich",
                Status = JobSeekerStatus.New
            };
    }
}

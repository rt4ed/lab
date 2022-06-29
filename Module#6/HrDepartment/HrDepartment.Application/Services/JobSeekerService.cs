using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HrDepartment.Application.Dto;
using HrDepartment.Application.Interfaces;
using HrDepartment.Domain.Entities;
using HrDepartment.Domain.Enums;
using HrDepartment.Infrastructure.Interfaces;

namespace HrDepartment.Application.Services
{
	public class JobSeekerService : IJobSeekerService
	{
		private readonly IStorage _storage;
		private readonly IRateService<JobSeeker> _rateService;
		private readonly INotificationService _notificationService;
		private readonly IMapper _mapper;

		public JobSeekerService(IStorage storage, IRateService<JobSeeker> rateService, IMapper mapper, INotificationService notificationService)
		{
			_storage = storage;
			_rateService = rateService;
			_mapper = mapper;
			_notificationService = notificationService;
		}

		public async Task<JobSeeker> AddAsync(BackgroundInformationDto backgroundInformation)
		{
			var jobSeeker = _mapper.Map<JobSeeker>(backgroundInformation);
			await _storage.AddAsync(jobSeeker);
			return jobSeeker;
		}

		public Task MoveToPersonnelReserveAsync(int jobSeekerId)
		{
			throw new System.NotImplementedException();
		}

		public Task<IList<JobSeeker>> GetAllAsync()
		{
			throw new System.NotImplementedException();
		}

		public Task<IList<JobSeeker>> GetAllInStatusesAsync(IEnumerable<JobSeekerStatus> statuses)
		{
			throw new System.NotImplementedException();
		}

		public async Task<int> RateJobSeekerAsync(int jobSeekerId)
		{
			var jobSeeker = await _storage.GetByIdAsync<JobSeeker, int>(jobSeekerId);
			var rating = await _rateService.CalculateJobSeekerRatingAsync(jobSeeker);

			if (rating == 100)
				await _notificationService.NotifyRockStarFoundAsync(jobSeeker);

			return rating;
		}
	}
}

using Application.Interfaces;
using Infrastructure.Persistence;

namespace Application.Services
{
	public class DataSeedingService : IDataSeedingService
	{
		private readonly IApplicationDbContext _dbContext;
		public DataSeedingService(IApplicationDbContext dbContext)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
		}

		public async Task SeedDataAsync()
		{
		}
	}
}

using Chaching.Data;
using Chaching.Models;
using Microsoft.EntityFrameworkCore;

namespace Chaching.Services
{
	public class DriverService : IDriverService
	{
		private readonly ApiDbContext apiDbContext;
		public DriverService(ApiDbContext _apiDbContext)
		{
			apiDbContext= _apiDbContext;
		}
		public async Task<Driver> AddDriver(Driver driver)
		{
			var result = await apiDbContext.Drivers.AddAsync(driver);	
			await apiDbContext.SaveChangesAsync();
			return result.Entity;
		}


		public async Task<IEnumerable<Driver>> GetDriversAsync()
		{
			return await apiDbContext.Drivers.ToListAsync();
		}

		public async Task<Driver?> GetDriversByIdAsync(int id)
		{
			return await apiDbContext.Drivers.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<bool> RemoveDriver(int id)
		{
			var driver =await  GetDriversByIdAsync(id);
			apiDbContext.Drivers.Remove(driver);
			await apiDbContext.SaveChangesAsync();
			return driver!= null;

		}

		public async Task<Driver> UpdateDriverAsync(Driver driver)
		{
			var result = apiDbContext.Drivers.Update(driver);
			await apiDbContext.SaveChangesAsync();
			return result.Entity;
		}
	}
}

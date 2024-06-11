using Chaching.Models;

namespace Chaching.Services
{
	public interface IDriverService
	{
		public Task<IEnumerable<Driver>> GetDriversAsync();

		public Task<Driver> GetDriversByIdAsync(int id);

		public Task<Driver> AddDriver(Driver driver);

		public Task<bool> RemoveDriver(int id);
		public Task<Driver> UpdateDriverAsync(Driver driver);
	}
}

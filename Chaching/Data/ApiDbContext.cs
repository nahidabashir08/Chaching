using Chaching.Models;
using Microsoft.EntityFrameworkCore;

namespace Chaching.Data
{
	public class ApiDbContext:DbContext
	{
		public DbSet<Driver> Drivers { get; set; }
		public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
		{

		}
	}
}

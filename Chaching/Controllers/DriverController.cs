using Chaching.Data;
using Chaching.Exceptions;
using Chaching.Models;
using Chaching.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chaching.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DriverController : ControllerBase
	{
		private readonly ILogger<DriverController> logger;
		private readonly ICachingService cachingService;
		private readonly IDriverService driverService;
		public DriverController(ICachingService _cachingService, IDriverService _driverService, ILogger<DriverController> _logger)
		{
			driverService = _driverService;
			cachingService = _cachingService;
			logger = _logger;
		}

		[HttpGet("driversList")]
		public async Task<IActionResult> DriverList()
		{
			var cacheDrivers = cachingService.GetData<IEnumerable<Driver>>("drivers");
			if(cacheDrivers!=null && cacheDrivers.Count()>0)
			{
				return Ok(cacheDrivers);
			}

			var drivers = await driverService.GetDriversAsync();
			var expiryTime = DateTimeOffset.Now.AddMinutes(2);
			cachingService.SetData<IEnumerable<Driver>>("drivers", drivers, expiryTime);

			return Ok(drivers);
			
		}

		[HttpPost("AddDriver")]

		public async Task<IActionResult> AddDriver(Driver driver)
		{
			var result =await driverService.AddDriver(driver);
			return Ok(result);
		}

		[HttpGet("GetDriverById")]

		public async Task<IActionResult> GetDriverById(int id)
		{
			var cacheDriverById = cachingService.GetData<Driver>(id.ToString());
			if (cacheDriverById != null)
			{
				return Ok(cacheDriverById);
			}

			var driver = await driverService.GetDriversByIdAsync(id);
			if(driver != null)
			{
				var expiryTime = DateTimeOffset.Now.AddMinutes(2);
				cachingService.SetData(id.ToString(),driver, expiryTime);
				return Ok(driver);
			}
			//return NotFound();
			throw new NotFoundException("the id is an invalid id");
		}
	}
}

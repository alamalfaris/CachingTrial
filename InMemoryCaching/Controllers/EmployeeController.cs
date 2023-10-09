using InMemoryCaching.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InMemoryCaching.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeeController : ControllerBase
	{
		private readonly IEmployeeRepository _employeeRepository;

		public EmployeeController(IEmployeeRepository employeeRepository)
		{
			_employeeRepository = employeeRepository;
		}

		[HttpGet("{id}")]
		public async Task<ActionResult> GetEmployee(int id)
		{
			return Ok(await _employeeRepository.GetEmployee(id));
		}
	}
}

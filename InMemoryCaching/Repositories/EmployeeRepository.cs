using InMemoryCaching.Database;
using InMemoryCaching.Interfaces;
using InMemoryCaching.Models;
using Microsoft.EntityFrameworkCore;

namespace InMemoryCaching.Repositories
{
	public class EmployeeRepository : IEmployeeRepository
	{
		private readonly DatabaseContext _context;

		public EmployeeRepository(DatabaseContext context)
		{
			_context = context;
		}

		public async Task<Employee> GetEmployee(int id)
		{
			return await _context.Employee.FirstAsync(x => x.Id == id);
		}
	}
}

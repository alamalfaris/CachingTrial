using InMemoryCaching.Models;

namespace InMemoryCaching.Interfaces
{
	public interface IEmployeeRepository
	{
		Task<Employee> GetEmployee(int id);
	}
}

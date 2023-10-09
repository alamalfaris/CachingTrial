using System.ComponentModel.DataAnnotations;

namespace InMemoryCaching.Models
{
	public class Employee
	{
		public int Id { get; set; }
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
	}
}

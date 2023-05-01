using Entities.Models;

namespace Repository.Extensions;

public static class RepositoryEmployeeExtensions
{
    public static IQueryable<Employee> FilterEmployees(this IQueryable<Employee> employees, uint minAge, uint maxAge) =>
        employees.Where(employee => employee.Age >= minAge && employee.Age <= maxAge);

    public static IQueryable<Employee> SearchEmployees(this IQueryable<Employee> employees, string? searchTerm) =>
        string.IsNullOrEmpty(searchTerm)
            ? employees
            : employees.Where(employee =>
                employee.Name != null && employee.Name.ToLower().Contains(searchTerm.ToLower()));
}
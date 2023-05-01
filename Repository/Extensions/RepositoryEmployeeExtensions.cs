using Entities.Models;
using Repository.Extensions.Utility;
using System.Linq.Dynamic.Core;

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

    public static IQueryable<Employee> SortEmployees(this IQueryable<Employee> employees, string? orderByQueryString)
    {
        if (string.IsNullOrEmpty(orderByQueryString))
        {
            return employees.OrderBy(e => e.Name);
        }

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<Employee>(orderByQueryString);
        return string.IsNullOrEmpty(orderQuery) ? employees.OrderBy(e => e.Name) : employees.OrderBy(orderQuery);
    }
}
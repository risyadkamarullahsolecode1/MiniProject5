using MiniProject5.Application.Dtos;
using MiniProject5.Application.Dtos.Search;
using MiniProject5.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject5.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<PaginatedList<EmployeeSortedDto>> GetFilteredSortedEmployeesAsync(EmployeeFilterSortRequest request);
        Task<EmployeeDetailDto> GetEmployeeDetailsByIdAsync(int empNo);
        Task RegisterEmployeeAsync(EmployeeRegistrationDto employeeDto);
        Task UpdateEmployeeAsync(int empNo, UpdateDto updateDto);
        Task DeactivateEmployeeAsync(int empNo, string reason);
        Task<object> GetFilteredSortedEmployeesAsync(SearchDto searchDto);
        Task<EmployeeWithDepartmentDto> GetEmployeeWithDepartmentByIdAsync(int empNo);
        Task AddDependentAsync(int empNo, DependentDto dependentDto);
    }
}

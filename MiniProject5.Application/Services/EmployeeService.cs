using Microsoft.EntityFrameworkCore;
using MiniProject5.Application.Dtos;
using MiniProject5.Application.Dtos.Search;
using MiniProject5.Application.Helpers;
using MiniProject5.Application.Interfaces;
using MiniProject5.Domain.Entities;
using MiniProject5.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject5.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IDependentRepository _dependentRepository;

        public EmployeeService(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository, IDependentRepository dependentRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _dependentRepository = dependentRepository;
        }

        //Filter And Sort Employee by Fname 
        public async Task<PaginatedList<EmployeeSortedDto>> GetFilteredSortedEmployeesAsync(EmployeeFilterSortRequest request)
        {
            // Get all employees and departments
            var employees = _employeeRepository.GetAllEmployee().AsQueryable();
            var departments = _departmentRepository.GetAllDepartment().AsQueryable();

            // Apply filtering on employees
            if (!string.IsNullOrEmpty(request.Fname))
                employees = employees.Where(e => e.Fname.ToLower().Contains(request.Fname.ToLower()));
            if (!string.IsNullOrEmpty(request.Position))
                employees = employees.Where(e => e.Position.ToLower().Contains(request.Position.ToLower()));
            if (request.Level > 0)
                employees = employees.Where(e => e.Level == request.Level);
            if (!string.IsNullOrEmpty(request.Employeetype))
                employees = employees.Where(e => e.Employeetype.ToLower().Equals(request.Employeetype.ToLower()));

            // Apply filtering on departments
            if (!string.IsNullOrEmpty(request.Deptname))
                departments = departments.Where(d => d.Deptname.ToLower().Contains(request.Deptname.ToLower()));

            // Join employees with departments to get department names
            var query = from e in employees
                        join d in departments on e.Deptno equals d.Deptno
                        select new EmployeeSortedDto
                        {
                            Fname = e.Fname + " " + e.Lname,
                            Deptname = d.Deptname,
                            Position = e.Position,
                            Level = e.Level,
                            Employeetype = e.Employeetype,
                            Lastupdateddate = e.Lastupdateddate
                        };

            // Apply sorting
            query = request.SortBy switch
            {
                "Name" => request.SortDescending ? query.OrderByDescending(e => e.Fname) : query.OrderBy(e => e.Fname),
                "Department" => request.SortDescending ? query.OrderByDescending(e => e.Deptname) : query.OrderBy(e => e.Deptname),
                "JobPosition" => request.SortDescending ? query.OrderByDescending(e => e.Position) : query.OrderBy(e => e.Position),
                "Level" => request.SortDescending ? query.OrderByDescending(e => e.Level) : query.OrderBy(e => e.Level),
                "EmploymentType" => request.SortDescending ? query.OrderByDescending(e => e.Employeetype) : query.OrderBy(e => e.Employeetype),
                "LastUpdatedDate" => request.SortDescending ? query.OrderByDescending(e => e.Lastupdateddate) : query.OrderBy(e => e.Lastupdateddate),
                _ => query.OrderBy(e => e.Fname), // Default sorting
            };

            // Paginate
            var paginatedEmployees = await PaginatedList<EmployeeSortedDto>.CreateAsync(query.AsQueryable(), request.PageNumber, request.PageSize);

            return paginatedEmployees;
        }

        public async Task<object> GetFilteredSortedEmployeesAsync(SearchDto searchDto)
        {
            // Retrieve employee and department data as queryable
            var employees = _employeeRepository.GetAllEmployee().AsQueryable();
            var departments = _departmentRepository.GetAllDepartment().AsQueryable();

            // Apply keyword filtering across all relevant fields
            if (!string.IsNullOrEmpty(searchDto.Keyword))
            {
                employees = employees.Where(e =>
                    e.Fname.ToLower().Contains(searchDto.Keyword.ToLower()) ||
                    e.Lname.ToLower().Contains(searchDto.Keyword.ToLower()) ||
                    e.Position.ToLower().Contains(searchDto.Keyword.ToLower()) ||
                    e.Employeetype.ToLower().Contains(searchDto.Keyword.ToLower())
                );

                departments = departments.Where(d =>
                    d.Deptname.ToLower().Contains(searchDto.Keyword.ToLower())
                );
            }

            // Apply specific filters
            if (!string.IsNullOrEmpty(searchDto.EmployeeName))
                employees = employees.Where(e =>
                    (e.Fname + " " + e.Lname).ToLower().Contains(searchDto.EmployeeName.ToLower()));

            if (!string.IsNullOrEmpty(searchDto.Position))
                employees = employees.Where(e => e.Position.ToLower().Contains(searchDto.Position.ToLower()));

            if (searchDto.Level > 0)
                employees = employees.Where(e => e.Level == searchDto.Level);

            if (!string.IsNullOrEmpty(searchDto.EmploymentType))
                employees = employees.Where(e => e.Employeetype.ToLower().Equals(searchDto.EmploymentType.ToLower()));

            if (!string.IsNullOrEmpty(searchDto.DepartmentName))
                departments = departments.Where(d => d.Deptname.ToLower().Contains(searchDto.DepartmentName.ToLower()));

            // Join employees with departments to include department names
            var query = from e in employees
                        join d in departments on e.Deptno equals d.Deptno
                        select new EmployeeSortedDto
                        {
                            EmpNo = e.Empno,
                            Fname = e.Fname + " " + e.Lname,
                            Deptname = d.Deptname,
                            Position = e.Position,
                            Level = e.Level,
                            Employeetype = e.Employeetype,
                            Lastupdateddate = e.Lastupdateddate
                        };

            // Apply sorting
            if (!string.IsNullOrEmpty(searchDto.SortBy))
            {
                query = searchDto.SortBy.ToLower() switch
                {
                    "name" => searchDto.SortOrder.Equals("desc", StringComparison.OrdinalIgnoreCase) ?
                        query.OrderByDescending(e => e.Fname) :
                        query.OrderBy(e => e.Fname),
                    "department" => searchDto.SortOrder.Equals("desc", StringComparison.OrdinalIgnoreCase) ?
                        query.OrderByDescending(e => e.Deptname) :
                        query.OrderBy(e => e.Deptname),
                    "position" => searchDto.SortOrder.Equals("desc", StringComparison.OrdinalIgnoreCase) ?
                        query.OrderByDescending(e => e.Position) :
                        query.OrderBy(e => e.Position),
                    "level" => searchDto.SortOrder.Equals("desc", StringComparison.OrdinalIgnoreCase) ?
                        query.OrderByDescending(e => e.Level) :
                        query.OrderBy(e => e.Level),
                    "employmenttype" => searchDto.SortOrder.Equals("desc", StringComparison.OrdinalIgnoreCase) ?
                        query.OrderByDescending(e => e.Employeetype) :
                        query.OrderBy(e => e.Employeetype),
                    "lastupdateddate" => searchDto.SortOrder.Equals("desc", StringComparison.OrdinalIgnoreCase) ?
                        query.OrderByDescending(e => e.Lastupdateddate) :
                        query.OrderBy(e => e.Lastupdateddate),
                    _ => query.OrderBy(e => e.EmpNo) 
                };
            }

            // Get total record count before pagination
            var totalRecords = await query.CountAsync();

            // Apply pagination
            var skip = (searchDto.PageNumber - 1) * searchDto.PageSize;
            var paginatedEmployees = await query.Skip(skip).Take(searchDto.PageSize).ToListAsync();

            // Return the paginated data and total record count
            return new { total = totalRecords, data = paginatedEmployees };
        }


        //Get Employee Details
        public async Task<EmployeeDetailDto> GetEmployeeDetailsByIdAsync(int empNo)
        {
            var employee = await _employeeRepository.GetEmployeeById(empNo);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with No {empNo} not found.");
            }
            var department = await _departmentRepository.GetDepartmentById(employee.Deptno.Value);
            if (department == null)
            {
                throw new KeyNotFoundException($"Department with No {employee.Deptno} not found.");
            }

            // Retrieve the manager's name using the employee's and manager ID (mgrempno)
            Employee manager = null;
            if (department.Spvempno.HasValue)
            {
                manager = await _employeeRepository.GetEmployeeById(department.Spvempno.Value);
            }
            var supervisorName = manager != null ? manager.Fname + " " + manager.Lname : "No Manager";

            // Map the employee data to the EmployeeDetailDto
            var employeeDetail = new EmployeeDetailDto
            {
                EmpNo = employee.Empno,
                EmployeeName = employee.Fname + " " + employee.Lname,
                Address = employee.Address,
                PhoneNumber = employee.Phonenumber.HasValue ? employee.Phonenumber.Value.ToString() : "Not Available",
                Email = employee.Email,
                Position = employee.Position,
                SupervisorName = supervisorName,
                EmployeeType = employee.Employeetype,
            };

            return employeeDetail;
        }

        // Register Employee
        public async Task RegisterEmployeeAsync(EmployeeRegistrationDto employeeDto)
        {
            var employee = new Employee
            {
                Fname = employeeDto.Fname,
                Lname = employeeDto.Lname,
                Sex = employeeDto.Sex,
                Salary = employeeDto.Salary,
                Dob = employeeDto.Dob.Value,
                Address = employeeDto.Address,
                Phonenumber = employeeDto.Phonenumber,
                Email = employeeDto.Email,
                Position = employeeDto.Position,
                Deptno = employeeDto.Deptno,
                Employeetype = employeeDto.Employeetype
            };

            await _employeeRepository.AddEmployee(employee);
            await _employeeRepository.SaveChangesAsync();

            // Save dependents
            if (employeeDto.Dependents != null && employeeDto.Dependents.Any())
            {
                foreach (var dependentDto in employeeDto.Dependents)
                {
                    var dependent = new Dependent
                    {
                        Empno = employee.Empno, // Assuming Employee has Id field after saving
                        Name = dependentDto.Name,
                        Sex = dependentDto.Sex,
                        Relationship = dependentDto.Relationship,
                        Dob = dependentDto.Dob
                    };
                    _dependentRepository.AddDependent(dependent);

                }
                await _dependentRepository.SaveChangesAsync();
            }
        }

        //Deactive Employee 
        public async Task DeactivateEmployeeAsync(int empNo, string reason)
        {
            var employee = await _employeeRepository.GetEmployeeById(empNo);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with No {empNo} not found.");
            }

            employee.Status = "Not Active";
            employee.Statusreason = reason;
            employee.Lastupdateddate = DateTime.Now;

            await _employeeRepository.UpdateEmployee(employee);
            await _employeeRepository.SaveChangesAsync();
        }

        // update employee with timestamp
        public async Task UpdateEmployeeAsync(int empNo, UpdateDto updateDto)
        {
            var employee = await _employeeRepository.GetEmployeeById(empNo);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with No {empNo} not found.");
            }

            employee.Fname = updateDto.Fname ?? employee.Fname;
            employee.Lname = updateDto.Lname ?? employee.Lname;
            employee.Address = updateDto.Address ?? employee.Address;
            employee.Phonenumber = updateDto.Phonenumber ?? employee.Phonenumber;
            employee.Email = updateDto.Email ?? employee.Email;
            employee.Position = updateDto.Position ?? employee.Position;
            employee.Deptno = updateDto.Deptno ?? employee.Deptno;
            employee.Employeetype = updateDto.Employeetype ?? employee.Employeetype;
            employee.Statusreason = updateDto.Statusreason ?? employee.Statusreason;
            employee.Status = updateDto.Status ?? employee.Status;
            employee.Nik = updateDto.Nik ?? employee.Nik;
            employee.Lastupdateddate = DateTime.Now; // Update timestamp

            await _employeeRepository.UpdateEmployee(employee);
            await _employeeRepository.SaveChangesAsync();

            // Map to EmployeeDto for returning
            var employeeDto = new EmployeeDto
            {
                Fname = employee.Fname,
                Lname = employee.Lname,
                Address = employee.Address,
                Email = employee.Email,
                Position = employee.Position,
                Phonenumber = employee.Phonenumber,
                Employeetype = employee.Employeetype,
                Lastupdateddate = employee.Lastupdateddate
            };

            return ;
        }

        public async Task<EmployeeWithDepartmentDto> GetEmployeeWithDepartmentByIdAsync(int empNo)
        {
            // Fetch the employee by empNo
            var employee = await _employeeRepository.GetEmployeeById(empNo);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with No {empNo} not found.");
            }

            // Fetch the department using the deptNo from the employee
            var department = await _departmentRepository.GetDepartmentById(employee.Deptno.Value);
            if (department == null)
            {
                throw new KeyNotFoundException($"Department with No {employee.Deptno} not found.");
            }

            // Fetch the supervisor employee using SpvEmpNo from the department (if it's not null)
            string spvEmpName = null;
            if (department.Spvempno.HasValue)
            {
                var supervisor = await _employeeRepository.GetEmployeeById(department.Spvempno.Value);
                if (supervisor != null)
                {
                    spvEmpName = $"{supervisor.Fname} {supervisor.Lname}";  // Full name of the supervisor
                }
            }

            // Create a DTO and populate it with employee and department data
            var employeeWithDeptDto = new EmployeeWithDepartmentDto
            {
                Empno = employee.Empno,
                Fname = employee.Fname,
                Lname = employee.Lname,
                Position = employee.Position,
                DeptName = department.Deptname,
                Dob = employee.Dob,
                Address = employee.Address,
                Sex = employee.Sex,
                Deptno = employee.Deptno,
                Employeetype = employee.Employeetype,
                Level = employee.Level,
                Lastupdateddate= employee.Lastupdateddate,
                Nik = employee.Nik,
                Status = employee.Status,
                Statusreason = employee.Statusreason,
                Salary  = employee.Salary,
                SpvEmpName = spvEmpName,
            };

            return employeeWithDeptDto;
        }

        public async Task AddDependentAsync(int empNo, DependentDto dependentDto)
        {
            // Check if the employee exists
            var employee = await _employeeRepository.GetEmployeeById(empNo);

            if (employee == null)
            {
                throw new ArgumentException($"Employee with empNo {empNo} does not exist.");
            }

            // Create a new Dependent entity and map data from the DTO
            var dependent = new Dependent
            {
                Empno = empNo,
                Name = dependentDto.Name,
                Sex = dependentDto.Sex,
                Relationship = dependentDto.Relationship,
                Dob = dependentDto.Dob
            };

            // Add the dependent to the database
            await _dependentRepository.AddDependent(dependent);
            await _dependentRepository.SaveChangesAsync();
        }

    }
}

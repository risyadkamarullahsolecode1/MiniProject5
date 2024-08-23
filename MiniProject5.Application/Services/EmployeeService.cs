using MiniProject5.Application.Dtos;
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

            _employeeRepository.AddEmployee(employee);
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

            _employeeRepository.UpdateEmployee(employee);
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

            _employeeRepository.UpdateEmployee(employee);
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
    }
}

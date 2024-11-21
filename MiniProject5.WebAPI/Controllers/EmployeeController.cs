using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MiniProject5.Application.Dtos;
using MiniProject5.Application.Dtos.Search;
using MiniProject5.Application.Helpers;
using MiniProject5.Application.Interfaces;
using MiniProject5.Application.Mappers;
using MiniProject5.Domain.Entities;
using MiniProject5.Domain.Interfaces;

namespace MiniProject5.WebAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeRepository employeeRepository, IEmployeeService employeeService)
        {
            _employeeRepository = employeeRepository;
            _employeeService = employeeService;
        }
        /// <summary>
        /// You can see the Get, Get By Id or Delete Employee Here .
        /// </summary>
        /// <remarks>
        /// The parameters in the request body can be null. 
        ///
        ///  You can search by using any of the parameters in the request.
        ///  
        ///  NOTE: You can only add by one parameter at a time
        ///  
        /// Sample request : Get All Employee
        ///             
        ///     GET /api/v1/Employee
        ///     
        ///     OR
        ///     
        /// Sample request : Get Employee By Id
        ///             
        ///     GET /api/v1/Employee/{empNo}
        ///     
        ///     OR
        ///     
        /// Sample request : Delete Employee
        /// 
        ///     DELETE /api/v1/Employee/{empNo}
        /// 
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> This endpoint returns a list of Employee.</returns>
        [HttpGet]
        public ActionResult<IQueryable<Employee>> GetAllEmployee()
        {
            var employee = _employeeRepository.GetAllEmployee();
            var employeeDto = employee.Select(e => e.ToEmployeeDto()).ToList();
            return Ok(employeeDto);
        }
        /// <summary>
        /// You can see the Get, Get By Id or Delete Employee Here .
        /// </summary>
        /// <remarks>
        /// The parameters in the request body can be null. 
        ///
        ///  You can search by using any of the parameters in the request.
        ///  
        ///  NOTE: You can only add by one parameter at a time
        ///  
        /// Sample request : Get All Employee
        ///             
        ///     GET /api/v1/Employee
        ///     
        ///     OR
        ///     
        /// Sample request : Get Employee By Id
        ///             
        ///     GET /api/v1/Employee/{empNo}
        ///     
        ///     OR
        ///     
        /// Sample request : Delete Employee
        /// 
        ///     DELETE /api/v1/Employee/{empNo}
        /// 
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> This endpoint returns a list of Employee.</returns>
        [HttpGet("{empNo}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int empNo)
        {
            var employee = await _employeeService.GetEmployeeWithDepartmentByIdAsync(empNo);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
        /// <summary>
        /// You Add or Update Employee Here here.
        /// </summary>
        /// <remarks>
        /// The parameters in the request body can be null. 
        ///
        ///  You can search by using any of the parameters in the request.
        ///  
        ///  NOTE: You can only add by one parameter at a time
        ///  
        /// Sample request : Add Employee
        ///             
        ///     POST /api/v1/Employee
        ///     {
        ///       "empno": 1,
        ///       "fname": "John",
        ///       "lname": "Doe",
        ///       "address": "Jl. Widya Chandra, Setiabudi, Jakarta Selatan",
        ///       "dob": "1989-02-04",
        ///       "salary": 8000000,
        ///       "sex": "Male",
        ///       "phonenumber": 62899099,
        ///       "email": "John@gmail.com",
        ///       "position": "Programmer",
        ///       "deptno": 1,
        ///       "employeetype": "Full-Time",
        ///       "level": 6,
        ///       "lastupdateddate": "2024-08-22T00:00:00",
        ///       "nik": 1234567890
        ///     }
        ///     
        ///     OR
        ///     
        /// Sample request : Update Employee
        ///             
        ///     PUT /api/v1/Employee/{empNo}
        ///     {
        ///       "empno": 1,
        ///       "fname": "John",
        ///       "lname": "Doe",
        ///       "address": "Jl. Widya Chandra, Setiabudi, Jakarta Selatan",
        ///       "dob": "1989-02-04",
        ///       "salary": 8000000,
        ///       "sex": "Male",
        ///       "phonenumber": 62899099,
        ///       "email": "John@gmail.com",
        ///       "position": "Programmer",
        ///       "deptno": 1,
        ///       "employeetype": "Full-Time",
        ///       "level": 6,
        ///       "lastupdateddate": "2024-08-22T00:00:00",
        ///       "nik": 1234567890
        ///     }
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> This endpoint returns a list of Accounts.</returns>
        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee(Employee employee)
        {
            var createdEmployee = await _employeeRepository.AddEmployee(employee);
            return Ok(createdEmployee);
        }
        /// <summary>
        /// You Add or Update Employee Here here.
        /// </summary>
        /// <remarks>
        /// The parameters in the request body can be null. 
        ///
        ///  You can search by using any of the parameters in the request.
        ///  
        ///  NOTE: You can only add by one parameter at a time
        ///  
        /// Sample request : Add Employee
        ///             
        ///     POST /api/v1/Employee
        ///     {
        ///       "empno": 1,
        ///       "fname": "John",
        ///       "lname": "Doe",
        ///       "address": "Jl. Widya Chandra, Setiabudi, Jakarta Selatan",
        ///       "dob": "1989-02-04",
        ///       "salary": 8000000,
        ///       "sex": "Male",
        ///       "phonenumber": 62899099,
        ///       "email": "John@gmail.com",
        ///       "position": "Programmer",
        ///       "deptno": 1,
        ///       "employeetype": "Full-Time",
        ///       "level": 6,
        ///       "lastupdateddate": "2024-08-22T00:00:00",
        ///       "nik": 1234567890
        ///     }
        ///     
        ///     OR
        ///     
        /// Sample request : Update Employee
        ///             
        ///     PUT /api/v1/Employee/{empNo}
        ///     {
        ///       "empno": 1,
        ///       "fname": "John",
        ///       "lname": "Doe",
        ///       "address": "Jl. Widya Chandra, Setiabudi, Jakarta Selatan",
        ///       "dob": "1989-02-04",
        ///       "salary": 8000000,
        ///       "sex": "Male",
        ///       "phonenumber": 62899099,
        ///       "email": "John@gmail.com",
        ///       "position": "Programmer",
        ///       "deptno": 1,
        ///       "employeetype": "Full-Time",
        ///       "level": 6,
        ///       "lastupdateddate": "2024-08-22T00:00:00",
        ///       "nik": 1234567890
        ///     }
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> This endpoint returns a list of Accounts.</returns>
        [HttpPut("{empNo}")]
        public async Task<IActionResult> UpdateEmployee(int empNo, Employee employee)
        {
            if (empNo != employee.Empno) return BadRequest();

            var updatedEmployee = await _employeeRepository.UpdateEmployee(employee);
            var employeeDto = updatedEmployee.ToEmployeeDto();
            return Ok(employeeDto);
        }
        /// <summary>
        /// You can see the Get, Get By Id or Delete Employee Here .
        /// </summary>
        /// <remarks>
        /// The parameters in the request body can be null. 
        ///
        ///  You can search by using any of the parameters in the request.
        ///  
        ///  NOTE: You can only add by one parameter at a time
        ///  
        /// Sample request : Get All Employee
        ///             
        ///     GET /api/v1/Employee
        ///     
        ///     OR
        ///     
        /// Sample request : Get Employee By Id
        ///             
        ///     GET /api/v1/Employee/{empNo}
        ///     
        ///     OR
        ///     
        /// Sample request : Delete Employee
        /// 
        ///     DELETE /api/v1/Employee/{empNo}
        /// 
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> This endpoint returns a list of Employee.</returns>
        [HttpDelete("{empNo}")]
        public async Task<ActionResult<bool>> DeleteBook(int empNo)
        {
            var deleted = await _employeeRepository.DeleteEmployee(empNo);
            if (!deleted) return NotFound();
            return Ok("Employee has been deleted !");
        }
        /// <summary>
        /// You can see the Get List Of Sorting by First Name of Employee Here .
        /// </summary>
        /// <remarks>
        /// The parameters in the request body can be null. 
        ///
        ///  You can search by using any of the parameters in the request.
        ///  
        ///  NOTE: You can only add by one parameter at a time
        ///  
        /// Sample request : Get Employee Details
        ///             
        ///     GET /api/v1/Employee/Get-employee?Fname=John&Deptname=IT&Position=programmer&Employeetype=Full-Time&SortBy=Fname&SortDescending=true&PageNumber=1&PageSize=2
        /// 
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> This endpoint returns a list of Employee.</returns>
        [HttpGet("Get-employee")]
        public async Task<IActionResult> GetEmployees([FromQuery] EmployeeFilterSortRequest request)
        {
            var employees = await _employeeService.GetFilteredSortedEmployeesAsync(request);
            return Ok(employees);
        }
        /// <summary>
        /// You can see the Get Details of Employee Here .
        /// </summary>
        /// <remarks>
        /// The parameters in the request body can be null. 
        ///
        ///  You can search by using any of the parameters in the request.
        ///  
        ///  NOTE: You can only add by one parameter at a time
        ///  
        /// Sample request : Get Employee Details
        ///             
        ///     GET /api/v1/Employee/Get-details/{empNo}
        /// 
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> This endpoint returns a list of Employee.</returns>
        [HttpGet("Get-details/{empNo}")]
        public async Task<IActionResult> GetEmployeeDetails(int empNo)
        {
            var employeeDetail = await _employeeService.GetEmployeeDetailsByIdAsync(empNo);
            return Ok(employeeDetail);
        }
        [HttpPost("add-employee-dependent")]
        public async Task<ActionResult>RegisterEmployeeAsync(EmployeeRegistrationDto employeeDto)
        {
            _employeeService.RegisterEmployeeAsync(employeeDto);
            return Ok(employeeDto);
        }
        [HttpPut("deactivate/{empNo}")]
        public async Task<IActionResult> DeactivateEmployee(int empNo, [FromBody] DeactivateEmployeeDto deactivateDto)
        {
            if (string.IsNullOrWhiteSpace(deactivateDto.Reason))
            {
                return BadRequest("Deactivation reason is required.");
            }

            try
            {
                await _employeeService.DeactivateEmployeeAsync(empNo, deactivateDto.Reason);
                return Ok(new { Message = $"Employee {empNo} deactivated successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }

        [HttpPut("update-employee-timestamp/{empNo}")]
        public async Task<IActionResult> UpdateEmployeeAsync(int empNo, UpdateDto updateDto)
        {
            await _employeeService.UpdateEmployeeAsync(empNo, updateDto).ConfigureAwait(false);
            return Ok(new { Message = "Employee updated successfully" });
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchEmployee([FromQuery]SearchDto searchDto)
        {
           var res = await _employeeService.GetFilteredSortedEmployeesAsync(searchDto);
           return Ok(res);
        }
        [HttpPost("employees/{empNo}/dependents")]
        public async Task<IActionResult> AddDependent(int empNo, [FromBody] DependentDto dependentDto)
        {
            try
            {
                await _employeeService.AddDependentAsync(empNo, dependentDto);
                return Ok(new { message = "Dependent added successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An unexpected error occurred.", details = ex.Message });
            }
        }
        [HttpGet("dependent/${empNo}")]
        public async Task<IActionResult> GetEmployeeDependent(int empNo)
        {
            var res = await _employeeRepository.GetEmployeeById(empNo);
            return Ok(res);
        }
    }
}

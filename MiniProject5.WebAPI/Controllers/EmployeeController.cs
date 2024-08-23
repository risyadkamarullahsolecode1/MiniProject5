using Microsoft.AspNetCore.Mvc;
using MiniProject5.Application.Dtos;
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
        [HttpGet]
        public ActionResult<IQueryable<Employee>> GetAllEmployee()
        {
            var employee = _employeeRepository.GetAllEmployee();
            var employeeDto = employee.Select(e => e.ToEmployeeDto()).ToList();
            return Ok(employeeDto);
        }

        [HttpGet("{empNo}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int empNo)
        {
            var employee = await _employeeRepository.GetEmployeeById(empNo);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee(Employee employee)
        {
            var createdEmployee = await _employeeRepository.AddEmployee(employee);
            return Ok(createdEmployee);
        }

        [HttpPut("{empNo}")]
        public async Task<IActionResult> UpdateEmployee(int empNo, Employee employee)
        {
            if (empNo != employee.Empno) return BadRequest();

            var updatedEmployee = await _employeeRepository.UpdateEmployee(employee);
            var employeeDto = updatedEmployee.ToEmployeeDto();
            return Ok(employeeDto);
        }

        [HttpDelete("{empNo}")]
        public async Task<ActionResult<bool>> DeleteBook(int empNo)
        {
            var deleted = await _employeeRepository.DeleteEmployee(empNo);
            if (!deleted) return NotFound();
            return Ok("Employee has been deleted !");
        }
        [HttpGet("Get-employee")]
        public async Task<IActionResult> GetEmployees([FromQuery] EmployeeFilterSortRequest request)
        {
            var employees = await _employeeService.GetFilteredSortedEmployeesAsync(request);
            return Ok(employees);
        }

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
        [HttpPost("Deactivate-employee/{empNo}")]
        public async Task<IActionResult> DeactivateEmployee(int empNo, [FromBody] string statusReason)
        {
            await _employeeService.DeactivateEmployeeAsync(empNo, statusReason);
            return Ok(new { Message = "Employee deactivated successfully" });
        }

        [HttpPut("update-employee-timestamp/{empNo}")]
        public async Task<IActionResult> UpdateEmployeeAsync(int empNo, UpdateDto updateDto)
        {
            await _employeeService.UpdateEmployeeAsync(empNo, updateDto).ConfigureAwait(false);
            return Ok(new { Message = "Employee updated successfully" });
        }
    }
}

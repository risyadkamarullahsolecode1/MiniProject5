using Microsoft.AspNetCore.Mvc;
using MiniProject5.Domain.Entities;
using MiniProject5.Domain.Interfaces;
using MiniProject5.Infrastucture.Data.Repository;

namespace MiniProject5.WebAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class WorksonController : ControllerBase
    {
        private readonly IWorksonRepository _worksonRepository;

        public WorksonController(IWorksonRepository worksonRepository)
        {
            _worksonRepository = worksonRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Workson>>> GetAllWorkson()
        {
            var workson = await _worksonRepository.GetAllWorkson();
            return Ok(workson);
        }

        [HttpGet("{empNo}/{projNo}")]
        public async Task<ActionResult<Workson>> GetProjectById(int empNo, int projNo)
        {
            var workson = await _worksonRepository.GetWorksonById(empNo, projNo);
            if (workson == null)
            {
                return NotFound();
            }
            return Ok(workson);
        }

        [HttpPost]
        public async Task<ActionResult<Project>> AddDepartment(Workson workson)
        {
            var createdworkson = await _worksonRepository.AddWorkson(workson);
            return Ok(createdworkson);
        }

        [HttpPut("{empNo}/{projNo}")]
        public async Task<IActionResult> UpdateEmployee(int empNo, int projNo, Workson workson)
        {
            if (projNo != workson.Projno && empNo != workson.Empno) return BadRequest();

            var updatedworkson = await _worksonRepository.UpdateWorkson(workson);
            return Ok(updatedworkson);
        }

        [HttpDelete("{empNo}/{projNo}")]
        public async Task<ActionResult<bool>> DeleteBook(int empNo, int projNo)
        {
            var deleted = await _worksonRepository.DeleteWorkson(empNo, projNo);
            if (!deleted) return NotFound();
            return Ok("project has been deleted !");
        }
    }
}

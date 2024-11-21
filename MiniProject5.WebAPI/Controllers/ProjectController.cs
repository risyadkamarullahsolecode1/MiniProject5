using Microsoft.AspNetCore.Mvc;
using MiniProject5.Domain.Entities;
using MiniProject5.Domain.Interfaces;

namespace MiniProject5.WebAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetAllProject()
        {
            var project = await _projectRepository.GetAllProject();
            return Ok(project);
        }

        [HttpGet("{projNo}")]
        public async Task<ActionResult<Project>> GetProjectById(int projNo)
        {
            var project = await _projectRepository.GetProjectById(projNo);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        [HttpPost]
        public async Task<ActionResult<Project>> AddDepartment(Project project)
        {
            var createdproject = await _projectRepository.AddProject(project);
            return Ok(createdproject);
        }

        [HttpPut("{projNo}")]
        public async Task<IActionResult> UpdateEmployee(int projNo, Project project)
        {
            if (projNo != project.Projno) return BadRequest();

            var updatedproject = await _projectRepository.UpdateProject(project);
            return Ok(updatedproject);
        }

        [HttpDelete("{projNo}")]
        public async Task<ActionResult<bool>> DeleteBook(int projNo)
        {
            var deleted = await _projectRepository.DeleteProject(projNo);
            if (!deleted) return NotFound();
            return Ok("project has been deleted !");
        }

        [HttpGet("{projNo}/department")]
        public async Task<IActionResult> GetDepartmentAsync(int projNo)
        {
            var res = await _projectRepository.GetDepartmentAsync(projNo);
            return Ok(res);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using PrikazKorisnika.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrikazKorisnika.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectsController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        // GET: /Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            var projects = await _projectRepository.GetAllProjects();
            return Ok(projects);
        }

        // GET: /Projects/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            var project = await _projectRepository.GetProjectById(id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        // POST: /Projects
        [HttpPost]
        public async Task<ActionResult<Project>> CreateProject(Project project)
        {
            await _projectRepository.AddProject(project);
            return CreatedAtAction(nameof(GetProject), new { id = project.ProjectId }, project);
        }

        // PUT: /Projects/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] Project project)
        {
            if (id != project.ProjectId)
            {
                return BadRequest("Project ID mismatch");
            }

            await _projectRepository.UpdateProject(project);
            return NoContent();
        }

        // DELETE: /Projects/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            await _projectRepository.DeleteProject(id);
            return NoContent();
        }
    }
}

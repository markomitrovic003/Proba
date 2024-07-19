using Microsoft.AspNetCore.Mvc;
using PrikazKorisnika.Model;
using System.Threading.Tasks;

namespace PrikazKorisnika.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectUsersController : ControllerBase
    {
        private readonly IProjectUserRepository _projectUserRepository;

        public ProjectUsersController(IProjectUserRepository projectUserRepository)
        {
            _projectUserRepository = projectUserRepository;
        }

        // POST: api/projects/1/users/1
        [HttpPost("projects/{projectId}/users/{userId}")]
        public async Task<IActionResult> AddUserToProject(int projectId, int userId)
        {
            var projectUser = new ProjectUser { ProjectId = projectId, UserId = userId };
            await _projectUserRepository.AddProjectUser(projectUser);
            return CreatedAtAction(nameof(GetProjectUser), new { projectId, userId }, projectUser);
        }

        // GET: api/projects/1/users/1
        [HttpGet("{projectId}/users/{userId}")]
        public async Task<ActionResult<ProjectUser>> GetProjectUser(int projectId, int userId)
        {
            var projectUser = await _projectUserRepository.GetProjectUser(projectId, userId);
            if (projectUser == null)
            {
                return NotFound();
            }
            return Ok(projectUser);
        }
        [HttpPut("projects/{projectId}/users/{userId}")]
        public async Task<IActionResult> UpdateProjectUser(int projectId, int userId, [FromBody] ProjectUser projectUser)
        {
            if (projectId != projectUser.ProjectId || userId != projectUser.UserId)
            {
                return BadRequest("Project ID or User ID mismatch");
            }

            await _projectUserRepository.UpdateProjectUser(projectUser);

            return NoContent();
        }
    }
}

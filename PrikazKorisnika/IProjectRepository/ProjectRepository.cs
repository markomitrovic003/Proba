using Microsoft.EntityFrameworkCore;
using PrikazKorisnika.Model;

public class ProjectRepository : IProjectRepository
{
    private readonly AuthorDbContext _context;

    public ProjectRepository(AuthorDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Project>> GetAllProjects()
    {
        return await _context.Projects.ToListAsync();
    }

    public async Task<Project> GetProjectById(int id)
    {
        return await _context.Projects.FindAsync(id);
    }

    public async Task AddProject(Project project)
    {
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateProject(Project project)
    {
        var existingProject = await _context.Projects.FindAsync(project.ProjectId);
        if (existingProject == null)
        {
            throw new KeyNotFoundException("Project not found");
        }

        existingProject.ProjectName = project.ProjectName;
        existingProject.DataStart = project.DataStart;
        existingProject.DataEnd = project.DataEnd;
        existingProject.Description = project.Description;

        await _context.SaveChangesAsync();
    }
   

    public async Task DeleteProject(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project != null)
        {
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }
    }
}

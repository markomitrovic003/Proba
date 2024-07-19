using Microsoft.EntityFrameworkCore;
using PrikazKorisnika.Model;

public class ProjectUserRepository : IProjectUserRepository
{
    private readonly AuthorDbContext _context;

    public ProjectUserRepository(AuthorDbContext context)
    {
        _context = context;
    }

    public async Task AddProjectUser(ProjectUser projectUser)
    {
        _context.ProjectUsers.Add(projectUser);
        await _context.SaveChangesAsync();
    }

    public async Task<ProjectUser> GetProjectUser(int projectId, int userId)
    {
        return await _context.ProjectUsers
            .FirstOrDefaultAsync(pu => pu.ProjectId == projectId && pu.UserId == userId);
    }

    public async Task UpdateProjectUser(ProjectUser projectUser)
    {
        _context.Entry(projectUser).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}

using PrikazKorisnika.Model;

public interface IProjectUserRepository
{
    Task AddProjectUser(ProjectUser projectUser);
    Task<ProjectUser> GetProjectUser(int projectId, int userId);
    Task UpdateProjectUser(ProjectUser projectUser);
}

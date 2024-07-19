using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace PrikazKorisnika.Model
{
    public class AuthorDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectUser> ProjectUsers { get; set; }

        public AuthorDbContext(DbContextOptions<AuthorDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .Property(u => u.NameUser)
            .IsRequired(); // Primer za zahtevanost polja

            modelBuilder.Entity<Project>()
                .Property(p => p.ProjectName)
                .IsRequired()
                .HasMaxLength(100); // Primer za zahtevanost i maksimalnu dužinu polja

            modelBuilder.Entity<ProjectUser>()
                .HasKey(pu => new { pu.UserId, pu.ProjectId });

            modelBuilder.Entity<ProjectUser>()
                .HasOne(pu => pu.Project)
                .WithMany()
                .HasForeignKey(pu => pu.ProjectId);

            modelBuilder.Entity<ProjectUser>()
                .HasOne(pu => pu.User)
                .WithMany()
                .HasForeignKey(pu => pu.UserId);
        }
    }
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly AuthorDbContext _context;

        public ProjectsController(AuthorDbContext context)
        {
            _context = context;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            return await _context.Projects.ToListAsync();
        }
    }


}

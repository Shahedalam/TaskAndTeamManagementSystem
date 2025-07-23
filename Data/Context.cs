using Microsoft.EntityFrameworkCore;
using TaskAndTeamManagementSystem.Models;

namespace TaskAndTeamManagementSystem.Data
{
    public class Context:DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }

        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = "server=localhost;port=3306;database=taskteamdb;user=shahed;password=1234;";
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Many-to-many Relation: User <--> Team
            modelBuilder.Entity<User>()
                .HasMany(u => u.Teams)
                .WithMany(t => t.Members)
                .UsingEntity(j => j.ToTable("UserTeams"));
                //.UsingEntity<Dictionary<string, object>>(
                //    "UserTeams", 
                //    j => j.HasOne<Team>().WithMany().HasForeignKey("TeamId"),
                //    j => j.HasOne<User>().WithMany().HasForeignKey("UserId"))
                //.ToTable("UserTeams");

            // One-to-many Relation: UserTask.AssignedToUser --> User.AssignedTasks
            modelBuilder.Entity<UserTask>()
                .HasOne(t => t.AssignedToUser)
                .WithMany(u => u.AssignedTasks)
                .HasForeignKey(t => t.AssignedToUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // One-to-many Relation: UserTask.CreatedByUser --> User (no reverse navigation)
            modelBuilder.Entity<UserTask>()
                .HasOne(t => t.CreatedByUser)
                .WithMany()
                .HasForeignKey(t => t.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // One-to-many Relation: UserTask.Team --> Team.Tasks
            modelBuilder.Entity<UserTask>()
                .HasOne(t => t.Team)
                .WithMany(team => team.Tasks)
                .HasForeignKey(t => t.TeamId);

            base.OnModelCreating(modelBuilder);
        }
    }
   
}

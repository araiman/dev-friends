using System.Data.Entity;

namespace DevFriends.Models
{
	public class ProjectsContext : DbContext
	{
		public ProjectsContext() : base(nameOrConnectionString: "ProjectsContext") { }

		public DbSet<User> Users { get; set; }

		public DbSet<Project> Projects { get; set; }

		public DbSet<Tag> Tags { get; set; }

		public DbSet<TagProjectRelation> TagProjectRelations { get; set; }
	}
}
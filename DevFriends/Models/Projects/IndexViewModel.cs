using System.Collections.Generic;

namespace DevFriends.Models.Projects
{
	public class IndexViewModel
	{
		public IEnumerable<ProjectWithContext> ProjectsWithContext { get; set; }
	}

	public class ProjectWithContext
	{
		public Project Project { get; set; }
		
		public string OwnerName { get; set; }
		
		public IEnumerable<string> TagNames { get; set; } 
	}
}
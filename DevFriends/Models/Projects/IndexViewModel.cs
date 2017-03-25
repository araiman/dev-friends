using System.Collections.Generic;
using System.Data.Linq;

namespace DevFriends.Models
{
	public class IndexViewModel
	{
		public IEnumerable<ProjectWithRelatedInfo> ProjectsWithContext { get; set; }
	}

	public class ProjectWithRelatedInfo
	{
		public string ProjectName { get; set; }
		
		public string OwnerName { get; set; }
		
		public string ProjectDescription { get; set; }
		
		public IEnumerable<string> TagNames { get; set; } 
	}
}
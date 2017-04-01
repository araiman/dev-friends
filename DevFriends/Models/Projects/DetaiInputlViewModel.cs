using System.Collections.Generic;

namespace DevFriends.Models.Projects
{
	public class DetailInputViewModel
	{
		public Project Project { get; set; }

		public User Owner { get; set; }

		public IEnumerable<Tag> Tags { get; set; }

		public IEnumerable<Project> RelatedProjects { get; set; }

		public string HashedOwnerEmailForProfileImage { get; set; }
	}
}
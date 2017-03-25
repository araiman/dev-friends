using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DevFriends.Models
{
	public class DevFriendsContext : DbContext
	{
		public DevFriendsContext() : base(nameOrConnectionString: "DevFriendsContext") { }

		public DbSet<Project> Projects { get; set; }

		public DbSet<User> Users { get; set; }

		public DbSet<Tag> Tags { get; set; }

		public DbSet<TagProjectRelation> TagProjectRelations { get; set; }
	}
}
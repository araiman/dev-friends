using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DevFriends.Models
{
	public partial class NiconicoContext : DbContext
	{
		public NiconicoContext() : base(nameOrConnectionString: "NiconicoContext") { }

		public DbSet<User> User { get; set; }
	}
}
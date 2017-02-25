using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevFriends.Models
{
	public class DayViewModel
	{
		public IReadOnlyList<User> Users { get; set; }
	}
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DevFriends.Models
{
	[Table("tag_project_rltn", Schema = "devfriends")]
	public class TagProjectRelation
	{
		[Key]
		[Column("tag_id", 
		Order = 0)]
		public int TagId { get; set; }

		[Key]
		[Column("project_id", Order = 1)]
		public int ProjectId { get; set; }
	}
}
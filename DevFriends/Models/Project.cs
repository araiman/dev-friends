using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity;

namespace DevFriends.Models
{
	[Table("project", Schema = "devfriends")]
	public class Project
	{
		[Key]
		[Column("id")]
		public int Id { get; set; }

		[Required]
		[MaxLength(20)]
		[Column("owner_id")]
		public string OwnerId { get; set; }

		[Required]
		[Column("name")]
		public string ProjectName { get; set; }
		
		[Column("description")]
		[MaxLength(300)]
		public string Description { get; set; }
	}
}
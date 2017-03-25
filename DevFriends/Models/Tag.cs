using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DevFriends.Models
{
	[Table("tag", Schema = "devfriends")]
	public class Tag
	{
		[Key]
		[Column("id")]
		public int Id { get; set; }

		[Column("name")]
		[MaxLength(20)]
		public string Name { get; set; }
	}
}
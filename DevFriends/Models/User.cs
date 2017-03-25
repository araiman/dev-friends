using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevFriends.Models
{
	[Table("user", Schema = "devfriends")]
	public class User
	{
		[Column("id")]
		public string Id { get; set; }

		[Key]
		[MaxLength(20)]
		[Column("user_id")]
		public string UserId { get; set; }

		[Column("name")]
		[Required]
		[MaxLength(20)]
		public string Name { get; set; }

		[MaxLength(100)]
		public string Link { get; set; }

		[MaxLength(300)]
		public string Profile { get; set; }
	}
}
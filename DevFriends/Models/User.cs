using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevFriends.Models
{
	[Table("user", Schema = "devfriends")]
	public class User
	{
		[Key]
		[MaxLength(20)]
		[Column("user_id")]
		public string UserId { get; set; }

		[Column("name")]
		[Required]
		[MaxLength(20)]
		public string Name { get; set; }

		[Column("description")]
		[MaxLength(300)]
		public string Profile { get; set; }

		[Column("email")]
		[MaxLength(256)]
		[EmailAddress]
		public string Email { get; set; }
		
		[Column("link")]
		[MaxLength(100)]
		public string Link { get; set; }
	}
}
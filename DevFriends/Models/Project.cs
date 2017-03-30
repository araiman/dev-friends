using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

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

		[Column("requirements")]
		[MaxLength(300)]
		public string Requirements { get; set; }

		public virtual ICollection<TagProjectRelation> TagProjectRelations { get; set; }

		public class EntityConfiguration : EntityTypeConfiguration<Project>
		{
			public EntityConfiguration()
			{
				HasMany(p => p.TagProjectRelations).WithMany().Map(
					r => r.ToTable("TagProjectRelation").MapLeftKey("Id").MapRightKey("ProjectId"));
			}
		}
	}
}
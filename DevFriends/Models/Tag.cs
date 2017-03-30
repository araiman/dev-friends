using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

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

		[Column("tag_type")]
		public TagType Type { get; set; }

		public virtual ICollection<TagProjectRelation> TagProjectRelations { get; set; }

		public class EntityConfiguration : EntityTypeConfiguration<Tag>
		{
			public EntityConfiguration()
			{
				HasMany(p => p.TagProjectRelations).WithMany().Map(
					r => r.ToTable("TagProjectRelation").MapLeftKey("Id").MapRightKey("TagId"));
			}
		}
	}

	public enum TagType
	{
		ProjectPurpose,
		Technology
	}
}
using Items.Domain.Common;

namespace Items.Domain.Entities;

public class Category : BaseAuditableEntity
{
	public string Name { get; set; } = null!;

	public virtual ICollection<Item> Items { get; set; } = new HashSet<Item>();
}

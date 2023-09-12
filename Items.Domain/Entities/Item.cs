using Items.Domain.Common;

namespace Items.Domain.Entities;

public class Item : BaseAuditableEntity
{
	public string Name { get; set; } = null!;
	public string Description { get; set; } = null!;
	public int CategoryId { get; set; }
	public string Producer { get; set; } = null!;
	public string Supplier { get; set; } = null!;
	public decimal Price { get; set; }

	public virtual Category Category { get; set; } = new();
}

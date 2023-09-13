namespace Items.Shared.DTO;

public record ItemDto
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public string Description { get; set; } = null!;
	public int CategoryId { get; set; }
	public string Category { get; set; } = null!;
	public string Producer { get; set; } = null!;
	public string Supplier { get; set; } = null!;
	public decimal Price { get; set; }
	public DateTime Created { get; set; }
	public DateTime? LastModified { get; set; }
	public bool Active { get; set; }
}

﻿namespace Items.Shared.Contracts.Items;

public record AddItemRequest
{
	public string Name { get; set; } = null!;
	public string Description { get; set; } = null!;
	public int CategoryId { get; set; }
	public decimal Price { get; set; }
	public string Producer { get; set; } = null!;
	public string Supplier { get; set; } = null!;
}

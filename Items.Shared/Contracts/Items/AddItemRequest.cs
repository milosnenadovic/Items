﻿namespace Items.Shared.Contracts.Items;

public record AddItemRequest
{
	public string Name { get; set; }
	public string Description { get; set; }
	public int CategoryId { get; set; }
	public decimal Price { get; set; }
	public string Producer { get; set; }
	public string Supplier { get; set; }
}

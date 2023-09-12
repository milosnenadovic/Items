using System.ComponentModel;

namespace Items.Shared.Enums;

public enum GetItemsSortBy
{
	[Description("Id")]
	Id = 1,
	[Description("Name")]
	Name = 2,
	[Description("Description")]
	Description = 3,
	[Description("Category")]
	Category = 4,
	[Description("Producer")]
	Producer = 5,
	[Description("Supplier")]
	Supplier = 6,
	[Description("Price")]
	Price = 7,
	[Description("Created")]
	Created = 8,
	[Description("StartDate")]
	LastModified = 9,
}
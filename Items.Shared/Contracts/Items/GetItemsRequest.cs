using Items.Shared.DTO.Common;
using Items.Shared.Enums;

namespace Items.Shared.Contracts.Items;

public record GetItemsRequest : BaseQueryStringParameters<GetItemsSortBy>
{
	public string? FilterName { get; set; }
	public string? FilterDescription { get; set; }
	public int? Category { get; set; }
	public decimal? MinPrice { get; set; }
	public decimal? MaxPrice { get; set; }
	public DateTime? CreatedFrom { get; set; }
	public DateTime? CreatedTo { get; set; }
	public bool? Active { get; set; }
}

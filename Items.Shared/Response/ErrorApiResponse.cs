using System.Text.Json.Serialization;

namespace Items.Shared.Response;

public class ErrorApiResponse
{
	[JsonPropertyName("title")]
	[JsonPropertyOrder(1)]
	public string Title { get; set; } = null!;

	[JsonPropertyName("detail")]
	[JsonPropertyOrder(2)]
	public string Detail { get; set; } = null!;

	[JsonPropertyName("errorCode")]
	[JsonPropertyOrder(3)]
	public int? ErrorCode { get; set; } = null!;

	[JsonPropertyName("errorCodes")]
	[JsonPropertyOrder(4)]
	public List<ErrorCode> ErrorCodes { get; set; } = null!;
}

public class ErrorCode
{
	[JsonPropertyName("code")]
	public string Code { get; set; } = null!;

	[JsonPropertyName("description")]
	public string Description { get; set; } = null!;
}

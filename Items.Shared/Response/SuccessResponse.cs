namespace Items.Shared.Response;

public class SuccessResponse
{
	public bool IsSuccess => true;
	public string? Message { get; }
	public string? DetailedMessage { get; }
	public int? ErrorCode { get; }

	public static SuccessResponse Success()
		=> new();

	public static SuccessResponse<T> Success<T>(T result) => new(result);
}

public class SuccessResponse<T> : SuccessResponse, IResponse<T>
{
	public SuccessResponse()
	{
	}

	public SuccessResponse(T data)
	{
		Result = data;
	}

	public T Result { get; set; }
}

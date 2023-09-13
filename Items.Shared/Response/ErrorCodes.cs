namespace Items.Shared.Response;

public enum ErrorCodes
{
	UnspecifiedError = 0,
	DatabaseAdd,
	DatabaseUpdate,
	DatabaseGet,
	CantFind,
	AlreadyExists
}

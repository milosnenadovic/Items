namespace Items.Shared.Errors;

public static partial class Errors
{
	public static class InvalidData
	{
		public static string PageNumber => "PageNumber must be positive";
		public static string PageSize => "PageSize must be positive";
		public static string LongerThan24 => "String is longer than 24";
		public static string LongerThan48 => "String is longer than 48";
		public static string LongerThan96 => "String is longer than 96";
	}
}

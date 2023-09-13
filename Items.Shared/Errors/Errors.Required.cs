namespace Items.Shared.Errors;

public static partial class Errors
{
	public static class Required
	{
		public static string PageNumber => "PageNumber is required";
		public static string PageSize => "PageSize is required";
        public static string Name => "Name is required";
        public static string Description => "Description is required";
        public static string Category => "Category is required";
        public static string Producer => "Producer is required";
        public static string Supplier => "Supplier is required";
        public static string Price => "Price is required";
		public static string Id => "Id is required";
	}
}

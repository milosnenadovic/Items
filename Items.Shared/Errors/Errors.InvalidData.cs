namespace Items.Shared.Errors;

public static partial class Errors
{
	public static class InvalidData
	{
		public static string PageNumber => "PageNumber must be positive number";
		public static string PageSize => "PageSize must be positive number";
        public static string CategoryId => "CategoryId must be positive number";
        public static string Price => "Price must be positive decimal number";
		public static string Id => "Id must be positive number";
		public static string LongerThan24 => "String is longer than 24";
		public static string NameLongerThan48 => "Name is longer than 48";
        public static string ProducerLongerThan48 => "Producer is longer than 48";
        public static string SupplierLongerThan48 => "Supplier is longer than 48";
        public static string DescriptionLongerThan96 => "Description is longer than 96";
    }
}

namespace Items.Shared.DTO;

public record CategoryDto
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
}

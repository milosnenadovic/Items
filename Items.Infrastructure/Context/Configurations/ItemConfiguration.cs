using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Items.Domain.Entities;

namespace Items.Infrastructure.Context.Configurations;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
	public void Configure(EntityTypeBuilder<Item> builder)
	{
		builder
			.Property(x => x.Name)
			.HasMaxLength(48)
			.IsRequired();

		builder
			.Property(x => x.Description)
			.IsRequired();

		builder
			.Property(x => x.Producer)
			.HasMaxLength(48)
			.IsRequired();

		builder
			.Property(x => x.Supplier)
			.HasMaxLength(48)
			.IsRequired();

		builder
			.Property(x => x.Price)
			.IsRequired();

		builder.HasIndex(x => new { x.CategoryId, x.Name })
			.IsUnique();

		builder
			.HasOne(x => x.Category)
			.WithMany(y => y.Items)
			.HasForeignKey(x => x.CategoryId);

		builder.HasCheckConstraint("CK_Price_Positive", "Price > 0");
	}
}
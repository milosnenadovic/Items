using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Items.Domain.Entities;

namespace Items.Infrastructure.Context.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
	public void Configure(EntityTypeBuilder<Category> builder)
	{
		builder
			.Property(x => x.Name)
			.HasMaxLength(24)
			.IsRequired();

		builder
			.HasMany(x => x.Items)
			.WithOne(y => y.Category)
			.HasForeignKey(y => y.CategoryId);
	}
}
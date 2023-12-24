using CTBS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CTBS.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.Property(u => u.FirstName)
			.IsRequired();
		builder.Property(u => u.MiddleName)
			.IsRequired();
		builder.Property(u => u.LastName)
			.IsRequired();
	}
}
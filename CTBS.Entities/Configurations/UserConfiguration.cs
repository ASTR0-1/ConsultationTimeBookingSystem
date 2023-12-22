using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTBS.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CTBS.Entities.Configurations;

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

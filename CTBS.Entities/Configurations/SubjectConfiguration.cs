using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTBS.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CTBS.Entities.Configurations;

public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
{
	public void Configure(EntityTypeBuilder<Subject> builder)
	{
		builder.HasKey(s => s.Id);
		builder.Property(s => s.Name)
			.IsRequired();
		builder.HasMany(s => s.Lecturers)
			.WithOne(l => l.Subject)
			.HasForeignKey(l => l.SubjectId);
	}
}

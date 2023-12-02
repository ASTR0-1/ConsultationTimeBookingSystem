using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTBS.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CTBS.Entities.Configurations;
public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
	public void Configure(EntityTypeBuilder<Student> builder)
	{
		builder.Property(s => s.FirstName)
			.IsRequired();
		builder.Property(s => s.MiddleName)
			.IsRequired();
		builder.Property(s => s.LastName)
			.IsRequired();
		builder.HasMany(s => s.Appointments)
			.WithOne(a => a.Student)
			.HasForeignKey(a => a.StudentId);
	}
}

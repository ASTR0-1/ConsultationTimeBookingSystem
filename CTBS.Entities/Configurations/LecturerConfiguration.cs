using CTBS.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CTBS.Entities.Configurations;

public class LecturerConfiguration : IEntityTypeConfiguration<Lecturer>
{
	public void Configure(EntityTypeBuilder<Lecturer> builder)
	{
		builder.HasKey(l => l.Id);
		builder.Property(l => l.FirstName)
			.IsRequired();
		builder.Property(l => l.MiddleName)
			.IsRequired();
		builder.Property(l => l.LastName)
			.IsRequired();
		builder.HasOne(l => l.Subject)
			.WithMany(s => s.Lecturers)
			.HasForeignKey(l => l.SubjectId);
		builder.HasMany(l => l.Appointments)
			.WithOne(a => a.Lecturer)
			.HasForeignKey(a => a.LecturerId);
	}
}

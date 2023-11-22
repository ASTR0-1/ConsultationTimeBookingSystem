using CTBS.Entities.Enums;
using CTBS.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CTBS.Entities.Configurations;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
	public void Configure(EntityTypeBuilder<Appointment> builder)
	{
		builder.HasKey(a => a.Id);
		builder.Property(a => a.Priority)
			.IsRequired();
		builder.Property(a => a.Date)
			.IsRequired();
		builder.Property(a => a.State)
			.HasDefaultValue(AppointmentState.Pending)
			.IsRequired();
		builder.HasOne(a => a.Lecturer)
			.WithMany(l => l.Appointments)
			.HasForeignKey(a => a.LecturerId);
		builder.HasOne(a => a.Student)
			.WithMany(s => s.Appointments)
			.HasForeignKey(a => a.StudentId);
		builder.HasOne(a => a.QuestionsCategory)
			.WithMany(qc => qc.Appointments)
			.HasForeignKey(a => a.QuestionsCategoryId);
	}
}

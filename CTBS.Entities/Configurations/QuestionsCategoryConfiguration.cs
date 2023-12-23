using CTBS.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CTBS.Entities.Configurations;

public class QuestionsCategoryConfiguration : IEntityTypeConfiguration<QuestionsCategory>
{
	public void Configure(EntityTypeBuilder<QuestionsCategory> builder)
	{
		builder.HasKey(qc => qc.Id);
		builder.Property(qc => qc.Name)
			.IsRequired();
		builder.Property(qc => qc.ImpactOnAmountOfTime)
			.IsRequired();
		builder.HasMany(qc => qc.Appointments)
			.WithOne(a => a.QuestionsCategory)
			.HasForeignKey(a => a.QuestionsCategoryId);
	}
}

using CTBS.Entities.Configurations;
using CTBS.Entities.Helpers;
using CTBS.Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CTBS.Entities;
public class ApplicationContext : IdentityDbContext
{
	public DbSet<Appointment> Appointments { get; set; }
	public DbSet<Lecturer> Lecturers { get; set; }
	public DbSet<QuestionsCategory> QuestionsCategories { get; set; }
	public DbSet<Student> Students { get; set; }
	public DbSet<Subject> Subjects { get; set; }

	public ApplicationContext(DbContextOptions options)
		: base(options)
	{ }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.ApplyConfiguration(new AppointmentConfiguration());
		builder.ApplyConfiguration(new LecturerConfiguration());
		builder.ApplyConfiguration(new QuestionsCategoryConfiguration());
		builder.ApplyConfiguration(new StudentConfiguration());
		builder.ApplyConfiguration(new SubjectConfiguration());
	}

	protected override void ConfigureConventions(ModelConfigurationBuilder builder)
	{
		base.ConfigureConventions(builder);

		builder.Properties<DateOnly>()
			.HaveConversion<DateOnlyConverter>();
	}
}

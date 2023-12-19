using CTBS.Entities.Configurations;
using CTBS.Entities.Helpers;
using CTBS.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CTBS.Entities;

public class ApplicationContext : IdentityDbContext<User, IdentityRole<int>, int>
{
	public DbSet<Appointment> Appointments { get; set; } = null!;
	public DbSet<Lecturer> Lecturers { get; set; } = null!;
	public DbSet<QuestionsCategory> QuestionsCategories { get; set; } = null!;
	public DbSet<Student> Students { get; set; } = null!;
	public DbSet<Subject> Subjects { get; set; } = null!;

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
